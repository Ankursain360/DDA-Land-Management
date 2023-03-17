
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
using DamagePayee.Filters;
using Core.Enum;
using Dto.Master;
using Utility.Helper;
using Microsoft.AspNetCore.Http;

namespace DamagePayee.Controllers
{
    public class DefaulterListingReportController : BaseController
    {
        private readonly IDemandLetterService _demandLetterService;

        public DefaulterListingReportController(IDemandLetterService demandLetterService)
        {
            _demandLetterService = demandLetterService;
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            Demandletters model = new Demandletters();

            model.Damagelist = await _demandLetterService.GetAllDemandletter();
            return View(model);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] DefaulterListingReportSearchDto defaulterListingReportSearchDto)
        {
            var result = await _demandLetterService.GetDefaultListingReportData(defaulterListingReportSearchDto);
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
        public async Task<IActionResult> GetDefaultListingReportList([FromBody] DefaulterListingReportSearchDto model)
        {
            var result = await _demandLetterService.GetDefaultListingReportDataList(model);
            List<DefaulterListingReportListDto> data = new List<DefaulterListingReportListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new DefaulterListingReportListDto()
                    {
                        Locality = result[i].Locality ==null?"":result[i].Locality.Name,
                        FileNo = result[i].FileNo,
                        DemandNo = result[i].DemandNo,
                        DueAmount = result[i].DepositDue,
                        PaymentDueDate = result[i].UptoDate
                    });

                }

            }
            var memory = ExcelHelper.CreateExcel(data);
            HttpContext.Session.Set("file", memory);
            return Ok();
        }
        [HttpGet]
        public virtual ActionResult download()
        {
            byte[] data = HttpContext.Session.Get("file") as byte[];
            HttpContext.Session.Remove("file");
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DefaultListingReport.xlsx");
        }
    }
}
