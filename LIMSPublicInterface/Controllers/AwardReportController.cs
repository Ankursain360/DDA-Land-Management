using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using Dto.Search;
using Dto.Master;
using LIMSPublicInterface.Filters;
using Core.Enum;
using System.Collections.Generic;
using Utility.Helper;

namespace LIMSPublicInterface.Controllers
{
    public class AwardReportController : BaseController
    {
        private readonly IAwardplotDetailService _awardPlotDetailsService;

        public AwardReportController(IAwardplotDetailService awardPlotDetailsService)
        {
            _awardPlotDetailsService = awardPlotDetailsService;
        }
        //[AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            AwardReportDtoProfile data= new AwardReportDtoProfile();
            ViewBag.AwardNoDateList = await _awardPlotDetailsService.BindAwardNoDateList();
            return View(data);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] AwardReportSearchDto model)
        {
            var result = await _awardPlotDetailsService.GetPagedAwardReport(model);

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
        public async Task<IActionResult> GetAllAwardDetails([FromBody] AwardReportSearchDto model)
        {
            var result = await _awardPlotDetailsService.GetAllAwardplotdetails(model);
            List<AwardDetailsDto> data = new List<AwardDetailsDto>();
            if (result !=null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new AwardDetailsDto()
                    {
                        VillageName = result[i].Village == null ? "" : result[i].Village.Name,
                        KhasraNo = result[i].Khasra == null ? "" : result[i].Khasra.Name,
                        Area = result[i].Khasra.Bigha == 0?null: result[i].Khasra.Bigha.ToString()+"-"+ result[i].Khasra.Biswa.ToString() +"_"+result[i].Khasra.Biswanshi.ToString(),
                        NotifiedArea = result[1].Bigha == 0 ? null : result[i].Bigha.ToString() +"_"+ result[i].Biswa.ToString() + "_" + result[i].Biswanshi.ToString(),
                        AwardDate = result[i].AwardMaster.AwardDate.HasValue?Convert.ToDateTime(result[i].AwardMaster.AwardDate).ToString("yyyy-dd-MM"):""
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
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "LegalManagementSystemData.xlsx");
        }
    }
}