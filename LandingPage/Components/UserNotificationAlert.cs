using Dto.Master;
using Microsoft.AspNetCore.Mvc;
using Service.IApplicationService;
using System.Threading.Tasks;
using LandingPage.Helper;
using Libraries.Service.IApplicationService;
using System.Collections.Generic;

namespace LandingPage.Components
{
    public class UserNotificationAlertViewComponent : ViewComponent
    {
        private readonly ISiteContext _siteContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IUserNotificationService _userNotificationService;
        public UserNotificationAlertViewComponent(ISiteContext siteContext,
            IUserProfileService userProfileService, IUserNotificationService userNotificationService)
        {
            _siteContext = siteContext;
            _userProfileService = userProfileService;
            _userNotificationService = userNotificationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<UserNotificationAlertDto> user = await _userNotificationService.GetUserNotficationAlert(_siteContext.UserId);
            return View("UserNotificationAlert", user);
        }
    }
}
