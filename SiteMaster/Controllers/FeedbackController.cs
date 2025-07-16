using Core.Enum;
using Dto.Search;
using Libraries.Service.ApplicationService;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteMaster.Filters;
using System.Threading.Tasks;

namespace SiteMaster.Controllers
{
    [Authorize]
    public class FeedbackController : BaseController
    {
        private readonly IFeedbackService _service;

        public FeedbackController(IFeedbackService service)
        {
            _service = service;
        }

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<PartialViewResult> List([FromBody] FeedbackSearchDto model)
        {
            var result = await _service.GetPagedResult(model);
            return PartialView("_List", result);
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _service.GetSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
    }
}
