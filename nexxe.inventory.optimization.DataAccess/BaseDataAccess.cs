using dm.lib.core.nuget;
using Microsoft.Extensions.Logging;
using System;

namespace nexxe.inventory.optimization.DataAccess
{
    public abstract class BaseDataAccess
    {
        public readonly IGepService gepService;
        IGepLogger logger;
        protected ILogger internalLogger;
        protected UserContext UserContext;

        public BaseDataAccess(IGepService gepservice)
        {
            gepService = gepservice;
            logger = gepService.GetLogger();
            internalLogger = (ILogger)logger.GetInternalLogger();
            UserContext = gepservice.GetUserContext();
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
