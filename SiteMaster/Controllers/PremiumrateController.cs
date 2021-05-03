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
using SiteMaster.Filters;
using Utility.Helper;

namespace SiteMaster.Controllers
{
  
    public class PremiumrateController : BaseController
    {
        private readonly IPremiumrateService _premiumrateService;

        public PremiumrateController(IPremiumrateService premiumrateService)
        {
            _premiumrateService = premiumrateService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] PremiumrateSearchDto model)
        {

            var result = await _premiumrateService.GetPagedPremiumrate(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Premiumrate rate = new Premiumrate();
            rate.IsActive = 1;
          
            rate.LeasePurposeList = await _premiumrateService.GetAllLeasepurpose();
            rate.LeaseSubPurposeList = await _premiumrateService.GetAllLeaseSubpurpose(rate.LeasePurposesTypeId);
            return View(rate);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Premiumrate rate)
        {
            rate.LeasePurposeList = await _premiumrateService.GetAllLeasepurpose();
            rate.LeaseSubPurposeList = await _premiumrateService.GetAllLeaseSubpurpose(rate.LeasePurposesTypeId);
            try
            {

                if (ModelState.IsValid)
                {
                    
                    rate.CreatedBy = SiteContext.UserId;
                     var result = await _premiumrateService.Create(rate);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        //return View();
                        var list = await _premiumrateService.GetAllPremiumrate();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(rate);

                    }
                }
                else
                {
                    return View(rate);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(rate);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _premiumrateService.FetchSingleResult(id);
            //Data.PropertyTypeList = await _premiumrateService.GetAllPropertyType();
            Data.LeasePurposeList = await _premiumrateService.GetAllLeasepurpose();
            Data.LeaseSubPurposeList = await _premiumrateService.GetAllLeaseSubpurpose(Data.LeasePurposesTypeId);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Premiumrate rate)
        {
            rate.LeasePurposeList = await _premiumrateService.GetAllLeasepurpose();
            rate.LeaseSubPurposeList = await _premiumrateService.GetAllLeaseSubpurpose(rate.LeasePurposesTypeId);
            if (ModelState.IsValid)
            {
                try
                {
                   
                    rate.ModifiedBy = SiteContext.UserId;
                    var result = await _premiumrateService.Update(id, rate);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                        var list = await _premiumrateService.GetAllPremiumrate();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(rate);

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(rate);

                }
            }
            return View(rate);
        }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _premiumrateService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _premiumrateService.GetAllPremiumrate();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _premiumrateService.GetAllPremiumrate();
                return View("Index", result1);
            }
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _premiumrateService.FetchSingleResult(id);
            //Data.PropertyTypeList = await _premiumrateService.GetAllPropertyType();
            Data.LeasePurposeList = await _premiumrateService.GetAllLeasepurpose();
            Data.LeaseSubPurposeList = await _premiumrateService.GetAllLeaseSubpurpose(Data.LeasePurposesTypeId);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Download()
        {
            List<Premiumrate> result = await _premiumrateService.GetAllPremiumrate();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"Premiumrate.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }
        [HttpGet]
        public async Task<JsonResult> GetAllLeaseSubpurpose(int? purposeUseId)
        {
            purposeUseId = purposeUseId ?? 0;
            return Json(await _premiumrateService.GetAllLeaseSubpurpose(Convert.ToInt32(purposeUseId)));
        }
    }
}
