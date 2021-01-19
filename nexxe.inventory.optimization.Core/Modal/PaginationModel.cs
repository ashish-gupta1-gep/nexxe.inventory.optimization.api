using System;
using System.Collections.Generic;
using System.Text;

namespace nexxe.inventory.optimization.Core.Modal
{
    public class PaginationModel
    {
        // skip
        public int Offset { get; set; }
        public int PageLimit { get; set; }
    }
}
