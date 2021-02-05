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
    public class RateController : BaseController
    {

        private readonly IRateService _rateService;

        public RateController(IRateService rateService)
        {
            _rateService = rateService;
        }

        //public async Task<IActionResult> Index()
        //{
        //    var result = await _rateService.GetAllRate();
        //    return View(result);
        //}
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody]RateSearchDto model)
        {
            var result = await _rateService.GetSearchResult(model);
            return PartialView("_List", result);
        }
        async Task BindDropDown(Rate rate)
        {
            rate.PropertyTypeList = await _rateService.GetDropDownList();
        }


        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Rate rate = new Rate();
            rate.IsActive = 1;
            await BindDropDown(rate);
            return View(rate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Rate rate)
        {
            try
            {
                await BindDropDown(rate);
                if (ModelState.IsValid)
                {
                    if (rate.ToDate <= rate.FromDate)
                    {
                        ViewBag.Message = Alert.Show("To Date Must be Greater Than From Date", "", AlertType.Warning);
                        return View(rate);
                    }
                    var result = await _rateService.Create(rate);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        
                        var result1 = await _rateService.GetAllRate();
                        return View("Index", result1);
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
            var Data = await _rateService.FetchSingleResult(id);
            await BindDropDown(Data);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Rate rate)
        {
            await BindDropDown(rate);
            if (ModelState.IsValid)
            {
                try
                {
                    if (rate.ToDate <= rate.FromDate)
                    {
                        ViewBag.Message = Alert.Show("To Date Must be Greater Than From Date", "", AlertType.Warning);
                        return View(rate);
                    }
                    var result = await _rateService.Update(id, rate);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var result1 = await _rateService.GetAllRate();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(rate);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(rate);
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)  // Used to Perform Delete Functionality added by Renu
        {

            var result = await _rateService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _rateService.GetAllRate();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _rateService.GetAllRate();
                return View("Index", result1);
            }

        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _rateService.FetchSingleResult(id);
            await BindDropDown(Data);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpGet]
        public string GetFromDate(int? propertyId)
        {
            Rate rate = new Rate();
            propertyId = propertyId ?? 0;
            int IsRecordExist = _rateService.IsRecordExist(Convert.ToInt32(propertyId));
            var result = _rateService.GetFromDateData(Convert.ToInt32(propertyId));
            if (IsRecordExist ==0)
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
            List<Rate> result = await _rateService.GetAllRate();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"Rate.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }
    }

}
