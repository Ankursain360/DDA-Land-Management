using Dto.Master;
using GIS.Helper;
using GIS.Models;
using Libraries.Service.ApplicationService;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IApplicationService;
using System.Diagnostics;
using System.Threading.Tasks;
using Core.Enum;
using GIS.Filters;
namespace GIS.Controllers
{
    public class DecisionSupportSystemHomeController : Controller
    {
        private readonly IGISService _GISService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISiteContext _siteContext;
        private readonly IUserProfileService _userProfileService;
        public DecisionSupportSystemHomeController(IGISService GISService, IHttpContextAccessor httpContextAccessor, IUserProfileService userProfileService, ISiteContext siteContext)
        {
            _GISService = GISService;
            _httpContextAccessor = httpContextAccessor;
            _siteContext = siteContext;
            _userProfileService = userProfileService;
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            UserProfileDto user = await _userProfileService.GetUserById(_siteContext.UserId);
            return View(user);
        }
      
    }
}
