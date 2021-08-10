using Dto.Master;
using Microsoft.AspNetCore.Mvc;
using Service.IApplicationService;
using LeaseDetails.Helper;
using LeaseDetails.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;

namespace LeaseDetails.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ISiteContext _siteContext;
        private readonly IUserProfileService _userProfileService;

        public HomeController(ISiteContext siteContext,
           IUserProfileService userProfileService)
        {
            _siteContext = siteContext;
            _userProfileService = userProfileService;
        }

        [HttpGet]
        public async Task<JsonResult> KycApplicationDetails(int? Id)
        {
            Id = Id ?? 0;
            var data = await _userProfileService.KycApplicationDetails(_siteContext.UserId);
            return Json(data.Select(x => new
            {
                x.KycApplicaionPending,
                x.KycApplicaionApprove,
                x.KycApplicaionInProcess,
                
            }));
        }

        [HttpGet]
        public async Task<JsonResult> KycDemandPaymentDetails(int? Id)
        {
            Id = Id ?? 0;
            var data = await _userProfileService.KycDemandPaymentDetails(_siteContext.UserId);
            return Json(data.Select(x => new
            {
                x.KycDemandPaymentPending,
                x.KycDemandPaymentApprove,
                x.KycDemandPaymentInProcess,               
            }));
        }


        


        public async Task<IActionResult> Index()
        {
            UserProfileDto user = await _userProfileService.GetUserById(_siteContext.UserId);
            return View(user);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult UnAuthorized()
        {
            return View();
        }

        public IActionResult ExceptionLog()
        {
            return View();
        }
    }
}
