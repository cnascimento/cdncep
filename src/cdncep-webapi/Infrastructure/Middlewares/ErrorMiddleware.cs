using System.Net;
using cdncep_webapi.ViewModels;
using Newtonsoft.Json;

namespace cdncep_webapi.Infrastructure.Middlewares
{
    /// <summary>
    ///     Middleware de erro
    /// </summary>
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        ///     Construtor do middleware
        /// </summary>
        /// <param name="next"></param>
        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            ErrorResponse errorResponse;

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                errorResponse = new ErrorResponse(HttpStatusCode.InternalServerError.ToString(),
                    $"{exception.Message} {exception?.InnerException?.Message}");
            }
            else
            {
                errorResponse = new ErrorResponse(HttpStatusCode.InternalServerError.ToString(),
                    "An internal server error has occurred.");
            }

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonConvert.SerializeObject(errorResponse);
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(result);
        }
    }
}