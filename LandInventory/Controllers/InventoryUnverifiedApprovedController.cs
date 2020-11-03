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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Dto.Search;

namespace LandInventory.Controllers
{
    public class InventoryUnverifiedApprovedController : Controller
    {
        private readonly IPropertyRegistrationService _propertyregistrationService;
        public IConfiguration _Configuration;
        string targetPathLayout = "";
        string targetPathGeo = "";
        string targetPathHandedOver = "";
        string targetPathTakenOver = "";
        string targetPathDisposal = "";
        string targetPathHandedOverCopyOfOrder = "";
        string targetPathATR = "";
        public InventoryUnverifiedApprovedController(IPropertyRegistrationService propertyregistrationService)
        {
            _propertyregistrationService = propertyregistrationService;
        }

        async Task BindDropDown(Propertyregistration propertyregistration)
        {
            propertyregistration.ClassificationOfLandList = await _propertyregistrationService.GetClassificationOfLandDropDownListReport();
            propertyregistration.LandUseList = await _propertyregistrationService.GetLandUseDropDownList();
            propertyregistration.DisposalTypeList = await _propertyregistrationService.GetDisposalTypeDropDownList();
            propertyregistration.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
            propertyregistration.TakenOverDepartmentList = await _propertyregistrationService.GetTakenDepartmentDropDownList();
            propertyregistration.HandOverDepartmentList = await _propertyregistrationService.GetHandedDepartmentDropDownList();

        }
        public async Task<IActionResult> Create()
        {
            Propertyregistration propertyregistration = new Propertyregistration();
            await BindDropDown(propertyregistration);
            return View(propertyregistration);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] InvnentoryUnverifiedVerifiedSearchDto model)
        {
            var result = await _propertyregistrationService.GetInventoryUnverifiedVerified(model);

            if (result != null)
            {
                return PartialView("_Index", result);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return PartialView();
            }
        }

        #region Dropdown Dependency calls added  by renu 
        [HttpGet]
        public async Task<JsonResult> GetZoneList(int? departmentId)
        {
            departmentId = departmentId ?? 0;
            return Json(await _propertyregistrationService.GetZoneDropDownList(Convert.ToInt32(departmentId)));
        }

        [HttpGet]
        public async Task<JsonResult> GetLocalityList(int? zoneId)
        {
            zoneId = zoneId ?? 0;
            return Json(await _propertyregistrationService.GetLocalityDropDownList(Convert.ToInt32(zoneId)));
        }

        [HttpGet]
        public async Task<JsonResult> GetDivisionList(int? zoneId)
        {
            zoneId = zoneId ?? 0;
            return Json(await _propertyregistrationService.GetDivisionDropDownList(Convert.ToInt32(zoneId)));
        }
        #endregion

        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _propertyregistrationService.FetchSingleResult(id);
            ViewBag.LayoutDocView = Data.LayoutFilePath;
            ViewBag.GeoDocView = Data.GeoFilePath;
            ViewBag.TakenOverDocView = Data.TakenOverFilePath;
            ViewBag.HandedOverDocView = Data.HandedOverFilePath;
            ViewBag.DisposalTypeDocView = Data.DisposalTypeFilePath;
            ViewBag.EncroachAtrDocView = Data.EncroachAtrfilepath;
            ViewBag.HandedOverCopyofOrderView = Data.HandedOverCopyofOrderFilepath;
            ViewBag.IsValidateUser = 2;
            await BindDropDown(Data);

            Data.ZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.DepartmentId);
            Data.LocalityList = await _propertyregistrationService.GetLocalityDropDownList(Data.ZoneId);
            Data.DivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.ZoneId);
            Data.HandedOverZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.DepartmentId);
            Data.HandedOverDivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.ZoneId);
            Data.TakenOverZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.DepartmentId);
            Data.TakenOverDivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.ZoneId);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Edit(int id, Propertyregistration propertyregistration, IFormFile Assignfile, IFormFile GeoAssignfile, IFormFile TakenOverAssignFile, IFormFile HandedOverAssignFile, IFormFile DisposalTypeAssignFile)
        {
            await BindDropDown(propertyregistration);
            propertyregistration.ZoneList = await _propertyregistrationService.GetZoneDropDownList(propertyregistration.DepartmentId);
            propertyregistration.LocalityList = await _propertyregistrationService.GetLocalityDropDownList(propertyregistration.ZoneId);
            propertyregistration.DivisionList = await _propertyregistrationService.GetDivisionDropDownList(propertyregistration.ZoneId);
            propertyregistration.HandedOverZoneList = await _propertyregistrationService.GetZoneDropDownList(propertyregistration.DepartmentId);
            propertyregistration.HandedOverDivisionList = await _propertyregistrationService.GetDivisionDropDownList(propertyregistration.ZoneId);
            propertyregistration.TakenOverZoneList = await _propertyregistrationService.GetZoneDropDownList(propertyregistration.DepartmentId);
            propertyregistration.TakenOverDivisionList = await _propertyregistrationService.GetDivisionDropDownList(propertyregistration.ZoneId);
            ViewBag.LayoutDocView = propertyregistration.LayoutFilePath;
            ViewBag.GeoDocView = propertyregistration.GeoFilePath;
            ViewBag.TakenOverDocView = propertyregistration.TakenOverFilePath;
            ViewBag.HandedOverDocView = propertyregistration.HandedOverFilePath;
            ViewBag.DisposalTypeDocView = propertyregistration.DisposalTypeFilePath;
            ViewBag.IsValidateUser = 2;
            if (ModelState.IsValid)
            {
                if (propertyregistration.IsValidateData == true)
                {
                    propertyregistration.IsValidate = 1;
                }
                else
                {
                    propertyregistration.IsValidate = 0;
                }
                if (propertyregistration.MainLandUseId == 0)
                {
                    propertyregistration.MainLandUseId = 1;
                }
                if (propertyregistration.DisposalTypeId == 0)
                {
                    propertyregistration.DisposalTypeId = 1;
                }
                if (propertyregistration.Encroached != null)
                {
                    if (propertyregistration.Encroached > propertyregistration.TotalArea)
                    {
                        ViewBag.Message = Alert.Show("Encroached Value Must Not be Greater than Total Area", "", AlertType.Warning);
                        return View(propertyregistration);
                    }
                }
                if (propertyregistration.BuiltUpEncraochmentArea != null)
                {
                    if (propertyregistration.BuiltUpEncraochmentArea > propertyregistration.TotalArea)
                    {
                        ViewBag.Message = Alert.Show("Built Up Encraochment Area Value Must Not be Greater than Total Area", "", AlertType.Warning);
                        return View(propertyregistration);
                    }
                }
                if (propertyregistration.Vacant != null)
                {
                    if (propertyregistration.Vacant > propertyregistration.TotalArea)
                    {
                        ViewBag.Message = Alert.Show("Vacant Value Must Not be Greater than Total Area", "", AlertType.Warning);
                        return View(propertyregistration);
                    }
                }
                #region File Upload  Added by Renu 16 Sep 2020
                /* For Layout Plan File Upload*/
                string FileName = "";
                string filePath = "";
                propertyregistration.FileData = Assignfile;
                targetPathLayout = _Configuration.GetSection("FilePaths:PropertyRegistration:LayoutDocs").Value.ToString();
                if (propertyregistration.FileData != null)
                {
                    if (!Directory.Exists(targetPathLayout))
                    {
                        // Try to create the directory.
                        DirectoryInfo di = Directory.CreateDirectory(targetPathLayout);
                    }
                    FileName = Guid.NewGuid().ToString() + "_" + propertyregistration.FileData.FileName;
                    filePath = Path.Combine(targetPathLayout, FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        propertyregistration.FileData.CopyTo(stream);
                    }
                    propertyregistration.LayoutFilePath = filePath;
                }

                /* For GeoReferncing File Upload*/
                string GeoFileName = "";
                string GeofilePath = "";
                propertyregistration.GeoFileData = GeoAssignfile;
                targetPathGeo = _Configuration.GetSection("FilePaths:PropertyRegistration:GeoReferencingDocs").Value.ToString();
                if (propertyregistration.GeoFileData != null)
                {
                    if (!Directory.Exists(targetPathGeo))
                    {
                        // Try to create the directory.
                        DirectoryInfo di = Directory.CreateDirectory(targetPathGeo);
                    }
                    GeoFileName = Guid.NewGuid().ToString() + "_" + propertyregistration.GeoFileData.FileName;
                    GeofilePath = Path.Combine(targetPathGeo, GeoFileName);
                    using (var stream = new FileStream(GeofilePath, FileMode.Create))
                    {
                        propertyregistration.GeoFileData.CopyTo(stream);
                    }
                    propertyregistration.GeoFilePath = GeofilePath;
                }

                /* For Taken Over File Upload*/
                string TakenOverFileName = "";
                string TakenOverfilePath = "";
                propertyregistration.TakenOverFileData = TakenOverAssignFile;
                targetPathTakenOver = _Configuration.GetSection("FilePaths:PropertyRegistration:TakenOverDocs").Value.ToString();
                if (propertyregistration.TakenOverFileData != null)
                {
                    if (!Directory.Exists(targetPathTakenOver))
                    {
                        // Try to create the directory.
                        DirectoryInfo di = Directory.CreateDirectory(targetPathTakenOver);
                    }
                    TakenOverFileName = Guid.NewGuid().ToString() + "_" + propertyregistration.TakenOverFileData.FileName;
                    TakenOverfilePath = Path.Combine(targetPathTakenOver, TakenOverFileName);
                    using (var stream = new FileStream(TakenOverfilePath, FileMode.Create))
                    {
                        propertyregistration.TakenOverFileData.CopyTo(stream);
                    }
                    propertyregistration.TakenOverFilePath = TakenOverfilePath;
                }

                /* For Handed Over File Upload*/
                string HandedOverFileName = "";
                string HandedOverfilePath = "";
                propertyregistration.HandedOverFileData = HandedOverAssignFile;
                targetPathHandedOver = _Configuration.GetSection("FilePaths:PropertyRegistration:HandedOverDocs").Value.ToString();
                if (propertyregistration.HandedOverFileData != null)
                {
                    if (!Directory.Exists(targetPathHandedOver))
                    {
                        // Try to create the directory.
                        DirectoryInfo di = Directory.CreateDirectory(targetPathHandedOver);
                    }
                    HandedOverFileName = Guid.NewGuid().ToString() + "_" + propertyregistration.HandedOverFileData.FileName;
                    HandedOverfilePath = Path.Combine(targetPathHandedOver, HandedOverFileName);
                    using (var stream = new FileStream(HandedOverfilePath, FileMode.Create))
                    {
                        propertyregistration.HandedOverFileData.CopyTo(stream);
                    }
                    propertyregistration.HandedOverFilePath = HandedOverfilePath;
                }

                /* For Disposal Type File Upload*/
                string DisposalTypeFileName = "";
                string DisposalTypefilePath = "";
                propertyregistration.DisposalTypeFileData = DisposalTypeAssignFile;
                targetPathDisposal = _Configuration.GetSection("FilePaths:PropertyRegistration:DisposalTypeDocs").Value.ToString();
                if (propertyregistration.DisposalTypeFileData != null)
                {
                    if (!Directory.Exists(targetPathDisposal))
                    {
                        // Try to create the directory.
                        DirectoryInfo di = Directory.CreateDirectory(targetPathDisposal);
                    }
                    DisposalTypeFileName = Guid.NewGuid().ToString() + "_" + propertyregistration.DisposalTypeFileData.FileName;
                    DisposalTypefilePath = Path.Combine(targetPathDisposal, DisposalTypeFileName);
                    using (var stream = new FileStream(DisposalTypefilePath, FileMode.Create))
                    {
                        propertyregistration.DisposalTypeFileData.CopyTo(stream);
                    }
                    propertyregistration.DisposalTypeFilePath = DisposalTypefilePath;
                }
                /* For Handed Over Copy of Order*/
                string HandedOverCopyOrderFileName = "";
                string HandedOverCopyOrderfilePath = "";
                targetPathHandedOverCopyOfOrder = _Configuration.GetSection("FilePaths:PropertyRegistration:HandedOverCopyofOrderDocs").Value.ToString();
                if (propertyregistration.HandedOverCopyofOrderDoc != null)
                {
                    if (!Directory.Exists(targetPathHandedOverCopyOfOrder))
                    {
                        // Try to create the directory.
                        DirectoryInfo di = Directory.CreateDirectory(targetPathHandedOverCopyOfOrder);
                    }
                    HandedOverCopyOrderFileName = Guid.NewGuid().ToString() + "_" + propertyregistration.HandedOverCopyofOrderDoc.FileName;
                    HandedOverCopyOrderfilePath = Path.Combine(targetPathHandedOverCopyOfOrder, HandedOverCopyOrderFileName);
                    using (var stream = new FileStream(HandedOverCopyOrderfilePath, FileMode.Create))
                    {
                        propertyregistration.HandedOverCopyofOrderDoc.CopyTo(stream);
                    }
                    propertyregistration.HandedOverCopyofOrderFilepath = HandedOverCopyOrderfilePath;
                }

                string ATRFileName = "";
                string ATRfilePath = "";
                targetPathATR = _Configuration.GetSection("FilePaths:PropertyRegistration:EcroachedATRDocs").Value.ToString();
                if (propertyregistration.EncroachAtrDoc != null)
                {
                    if (!Directory.Exists(targetPathATR))
                    {
                        // Try to create the directory.
                        DirectoryInfo di = Directory.CreateDirectory(targetPathATR);
                    }
                    ATRFileName = Guid.NewGuid().ToString() + "_" + propertyregistration.EncroachAtrDoc.FileName;
                    ATRfilePath = Path.Combine(targetPathATR, ATRFileName);
                    using (var stream = new FileStream(ATRfilePath, FileMode.Create))
                    {
                        propertyregistration.EncroachAtrDoc.CopyTo(stream);
                    }
                    propertyregistration.EncroachAtrfilepath = ATRfilePath;
                }
                #endregion


                var result = await _propertyregistrationService.Update(id, propertyregistration);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    //   var result1 = await _propertyregistrationService.GetAllPropertyregistration(SiteContext.UserId);
                    ViewBag.Items = await _propertyregistrationService.GetClassificationOfLandDropDownList();
                    ViewBag.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
                    return View("Index");
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    //return View(propertyregistration);
                    //  var result1 = await _propertyregistrationService.GetAllPropertyregistration(SiteContext.UserId);
                    ViewBag.Items = await _propertyregistrationService.GetClassificationOfLandDropDownList();
                    ViewBag.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
                    return View("Index");

                }

            }
            return View(propertyregistration);
        }

    }
}
