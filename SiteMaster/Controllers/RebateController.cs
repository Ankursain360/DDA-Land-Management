using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using SiteMaster.Models;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Dto.Search;
using SiteMaster.Filters;
using Core.Enum;
using Utility.Helper;

namespace SiteMaster.Controllers
{
    public class RebateController : BaseController
    {

        private readonly IRebateService _rebateService;

        public RebateController(IRebateService rebateService)
        {
            _rebateService = rebateService;
        }
       


        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
       
        public async Task<PartialViewResult> List([FromBody] RebateSearchDto model)
        {
            var result = await _rebateService.GetPagedRebate(model);
            return PartialView("_List", result);
        }


        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Rebate rebate = new Rebate();
            rebate.IsActive = 1;
            return View(rebate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Rebate rebate)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if(rebate.ToDate <= rebate.FromDate)
                    {
                        ViewBag.Message = Alert.Show("To Date Must be Greater Than From Date", "", AlertType.Warning);
                        return View(rebate);
                    }
                    if (rebate.IsRebateOn == 0)
                        rebate.IsRebateOn = 0;
                    else if (rebate.IsRebateOn == 1)
                        rebate.IsRebateOn = 1;
                    else if (rebate.IsRebateOn == 2)
                        rebate.IsRebateOn = 2;

                    var result = await _rebateService.Create(rebate);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var result1 = await _rebateService.GetAllRebate();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(rebate);

                    }
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(rebate);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(rebate);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _rebateService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Rebate rebate)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (rebate.ToDate <= rebate.FromDate)
                    {
                        ViewBag.Message = Alert.Show("To Date Must be Greater Than From Date", "", AlertType.Warning);
                        return View(rebate);
                    }
                    if (rebate.IsRebateOn == 0)
                        rebate.IsRebateOn = 0;
                    else if (rebate.IsRebateOn == 1)
                        rebate.IsRebateOn = 1;
                    else if (rebate.IsRebateOn == 2)
                        rebate.IsRebateOn = 2;
                    var result = await _rebateService.Update(id, rebate);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var result1 = await _rebateService.GetAllRebate();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(rebate);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(rebate);
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)  // Used to Perform Delete Functionality added by Renu
        {

            var result = await _rebateService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _rebateService.GetAllRebate();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _rebateService.GetAllRebate();
                return View("Index", result1);
            }

        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _rebateService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpGet]
        public string GetFromDate(int? propertyId)
        {
            Rebate rebate = new Rebate();
            propertyId = propertyId ?? 0;
            int IsRecordExist = _rebateService.IsRecordExist(Convert.ToInt32(propertyId));
            var result = _rebateService.GetFromDateData(Convert.ToInt32(propertyId));
            if (IsRecordExist == 0)
            {
                return "";
            }
            else
            {
                DateTime lastFromDate = Convert.ToDateTime(result);
                DateTime NewDate = lastFromDate.AddYears(1);
                string newFromDate = NewDate.ToString("dd-MMM-yyyy");
                return newFromDate;
            }
        }

        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Download()
        {
            List<Rebate> result = await _rebateService.GetAllRebate();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"Rebate.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }
    }

}
