using nexxe.inventory.optimization.Core.Entity;
using nexxe.inventory.optimization.Core.Modal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace nexxe.inventory.optimization.DataAccess.Interface
{
    public interface ISafetyStockRepo
    {
        Task<List<SafetyStockValue>> GetDataToCalaculateSafeStock(SafetyStockViewModel safetyStockModel);
    }
}
