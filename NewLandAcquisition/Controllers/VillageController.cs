using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;
using NewLandAcquisition.Filters;
using Core.Enum;
using System.Data;
using Newtonsoft.Json;
using Utility.Helper;
using System.Collections.Generic;

namespace NewLandAcquisition.Controllers
{
    public class VillageController : BaseController
    {
        private readonly INewlandvillageService _newlandvillageService;
       
        public VillageController(INewlandvillageService newlandvillageService)
        {
            _newlandvillageService = newlandvillageService;
        }

        // GET: VillageController
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NewlandvillageSearchDto model)
        {
            var result = await _newlandvillageService.GetPagedNewlandvillage(model);

            return PartialView("_List", result);
        }


        //[AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Newlandvillage newlandvillage = new Newlandvillage();
            newlandvillage.IsActive = 1;
            newlandvillage.DistrictList = await _newlandvillageService.GetAllDistrict();
            newlandvillage.TehsilList = await _newlandvillageService.GetAllTehsil();
            newlandvillage.ZoneList = await _newlandvillageService.GetAllZone();
            return View(newlandvillage);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
       // [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Newlandvillage newlandvillage)
        {
            try
            {
                newlandvillage.DistrictList = await _newlandvillageService.GetAllDistrict();
                newlandvillage.TehsilList = await _newlandvillageService.GetAllTehsil();
                newlandvillage.ZoneList = await _newlandvillageService.GetAllZone();

                if (ModelState.IsValid)
                {
                    newlandvillage.CreatedBy = SiteContext.UserId;
                    var result = await _newlandvillageService.Create(newlandvillage);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _newlandvillageService.GetNewlandvillage();
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(newlandvillage);
                    }
                }
                else
                {
                    return View(newlandvillage);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(newlandvillage);
            }
        }



      //  [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _newlandvillageService.FetchSingleResult(id);
            Data.DistrictList = await _newlandvillageService.GetAllDistrict();
            Data.TehsilList = await _newlandvillageService.GetAllTehsil();
            Data.ZoneList = await _newlandvillageService.GetAllZone();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
      //  [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Newlandvillage newlandvillage)
        {

            newlandvillage.DistrictList = await _newlandvillageService.GetAllDistrict();
            newlandvillage.TehsilList = await _newlandvillageService.GetAllTehsil();
            newlandvillage.ZoneList = await _newlandvillageService.GetAllZone();

            if (ModelState.IsValid)
            {
                try
                {
                    newlandvillage.ModifiedBy = SiteContext.UserId;
                    var result = await _newlandvillageService.Update(id, newlandvillage);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _newlandvillageService.GetNewlandvillage();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(newlandvillage);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(newlandvillage);
                }
            }
            else
            {
                return View(newlandvillage);
            }
        }


     //   [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _newlandvillageService.Delete(id);
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
            var list = await _newlandvillageService.GetNewlandvillage();
            return View("Index", list);
        }


        //[AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _newlandvillageService.FetchSingleResult(id);
            Data.DistrictList = await _newlandvillageService.GetAllDistrict();
            Data.TehsilList = await _newlandvillageService.GetAllTehsil();
            Data.ZoneList = await _newlandvillageService.GetAllZone();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        public async Task<IActionResult> Download()
        {
            List<Newlandvillage> result = await _newlandvillageService.GetNewlandvillage();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"village.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }

    }
}
