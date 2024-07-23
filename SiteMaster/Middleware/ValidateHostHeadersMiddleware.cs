using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
namespace SiteMaster.Middleware
{
    public class ValidateHostHeadersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ValidateHostHeadersOptions _options;

        public ValidateHostHeadersMiddleware(RequestDelegate next, IOptions<ValidateHostHeadersOptions> options)
        {
            _next = next;
            _options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var forwardedHost = context.Request.Headers["X-Forwarded-Host"].ToString();
            if (!string.IsNullOrEmpty(forwardedHost))
            {
                // Validate forwardedHost against allowed hosts
                if (!_options.AllowedHosts.Contains(forwardedHost, StringComparer.OrdinalIgnoreCase))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteAsync("Invalid Host");
                    return;
                }
            }

            await _next(context);
        }
    }
}
