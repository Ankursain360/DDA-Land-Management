﻿using System;
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
using Dto.Master;

namespace SiteMaster.Controllers
{
    public class InterestController : BaseController
    {

        private readonly IInterestService _interestService;

        public InterestController(IInterestService interestService)
        {
            _interestService = interestService;
        }

      
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] InterestSearchDto model)
        {
            var result = await _interestService.GetSearchResult(model);
            return PartialView("_List", result);
        }
        async Task BindDropDown(Interest interest)
        {
            interest.PropertyTypeList = await _interestService.GetDropDownList();
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Interest interest = new Interest();
            interest.IsActive = 1;
            await BindDropDown(interest);
            return View(interest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Interest interest)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (interest.ToDate <= interest.FromDate)
                    {
                        ViewBag.Message = Alert.Show("To Date Must be Greater Than From Date", "", AlertType.Warning);
                        return View(interest);
                    }
                    var result = await _interestService.Create(interest);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var result1 = await _interestService.GetAllInterest();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(interest);

                    }
                }
                else
                {
                    return View(interest);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(interest);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _interestService.FetchSingleResult(id);
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
        public async Task<IActionResult> Edit(int id, Interest interest)
        {
            await BindDropDown(interest);
            if (ModelState.IsValid)
            {
                try
                {
                    if (interest.ToDate <= interest.FromDate)
                    {
                        ViewBag.Message = Alert.Show("To Date Must be Greater Than From Date", "", AlertType.Warning);
                        return View(interest);
                    }
                    var result = await _interestService.Update(id, interest);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var result1 = await _interestService.GetAllInterest();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(interest);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(interest);
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)  // Used to Perform Delete Functionality added by Renu
        {

            var result = await _interestService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _interestService.GetAllInterest();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _interestService.GetAllInterest();
                return View("Index", result1);
            }

        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _interestService.FetchSingleResult(id);
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
            Interest interest = new Interest();
            propertyId = propertyId ?? 0;
            int IsRecordExist = _interestService.IsRecordExist(Convert.ToInt32(propertyId));
            var result = _interestService.GetFromDateData(Convert.ToInt32(propertyId));
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
        public async Task<IActionResult> InterestList()
        {
            var result = await _interestService.GetAllInterest();
            List<InterestListDto> data = new List<InterestListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new InterestListDto()
                    {
                        Id = result[i].Id,
                        PropertyType = result[i].Property == null ? "": result[i].Property.Name,
                        FromDate = result[i].FromDate.ToString("dd-MMM-yyyy"),
                        ToDate = result[i].ToDate.ToString("dd-MMM-yyyy"),
                        InterestPercentage = result[i].Percentage.ToString(),
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

    }

}
