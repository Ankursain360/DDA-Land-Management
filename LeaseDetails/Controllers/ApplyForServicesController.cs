using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using LeaseDetails.Filters;
using Utility.Helper;

namespace LeaseDetails.Controllers
{
    public class ApplyForServicesController : BaseController
    {

        private readonly IApplyForServicesService _applyForServicesService;

        public ApplyForServicesController(IApplyForServicesService applyForServicesService)
        {
            _applyForServicesService = applyForServicesService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] ServiceSearchDto model)
        {
            var result = await _applyForServicesService.GetPagedServicetype(model);
            return PartialView("_List", result);
        }
    }
}
