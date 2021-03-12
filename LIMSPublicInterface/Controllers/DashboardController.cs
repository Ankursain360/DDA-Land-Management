using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;

namespace LIMSPublicInterface.Controllers
{
    public class DashboardController : BaseController
    {
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
