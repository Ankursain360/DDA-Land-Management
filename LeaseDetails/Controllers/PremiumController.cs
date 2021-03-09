using Core.Enum;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using LeaseDetails.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Helper;

namespace LeaseDetails.Controllers
{
    public class PremiumController : BaseController
    {
        public readonly IPremiumService _premiumService;
        public PremiumController(IPremiumService premiumService)
        {
            _premiumService = premiumService;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
