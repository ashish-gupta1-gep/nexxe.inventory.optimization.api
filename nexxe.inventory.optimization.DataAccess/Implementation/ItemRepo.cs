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
    public class ItemRepo : BaseSqlDataAccess, IItemRepo
    {
        public ItemRepo(IGepService gepService) : base(gepService) { }
        public async Task<List<Item>> GetAllItems(ItemRequest itemRequest)
        {
            SqlParameter[] parameter = {
                new SqlParameter("@Offset", itemRequest.Offset),
                new SqlParameter("@PageLimit", itemRequest.PageLimit),
                new SqlParameter("@StartDate", itemRequest.StartDate),
                new SqlParameter("@EndDate", itemRequest.EndDate)
            };
            var items = new List<Item>();
            return await ExecuteSqlReaderWithStoreProcAsync(SqlConstants.GetAllItem, parameter, (dr) =>
            {
                while (dr.Read())
                {
                    items.Add(new Item
                    {
                        Id = dr.GetInt64(dr.GetOrdinal("ItemId")),
                        ItemDescription = dr.GetString(dr.GetOrdinal("ItemDescription")),
                        LocationId = dr.GetInt64(dr.GetOrdinal("LocationID")),
                        LocationName = dr.GetString(dr.GetOrdinal("locationName")),
                        LeadTimeVaration = dr.GetInt32(dr.GetOrdinal("LeadTimeVaration")),
                        AverageDemand = dr.GetInt32(dr.GetOrdinal("AverageDemand")),
                        ItemCount = dr.GetInt32(dr.GetOrdinal("ItemCount")),
                    });
                }
                return items;
            });
        }
    }
}
