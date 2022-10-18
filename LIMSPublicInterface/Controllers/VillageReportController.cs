
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
using LIMSPublicInterface.Filters;
using Core.Enum;
using Dto.Master;
using Utility.Helper;

namespace LIMSPublicInterface.Controllers
{
    public class VillageReportController : BaseController
    {
        private readonly IAcquiredlandvillageService _acquiredlandvillageService;

        public VillageReportController(IAcquiredlandvillageService acquiredlandvillageService)
        {
            _acquiredlandvillageService = acquiredlandvillageService;
        }


        
        public async Task<IActionResult> Index()
        {
            Acquiredlandvillage model = new Acquiredlandvillage();

            model.VillageList = await _acquiredlandvillageService.GetAllVillageList();
            return View(model);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] VillageReportSearchDto model)
        {
            var result = await _acquiredlandvillageService.GetPagedVillageReport(model);
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
        
        public async Task<IActionResult> DownloadAllVillageReport([FromBody] VillageReportSearchDto model)
        {
            var result = await _acquiredlandvillageService.GetAllAcquiredlandvillages(model);
            List<VillageReportDto> data = new List<VillageReportDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new VillageReportDto()
                    {
                        name = result[i].Name,
                        yearofConsolidation = result[i].YearofConsolidation,
                        totalNoOfSheet = result[i].TotalNoOfSheet,
                        circle = result[i].Circle,
                        acquired = result[i].Acquired
                    });
                   
                }

            }
            var memory = ExcelHelper.CreateExcel(data);
            TempData["file"] = memory;
            return Ok();

        }
        [HttpGet]
        public virtual ActionResult Download()
        {
            byte[] data = TempData["file"] as byte[];
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "VillageReport.xlsx");

        }
    }
}
