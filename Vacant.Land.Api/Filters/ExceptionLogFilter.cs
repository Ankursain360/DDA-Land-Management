using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using NLog;
using NLog.Web;
using System;
using System.Net;

namespace Vacant.Land.Api.Filters
{
    public class ExceptionLogFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            string traceId = Guid.NewGuid().ToString();
            LogManager.Configuration.Variables["traceId"] = traceId;
            logger.Error(filterContext.Exception, filterContext.Exception.Message);
            //filterContext.Result = new RedirectToRouteResult(
            //            new RouteValueDictionary {
            //                                { "controller", "Home" },
            //                                { "action", "Error" },
            //                                { "traceId", traceId}
            //                            });


            // filterContext.Result = new ObjectResult(new ApiResponse { Message = filterContext.Exception.Message, Data = null });

            HttpStatusCode status = HttpStatusCode.InternalServerError;
            String message = String.Empty;

            var exceptionType = filterContext.Exception.GetType();
            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                message = "Unauthorized Access";
                status = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                message = "A server error occurred.";
                status = HttpStatusCode.NotImplemented;
            }
            //else if (exceptionType == typeof(MyAppException))
            //{
            //    message = context.Exception.ToString();
            //    status = HttpStatusCode.InternalServerError;
            //}
            else
            {
                message = filterContext.Exception.Message;
                status = HttpStatusCode.NotFound;
            }
            filterContext.ExceptionHandled = true;

            HttpResponse response = filterContext.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            var err = message + " " + filterContext.Exception.StackTrace;
            response.WriteAsync(err);
        }
    }
}
  