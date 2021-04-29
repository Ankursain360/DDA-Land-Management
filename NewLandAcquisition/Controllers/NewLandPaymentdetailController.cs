using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;
using NewLandAcquisition.Filters;
using Core.Enum;
using Dto.Master;
using Utility.Helper;

namespace NewLandAcquisition.Controllers
{
    public class NewLandPaymentdetailController : Controller
    {
        private readonly INewLandPaymentdetailService _newLandPaymentdetailService;


        public NewLandPaymentdetailController(INewLandPaymentdetailService newLandPaymentdetailService)
        {
            _newLandPaymentdetailService = newLandPaymentdetailService;
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            var list = await _newLandPaymentdetailService.GetAllPaymentdetail();
            return View(list);
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NewLandPaymentDetailSearchDto model)
        {
            var result = await _newLandPaymentdetailService.GetPagedPaymentdetail(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Newlandpaymentdetail newlandpaymentdetail)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    var result = await _newLandPaymentdetailService.Create(newlandpaymentdetail);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _newLandPaymentdetailService.GetAllPaymentdetail();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(newlandpaymentdetail);
                    }
                }
                else
                {
                    return View(newlandpaymentdetail);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(newlandpaymentdetail);
            }
        }



        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _newLandPaymentdetailService.FetchSingleResult(id);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Newlandpaymentdetail newlandpaymentdetail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _newLandPaymentdetailService.Update(id, newlandpaymentdetail);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _newLandPaymentdetailService.GetAllPaymentdetail();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(_newLandPaymentdetailService);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(_newLandPaymentdetailService);
                }
            }
            else
            {
                return View(newlandpaymentdetail);
            }
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _newLandPaymentdetailService.Delete(id);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            var list = await _newLandPaymentdetailService.GetAllPaymentdetail();
            return View("Index", list);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _newLandPaymentdetailService.FetchSingleResult(id);




            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        public async Task<IActionResult> NewLandPaymentDetailsList()
        {
            var result = await _newLandPaymentdetailService.GetAllPaymentdetail();
            List<NewLandPaymentDelailsListDto> data = new List<NewLandPaymentDelailsListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new NewLandPaymentDelailsListDto()
                    {
                        Id = result[i].Id,
                        DemandListNo = result[i].DemandListNo,
                        EnmSno = result[i].EnmSno,
                        BankName = result[i].BankName,
                        VoucherNo = result[i].VoucherNo,
                        ChequeNo = result[i].ChequeNo,
                        ChequeDate = result[i].ChequeDate.ToString(),
                        PercentPaid = result[i].PercentPaid.ToString(),
                        IsActive = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }























    }
}

