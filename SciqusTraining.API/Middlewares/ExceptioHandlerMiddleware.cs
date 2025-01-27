using System.Net;

namespace SciqusTraining.API.Middlewares
{
    public class ExceptioHandlerMiddleware
    {
        private readonly ILogger<ExceptioHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptioHandlerMiddleware(ILogger<ExceptioHandlerMiddleware> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvoleAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                //log this exception 
                logger.LogError(ex, $"{errorId} : {ex.Message}");

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";
                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "something went wrong! we are looking into resolving it."
                };
                await httpContext.Response.WriteAsJsonAsync(error); 
                
            }
        }
    }
}
