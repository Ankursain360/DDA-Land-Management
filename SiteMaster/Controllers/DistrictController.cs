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
            //var result = await _districtService.GetAllDistrict();
            return View();

        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody]DistrictSearchDto model)
        {
            var result = await _districtService.GetPagedDistrict(model);
            return PartialView("_List", result);
        }

        public IActionResult Create()
        {

            return View();

        }



        private bool Exist(int id, District district)
        {
            var result = _districtService.CheckUniqueName(id, district);
            return result;
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(District district)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (Exist(0, district))
                    {
                        ViewBag.Message = Alert.Show("Unique Name Required for Designation Name", "", AlertType.Info);
                        return View(district);

                    }

                    var result = await _districtService.Create(district);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        return View("Index");
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, District district)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (Exist(id, district))
                    {
                        ViewBag.Message = Alert.Show("Unique Name Required for District Name", "", AlertType.Info);
                        return View(district);

                    }

                    var result = await _districtService.Update(id, district);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        return View("Index");

                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(district);

                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Exist(district.Id, district))
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(district);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(district);
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



        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var form = await _districtService.Delete(id);
            if (form == false)
            {
                return NotFound();
            }

            ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            return View(form);
        }




        public async Task<IActionResult> DeleteConfirmed(int id)
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
            return RedirectToAction("Index", "District");


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



    }
}