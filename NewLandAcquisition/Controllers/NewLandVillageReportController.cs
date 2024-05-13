
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
    public class NewLandVillageReportController : BaseController
    {
        private readonly INewlandvillageService _NewlandvillageService;

        public NewLandVillageReportController(INewlandvillageService NewlandvillageService)
        {
            _NewlandvillageService = NewlandvillageService;  
        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            Newlandvillage model = new Newlandvillage();



            model.VillageList = await _NewlandvillageService.GetNewlandvillage();
            return View(model);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] NewlandVillageReportSearchDto model)
        {
            var result = await _NewlandvillageService.GetPagedNewLandVillageReport(model);
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
        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> NewLandVillageReportList()
        {
            var result = await _NewlandvillageService.GetAllVillageList();
            List<NewLandVillageReportListDto> data = new List<NewLandVillageReportListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new NewLandVillageReportListDto()
                    {
                        Id = result[i].Id,
                        Village = result[i].Name,
                        YearOfConsolidation = result[i].YearofConsolidation.ToString(),
                        TotalSheet = result[i].TotalNoOfSheet.ToString(),
                        Circle = result[i].Circle,

                        Acquired = result[i].Acquired,
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }



    }
}
