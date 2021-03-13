using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Service.IApplicationService;
using LeasePublicInterface.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeasePublicInterface.Filters
{
    public class AddHeaderResultServiceFilter : IActionFilter
    {
       
        public AddHeaderResultServiceFilter()
        {
         
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
           
        }
    }
}
