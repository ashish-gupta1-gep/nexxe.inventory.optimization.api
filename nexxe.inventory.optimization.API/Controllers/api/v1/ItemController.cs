using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nexxe.inventory.optimization.Core.Entity;
using nexxe.inventory.optimization.Core.Modal;
using nexxe.inventory.optimization.Service.Interface;

namespace nexxe.inventory.optimization.API.Controllers.api.v1
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }
        /// <summary>
        /// Get All the Items
        /// </summary>
        [HttpPost("GetAllItem")]
        public async Task<ResponseDTO<List<Item>>> GetAllItems(ItemRequest itemRequest)
        {
            return await _itemService.GetAllItems(itemRequest);
        }
    }
}