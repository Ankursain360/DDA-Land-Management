using Dto.Search;
using EncroachmentDemolition.Helper;
using Libraries.Service.ApplicationService;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Service.IApplicationService;
using System.Threading.Tasks;

namespace EncroachmentDemolition.Controllers
{
    public class NavigateToGCPController : Controller
    {
        private readonly IGISService _GISService;
        private readonly ISiteContext _siteContext;
        private readonly IUserProfileService _userProfileService;
        
        public NavigateToGCPController(IGISService GISService, IUserProfileService userProfileService, ISiteContext siteContext)
        {
            _siteContext = siteContext;
            _userProfileService = userProfileService;
            _GISService = GISService;
            
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NavigateGCPDetailsSearchDto Dto)
        {
            var data = await _GISService.NavigateGCPDetails(Dto);
            return PartialView("_List", data);
        }
    }
}
