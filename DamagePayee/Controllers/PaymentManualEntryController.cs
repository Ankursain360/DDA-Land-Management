using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using System.Threading.Tasks;
using DamagePayee.Filters;
using Core.Enum;
using Dto.Master;
using Utility.Helper;
using System.Collections.Generic;

namespace DamagePayee.Controllers
{
    
    public class PaymentManualEntryController : Controller
    {
        private readonly IPaymentverificationService _paymentverificationService;


        public PaymentManualEntryController(IPaymentverificationService paymentverification)
        {
            _paymentverificationService = paymentverification;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }
        [AuthorizeContext(ViewAction.Add)]
        public IActionResult Create()
        {
            return View();
        }


        //[HttpPost]
        //public async Task<PartialViewResult> GetDetails([FromBody] ManualPaymentSearchDto model)
        //{
        //    try
        //    {
        //        var result = await _paymentverificationService.GetPagedPaymentverification(model);
        //        if (result != null)
        //        {
        //            return PartialView("_List", result);
        //        }
               
        //    }
        //    catch (Exception ex)
        //    {
        //        return PartialView(ex);
        //    }
        //}


        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] ManualPaymentSearchDto model)
        {
            var result = await _paymentverificationService.GetPagedPaymentverification(model);

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

        [HttpPost]
        public async Task<IActionResult> Create(Paymentverification paymentverification)
        {
            try
            {              
                if (ModelState.IsValid)
                {

                    var result = await _paymentverificationService.Create(paymentverification);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _paymentverificationService.GetAllPaymentList();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(paymentverification);
                    }
                }
                else
                {
                    return View(paymentverification);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(paymentverification);
            }

        }

        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {

            var Data = await _paymentverificationService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Paymentverification paymentverification)
        {

            if (ModelState.IsValid)
            {
                var result = await _paymentverificationService.Update(id, paymentverification);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    var list = await _paymentverificationService.GetAllPaymentList();
                    return View("Index", list);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(paymentverification);
                }
            }
            return View(paymentverification);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {

            var Data = await _paymentverificationService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> PaymentManaulEntryList([FromBody] ManualPaymentSearchDto model)
        {
            var result = await _paymentverificationService.GetAllPaymentVerificationList(model);
            List<ManualPaymentEntryListDto> data = new List<ManualPaymentEntryListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new ManualPaymentEntryListDto()
                    {
                        Id = result[i].Id,
                        FleNo = result[i].FileNo,
                        PayeeName=result[i].PayeeName,
                        TotalAmount =Convert.ToDecimal( result[i].TotalAmount),
                        BankName = result[i].BankName,
                        PaymentMode = result[i].PaymentMode,
                        PropertyNo=result[i].PropertyNo,
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            TempData["file"] = memory;
            //return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
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
