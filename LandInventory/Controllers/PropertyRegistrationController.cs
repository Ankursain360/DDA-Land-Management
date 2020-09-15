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
using LandInventory.Models;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;

namespace SiteMaster.Controllers
{
    public class PropertyRegistrationController : Controller
    {

        private readonly IPropertyRegistrationService _propertyregistrationService;

        public PropertyRegistrationController(IPropertyRegistrationService propertyregistrationService)
        {
            _propertyregistrationService = propertyregistrationService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _propertyregistrationService.GetAllPropertyregistration();
            return View(result);
        }

        async Task BindDropDown(Propertyregistration propertyregistration)
        {
            propertyregistration.ClassificationOfLandList = await _propertyregistrationService.GetClassificationOfLandDropDownList();
            propertyregistration.ZoneList = await _propertyregistrationService.GetZoneDropDownList();
            propertyregistration.LocalityList = await _propertyregistrationService.GetLocalityDropDownList();
            propertyregistration.LandUseList = await _propertyregistrationService.GetLandUseDropDownList();
            propertyregistration.DisposalTypeList = await _propertyregistrationService.GetDisposalTypeDropDownList();
        }
        public async Task<IActionResult> Create()
        {
            Propertyregistration propertyregistration = new Propertyregistration();
            await BindDropDown(propertyregistration);
            return View(propertyregistration);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Propertyregistration propertyregistration)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var result = await _propertyregistrationService.Create(propertyregistration);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        await BindDropDown(propertyregistration);
                        return View(propertyregistration);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(propertyregistration);

                    }
                }
                else
                {
                    return View(propertyregistration);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(propertyregistration);
            }
        }

        //public async Task<IActionResult> Edit(int id)
        //{
        //    var Data = await _propertyregistrationService.FetchSingleResult(id);
        //    await BindDropDown(Data);
        //    if (Data == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(Data);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, Propertyregistration propertyregistration)
        //{
        //    await BindDropDown(propertyregistration);
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var result = await _propertyregistrationService.Update(id, propertyregistration);
        //            if (result == true)
        //            {
        //                ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
        //                var result1 = await _propertyregistrationService.GetAllPropertyregistration();
        //                return View("Index", result1);
        //            }
        //            else
        //            {
        //                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //                return View(propertyregistration);

        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //    }
        //    return View(propertyregistration);
        //}

        //public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Renu
        //{

        //    var result = await _propertyregistrationService.Delete(id);
        //    if (result == true)
        //    {
        //        ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
        //        var result1 = await _propertyregistrationService.GetAllPropertyregistration();
        //        return View("Index", result1);
        //    }
        //    else
        //    {
        //        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //        var result1 = await _propertyregistrationService.GetAllPropertyregistration();
        //        return View("Index", result1);
        //    }

        //}

        //public async Task<IActionResult> View(int id)
        //{
        //    var Data = await _propertyregistrationService.FetchSingleResult(id);
        //    await BindDropDown(Data);
        //    if (Data == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(Data);
        //}

        //[HttpGet]
        //public string GetFromDate(int? propertyId)
        //{
        //    propertyId = propertyId ?? 0;
        //    var result = _propertyregistrationService.GetFromDateData(Convert.ToInt32(propertyId));
        //    DateTime lastFromDate = Convert.ToDateTime(result);
        //    DateTime NewDate = lastFromDate.AddYears(1);
        //    string newFromDate = NewDate.ToString("dd-MMM-yyyy");
        //    return (string)newFromDate;
        //}
    }

}
