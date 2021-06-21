using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LandingPage.Models;
using Libraries.Service.IApplicationService;
using LandingPage.Helper;
using Service.IApplicationService;
using Dto.Master;
using Libraries.Model.Entity;

namespace LandingPage.Controllers
{
    public class UserNotificationController : BaseController
    {
        private readonly ISiteContext _siteContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IModuleService _moduleService;
        private readonly IModuleCategoryService _modulecategoryService;
        private readonly IUserNotificationService _userNotificationService;



        public UserNotificationController(ISiteContext siteContext,
          IUserProfileService userProfileService, IModuleService moduleService, IModuleCategoryService modulecategoryService,
          IUserNotificationService userNotificationService)
        {
            _siteContext = siteContext;
            _userProfileService = userProfileService;
            _moduleService = moduleService;
            _modulecategoryService = modulecategoryService;
            _userNotificationService = userNotificationService;
        }

        public async Task<IActionResult> Index()
        {
            List<UserNotificationAlertDto> user = await _userNotificationService.GetUserNotficationAlertAll(_siteContext.UserId);
            
            return View(user);
        }

        public async Task<IActionResult> UpdateUserNotification(int id, string url)
        {
            List<string> JsonMsg = new List<string>();
            var result = await _userNotificationService.Update(id, SiteContext.UserId);
            if (result)
            {
                return Redirect(url);
            }
            else
            {
                JsonMsg.Add("false");
                JsonMsg.Add("Unable to process request");
                return Json(JsonMsg);
            }

        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserNotification([FromBody] UserNotificationUpdateDto model)
        {
            List<string> JsonMsg = new List<string>();
            var result = await _userNotificationService.Update(model.id, SiteContext.UserId);
            if (result)
            {
                JsonMsg.Add("true");
                JsonMsg.Add(model.url);
                return Json(JsonMsg);
            }
            else
            {
                JsonMsg.Add("false");
                JsonMsg.Add("Unable to process request");
                return Json(JsonMsg);
            }

        }

    }
}
