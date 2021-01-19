using nexxe.inventory.optimization.Core.Entity;
using nexxe.inventory.optimization.Core.Modal;
using nexxe.inventory.optimization.DataAccess.Interface;
using nexxe.inventory.optimization.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace nexxe.inventory.optimization.Service.Implementation
{
    public class ItemService : IItemService
    {
        IItemRepo _itemRepo;
        public ItemService(IItemRepo itemRepo)
        {
            _itemRepo = itemRepo;
        }
        public async Task<ResponseDTO<List<Item>>> GetAllItems(ItemRequest itemRequest)
        {
            try
            {
                return new ResponseDTO<List<Item>>()
                {
                    IsSuccess = true,
                    ReturnValue = await _itemRepo.GetAllItems(itemRequest),
                    Exception = null,
                    ErrorCode = null,
                    CorrelationId = null,
                    ErrorMessage = null,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<List<Item>>()
                {
                    IsSuccess = false,
                    ReturnValue = null,
                    Exception = ex,
                    ErrorCode = null,
                    CorrelationId = null,
                    ErrorMessage = ex.Message,
                    Errors = null
                };
            }
        }
    }
}
