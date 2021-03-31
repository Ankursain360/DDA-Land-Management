using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using LeaseDetails.Filters;
using Utility.Helper;

namespace LeaseDetails.Controllers
{

    public class LeasesubpurposeController : BaseController
    {
        private readonly ILeasesubpurposeService _LeasesubpurposeService;

        public LeasesubpurposeController(ILeasesubpurposeService LeasesubpurposeService)
        {
            _LeasesubpurposeService = LeasesubpurposeService;
        }
        //  [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] LeasesubpurposeSearchDto model)
        {

            var result = await _LeasesubpurposeService.GetPagedLeasesubpurpose(model);
            return PartialView("_List", result);
        }
        //   [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Leasesubpurpose Leasesubpurpose = new Leasesubpurpose();
            Leasesubpurpose.IsActive = 1;

            Leasesubpurpose.LeasePurposeUseList = await _LeasesubpurposeService.GetAllLeasepurpose();
           
            return View(Leasesubpurpose);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //  [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Leasesubpurpose Leasesubpurpose)
        {
            Leasesubpurpose.LeasePurposeUseList = await _LeasesubpurposeService.GetAllLeasepurpose();
       
            try
            {

                if (ModelState.IsValid)
                {

                    Leasesubpurpose.CreatedBy = SiteContext.UserId;
                    var result = await _LeasesubpurposeService.Create(Leasesubpurpose);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        //return View();
                        var list = await _LeasesubpurposeService.GetAllLeaseSubpurpose();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(Leasesubpurpose);

                    }
                }
                else
                {
                    return View(Leasesubpurpose);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(Leasesubpurpose);
            }
        }
        // [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _LeasesubpurposeService.FetchSingleResult(id);
            //Data.PropertyTypeList = await _premiumrateService.GetAllPropertyType();
            Data.LeasePurposeUseList = await _LeasesubpurposeService.GetAllLeasepurpose();
          
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        //  [ValidateAntiForgeryToken]
        //  [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Leasesubpurpose Leasesubpurpose)
        {
            Leasesubpurpose.LeasePurposeUseList = await _LeasesubpurposeService.GetAllLeasepurpose();
         
            if (ModelState.IsValid)
            {
                try
                {

                    Leasesubpurpose.ModifiedBy = SiteContext.UserId;
                    var result = await _LeasesubpurposeService.Update(id, Leasesubpurpose);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                        var list = await _LeasesubpurposeService.GetAllLeaseSubpurpose();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(Leasesubpurpose);

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(Leasesubpurpose);

                }
            }
            return View(Leasesubpurpose);
        }

        //  [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _LeasesubpurposeService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _LeasesubpurposeService.GetAllLeaseSubpurpose();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _LeasesubpurposeService.GetAllLeaseSubpurpose();
                return View("Index", result1);
            }
        }

        //  [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _LeasesubpurposeService.FetchSingleResult(id);
            //Data.PropertyTypeList = await _premiumrateService.GetAllPropertyType();
            Data.LeasePurposeUseList = await _LeasesubpurposeService.GetAllLeasepurpose();
            
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        //[AuthorizeContext(ViewAction.Download)]
        //public async Task<IActionResult> Download()
        //{
        //    List<Premiumrate> result = await _LeasesubpurposeService.GetAllLeasepurpose();
        //    var memory = ExcelHelper.CreateExcel(result);
        //    string sFileName = @"Premiumrate.xlsx";
        //    return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        //}


    }
}
