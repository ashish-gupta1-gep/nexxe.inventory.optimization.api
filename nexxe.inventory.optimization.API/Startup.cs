using dm.lib.exceptionmanager.nuget;
using dm.lib.infrastructure.nuget;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using DM.Lib.Configuration;
namespace nexxe.inventory.optimization.API
{
    public class Startup : GepStartup
    {

        // Constant Across System & Enviornment
        // public const string APPLICATION_ID = "108";
        public const string APPLICATION_ID = "17";
        public const string APPLICATION_NAME = "Nexxe.Inventory.Optimization.API";
        public const string API_VERSION = "1";
        public static string IngressRouteName = string.Empty;

        public Startup(IConfiguration configuration) : base(configuration, APPLICATION_ID)
        {
            Console.WriteLine("start Startup");
            try
            {
                Startup.IngressRouteName = GetLocalConfiguration("IngressRouteName");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Startup ERROR:" + ex.ToString());

            }
            Console.WriteLine("end Startup");
        }


        public override void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("start ConfigureServices");
            try
            {

                if (Convert.ToBoolean(GetLocalConfiguration("ShowProfiling")))
                {
                    services.AddMiniProfiler(options =>
                         options.RouteBasePath = "/profiler"
                      );
                }
                // services.AddCors();
                this.ConfigureControllers(services);
                base.ConfigureServices(services);
                services.AddApplicationInsightsTelemetry();
                RegisterDataAccess.Register(services);
                RegisterBusinessService.Register(services);
                RegisterSwagger.Register(services);

            }
            catch (Exception ex)
            {
                Console.WriteLine("ConfigureServices ERROR:" + ex.ToString());

            }
            Console.WriteLine("end ConfigureServices");
        }


        public override void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env,
                              IServiceProvider service,
                              IHostApplicationLifetime applicationLifetime)
        {
            try
            {
                Console.WriteLine("start Configure");
                try
                {
                    var ShowProfiling = GetLocalConfiguration("ShowProfiling");
                    Console.WriteLine("Configure ShowProfiling :" + ShowProfiling);
                    if (Convert.ToBoolean(ShowProfiling))
                    {
                        app.UseMiniProfiler();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ShowProfiling ERROR:" + ex.ToString());

                }

                bool ShowOnlyCustomeAPIonUI = false;
                try
                {
                    var ShowOnlyCustomeAPIonUI1 = GetLocalConfiguration("ShowOnlyCustomeAPIonUI");
                    Console.WriteLine("Configure ShowOnlyCustomeAPIonUI1 :" + ShowOnlyCustomeAPIonUI1);
                    ShowOnlyCustomeAPIonUI = Convert.ToBoolean(ShowOnlyCustomeAPIonUI1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception3 " + ex.ToString());
                }

                RegisterSwagger.UseSwagger(app, ShowOnlyCustomeAPIonUI);

                base.Configure(app, env, service, applicationLifetime);

                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    app.UseHsts();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Configure ERROR:" + ex.ToString());

            }
            Console.WriteLine("end Configure");

        }

        private string GetLocalConfiguration(string key)
        {
            string rt = string.Empty;
            try
            {
                Console.WriteLine("GetLocalConfiguration KEY:" + key);
                rt = Configuration[GepConstants.APP_SETTINGS_PREFIX + key];
                Console.WriteLine("GetLocalConfiguration VALUE:" + rt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetLocalConfiguration ERROR:" + ex.ToString());
            }

            return rt;
        }

        private void ConfigureControllers(IServiceCollection Services)
        {
            // find all controllers
            var Controllers =
                from a in AppDomain.CurrentDomain.GetAssemblies().AsParallel()
                from t in a.GetTypes()
                let attributes = t.GetCustomAttributes(typeof(ControllerAttribute), true)
                where attributes?.Length > 0
                select new { Type = t };

            var ControllersList = Controllers.ToList();
            Console.WriteLine($"Found {ControllersList.Count} controllers");

            // register them
            foreach (var Controller in ControllersList)
            {
                Console.WriteLine($"[Controller] Registering {Controller.Type.Name}");
                Services
                    .AddMvc()
                    .AddNewtonsoftJson(Options => Options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver())
                    .AddApplicationPart(Controller.Type.Assembly);
            }
        }


    }
}
