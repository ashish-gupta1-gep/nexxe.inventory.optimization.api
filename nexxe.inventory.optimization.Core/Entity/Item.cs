using System;
using System.Collections.Generic;
using System.Text;

namespace nexxe.inventory.optimization.Core.Entity
{
    public class Item : BaseEntity
    {
        public string ItemDescription { get; set; }
        //public string UOM { get; set; }
        public long LocationId { get; set; }
        public string LocationName { get; set; }
        public int LeadTimeVaration { get; set; }
        public int AverageDemand { get; set; }
        public int ItemCount { get; set; }
    }
}
