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
    public class PossessionReportController : BaseController
    {
        private readonly IPossessiondetailsService _possessiondetailsService;

        public PossessionReportController(IPossessiondetailsService possessiondetailsService)
        {
            _possessiondetailsService = possessiondetailsService;
        }
        //[AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            PossessionReportDtoProfile demandletter = new PossessionReportDtoProfile();
            ViewBag.PossessionDateList = await _possessiondetailsService.BindPossessionDateList();
            return View(demandletter);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] PossessionReportSearchDto model)
        {
            var result = await _possessiondetailsService.GetPagedPossessionReport(model);

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
        public async Task<IActionResult> GetAllPossionReportDetails([FromBody] PossessionReportSearchDto model)
        {
            var result = await _possessiondetailsService.GetAllPossessionReport(model);
            List<PossitionReportDetailsDto> data = new List<PossitionReportDetailsDto>();
            if (result !=null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new PossitionReportDetailsDto()
                    {
                        VillageName = result[i].Village == null?"":result[i].Village.Name,
                        KhasraNo = result[i].Khasra == null?"":result[i].Khasra.Name,
                        Area = result[i].Khasra.Bigha == 0 ? null : result[i].Khasra.Bigha.ToString() + "-" + result[i].Khasra.Biswa.ToString() + "_" + result[i].Khasra.Biswanshi.ToString(),
                        NotifiedArea =  result[i].Bigha.ToString() + "_" + result[i].Biswa.ToString() +"_"+0,
                        PossessionDate =  Convert.ToDateTime(result[i].PossDate).ToString("dd-MMM-yyyy")
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
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PossessionReport.xlsx");
        } 
    }
}