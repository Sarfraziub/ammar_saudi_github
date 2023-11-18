using Application.Interface.Context;
using Microsoft.Extensions.Primitives;

namespace WebApi.Startup.Middleware
{
    public class RequestContextResolver
    {
        private readonly RequestDelegate _next;

        public RequestContextResolver(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IRequestContext requestContext)
        {
            context.Request.Headers.TryGetValue("currency", out StringValues currency);
            requestContext.Currency = Convert.ToString(currency);
            requestContext.Currency = requestContext.Currency ?? "SAR";
            //var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //if (environment == "Stage")
            //{
            //    requestContext.Currency = "USD";
            //}
            //else
            //{
            //    context.Request.Headers.TryGetValue("currency", out StringValues currency);
            //    requestContext.Currency = Convert.ToString(currency);
            //    requestContext.Currency = requestContext.Currency ?? "SAR";
            //}
            await _next(context);
        }
    }
}
