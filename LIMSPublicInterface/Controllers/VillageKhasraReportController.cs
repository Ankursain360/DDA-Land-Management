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
    public class VillageKhasraReportController : BaseController
    {
        private readonly IKhasraService _khasraService;

        public VillageKhasraReportController(IKhasraService khasraService)
        {
            _khasraService = khasraService;
        }
        // Dropdown Dependency  calls below
        [HttpGet]
        public async Task<JsonResult> GetAllKhasraList(int? AcquiredlandvillageId)
        {
            AcquiredlandvillageId = AcquiredlandvillageId ?? 0;
            return Json(await _khasraService.GetAllKhasraList(Convert.ToInt32(AcquiredlandvillageId)));
        }

       
  
        public async Task<IActionResult> Index()
        {
            Khasra model = new Khasra();
            model.VillageList = await _khasraService.GetAllVillageList();
            model.KhasraList = await _khasraService.GetAllKhasraList(model.AcquiredlandvillageId);
           
            return View(model);
        }
        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] VillageDetailsKhasraWiseReportSearchDto dto)
        {
            var result = await _khasraService.GetPagedVillageKhasraReport(dto);
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
        public async Task<IActionResult> DownloadAllVillageWiseKhasra([FromBody] VillageDetailsKhasraWiseReportSearchDto model)
        {
            var result = await _khasraService.getAllVillageDetailsKhasraWise(model);
            List<VillageWiseKhasraDetailsDto> data = new List<VillageWiseKhasraDetailsDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new VillageWiseKhasraDetailsDto()
                    {
                        Village = result[i].Acquiredlandvillage.Name,
                        Khasra = result[i].Name,
                        RectNo = result[i].RectNo

                    });

                }              
            }
            var memory = ExcelHelper.CreateExcel(data);
            TempData["file"] = memory;
            return Ok(); 
        }

        [HttpGet]
        public virtual IActionResult download()
        {
            byte[] data = TempData["file"] as byte[];
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "VillageKhasraReport.xlsx");
        }
     

    }
}




