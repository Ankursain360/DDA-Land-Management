using Dto.Master;
using Microsoft.AspNetCore.Mvc;
using Service.IApplicationService;

using System.Threading.Tasks;
using CourtCasesManagement.Helper;

namespace CourtCasesManagement.Components
{
    public class SignInProfileViewComponent : ViewComponent
    {
        private readonly ISiteContext _siteContext;
        private readonly IUserProfileService _userProfileService;
        public SignInProfileViewComponent(ISiteContext siteContext,
            IUserProfileService userProfileService)
        {
            _siteContext = siteContext;
            _userProfileService = userProfileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            UserProfileDto user = await _userProfileService.GetUserById(_siteContext.UserId);
            return View("SignInProfile", user);
        }
    }
}
