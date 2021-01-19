using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using dm.lib.exceptionmanager.nuget;
using dm.lib.infrastructure.nuget;
using nexxe.inventory.optimization.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using dm.lib.communicationlayer.nuget.Constants;

namespace nexxe.inventory.optimization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CatchallController : ControllerBase
    {
        [Route("/{**catchAll}")]
        [HttpPost("post", Order = int.MaxValue)]
        public GepReturn<string> CatchAll(string catchAll)
        {
            GepReturn<string> gepReturn = new GepReturn<string>();
            HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            HttpContext.Response.ContentType = GepConstants.CONTENT_TYPE_JSON;
            gepReturn.ErrorMessage = AppConstants.ERROR_404;
            gepReturn.ErrorCode = ((int)HttpStatusCode.NotFound).ToString();
            return gepReturn;
        }
    }
}