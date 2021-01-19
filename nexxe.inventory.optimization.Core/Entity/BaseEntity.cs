using System;
using System.Collections.Generic;
using System.Text;

namespace nexxe.inventory.optimization.Core.Entity
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
