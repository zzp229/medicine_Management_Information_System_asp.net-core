using Microsoft.AspNetCore.Mvc.Filters;

namespace Fresh.Filter
{
    public class CustomResourceFilter : Attribute, IResourceFilter
    {
        private readonly ILogger<CustomResourceFilter> _logger;
        public CustomResourceFilter(ILogger<CustomResourceFilter> logger) => _logger = logger;
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            _logger.LogInformation("OnResourceExecuted");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            _logger.LogInformation("OnResourceExecuting");
        }
    }
}
