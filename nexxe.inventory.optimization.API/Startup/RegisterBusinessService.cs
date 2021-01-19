using dm.lib.csm.nuget;
using dm.lib.impex.nuget;

using Microsoft.Extensions.DependencyInjection;
using nexxe.inventory.optimization.Service.Implementation;
using nexxe.inventory.optimization.Service.Interface;

namespace nexxe.inventory.optimization.API
{
    public static class RegisterBusinessService
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped(typeof(IItemService), typeof(ItemService));
            services.AddScoped(typeof(ISafetyStockService), typeof(SafetyStockService));
        }
    }
}
