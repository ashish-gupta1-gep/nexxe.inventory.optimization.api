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
    public class SafetyStockController : ControllerBase
    {
         ISafetyStockService _safetyStockService;
        public SafetyStockController(ISafetyStockService safetyStockService)
        {
            _safetyStockService = safetyStockService;
        }
        /// <summary>
        /// Get and Calculate the safety stock by item and supplier and location 
        /// </summary>
        [HttpPost("GetSafetyStock")]
        public async Task<ResponseDTO<List<SafetyStockValue>>> GetSafetyStock(SafetyStockViewModel safetyStockModel)
        {
            return await _safetyStockService.GetSafetyStock(safetyStockModel);
        }
    }
}