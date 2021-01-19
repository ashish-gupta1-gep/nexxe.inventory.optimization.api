using Microsoft.Extensions.DependencyInjection;
using nexxe.inventory.optimization.DataAccess.Implementation;
using nexxe.inventory.optimization.DataAccess.Interface;

namespace nexxe.inventory.optimization.API
{
    public static class RegisterDataAccess
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped(typeof(IItemRepo), typeof(ItemRepo));
            services.AddScoped(typeof(ISafetyStockRepo), typeof(SafetyStockRepo));
        }
    }
}
