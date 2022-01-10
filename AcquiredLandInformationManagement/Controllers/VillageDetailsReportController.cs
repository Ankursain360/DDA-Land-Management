using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Dto.Search;
using AcquiredLandInformationManagement.Filters;
using Core.Enum;
using Utility.Helper;
using Dto.Master;
using Newtonsoft.Json;
using System.Text;

namespace AcquiredLandInformationManagement.Controllers
{
    public class VillageDetailsReportController : BaseController
    {
        private readonly IAcquiredlandvillageService _acquiredlandvillageService;

        public VillageDetailsReportController(IAcquiredlandvillageService acquiredlandvillageService)
        {
            _acquiredlandvillageService = acquiredlandvillageService;
        }
        public async Task<IActionResult> Index()
        {           
            VillageReportDetailsSearchDto dto = new VillageReportDetailsSearchDto();
            ViewBag.VillageList = await _acquiredlandvillageService.GetAllVillageList();          
            return View(dto);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] VillageReportDetailsSearchDto model)
        {
            var result = await _acquiredlandvillageService.GetPagedvillagedetailsListByVillageId(model);
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
