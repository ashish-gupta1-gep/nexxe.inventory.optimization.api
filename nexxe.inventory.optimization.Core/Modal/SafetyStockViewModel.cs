using System;
using System.Collections.Generic;
using System.Text;

namespace nexxe.inventory.optimization.Core.Modal
{
    public class SafetyStockViewModel : PaginationModel
    {
        public long ItemId { get; set; }
        public long SupplierId { get; set; }
        public long LocationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
