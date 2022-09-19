using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using LandInventory.Models;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Dto.Search;
using LandInventory.Filters;
using Core.Enum;
using Utility.Helper;
using Dto.Master;
using Dto.Common;

namespace LandInventory.Controllers
{
    public class LandDetailsController : Controller
    {
        private readonly IPropertyRegistrationService _propertyregistrationService;

        public LandDetailsController(IPropertyRegistrationService propertyregistrationService, IConfiguration configuration)
        {
            _propertyregistrationService = propertyregistrationService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<PartialViewResult> List(string name)
        {
            var result = await _propertyregistrationService.GetLandBankdata(name);
            return PartialView("_LandData", result);
        }
    }
}
