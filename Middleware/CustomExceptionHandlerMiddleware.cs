using Billing.Application.Exceptions;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Billing.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
            => _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            switch (ex)
            {
                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (result == string.Empty)
            {
                result = JsonConvert.SerializeObject(new { error = ex.Message });
            }

            return context.Response.WriteAsync(result);
        }
    }
}
