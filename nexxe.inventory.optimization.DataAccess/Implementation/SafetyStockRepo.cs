using dm.lib.core.nuget;
using nexxe.inventory.optimization.Core.Constant;
using nexxe.inventory.optimization.Core.Entity;
using nexxe.inventory.optimization.Core.Modal;
using nexxe.inventory.optimization.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace nexxe.inventory.optimization.DataAccess.Implementation
{
    public class SafetyStockRepo : BaseSqlDataAccess, ISafetyStockRepo
    {

        public SafetyStockRepo(IGepService gepservice) : base(gepservice)
        {

        }
        public async Task<List<SafetyStockValue>> GetDataToCalaculateSafeStock(SafetyStockViewModel safetyStockModel)
        {
            SqlParameter[] parameter = {
                new SqlParameter("@ItemId", safetyStockModel.ItemId),
                new SqlParameter("@SupplierId", safetyStockModel.SupplierId),
                new SqlParameter("@LocationId", safetyStockModel.LocationId),
                new SqlParameter("@CurrentDate", safetyStockModel.StartDate),
                new SqlParameter("@EndDate", safetyStockModel.EndDate)
            };
            var safetyStockValueList = new List<SafetyStockValue>();
            return await ExecuteSqlReaderWithStoreProcAsync(SqlConstants.GetDataForCalculationOfSafetyStock, parameter, (dr) =>
            {
                while (dr.Read())
                {
                    safetyStockValueList.Add(new SafetyStockValue
                    {
                        Id = dr.GetInt64(dr.GetOrdinal("DemandAndInventoryValuesId")),
                        ItemId = dr.GetInt64(dr.GetOrdinal("ItemId")),
                        LocationId = dr.GetInt64(dr.GetOrdinal("LocationId")),
                        SupplierId = dr.GetInt64(dr.GetOrdinal("SupplierID")),
                        DemandDate = dr.GetDateTime(dr.GetOrdinal("DemandDate")),
                        Demand = dr.GetInt32(dr.GetOrdinal("Demand")),
                        SystemLeadTime = dr.GetInt32(dr.GetOrdinal("SystemLeadTime"))
                    });
                }
                return safetyStockValueList;
            });

        }
    }
}
