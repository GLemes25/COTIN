using Microsoft.AspNetCore.Http;
using System.Net;

namespace Common.Extensions.Logic
{
    public class ExceptionMiddleware //ele trabalha em cima da requisição do cursor do ASP CORE. Qualquer ação que é feita dentro do projeto ele entra no fluxo e precisa chega ao seu final. Ele entra e executa e continua sua processo normal.
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext) // "httpContext" ela vem com as informações que foi feita. Se houve ou não excessão etc, e te fala o que vem na excessao
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
