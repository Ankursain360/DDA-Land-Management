using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;

using SiteMaster.Filters;

namespace SiteMaster.Controllers
{
    public class DashboardController : Controller
    {
        [ServiceFilter(typeof(AuditFilterAttribute))]
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
