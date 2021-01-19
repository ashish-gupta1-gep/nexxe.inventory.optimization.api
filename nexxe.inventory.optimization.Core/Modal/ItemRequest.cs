using System;
using System.Collections.Generic;
using System.Text;

namespace nexxe.inventory.optimization.Core.Modal
{
    public class ItemRequest : PaginationModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
