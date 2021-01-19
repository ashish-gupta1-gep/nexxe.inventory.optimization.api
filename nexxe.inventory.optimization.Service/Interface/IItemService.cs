using nexxe.inventory.optimization.Core.Entity;
using nexxe.inventory.optimization.Core.Modal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace nexxe.inventory.optimization.Service.Interface
{
   public interface IItemService
    {
        Task<ResponseDTO<List<Item>>> GetAllItems(ItemRequest itemRequest);
    }
}
