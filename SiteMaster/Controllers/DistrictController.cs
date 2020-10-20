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

namespace SiteMaster.Controllers
{
    public class DistrictController : BaseController
    {

        private readonly IDistrictService _districtService;

        public DistrictController(IDistrictService districtService)
        {
            _districtService = districtService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _districtService.GetAllDistrict();
            return View(result);
        }
        public async Task<PartialViewResult> List([FromBody] DistrictSearchDto model)
        {
            var result = await _districtService.GetPagedDistrict(model);
            return PartialView("_List", result);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(District district)
        {
            try
            {

                if (ModelState.IsValid)
                {


                    var result = await _districtService.Create(district);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        //return View();
                        var list = await _districtService.GetAllDistrict();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(district);

                    }
                }
                else
                {
                    return View(district);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(district);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _districtService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, District district)
        {
            if (ModelState.IsValid)
            {
                try
                {



                    var result = await _districtService.Update(id, district);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                        var list = await _districtService.GetAllDistrict();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(district);

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(district);

                }
            }
            return View(district);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _districtService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Department: {Name} already exist");
            }
        }



        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Renu
        {
            //try
            //{

            var result = await _districtService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            return RedirectToAction("Index", "District");
            //}
            //catch(Exception ex)
            //{
            //    ViewData["Msg"] = new Message { Msg = "Dear User,<br/>Something went wrong", Status = "S", BackPageAction = "Index", BackPageController = "Designation" };
            //    return View();
            //}

        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _districtService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        public async Task<IActionResult> Delete(int id)  // Used to Perform Delete Functionality added by Praveen
        {
            try
            {

                var result = await _districtService.Delete(id);
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
            var list = await _districtService.GetAllDistrict();
            return View("Index", list);
        }
    }

}
