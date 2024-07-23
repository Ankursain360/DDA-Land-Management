using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace NewLandAcquisition.Middleware
{
    public class KeywordFilterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string[] _unwantedKeywords = { "<script>", "</script>", "drop", "select", "alert", "iframe", "onerror" }; // Add more keywords as needed

        public KeywordFilterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put)
            {
                context.Request.EnableBuffering();
                var body = await new System.IO.StreamReader(context.Request.Body).ReadToEndAsync();

                if (_unwantedKeywords.Any(keyword => Regex.IsMatch(body, @"\b" + keyword + @"\b", RegexOptions.IgnoreCase)))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Request contains forbidden keywords.");
                    return;
                }

                context.Request.Body.Position = 0;
            }

            await _next(context);
        }
    }
}
