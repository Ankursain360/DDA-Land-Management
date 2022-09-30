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
using LandInventory.Filters;
using Core.Enum;
using Utility.Helper;
using Dto.Master;
using Dto.Common;




namespace LandInventory.Controllers
{
    public class PropertyRegistrationController : BaseController
    {
        // Let suppose user = 13 can Create, user = 14 can validate, user = 13 can delete, user = 15 can dispose, and untill validate 1,2 can only look index
        private readonly IPropertyRegistrationService _propertyregistrationService;
        public IConfiguration _Configuration;
        string targetPathLayout = "";
        string targetPathGeo = "";
        string targetPathHandedOver = "";
        string targetPathTakenOver = "";
        string targetPathDisposal = "";
        string targetPathHandedOverCopyOfOrder = "";
        string targetPathATR = "";
        public object JsonRequestBehavior { get; private set; }

        public PropertyRegistrationController(IPropertyRegistrationService propertyregistrationService, IConfiguration configuration)
        {
            _propertyregistrationService = propertyregistrationService;
            _Configuration = configuration;
        }
      
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            ViewBag.Items = await _propertyregistrationService.GetClassificationOfLandDropDownList();
            ViewBag.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
            ViewBag.IsUserCreation = SiteContext.UserId;
            ViewBag.IsDisposedRightsUser = SiteContext.UserId;
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] PropertyRegisterationSearchDto model)
        {
            int userId = SiteContext.UserId;
            var result = await _propertyregistrationService.GetPagedPropertyRegisteration(model, userId);
            ViewBag.IsDisposedRightsUser = SiteContext.UserId;
            ViewBag.IsUserCanEdit = SiteContext.UserId;
            return PartialView("_List", result);
        }
        async Task BindDropDown(Propertyregistration propertyregistration)
        {
            propertyregistration.ClassificationOfLandList = await _propertyregistrationService.GetClassificationOfLandDropDownList();
         
            propertyregistration.LandUseList = await _propertyregistrationService.GetLandUseDropDownList();
            propertyregistration.DisposalTypeList = await _propertyregistrationService.GetDisposalTypeDropDownList();
            propertyregistration.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
            propertyregistration.TakenOverDepartmentList = await _propertyregistrationService.GetTakenDepartmentDropDownList();
            propertyregistration.HandOverDepartmentList = await _propertyregistrationService.GetHandedDepartmentDropDownList();         
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Propertyregistration propertyregistration = new Propertyregistration();
            propertyregistration.IsActive = 1;
            await BindDropDown(propertyregistration);
            return View(propertyregistration);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Create(Propertyregistration propertyregistration, IFormFile Assignfile, IFormFile GeoAssignfile, IFormFile TakenOverAssignFile, IFormFile HandedOverAssignFile, IFormFile DisposalTypeAssignFile)
        {
            FileHelper fileHelper = new FileHelper();
            bool IsValidpdf = CheckMimeType(propertyregistration);
            await BindDropDown(propertyregistration);
            propertyregistration.ZoneList = await _propertyregistrationService.GetZoneDropDownList(propertyregistration.DepartmentId);
            propertyregistration.LocalityList = await _propertyregistrationService.GetLocalityDropDownList(propertyregistration.ZoneId);
            propertyregistration.DivisionList = await _propertyregistrationService.GetDivisionDropDownList(propertyregistration.ZoneId);

            if (ModelState.IsValid)
            {
                if (IsValidpdf == true)
                {


                    propertyregistration.IsValidate = 0;
                    propertyregistration.IsDeleted = 1;

                    if (propertyregistration.MainLandUseId == 0)
                    {
                        propertyregistration.MainLandUseId = 1;
                    }
                    if (propertyregistration.DisposalTypeId == 0)
                    {
                        propertyregistration.DisposalTypeId = 1;
                    }
                    if (propertyregistration.PlannedUnplannedLand == "Unplanned Land")
                    {
                        if (propertyregistration.LocalityId.ToString() == "")
                        {
                            ViewBag.Message = Alert.Show("Locality is Mandatory in case of Unplanned Land", "", AlertType.Warning);
                            return View(propertyregistration);
                        }
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
                    //targetPathDisposal = _Configuration.GetSection("FilePaths:PropertyRegistration:DisposalTypeDocs").Value.ToString();
                   string strPathDisposal = _Configuration.GetSection("FilePaths:PropertyRegistration:DisposalTypeDocs").Value.ToString();
                    if (propertyregistration.DisposalTypeFileData != null) 
                    {
                        //if (!Directory.Exists(targetPathDisposal))
                        //{
                        //    // Try to create the directory.
                        //    DirectoryInfo di = Directory.CreateDirectory(targetPathDisposal);
                        //}
                        //DisposalTypeFileName = Guid.NewGuid().ToString() + "_" + propertyregistration.DisposalTypeFileData.FileName;
                        //DisposalTypefilePath = Path.Combine(targetPathDisposal, DisposalTypeFileName);
                        //using (var stream = new FileStream(DisposalTypefilePath, FileMode.Create))
                        //{
                        //    propertyregistration.DisposalTypeFileData.CopyTo(stream);
                        //}
                        //propertyregistration.DisposalTypeFilePath = DisposalTypefilePath;
                        propertyregistration.DisposalTypeFilePath = fileHelper.SaveFile(strPathDisposal, propertyregistration.DisposalTypeFileData);
                    }

                    //string strDisposalTypeFilePath = _Configuration.GetSection("FilePaths:PropertyRegistration:DisposalTypeDocs").Value.ToString();
                    //if (propertyregistration.DisposalTypeFileData !=null)
                    //{
                    //    propertyregistration.DisposalTypeFilePath = fileHelper.SaveFile(strDisposalTypeFilePath, propertyregistration.DisposalTypeFileData);

                    //}

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

                    propertyregistration.CreatedBy = SiteContext.UserId;
                    var result = await _propertyregistrationService.Create(propertyregistration);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);                    
                        ViewBag.Items = await _propertyregistrationService.GetClassificationOfLandDropDownList();
                        ViewBag.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
                        ViewBag.IsUserCreation = SiteContext.UserId;
                        ViewBag.IsDisposedRightsUser = SiteContext.UserId;
                        SendSMSDto SMS = new SendSMSDto();
                        // Add SMS 
                        string Mobile = _propertyregistrationService.GetMobileNo(SiteContext.UserId);
                        SMS.GenerateSendSMSForSaveLandInventory(propertyregistration.PrimaryListNo,Mobile);
                        //END
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        await BindDropDown(propertyregistration);
                        return View(propertyregistration);

                    }
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                    return View(propertyregistration);
                }
            }
            else
            {
                return View(propertyregistration);
            }

        }
        [AuthorizeContext(ViewAction.Edit)]
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
            ViewBag.IsValidateUser = SiteContext.UserId;
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
        [AuthorizeContext(ViewAction.Edit)]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Edit(int id, Propertyregistration propertyregistration, IFormFile Assignfile, IFormFile GeoAssignfile, IFormFile TakenOverAssignFile, IFormFile HandedOverAssignFile, IFormFile DisposalTypeAssignFile)
        {
            bool IsValidpdf = CheckMimeType(propertyregistration);
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
            ViewBag.IsValidateUser = SiteContext.UserId;
            if (ModelState.IsValid)
            {
                if (IsValidpdf == true)
                {
                    if (propertyregistration.IsValidate == 1)
                    {
                        propertyregistration.IsValidate = 1;
                    }
                    else
                    {
                        if (propertyregistration.IsValidateData == true)
                        {
                            propertyregistration.IsValidate = 1;
                        }
                        else
                        {
                            propertyregistration.IsValidate = 0;
                        }
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


                    propertyregistration.ModifiedBy = SiteContext.UserId;
                    var result = await _propertyregistrationService.Update(id, propertyregistration);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        //   var result1 = await _propertyregistrationService.GetAllPropertyregistration(SiteContext.UserId);

                        ViewBag.IsUserCreation = SiteContext.UserId;
                        ViewBag.IsDisposedRightsUser = SiteContext.UserId;
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
                        return View("propertyregistration");

                    }
                }
                else
                {

                    ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                    ViewBag.Items = await _propertyregistrationService.GetClassificationOfLandDropDownList();
                    ViewBag.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
                    return View(propertyregistration);
                }

            }
            return View(propertyregistration);
        }
        
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _propertyregistrationService.FetchSingleResult(id);
            ViewBag.LayoutDocView = Data.LayoutFilePath;
            ViewBag.GeoDocView = Data.GeoFilePath;
            ViewBag.TakenOverDocView = Data.TakenOverFilePath;
            ViewBag.HandedOverDocView = Data.HandedOverFilePath;
            ViewBag.DisposalTypeDocView = Data.DisposalTypeFilePath;
            ViewBag.EncroachAtrDocView = Data.EncroachAtrfilepath;
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
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id, Propertyregistration propertyregistration)
        {
            Deletedproperty model = new Deletedproperty();
            model.Reason = propertyregistration.Reason;
            var result = await _propertyregistrationService.Delete(id);

            model.DeletedBy = SiteContext.UserId;
            var result2 = await _propertyregistrationService.InsertInDeletedProperty(id, model);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                ViewBag.Items = await _propertyregistrationService.GetClassificationOfLandDropDownList();
                ViewBag.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
                return View("Index");
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                ViewBag.Items = await _propertyregistrationService.GetClassificationOfLandDropDownList();
                ViewBag.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
                return View("Index");
            }
        }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var Data = await _propertyregistrationService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        public async Task<IActionResult> Download(int Id)
        {
            string filename = _propertyregistrationService.GetFile(Id);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> TakenOverDownload(int Id)
        {
            string filename = _propertyregistrationService.GetTakenOverFile(Id);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> HandedOverDownload(int Id)
        {
            string filename = _propertyregistrationService.GetHandedOverFile(Id);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DisposalTypeDownload(int Id)
        {
            string filename = _propertyregistrationService.GetDisposalFile(Id);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> EncroachAtrDownload(int Id)
        {
            string filename = _propertyregistrationService.GetEncroachAtr(Id);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filename), Path.GetFileName(filename));
        }

        public async Task<IActionResult> HandedOverCopyofOrderDownload(int Id)
        {
            string filename = _propertyregistrationService.GetHandedOverCopyofOrderFile(Id);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filename), Path.GetFileName(filename));
        }

        #region Document Download added By Renu 16 Sep 2020
        public async Task<IActionResult> GeoDownload(int Id)
        {
            string filename = _propertyregistrationService.GetGeoFile(Id);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filename), Path.GetFileName(filename));
        }


        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
        #endregion


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

        [AuthorizeContext(ViewAction.Dispose)]
        public async Task<IActionResult> Dispose(int id)
        {
            var Data = await _propertyregistrationService.FetchSingleResult(id);
            Data.DisposalTypeList = await _propertyregistrationService.GetDisposalTypeDropDownList();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Dispose)]
        public async Task<IActionResult> Dispose(int id, Propertyregistration propertyregistration)
        {
            FileHelper fileHelper = new FileHelper();
            Disposedproperty model = new Disposedproperty();
            model.DisposalTypeId = propertyregistration.DisposalTypeId;
            model.DisposalDate = propertyregistration.DisposalDate;
            string strDisposalTypeFilePath = _Configuration.GetSection("FilePaths:PropertyRegistration:DisposalTypeDocs").Value.ToString();
            if (propertyregistration.DisposalTypeFileData != null)
            {
                model.DisposalTypeFilePath = fileHelper.SaveFile(strDisposalTypeFilePath, propertyregistration.DisposalTypeFileData);

            }
           // model.DisposalTypeFilePath = propertyregistration.DisposalTypeFileData.ToString(); 
            model.DisposalComments = propertyregistration.DisposalComments;
            model.DisposedBy = SiteContext.UserId;
            var result = await _propertyregistrationService.DisposeDetails(id, model);
            var result2 = await _propertyregistrationService.InsertInDisposedProperty(id, model);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                ViewBag.Items = await _propertyregistrationService.GetClassificationOfLandDropDownList();
                ViewBag.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
                return View("Index");
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                ViewBag.Items = await _propertyregistrationService.GetClassificationOfLandDropDownList();
                ViewBag.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
                return View("Index");
            }
        }

        [AuthorizeContext(ViewAction.Download)]
        [HttpPost]
        public async Task<IActionResult> DownloadIndex([FromBody] PropertyRegisterationSearchDto model)
        {
            var result = await _propertyregistrationService.GetAllPropertInventory(model, SiteContext.UserId);
            List<PropertyInventoryListDto> data = new List<PropertyInventoryListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new PropertyInventoryListDto()
                    {
                        Id = result[i].Id,
                        InventoriedIn = result[i].InventoriedInId.ToString() == "1" ? "VLMS" : "Used",
                        PlannedUnplannedLand = result[i].PlannedUnplannedLand,

                        ClassificationofLand = result[i].ClassificationOfLand == null ? " " : result[i].ClassificationOfLand.Name,
                        Department = result[i].Department == null ? " " : result[i].Department.Name,
                        Zone = result[i].Zone == null ? " " : result[i].Zone.Name,
                        Division = result[i].Division == null ? " " : result[i].Division.Name,
                        Locality = result[i].Locality == null ? " " : result[i].Locality.Name,
                        KhasraNo = result[i].KhasraNo,
                        Colony = result[i].Colony,
                        Sector = result[i].Sector,
                        Block = result[i].Block,
                        Pocket = result[i].Pocket,
                        PlotNo = result[i].PlotNo,
                        PrimaryListNo = result[i].PrimaryListNo,
                        AddressWithLandmark = result[i].Palandmark,
                        AreaUnit = result[i].AreaUnit == 0 ? "bigha-biswa-bishwani" : result[i].AreaUnit == 1 ? "Sq Yd." : result[i].AreaUnit == 2 ? "Acre" : result[i].AreaUnit == 3 ? "Hectare" : "Sq. mt.",
                        Bigha = result[i].TotalAreaInBigha.ToString(),
                        Biswa = result[i].TotalAreaInBiswa.ToString(),
                        Bishwani = result[i].TotalAreaInBiswani.ToString(),
                        TotalArea = result[i].TotalAreaInSqAcreHt.ToString(),
                        //    if (result[i].AreaUnit == 0)
                        //{
                        //   resu
                        //}
                        //  TotalArea = result[i].AreaUnit==0? result[i].TotalAreaInBigha.ToString() ?
                        TotalAreaSqmt = result[i].TotalArea.ToString(),
                        Encroachment = result[i].EncroachmentStatusId == 0 ? "No" : "Yes".ToString(),
                        EncroachmentStatus = result[i].EncroachedPartiallyFully == "0" ? "Partially Encroached" : result[i].EncroachedPartiallyFully == "1" ? "Fully Encroached" : " ",

                        EncroachmentArea = result[i].EncrochedArea.ToString(),
                        BuiltUpInEncroachmentArea = result[i].BuiltUpEncraochmentArea.ToString(),
                        Vacant = result[i].Vacant.ToString(),
                        ActionOnEncroachment = result[i].ActionOnEncroachment,
                        EncroachemntDetails = result[i].EncraochmentDetails,
                        ProtectionOfLand = result[i].Boundary == 0 ? "Boundary Wall" : result[i].AreaUnit == 1 ? "Fencing" : "None".ToString(),
                        AreaCovered = result[i].BoundaryAreaCovered.ToString(),
                        Dimension = result[i].BoundaryDimension,
                        BoundaryRemarks = result[i].BoundaryRemarks,
                        BuiltType = result[i].BuiltUp == 0 ? "No" : "Yes",
                        LitigationStatus = result[i].LitigationStatus == 0 ? "No" : "Yes",
                        CourtName = result[i].CourtName,
                        CaseNo = result[i].CaseNo,
                        OppositeParty = result[i].OppositeParty,
                        LitigationStatusRemarks = result[i].LitigationStatusRemarks,
                        GeoReferencing = result[i].GeoReferencing == 0 ? "No" : "Yes",
                        Remarks = result[i].Remarks,


                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            //string sFileName = @"LandInventory.xlsx";
            //return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            TempData["file"] = memory;
            return Ok();
        }

        [HttpGet]
        public virtual IActionResult DownloadIndex1()
        {
            //var memory = ExcelHelper.CreateExcel(data);
            //string sFileName = @"LandInventory.xlsx";
            //return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
            byte[] data = TempData["file"] as byte[];
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "LandInventory.xlsx");

        }



        public async Task<IActionResult> PropertyInventoryList()
        {
            var result = await _propertyregistrationService.GetAllPropertInventorylist(SiteContext.UserId);
            List<PropertyInventoryListDto> data = new List<PropertyInventoryListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new PropertyInventoryListDto()
                    {
                        Id = result[i].Id,
                        InventoriedIn = result[i].InventoriedInId.ToString() == "1" ? "VLMS" : "Used",
                        PlannedUnplannedLand = result[i].PlannedUnplannedLand,

                        ClassificationofLand = result[i].ClassificationOfLand == null ? " " : result[i].ClassificationOfLand.Name,
                        Department = result[i].Department == null ? " " : result[i].Department.Name,
                        Zone = result[i].Zone == null ? " " : result[i].Zone.Name,
                        Division = result[i].Division == null ? " " : result[i].Division.Name,
                        Locality = result[i].Locality == null ? " " : result[i].Locality.Name,
                        KhasraNo = result[i].KhasraNo,
                        Colony = result[i].Colony,
                        Sector = result[i].Sector,
                        Block = result[i].Block,
                        Pocket = result[i].Pocket,
                        PlotNo = result[i].PlotNo,
                        PrimaryListNo = result[i].PrimaryListNo,
                        AddressWithLandmark = result[i].Palandmark,
                        AreaUnit = result[i].AreaUnit == 0 ? "bigha-biswa-bishwani" : result[i].AreaUnit == 1 ? "Sq Yd." : result[i].AreaUnit == 2 ? "Acre" : result[i].AreaUnit == 3 ? "Hectare" : "Sq. mt.",
                        Bigha = result[i].TotalAreaInBigha.ToString(),
                        Biswa = result[i].TotalAreaInBiswa.ToString(),
                        Bishwani = result[i].TotalAreaInBiswani.ToString(),
                        TotalArea = result[i].TotalAreaInSqAcreHt.ToString(),
                        //    if (result[i].AreaUnit == 0)
                        //{
                        //   resu
                        //}
                        //  TotalArea = result[i].AreaUnit==0? result[i].TotalAreaInBigha.ToString() ?
                        TotalAreaSqmt = result[i].TotalArea.ToString(),
                        Encroachment = result[i].EncroachmentStatusId == 0 ? "No" : "Yes".ToString(),
                        EncroachmentStatus = result[i].EncroachedPartiallyFully == "0" ? "Partially Encroached" : result[i].EncroachedPartiallyFully == "1" ? "Fully Encroached" : " ",

                        EncroachmentArea = result[i].EncrochedArea.ToString(),
                        BuiltUpInEncroachmentArea = result[i].BuiltUpEncraochmentArea.ToString(),
                        Vacant = result[i].Vacant.ToString(),
                        ActionOnEncroachment = result[i].ActionOnEncroachment,
                        EncroachemntDetails = result[i].EncraochmentDetails,
                        ProtectionOfLand = result[i].Boundary == 0 ? "Boundary Wall" : result[i].AreaUnit == 1 ? "Fencing" : "None".ToString(),
                        AreaCovered = result[i].BoundaryAreaCovered.ToString(),
                        Dimension = result[i].BoundaryDimension,
                        BoundaryRemarks = result[i].BoundaryRemarks,
                        BuiltType = result[i].BuiltUp == 0 ? "No" : "Yes",
                        LitigationStatus = result[i].LitigationStatus == 0 ? "No" : "Yes",
                        CourtName = result[i].CourtName,
                        CaseNo = result[i].CaseNo,
                        OppositeParty = result[i].OppositeParty,
                        LitigationStatusRemarks = result[i].LitigationStatusRemarks,
                        GeoReferencing = result[i].GeoReferencing == 0 ? "No" : "Yes",
                        Remarks = result[i].Remarks,

                    }); 
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }





        [HttpPost]
        public JsonResult CheckFile()
        {
            bool IsImg = true;
            string fullpath = string.Empty;
            //   string fullpath = string.Empty;
            string extension = string.Empty;
            targetPathLayout = _Configuration.GetSection("FilePaths:PropertyRegistration:LayoutDocs").Value.ToString();
            IFormFile files = Request.Form.Files["file"];
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                targetPathLayout = _Configuration.GetSection("FilePaths:PropertyRegistration:LayoutDocs").Value.ToString();
                string FilePath = Path.Combine(targetPathLayout, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(targetPathLayout))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(targetPathLayout);// Try to create the directory.
                    }
                    try
                    {
                        if (extension.ToLower() == ".pdf")
                        {
                            try
                            {
                                using (var stream = new FileStream(FilePath, FileMode.Create))
                                {
                                    files.CopyTo(stream);

                                }

                                iTextSharp.text.pdf.PdfReader oPdfReader = new iTextSharp.text.pdf.PdfReader(FilePath);
                                oPdfReader.Close();
                                fullpath = _Configuration.GetSection("FilePaths:PropertyRegistration:LayoutDocs").Value.ToString();
                                FileInfo doc = new FileInfo(fullpath);
                                if (doc.Exists)
                                {
                                    doc.Delete();
                                }
                            }
                            catch (iTextSharp.text.exceptions.InvalidPdfException)
                            {
                                IsImg = false;
                            }

                        }
                    }
                    catch (OutOfMemoryException ex)
                    {
                        IsImg = false;

                        if (System.IO.File.Exists(fullpath))
                        {
                            try
                            {
                                System.IO.File.Delete(fullpath);
                            }
                            catch (Exception exs)
                            {
                            }
                        }
                        // Image.FromFile will throw this if file is invalid.  
                    }

                }
            }

            return Json(IsImg, JsonRequestBehavior);
        }




        public bool CheckMimeType(Propertyregistration propertyregistration)
        {
            bool Flag = true;
            string fullpath = string.Empty;           
            string extension = string.Empty;
            targetPathLayout = _Configuration.GetSection("FilePaths:PropertyRegistration:LayoutDocs").Value.ToString();
            IFormFile files = propertyregistration.FileData;
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                targetPathLayout = _Configuration.GetSection("FilePaths:PropertyRegistration:LayoutDocs").Value.ToString();
                string FilePath = Path.Combine(targetPathLayout, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(targetPathLayout))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(targetPathLayout);// Try to create the directory.
                    }
                    try
                    {
                        if (extension.ToLower() == ".pdf")
                        {
                            try
                            {
                                using (var stream = new FileStream(FilePath, FileMode.Create))
                                {
                                    files.CopyTo(stream);

                                }

                                iTextSharp.text.pdf.PdfReader oPdfReader = new iTextSharp.text.pdf.PdfReader(FilePath);
                                oPdfReader.Close();
                                fullpath = _Configuration.GetSection("FilePaths:PropertyRegistration:LayoutDocs").Value.ToString();
                                FileInfo doc = new FileInfo(fullpath);
                                if (doc.Exists)
                                {
                                    doc.Delete();
                                }
                            }
                            catch (iTextSharp.text.exceptions.InvalidPdfException)
                            {
                                Flag = false;
                            }

                        }
                        else
                        {
                            Flag = false;
                        }
                    }
                    catch (OutOfMemoryException ex)
                    {
                        Flag = false;

                        if (System.IO.File.Exists(fullpath))
                        {
                            try
                            {
                                System.IO.File.Delete(fullpath);
                            }
                            catch (Exception exs)
                            {
                            }
                        }
                        // Image.FromFile will throw this if file is invalid.  
                    }

                }
            }

            return Flag;
        }


    }

}
