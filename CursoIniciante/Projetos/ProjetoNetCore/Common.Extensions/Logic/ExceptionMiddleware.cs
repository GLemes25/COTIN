using Microsoft.AspNetCore.Http;
using System.Net;

namespace Common.Extensions.Logic
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                HandleExceptionAsync(httpContext, ex);
            }
        }

        private static void HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception.Message.Contains("contains authorization metadata"))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}
