using System;
using System.Collections.Generic;
using System.Text;

namespace nexxe.inventory.optimization.Core.Entity
{
    public class SafetyStockValue : BaseEntity
    {
        public long ItemId { get; set; }
        public long SupplierId { get; set; }
        public long LocationId { get; set; }
        public double? SafeStock { get; set; }
        public DateTime DemandDate { get; set; }
        public int Demand { get; set; }
        public int SystemLeadTime { get; set; }
    }
}
