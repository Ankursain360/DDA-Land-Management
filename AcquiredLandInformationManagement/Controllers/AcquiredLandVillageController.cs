using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;
using AcquiredLandInformationManagement.Filters;
using Core.Enum;
using System.Data;
using Newtonsoft.Json;
using Utility.Helper;
using System.Collections.Generic;

namespace AcquiredLandInformationManagement.Controllers
{
    public class AcquiredLandVillageController : BaseController
    {
        private readonly IAcquiredlandvillageService _acquiredlandvillageService;


        public AcquiredLandVillageController(IAcquiredlandvillageService acquiredlandvillageService)
        {
            _acquiredlandvillageService = acquiredlandvillageService;
        }


       [AuthorizeContext(ViewAction.View)]
      public  IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] AcquiredLandVillageSearchDto model)
        {
            var result = await _acquiredlandvillageService.GetPagedAcquiredlandvillage(model);

            return PartialView("_List", result);
        }



        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Acquiredlandvillage acquiredlandvillage = new Acquiredlandvillage();
            acquiredlandvillage.IsActive = 1;
            acquiredlandvillage.DistrictList = await _acquiredlandvillageService.GetAllDistrict();
            acquiredlandvillage.TehsilList = await _acquiredlandvillageService.GetAllTehsil();
            acquiredlandvillage.ZoneList = await _acquiredlandvillageService.GetAllZone();
            return View(acquiredlandvillage);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Acquiredlandvillage acquiredlandvillage)
        {
            try
            {
                acquiredlandvillage.DistrictList = await _acquiredlandvillageService.GetAllDistrict();
                acquiredlandvillage.TehsilList = await _acquiredlandvillageService.GetAllTehsil();
                acquiredlandvillage.ZoneList = await _acquiredlandvillageService.GetAllZone();

                if (ModelState.IsValid)
                {
                    acquiredlandvillage.CreatedBy = SiteContext.UserId;
                    var result = await _acquiredlandvillageService.Create(acquiredlandvillage);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _acquiredlandvillageService.GetAcquiredlandvillage();
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(acquiredlandvillage);
                    }
                }
                else
                {
                    return View(acquiredlandvillage);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(acquiredlandvillage);
            }
        }



        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _acquiredlandvillageService.FetchSingleResult(id);
            Data.DistrictList = await _acquiredlandvillageService.GetAllDistrict();
            Data.TehsilList = await _acquiredlandvillageService.GetAllTehsil();
            Data.ZoneList = await _acquiredlandvillageService.GetAllZone();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Acquiredlandvillage acquiredlandvillage)
        {

            acquiredlandvillage.DistrictList = await _acquiredlandvillageService.GetAllDistrict();
            acquiredlandvillage.TehsilList = await _acquiredlandvillageService.GetAllTehsil();
            acquiredlandvillage.ZoneList = await _acquiredlandvillageService.GetAllZone();

            if (ModelState.IsValid)
            {
                try
                {
                    acquiredlandvillage.ModifiedBy = SiteContext.UserId;
                    var result = await _acquiredlandvillageService.Update(id, acquiredlandvillage);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _acquiredlandvillageService.GetAcquiredlandvillage();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(acquiredlandvillage);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(acquiredlandvillage);
                }
            }
            else
            {
                return View(acquiredlandvillage);
            }
        }


        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _acquiredlandvillageService.Delete(id);
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
            var list = await _acquiredlandvillageService.GetAcquiredlandvillage();
            return View("Index", list);
        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _acquiredlandvillageService.FetchSingleResult(id);
            Data.DistrictList = await _acquiredlandvillageService.GetAllDistrict();
            Data.TehsilList = await _acquiredlandvillageService.GetAllTehsil();
            Data.ZoneList = await _acquiredlandvillageService.GetAllZone();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        public async Task<IActionResult> Download()
        {
            List<Acquiredlandvillage> result = await _acquiredlandvillageService.GetAcquiredlandvillage();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"Acquiredlandvillage.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }


    }
}
