using Microsoft.AspNetCore.Mvc.Filters;

namespace Fresh.Filter
{
    public class CustomResultFilter : Attribute, IResultFilter
    {
        private readonly ILogger<CustomResultFilter> _logger;
        public CustomResultFilter(ILogger<CustomResultFilter> logger) => _logger = logger;
        public void OnResultExecuted(ResultExecutedContext context)
        {
            _logger.LogInformation("OnResultExecuted");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            _logger.LogInformation("OnResultExecuting");
        }
    }
}
