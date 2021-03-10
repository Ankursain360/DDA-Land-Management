using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;

namespace SiteMaster.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
