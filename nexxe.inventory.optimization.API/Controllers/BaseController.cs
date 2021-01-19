using dm.lib.core.nuget;
using Microsoft.AspNetCore.Mvc;
using System;

namespace nexxe.inventory.optimization.API.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        public readonly IGepService gepService;
        IGepLogger logger;
        protected UserContext userContext;

        public BaseController(IGepService gepservice)
        {
            gepService = gepservice;
            userContext = gepService.GetUserContext();
            logger = gepService.GetLogger();
        }


        protected void LogInfo(string cls, string method, string message)
        {
            logger.LogInformation(cls, method, message);
        }
        protected void LogError(string cls, string method, string message, Exception ex)
        {
            logger.LogError(cls, method, message, ex);
        }
        protected void LogVerbose(string cls, string method, string message)
        {
            logger.LogVerbose(cls, method, message);
        }


    }
}
