using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
namespace DamagePayee.Middleware
{
    public class NoCacheMiddleware
    {
        private readonly RequestDelegate _next;

        public NoCacheMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.OnStarting(() =>
            {
                context.Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate, max-age=0";
                context.Response.Headers["Pragma"] = "no-cache";
                context.Response.Headers["Expires"] = "Thu, 01 Jan 1970 00:00:00 GMT";

                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}
