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

namespace DamagePayee.Controllers
{
    public class NoticeGenerationReportController : BaseController
    {
        private readonly INoticeToDamagePayeeService _noticeToDamagePayeeService;

        public NoticeGenerationReportController(INoticeToDamagePayeeService noticeToDamagePayeeService)
        {
            _noticeToDamagePayeeService = noticeToDamagePayeeService;
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            Noticetodamagepayee model = new Noticetodamagepayee();

            model.FileNoList = await _noticeToDamagePayeeService.GetFileNoList();
            return View(model);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] NoticeGenerationReportSearchDto notice)
        {
            var result = await _noticeToDamagePayeeService.GetPagedNoticeGenerationReport(notice);
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
        public async Task<IActionResult> GetNoticeGenerationReportList([FromBody] NoticeGenerationReportSearchDto model)
        {
            var result = await _noticeToDamagePayeeService.GetAllNoticeGenerationReportList(model); 
            List<GetNoticeGenerationReportListDto> data = new List<GetNoticeGenerationReportListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new GetNoticeGenerationReportListDto()
                    {
                        
                        FileNo = result[i].FileNo,
                        PayeeName = result[i].Name,
                        PropertyNo = result[i].PropertyDetails,
                        Address = result[i].Address,
                        No_ofNoticeGenerated_Issued = result[i].FileNo.Count().ToString()
                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            TempData["file"] = memory;
            return Ok();

        }
        [HttpGet]
        public virtual ActionResult download()
        {
            byte[] data = TempData["file"] as byte[];
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }
    }
}
