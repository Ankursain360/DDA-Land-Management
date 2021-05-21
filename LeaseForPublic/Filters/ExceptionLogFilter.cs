using System;
using NLog;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NLog.Web;

namespace LeaseForPublic.Filters
{
    public class ExceptionLogFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
       {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            string traceId = Guid.NewGuid().ToString();
            LogManager.Configuration.Variables["traceId"] = traceId;
            logger.Error(filterContext.Exception, filterContext.Exception.Message);
            filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                                            { "controller", "Home" },
                                            { "action", "ExceptionLog" },
                                            { "traceId", traceId}
                                        });
        }
    }
}
