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
namespace AcquiredLandInformationManagement.Controllers
{
    public class PaymentdetailController : Controller
    {
        private readonly IPaymentdetailService _paymentdetailService;


        public PaymentdetailController(IPaymentdetailService paymentdetailService)
        {
            _paymentdetailService = paymentdetailService;
        }

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

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
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

        public async Task<IActionResult> View(int id)
        {
            var Data = await _paymentdetailService.FetchSingleResult(id);




            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }




    }
}

