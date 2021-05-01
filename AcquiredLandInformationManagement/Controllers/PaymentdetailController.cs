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
using AcquiredLandInformationManagement.Filters;
using Core.Enum;
using Utility.Helper;
using Dto.Master;

namespace AcquiredLandInformationManagement.Controllers
{
    public class PaymentdetailController : Controller
    {
        private readonly IPaymentdetailService _paymentdetailService;


        public PaymentdetailController(IPaymentdetailService paymentdetailService)
        {
            _paymentdetailService = paymentdetailService;
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            var list = await _paymentdetailService.GetAllPaymentdetail();
            return View(list);
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] PaymentdetailSearchDto model)
        {
            var result = await _paymentdetailService.GetPagedPaymentdetail(model);
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
        public async Task<IActionResult> Create(Paymentdetail paymentdetail)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    var result = await _paymentdetailService.Create(paymentdetail);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _paymentdetailService.GetAllPaymentdetail();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(paymentdetail);
                    }
                }
                else
                {
                    return View(paymentdetail);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(paymentdetail);
            }
        }



        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _paymentdetailService.FetchSingleResult(id);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Paymentdetail paymentdetail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _paymentdetailService.Update(id, paymentdetail);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _paymentdetailService.GetAllPaymentdetail();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(paymentdetail);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(paymentdetail);
                }
            }
            else
            {
                return View(paymentdetail);
            }
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _paymentdetailService.Delete(id);
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
            var list = await _paymentdetailService.GetAllPaymentdetail();
            return View("Index", list);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _paymentdetailService.FetchSingleResult(id);




            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        [AuthorizeContext(ViewAction.Download)]

        public async Task<IActionResult> PaymentdetailList()
        {
            var result = await _paymentdetailService.GetAllPaymentdetail();
            List<PaymentdetailListDto> data = new List<PaymentdetailListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new PaymentdetailListDto()
                    {
                        Id = result[i].Id,
                        DemandListNo = result[i].DemandListNo,
                        ENMSNO = result[i].EnmSno,
                        BankName = result[i].BankName,
                        VoucherNo = result[i].VoucherNo,
                        ChequeNo = result[i].ChequeNo,
                        ChequeDate = Convert.ToDateTime(result[i].ChequeDate).ToString("dd-MMM-yyyy"),
                        PercentPaid = result[i].PercentPaid.ToString(),
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }

    }
}

