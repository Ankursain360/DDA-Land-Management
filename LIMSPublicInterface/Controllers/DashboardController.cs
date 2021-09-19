using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using LIMSPublicInterface.Filters;

namespace LIMSPublicInterface.Controllers
{
    public class DashboardController : BaseController
    {
        [ServiceFilter(typeof(AuditFilterAttribute))]
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
