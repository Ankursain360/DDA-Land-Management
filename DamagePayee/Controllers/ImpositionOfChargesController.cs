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
using DamagePayee.Filters;
using Core.Enum;
using System.Collections.Generic;
using Utility.Helper;
using Microsoft.AspNetCore.Http;

namespace DamagePayee.Controllers
{
    public class ImpositionOfChargesController : BaseController
    {
        private readonly IDemandLetterService _demandLetterService;

        public ImpositionOfChargesController(IDemandLetterService demandLetterService)
        {
            _demandLetterService = demandLetterService;
        }


       [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            ImpositionOfChargesDtoProfile demandletter = new ImpositionOfChargesDtoProfile();
            ViewBag.FileNoList = await _demandLetterService.BindFileNoList();
            ViewBag.LocalityList = await _demandLetterService.BindLoclityList();
            return View(demandletter);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] ImpositionOfChargesSearchDto model)
        {
            var result = await _demandLetterService.GetPagedImpositionReportOfCharges(model);

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

        public async Task<IActionResult> GetImpositionReportList([FromBody] ImpositionOfChargesSearchDto model)
        {
            var result = await _demandLetterService.GetImpositionReportOfChargesList(model);
            List<ImpositionOfChargesListDto> data = new List<ImpositionOfChargesListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new ImpositionOfChargesListDto()
                    {
                        Locality = result[i].Locality == null ?"":result[i].Locality.Name,
                        FileNo = result[i].FileNo,
                        PropertyNo = result[i].PropertyNo,
                        DemandNumber = result[i].DemandNo,
                        DemandAmount = result[i].DepositDue,
                        Damagecharges = result[i].DamageCharges
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
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }
    }
}