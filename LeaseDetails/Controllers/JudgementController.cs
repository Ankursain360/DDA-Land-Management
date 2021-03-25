using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using LeaseDetails.Filters;
using Utility.Helper;
using System.Threading.Tasks;
using Dto.Search;

namespace LeaseDetails.Controllers
{
    public class JudgementController : BaseController
    {
        private readonly IJudgementService _judgementService;

        public JudgementController(IJudgementService judgementService)
        {
            _judgementService = judgementService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] RequestForProceedingSearchDto model)
        {
            var result = await _judgementService.GetPagedRequestForProceeding(model);

            return PartialView("_List", result);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult ViewLetter()
        {
            return View();
        }
    }
}
