using System.Net;
using System.Security.Authentication;

namespace TrainingApp.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Call the next middleware in the pipeline.
                await _next(context);
            }
          
            catch (Exception ex)
            {
                // Handle other exceptions and log them.
                _logger.LogError($"An error occurred: {ex.Message}");

                // Customize the response for other types of errors.
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("Internal Server Error");
            }
        }
    }
}
