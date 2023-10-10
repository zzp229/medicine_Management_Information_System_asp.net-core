using Microsoft.AspNetCore.Mvc.Filters;

namespace Fresh.Filter
{
    public class CustomActionFilter : Attribute, IActionFilter
    {
        private readonly ILogger<CustomActionFilter> _logger;
        public CustomActionFilter(ILogger<CustomActionFilter> logger) => _logger = logger;
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("OnActionExecuting");
        }
    }
}
