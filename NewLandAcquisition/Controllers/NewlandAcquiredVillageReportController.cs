
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
using Dto.Master;
using Utility.Helper;
namespace NewLandAcquisition.Controllers
{
    public class NewlandAcquiredVillageReportController : BaseController
    {
        private readonly INewlandvillageService _NewlandvillageService;

        public NewlandAcquiredVillageReportController(INewlandvillageService NewlandvillageService)
        {
            _NewlandvillageService = NewlandvillageService;
        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            Newlandvillage model = new Newlandvillage();


            model.VillageList = await _NewlandvillageService.GetAllVillageList();
            return View(model);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] NewlandAcquiredVillageReportSearchDto model)
        {
            var result = await _NewlandvillageService.GetPagedNewlandAcquiredVillageReport(model);
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


        public async Task<IActionResult> NewLandAcquiredVillageReportList()
        {
            var result = await _NewlandvillageService.GetAllVillageList();
            List<NewLandAcquiredVillageReportListDto> data = new List<NewLandAcquiredVillageReportListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new NewLandAcquiredVillageReportListDto()
                    {
                        Id = result[i].Id,
                        Village = result[i].Name,
                      
                        
                        Acquired = result[i].Acquired,
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }





    }
}

