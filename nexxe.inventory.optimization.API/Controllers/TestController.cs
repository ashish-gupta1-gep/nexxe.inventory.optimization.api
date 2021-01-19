using dm.lib.core.nuget;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace nexxe.inventory.optimization.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [VisibleToCustomer]
    public class TestController : BaseController
    {
        private const string VERSION_FORMAT = "Assembly : {0}, Assembly Created Date : {1}, AppConfigLastUpdatedOn : {2}, PartnerConfigLastUpdatedOn : {3}";
        public TestController(IGepService gepService) : base(gepService)
        {

        }

        [HttpGet()]
        public string Ping()
        {
            return "Success";
        }

        [HttpGet("GlobalError", Name = "GlobalError")]
        public string TriggerGlobalError()
        {
            throw new Exception("Global Error");
        }
        [HttpGet("Ping1", Name = "Ping1")]
        public string Ping1()
        {
            return "Success";
        }
        [HttpGet("Version", Name = "Version")]
        public string Version()
        {
            string AppConfigLastUpdatedOn, PartnerConfigLastUpdatedOn;
            try
            {
                AppConfigLastUpdatedOn = gepService.GetApplicationConfiguration("AppConfigLastUpdatedOn");
                PartnerConfigLastUpdatedOn = gepService.GetApplicationConfiguration("PartnerConfigLastUpdatedOn");
            }
            catch (Exception ex)
            {
                AppConfigLastUpdatedOn = "ERROR";
                PartnerConfigLastUpdatedOn = ex.ToString();
            }
            var objAssembly = GetType().Assembly;
            FileInfo fi = new FileInfo(objAssembly.Location);
            var created = fi.LastWriteTime;
            return string.Format(VERSION_FORMAT,
                objAssembly.GetName(),
                created,
                AppConfigLastUpdatedOn,
                PartnerConfigLastUpdatedOn
            );
        }

    }
}
