using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;
using SiteMaster.Filters;
using Core.Enum;
using System.Data;
using Newtonsoft.Json;
using Utility.Helper;
using System.Collections.Generic;


namespace SiteMaster.Controllers
{
    public class SchemeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
