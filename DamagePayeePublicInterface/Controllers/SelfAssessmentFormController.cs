using DamagePayeePublicInterface.Helper;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamagePayeePublicInterface.Controllers
{
    public class SelfAssessmentFormController : Controller
    {
        private readonly ISelfAssessmentDamageService _selfAssessmentDamageService;
        public SelfAssessmentFormController(ISelfAssessmentDamageService selfAssessmentDamageService)
        {

            _selfAssessmentDamageService = selfAssessmentDamageService;

        }

        async Task BindDropDown(SelfAssessmentForm selfAssessmentForm)
        {
            selfAssessmentForm.LocalityList = await _selfAssessmentDamageService.GetLocalityList();
            selfAssessmentForm.DistrictList = await _selfAssessmentDamageService.GetDistrictList();
        }

        public async Task<IActionResult> Create()
        {
            SelfAssessmentForm selfAssessmentForm = new SelfAssessmentForm();
            // For Print Button and Payment Button Show approval only  By Pankaj

            // var Data = await _selfAssessmentDamageService.FetchSelfAssessmentUserId(;
            // var value = await _selfAssessmentDamageService.GetRebateValue();
            //if (value == null)
            //     ViewBag.RebateValue = 0;
            // else
            //     ViewBag.RebateValue = Math.Round((value.RebatePercentage), 2);

            // Data.EncryptData = SetEncriptionKey();

            //if (Data != null)
            //{
            //     //Data.EncryptData = SetEncriptionKey();
            //    ViewBag.MainDamagePayeeId = Data.Id;
            //     await BindDropDown(Data);
            //     return View(Data);
            //  }
            // else
            // {
            //damagepayeeregistertemp.EncryptData = SetEncriptionKey();
            ViewBag.MainDamagePayeeId = 0;
            await BindDropDown(selfAssessmentForm);
            return View(selfAssessmentForm);
            // }





        }
       // [HttpPost]
       // public async Task<IActionResult> Create(SelfAssessmentForm selfAssessmentForm)
       // {
       //     if (ModelState.IsValid)
       //     {
       //         var result = await _selfAssessmentDamageService.Create(selfAssessmentForm);
       //         if (result)
       //         {
       //         }
       //     }
        //}
    }
}
