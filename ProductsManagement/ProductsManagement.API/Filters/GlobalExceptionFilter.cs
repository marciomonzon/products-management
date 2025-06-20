using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ProductsManagement.API.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Unhandled exception occurred.");

            var result = new ObjectResult(new
            {
                error = "An unexpected error occurred.",
                detail = context.Exception.Message
            })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}
