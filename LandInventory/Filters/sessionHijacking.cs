using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandInventory.Middleware;
namespace LandInventory.Filters
{
    public static class sessionHijacking
    {
        public static IApplicationBuilder preventSessionHijacking(this IApplicationBuilder app)
        {
            app.UseMiddleware<SessionRelay>();
            return app;
        }
    }
}
