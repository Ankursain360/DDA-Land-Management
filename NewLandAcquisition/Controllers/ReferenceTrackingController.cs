
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using NewLandAcquisition.Filters;
using Core.Enum;

namespace NewLandAcquisition.Controllers
{
    public class ReferenceTrackingController : Controller
    {
        private readonly IRequestService _requestService;

        public ReferenceTrackingController(IRequestService requestService)
        {
            _requestService = requestService;
        }


        public async Task<IActionResult> Index()
        {
            Request request = new Request();
            request.IsActive = 1;
            return View(request);
        }



        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] TrackingListSearchDto model)
        {
            var result = await _requestService.GetPagedTrackingList(model);
            if (result != null)
            {
                return PartialView("_List", result);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return PartialView();
            }
        }


    }
}
