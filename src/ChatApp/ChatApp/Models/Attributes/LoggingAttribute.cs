using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace ChatApp.Models.Attributes
{
    public class LoggingAttribute : ActionFilterAttribute
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.Info("OnActionExecuting");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.Info("OnActionExecuted");
        }
    }
}
