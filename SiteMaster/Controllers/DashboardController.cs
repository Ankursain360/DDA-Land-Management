using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;

namespace DDAPropertyREG.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            //ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
            return View();
        }
    }
}
