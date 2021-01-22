using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using System.IO;
using System.Threading.Tasks;
using Utility.Helper;
using LandTransfer.Filters;
using Core.Enum;
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
            return PartialView("_List", result);
        }
        [HttpPost]
        public async Task<PartialViewResult> UnverifiedList([FromBody] LandTransferSearchDto model)
        {
            var result = await _landTransferService.GetPropertyRegisterationUnverifiedDataForLandTransfer(model);
            return PartialView("_UnverifiedList", result);
        }
        public async Task<IActionResult> IndexUnverified()
        {
            Landtransfer landtransfer = new Landtransfer();
            Propertyregistration propertyRegistration = new Propertyregistration();
            propertyRegistration.ClassificationOfLandList = await _propertyregistrationService.GetClassificationOfLandDropDownListReport();
            propertyRegistration.LandUseList = await _propertyregistrationService.GetLandUseDropDownList();
            propertyRegistration.DisposalTypeList = await _propertyregistrationService.GetDisposalTypeDropDownList();
            propertyRegistration.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
            propertyRegistration.KhasraNoList = await _propertyregistrationService.GetKhasraReportList();
            landtransfer.Propertyregistration = propertyRegistration;
            return View(landtransfer);
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            Landtransfer landtransfer = new Landtransfer();
            Propertyregistration propertyRegistration = new Propertyregistration();
            propertyRegistration.ClassificationOfLandList = await _propertyregistrationService.GetClassificationOfLandDropDownListReport();
            propertyRegistration.LandUseList = await _propertyregistrationService.GetLandUseDropDownList();
            propertyRegistration.DisposalTypeList = await _propertyregistrationService.GetDisposalTypeDropDownList();
            propertyRegistration.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
            propertyRegistration.KhasraNoList = await _propertyregistrationService.GetKhasraReportList();
            landtransfer.Propertyregistration = propertyRegistration;
            return View(landtransfer);
        }


        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = new Landtransfer();//await _landTransferService.FetchSingleResultWithPropertyRegistration(id);
            if (Data == null)
            {
                Data = new Landtransfer();
            }
            if (SiteContext.UserId == 14)
            {
                ViewBag.IsValidateUser = 2;
            }
            else
            {
                ViewBag.IsValidateUser = 0;
            }
            Data.HandedOverZoneList = await _landTransferService.GetAllZone(Data.HandedOverDepartmentId ?? 0);
            Data.HandedOverDivisionList = await _landTransferService.GetAllDivisionList(Data.HandedOverZoneId == null ? 0 : Data.HandedOverZoneId);
            Data.TakenOverZoneList = await _landTransferService.GetAllZone(Data.TakenOverDepartmentId ?? 0);
            Data.TakenOverDivisionList = await _landTransferService.GetAllDivisionList(Data.TakenOverZoneId == null ? 0 : Data.HandedOverZoneId);

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
            Data.IsValidateData = Data.IsValidate == 1 ? true : false;
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _landTransferService.FetchSingleResultWithPropertyRegistration(id);
            if (Data == null)
            {
                Data = new Landtransfer();
            }
            if (SiteContext.UserId == 14)
            {
                ViewBag.IsValidateUser = 2;
            }
            else
            {
                ViewBag.IsValidateUser = 0;
            }
            Data.HandedOverZoneList = await _landTransferService.GetAllZone(Data.HandedOverDepartmentId ?? 0);
            Data.HandedOverDivisionList = await _landTransferService.GetAllDivisionList(Data.HandedOverZoneId == null ? 0 : Data.HandedOverZoneId);
            Data.TakenOverZoneList = await _landTransferService.GetAllZone(Data.TakenOverDepartmentId ?? 0);
            Data.TakenOverDivisionList = await _landTransferService.GetAllDivisionList(Data.TakenOverZoneId == null ? 0 : Data.HandedOverZoneId);

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
            Data.IsValidateData = Data.IsValidate == 1 ? true : false;
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        public async Task<IActionResult> Verify(int id)
        {
            var Data = await _landTransferService.FetchSingleResultWithPropertyRegistration(id);
            if (Data == null)
            {
                Data = new Landtransfer();
            }
            if (SiteContext.UserId == 14)
            {
                ViewBag.IsValidateUser = 2;
            }
            else
            {
                ViewBag.IsValidateUser = 0;
            }
            Data.HandedOverZoneList = await _landTransferService.GetAllZone(Data.HandedOverDepartmentId ?? 0);
            Data.HandedOverDivisionList = await _landTransferService.GetAllDivisionList(Data.HandedOverZoneId == null ? 0 : Data.HandedOverZoneId);
            Data.TakenOverZoneList = await _landTransferService.GetAllZone(Data.TakenOverDepartmentId ?? 0);
            Data.TakenOverDivisionList = await _landTransferService.GetAllDivisionList(Data.TakenOverZoneId == null ? 0 : Data.HandedOverZoneId);

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
            Data.IsValidateData = Data.IsValidate == 1 ? true : false;
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        [HttpPost]
        [AuthorizeContext(ViewAction.Verify)]
        public async Task<IActionResult> Verify(Landtransfer landtransfer)
        {
            //var Data = await _landTransferService.FetchSingleResult(landtransfer.Id);
            if (SiteContext.UserId == 14)
            {
                ViewBag.IsValidateUser = 2;
            }
            else
            {
                ViewBag.IsValidateUser = 0;
            }
            landtransfer.HandedOverZoneList = await _landTransferService.GetAllZone(landtransfer.HandedOverDepartmentId ?? 0);
            landtransfer.HandedOverDivisionList = await _landTransferService.GetAllDivisionList(landtransfer.HandedOverZoneId == null ? 0 : landtransfer.HandedOverZoneId);
            landtransfer.TakenOverZoneList = await _landTransferService.GetAllZone(landtransfer.TakenOverDepartmentId ?? 0);
            landtransfer.TakenOverDivisionList = await _landTransferService.GetAllDivisionList(landtransfer.TakenOverZoneId == null ? 0 : landtransfer.HandedOverZoneId);


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
                if (!landtransfer.IsValidateData)
                {
                    landtransfer.IsValidate = 0;
                }
                else
                {
                    landtransfer.IsValidate = 1;
                }
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
                var result = false;
                if (landtransfer.Id > 0)
                {
                    result = await _landTransferService.Update(landtransfer.Id, landtransfer);
                }
                else
                {
                    result = await _landTransferService.Create(landtransfer);
                }
                if (landtransfer.IsValidateData)
                {
                    PropertyRegistrationHistory propertyRegistrationHistory = new PropertyRegistrationHistory();
                    propertyRegistrationHistory.LandTransferId = landtransfer.Id;
                    propertyRegistrationHistory.PropertyRegistrationId = landtransfer.PropertyRegistrationId;
                    propertyRegistrationHistory.DepartmentId = landtransfer.HandedOverDepartmentId ?? 0;
                    propertyRegistrationHistory.ZoneId = landtransfer.HandedOverZoneId;
                    propertyRegistrationHistory.DivisionId = landtransfer.HandedOverDivisionId;
                    result = await _landTransferService.CreateHistory(propertyRegistrationHistory);
                    if (result)
                    {
                        Propertyregistration propertyregistration = new Propertyregistration();
                        propertyregistration.DepartmentId = landtransfer.HandedOverDepartmentId ?? 0;
                        propertyregistration.ZoneId = landtransfer.HandedOverZoneId;
                        propertyregistration.DivisionId = landtransfer.HandedOverDivisionId;

                        result = await _propertyregistrationService.UpdatePropertyRegistrationForLandTransfer(landtransfer.PropertyRegistrationId, propertyregistration);
                    }
                }
                if (result)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    Landtransfer landtransferForIndex = new Landtransfer();
                    Propertyregistration propertyRegistration = new Propertyregistration();
                    propertyRegistration.ClassificationOfLandList = await _propertyregistrationService.GetClassificationOfLandDropDownListReport();
                    propertyRegistration.LandUseList = await _propertyregistrationService.GetLandUseDropDownList();
                    propertyRegistration.DisposalTypeList = await _propertyregistrationService.GetDisposalTypeDropDownList();
                    propertyRegistration.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
                    propertyRegistration.KhasraNoList = await _propertyregistrationService.GetKhasraReportList();
                    landtransferForIndex.Propertyregistration = propertyRegistration;
                    return View("IndexUnverified", landtransferForIndex);
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
        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(Landtransfer landtransfer)
        {
            //var Data = await _landTransferService.FetchSingleResult(landtransfer.Id);
            landtransfer.HandedOverZoneList = await _landTransferService.GetAllZone(landtransfer.HandedOverDepartmentId ?? 0);
            landtransfer.HandedOverDivisionList = await _landTransferService.GetAllDivisionList(landtransfer.HandedOverZoneId == null ? 0 : landtransfer.HandedOverZoneId);
            landtransfer.TakenOverZoneList = await _landTransferService.GetAllZone(landtransfer.TakenOverDepartmentId ?? 0);
            landtransfer.TakenOverDivisionList = await _landTransferService.GetAllDivisionList(landtransfer.TakenOverZoneId == null ? 0 : landtransfer.HandedOverZoneId);


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
                if (!landtransfer.IsValidateData)
                {
                    landtransfer.IsValidate = 0;
                }
                else
                {
                    landtransfer.IsValidate = 1;
                }
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
                if (result)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    Landtransfer landtransferForIndex = new Landtransfer();
                    Propertyregistration propertyRegistration = new Propertyregistration();
                    propertyRegistration.ClassificationOfLandList = await _propertyregistrationService.GetClassificationOfLandDropDownListReport();
                    propertyRegistration.LandUseList = await _propertyregistrationService.GetLandUseDropDownList();
                    propertyRegistration.DisposalTypeList = await _propertyregistrationService.GetDisposalTypeDropDownList();
                    propertyRegistration.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
                    propertyRegistration.KhasraNoList = await _propertyregistrationService.GetKhasraReportList();
                    landtransferForIndex.Propertyregistration = propertyRegistration;
                    return View("Index", landtransferForIndex);
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
