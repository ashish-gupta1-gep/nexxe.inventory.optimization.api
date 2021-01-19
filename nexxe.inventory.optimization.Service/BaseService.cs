using dm.lib.core.nuget;
using System;

namespace nexxe.inventory.optimization.Service
{
    public abstract class BaseService
    {
        public readonly IGepService gepService;
        IGepLogger logger;
        protected UserContext userContext;
        public BaseService(IGepService gepservice)
        {
            gepService = gepservice;
            logger = gepService.GetLogger();
            userContext = gepService.GetUserContext();
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
