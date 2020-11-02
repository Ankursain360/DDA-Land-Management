using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using LandTransfer.Controllers;
using Utility;
using Utility.Helper;
using System.Net;

namespace LandTransfer.Controllers
{
    public class LandTransferController : BaseController
    {
        public IConfiguration _configuration;
        public readonly ILandTransferService _landTransferService;
        private readonly IPropertyRegistrationService _propertyregistrationService;
        string copyOfOrderDoc = string.Empty;
        string handedOverFile = string.Empty;
        string takenOverFile = string.Empty;
        string actionTakenReport = string.Empty;
        public LandTransferController(ILandTransferService landTransferService, IPropertyRegistrationService propertyregistrationService, IConfiguration configuration)
        {
            _landTransferService = landTransferService;
            _propertyregistrationService = propertyregistrationService;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] LandTransferSearchDto model)
        {
            var result = await _landTransferService.GetPropertyRegisterationDataForLandTransfer(model);
            //var result1 = await _propertyregistrationService.GetPropertyRegisterationReportData(null);
            //var result = await _landTransferService.GetPagedLandTransfer(model);
            return PartialView("_List", result);
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        //public async Task<IActionResult> Create()
        //{
        //    Landtransfer model = new Landtransfer();
        //    model.Propertyregistration = await _propertyregistrationService.FetchSingleResult(32);
        //    model.Propertyregistration.ClassificationOfLandList = await _propertyregistrationService.GetClassificationOfLandDropDownList();
        //    model.Propertyregistration.LandUseList = await _propertyregistrationService.GetLandUseDropDownList();
        //    model.Propertyregistration.DisposalTypeList = await _propertyregistrationService.GetDisposalTypeDropDownList();
        //    model.Propertyregistration.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
        //    model.Propertyregistration.TakenOverDepartmentList = await _propertyregistrationService.GetTakenDepartmentDropDownList();
        //    model.Propertyregistration.HandOverDepartmentList = await _propertyregistrationService.GetHandedDepartmentDropDownList();
        //    model.Propertyregistration.ZoneList = await _propertyregistrationService.GetZoneDropDownList(model.Propertyregistration.DepartmentId);
        //    model.Propertyregistration.LocalityList = await _propertyregistrationService.GetLocalityDropDownList(model.Propertyregistration.DivisionId);
        //    model.Propertyregistration.DivisionList = await _propertyregistrationService.GetDivisionDropDownList(model.Propertyregistration.ZoneId);

        //    model.LandTransferList = await _landTransferService.GetAllLandTransfer();
        //    model.DepartmentList = await _landTransferService.GetAllDepartment();
        //    model.ZoneList = await _landTransferService.GetAllZone(model.DepartmentId);
        //    model.DivisionList = await _landTransferService.GetAllDivisionList(model.ZoneId);
        //    model.LocalityList = await _landTransferService.GetAllLocalityList(model.DivisionId);
        //    return View(model);
        //}
        //[HttpPost]
        //public async Task<IActionResult> Create(Landtransfer landtransfer)
        //{
        //    landtransfer.DepartmentList = await _landTransferService.GetAllDepartment();
        //    landtransfer.ZoneList = await _landTransferService.GetAllZone(landtransfer.DepartmentId);
        //    landtransfer.DivisionList = await _landTransferService.GetAllDivisionList(landtransfer.ZoneId);
        //    landtransfer.LocalityList = await _landTransferService.GetAllLocalityList(landtransfer.DivisionId);
        //    if (ModelState.IsValid)
        //    {
        //        targetPathLayout = _configuration.GetSection("FilePaths:LandTransfer:CopyOfOrderDoc").Value.ToString();
        //        if (landtransfer.CopyofOrder != null)
        //        {
        //            FileHelper file = new FileHelper();
        //            landtransfer.CopyofOrderDocPath = file.SaveFile(targetPathLayout, landtransfer.CopyofOrder);
        //        }
        //        var result = await _landTransferService.Create(landtransfer);

        //        if (result == true)
        //        {
        //            ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
        //            var result1 = await _landTransferService.GetAllLandTransfer();
        //            return View("Index", result1);
        //        }
        //        else
        //        {
        //            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //            return View(landtransfer);
        //        }
        //    }

        //    return View(landtransfer);
        //}
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var result = await _landTransferService.Delete(id);
        //    if (result == true)
        //    {
        //        ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
        //    }
        //    else
        //    {
        //        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //    }
        //    var result1 = await _landTransferService.GetAllLandTransfer();
        //    return View("Index", result1);
        //}
        public async Task<IActionResult> Edit(int id)
        {
            //var Data = await _landTransferService.FetchSingleResult(id);
            var Data = new Landtransfer();
            Data.Propertyregistration = await _propertyregistrationService.FetchSingleResult(id);
            Data.Propertyregistration.ClassificationOfLandList = await _propertyregistrationService.GetClassificationOfLandDropDownList();
            Data.Propertyregistration.LandUseList = await _propertyregistrationService.GetLandUseDropDownList();
            Data.Propertyregistration.DisposalTypeList = await _propertyregistrationService.GetDisposalTypeDropDownList();
            Data.Propertyregistration.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
            Data.Propertyregistration.ZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.Propertyregistration.DepartmentId);
            Data.Propertyregistration.LocalityList = await _propertyregistrationService.GetLocalityDropDownList(Data.Propertyregistration.DivisionId);
            Data.Propertyregistration.DivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.Propertyregistration.ZoneId);

            Data.Propertyregistration.TakenOverDepartmentList = await _propertyregistrationService.GetTakenDepartmentDropDownList();
            Data.Propertyregistration.TakenOverZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.Propertyregistration.TakenOverDepartmentId ?? 0);
            Data.Propertyregistration.TakenOverDivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.Propertyregistration.TakenOverZoneId ?? 0);

            Data.Propertyregistration.HandOverDepartmentList = await _propertyregistrationService.GetHandedDepartmentDropDownList();
            Data.Propertyregistration.HandedOverZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.Propertyregistration.HandedOverDepartmentId ?? 0);
            Data.Propertyregistration.HandedOverDivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.Propertyregistration.HandedOverZoneId ?? 0);


            Data.LandTransferList = await _landTransferService.GetAllLandTransfer(Data.Propertyregistration.Id);
            if (Data == null)
            {
                return NotFound();  
            }
            return View(Data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Landtransfer landtransfer)
        {
            //var Data = await _landTransferService.FetchSingleResult(landtransfer.Id);
            landtransfer.PropertyRegistration = await _propertyregistrationService.FetchSingleResult(landtransfer.PropertyRegistrationId);
            landtransfer.Propertyregistration.ClassificationOfLandList = await _propertyregistrationService.GetClassificationOfLandDropDownList();
            landtransfer.Propertyregistration.LandUseList = await _propertyregistrationService.GetLandUseDropDownList();
            landtransfer.Propertyregistration.DisposalTypeList = await _propertyregistrationService.GetDisposalTypeDropDownList();
            landtransfer.Propertyregistration.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
            landtransfer.Propertyregistration.ZoneList = await _propertyregistrationService.GetZoneDropDownList(landtransfer.Propertyregistration.DepartmentId);
            landtransfer.Propertyregistration.LocalityList = await _propertyregistrationService.GetLocalityDropDownList(landtransfer.Propertyregistration.DivisionId);
            landtransfer.Propertyregistration.DivisionList = await _propertyregistrationService.GetDivisionDropDownList(landtransfer.Propertyregistration.ZoneId);

            landtransfer.Propertyregistration.TakenOverDepartmentList = await _propertyregistrationService.GetTakenDepartmentDropDownList();
            landtransfer.Propertyregistration.TakenOverZoneList = await _propertyregistrationService.GetZoneDropDownList(landtransfer.Propertyregistration.TakenOverDepartmentId ?? 0);
            landtransfer.Propertyregistration.TakenOverDivisionList = await _propertyregistrationService.GetDivisionDropDownList(landtransfer.Propertyregistration.TakenOverZoneId ?? 0);

            landtransfer.Propertyregistration.HandOverDepartmentList = await _propertyregistrationService.GetHandedDepartmentDropDownList();
            landtransfer.Propertyregistration.HandedOverZoneList = await _propertyregistrationService.GetZoneDropDownList(landtransfer.Propertyregistration.HandedOverDepartmentId ?? 0);
            landtransfer.Propertyregistration.HandedOverDivisionList = await _propertyregistrationService.GetDivisionDropDownList(landtransfer.Propertyregistration.HandedOverZoneId ?? 0);


            landtransfer.LandTransferList = await _landTransferService.GetAllLandTransfer(landtransfer.PropertyRegistrationId);
            if (ModelState.IsValid)
            {
                copyOfOrderDoc = _configuration.GetSection("FilePaths:LandTransfer:CopyOfOrderDoc").Value.ToString();
                actionTakenReport = _configuration.GetSection("FilePaths:LandTransfer:ActionTakenReport").Value.ToString();
                takenOverFile = _configuration.GetSection("FilePaths:LandTransfer:TakenOverFile").Value.ToString();
                handedOverFile = _configuration.GetSection("FilePaths:LandTransfer:HandedOverFile").Value.ToString();
                if (landtransfer.CopyofOrder != null)
                {
                    FileHelper file = new FileHelper();
                    landtransfer.CopyofOrderDocPath = file.SaveFile(copyOfOrderDoc, landtransfer.CopyofOrder);
                }
                actionTakenReport = _configuration.GetSection("FilePaths:LandTransfer:ActionTakenReport").Value.ToString();
                if (landtransfer.ActionTakenReport != null)
                {
                    FileHelper file = new FileHelper();
                    landtransfer.ActionTakenReportPath = file.SaveFile(actionTakenReport, landtransfer.ActionTakenReport);
                }
                takenOverFile = _configuration.GetSection("FilePaths:LandTransfer:TakenOverFile").Value.ToString();
                if (landtransfer.TakenOverFile != null)
                {
                    FileHelper file = new FileHelper();
                    landtransfer.TakenOverDocument = file.SaveFile(takenOverFile, landtransfer.TakenOverFile);
                }
                handedOverFile = _configuration.GetSection("FilePaths:LandTransfer:HandedOverFile").Value.ToString();
                if (landtransfer.HandedOverFiles != null)
                {
                    FileHelper file = new FileHelper();
                    landtransfer.HandedOverFile = file.SaveFile(handedOverFile, landtransfer.HandedOverFiles);
                }
                landtransfer.Id = 0;
                var result = await _landTransferService.Create(landtransfer);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    return View("Index");
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(landtransfer);
                }
            }
            else
            {
                return View(landtransfer);
            }
        }
        public async Task<IActionResult> View(int id)
        {
            var Data = await _landTransferService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpGet]
        public async Task<JsonResult> GetZoneList(int? DepartmentId)
        {
            DepartmentId = DepartmentId ?? 0;
            var zoneList = await _landTransferService.GetAllZone(Convert.ToInt32(DepartmentId));
            return Json(zoneList);
        }

        [HttpGet]
        public async Task<JsonResult> GetDivisionList(int? ZoneId)
        {
            ZoneId = ZoneId ?? 0;
            return Json(await _landTransferService.GetAllDivisionList(Convert.ToInt32(ZoneId)));
        }
        [HttpGet]
        public async Task<JsonResult> GetLocalityList(int? DivisionId)
        {
            DivisionId = DivisionId ?? 0;
            return Json(await _landTransferService.GetAllLocalityList(Convert.ToInt32(DivisionId)));
        }
        public async Task<PartialViewResult> GetHistoryDetails(string KhasraNo)
        {
            var result = await _landTransferService.GetHistoryDetails(KhasraNo);
            if (result != null)
            {
                return PartialView("_HistoryDetails", result);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return PartialView();
            }
        }
        public async Task<IActionResult> Download(int Id)
        {
            FileHelper file = new FileHelper();
            var Data = await _landTransferService.FetchSingleResult(Id);
            string filename = Data.CopyofOrderDocPath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public FileResult ViewDocument(string path)
        {
            FileHelper file = new FileHelper();
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
    }
}