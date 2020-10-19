using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;

namespace EncroachmentDemolition.Controllers
{
    public class WatchWardApprovalController : BaseController
    {
        public readonly IWatchAndWardApprovalService _watchAndWardApprovalService;
        public WatchWardApprovalController(IWatchAndWardApprovalService watchAndWardApprovalService)
        {
            _watchAndWardApprovalService = watchAndWardApprovalService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] WatchandwardSearchDto model)
        {
            var result = await _watchAndWardApprovalService.GetPagedWatchandward(model);
            return PartialView("_List", result);
        }
        public async Task<IActionResult> Create(int id)
        {
            var Data = await _watchAndWardApprovalService.FetchSingleResult(id);
            //Data.VillageList = await _watchAndWardApprovalService.GetAllVillage();
            Data.LocalityList = await _watchAndWardApprovalService.GetAllLocality();
            Data.KhasraList = await _watchAndWardApprovalService.GetAllKhasra();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
    }
}
