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
//using LandTransfer.Controllers;
using Utility;
using Utility.Helper;
using System.Net;
using LandInventory.Filters;
using Core.Enum;
using Dto.Master;


namespace LandInventory.Controllers
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
        public object JsonRequestBehavior { get; private set; }

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
            if (Data==null)
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
            Data.HandedOverZoneList= await _landTransferService.GetAllZone(Data.HandedOverDepartmentId??0);
            Data.HandedOverDivisionList = await _landTransferService.GetAllDivisionList(Data == null ? 0 : Data.HandedOverZoneId);
            Data.TakenOverZoneList= await _landTransferService.GetAllZone(Data.TakenOverDepartmentId??0);
            Data.TakenOverDivisionList= await _landTransferService.GetAllDivisionList(Data == null ? 0 : Data.TakenOverZoneId);

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
            if (Data==null)
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
            Data.HandedOverZoneList= await _landTransferService.GetAllZone(Data.HandedOverDepartmentId??0);
            Data.HandedOverDivisionList = await _landTransferService.GetAllDivisionList(Data == null ? 0 : Data.HandedOverZoneId);
            Data.TakenOverZoneList= await _landTransferService.GetAllZone(Data.TakenOverDepartmentId??0);
            Data.TakenOverDivisionList= await _landTransferService.GetAllDivisionList(Data == null ? 0 : Data.TakenOverZoneId);

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
        [AuthorizeContext(ViewAction.Verify)]
        public async Task<IActionResult> Verify(int id)
        {
            var Data = await _landTransferService.FetchSingleResultWithPropertyRegistration(id);
            if (Data==null)
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
            Data.HandedOverZoneList= await _landTransferService.GetAllZone(Data.HandedOverDepartmentId??0);
            Data.HandedOverDivisionList = await _landTransferService.GetAllDivisionList(Data == null ? 0 : Data.HandedOverZoneId);
            Data.TakenOverZoneList= await _landTransferService.GetAllZone(Data.TakenOverDepartmentId??0);
            Data.TakenOverDivisionList= await _landTransferService.GetAllDivisionList(Data == null ? 0 : Data.TakenOverZoneId);

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
            landtransfer.HandedOverDivisionList = await _landTransferService.GetAllDivisionList(landtransfer == null ? 0 : landtransfer.HandedOverZoneId);
            landtransfer.TakenOverZoneList = await _landTransferService.GetAllZone(landtransfer.TakenOverDepartmentId ?? 0);
            landtransfer.TakenOverDivisionList = await _landTransferService.GetAllDivisionList(landtransfer == null ? 0 : landtransfer.TakenOverZoneId);


          


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
                    propertyRegistrationHistory.DepartmentId = landtransfer.HandedOverDepartmentId??0;
                    propertyRegistrationHistory.ZoneId = landtransfer.HandedOverZoneId;
                    propertyRegistrationHistory.DivisionId = landtransfer.HandedOverDivisionId;
                    result = await _landTransferService.CreateHistory(propertyRegistrationHistory);
                    if (result)
                    {
                        Propertyregistration propertyregistration = new Propertyregistration();
                        propertyregistration.DepartmentId = landtransfer.HandedOverDepartmentId??0;
                        propertyregistration.ZoneId = landtransfer.HandedOverZoneId;
                        propertyregistration.DivisionId = landtransfer.HandedOverDivisionId;
                        propertyregistration.ModifiedBy = SiteContext.UserId;
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
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Edit(Landtransfer landtransfer)
        {
            bool IsValidpdf = CheckMimeType(landtransfer);
            bool IsValidpdf1 = CheckMimeType1(landtransfer);
            bool IsValidpdf2 = CheckMimeType2(landtransfer);
            //var Data = await _landTransferService.FetchSingleResult(landtransfer.Id);
            landtransfer.HandedOverZoneList = await _landTransferService.GetAllZone(landtransfer.HandedOverDepartmentId ?? 0);
            landtransfer.HandedOverDivisionList = await _landTransferService.GetAllDivisionList(landtransfer == null ? 0 : landtransfer.HandedOverZoneId);
            landtransfer.TakenOverZoneList = await _landTransferService.GetAllZone(landtransfer.TakenOverDepartmentId ?? 0);
            landtransfer.TakenOverDivisionList = await _landTransferService.GetAllDivisionList(landtransfer == null ? 0 : landtransfer.HandedOverZoneId);

            landtransfer.LandTransferList = await _landTransferService.GetAllLandTransfer(landtransfer.PropertyRegistrationId);
            if (ModelState.IsValid)
            {

                if (IsValidpdf == true)
                {

                    if (IsValidpdf1 == true)
                    {
                        if (IsValidpdf2 == true)
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
                            ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                            return View(landtransfer);
                        }
                    }

                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                        return View(landtransfer);
                    }
                }

                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
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
        // [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> DownloadIndex()
        {
            List<Propertyregistration> result = await _propertyregistrationService.GetAllPropertyregistration(SiteContext.UserId);
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"HandOverTakenOver.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }




        public async Task<IActionResult> HandoverTakeoverList()
        {
            var result = await _landTransferService.GetAllHandOverTakeOverList();
            List<HandoverTakeoverListDto> data = new List<HandoverTakeoverListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new HandoverTakeoverListDto()
                    {
                        Id = result[i].Id,
                        InventoriedIn = result[i].InventoriedInId.ToString() == "1" ? "VLMS" : "Used",
                        ClassificationofLand = result[i].ClassificationOfLand == null ? " " : result[i].ClassificationOfLand.Name,
                        PlannedUnplannedLand = result[i].PlannedUnplannedLand,


                        Department = result[i].Department == null ? " " : result[i].Department.Name,
                        Zone = result[i].Zone == null ? " " : result[i].Zone.Name,
                        Division = result[i].Division == null ? " " : result[i].Division.Name,
                        PrimaryListNo = result[i].PrimaryListNo,
                        AddressWithLandmark = result[i].Palandmark,


                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }




        public async Task<IActionResult> UnverifiedTransferRecordsList()
        {
            var result = await _landTransferService.GetAllUnverifiedTransferRecordList();
            List<UnverifiedTransferRecordsListDto> data = new List<UnverifiedTransferRecordsListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new UnverifiedTransferRecordsListDto()
                    {
                        Id = result[i].Id,
                        InventoriedIn = result[i].InventoriedInId.ToString() == "1" ? "VLMS" : "Used",
                        ClassificationofLand = result[i].ClassificationOfLand == null ? " " : result[i].ClassificationOfLand.Name,
                        PlannedUnplannedLand = result[i].PlannedUnplannedLand,


                        Department = result[i].Department == null ? " " : result[i].Department.Name,
                        Zone = result[i].Zone == null ? " " : result[i].Zone.Name,
                        Division = result[i].Division == null ? " " : result[i].Division.Name,
                        PrimaryListNo = result[i].PrimaryListNo,
                        AddressWithLandmark = result[i].Palandmark,


                    }); ;
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
            copyOfOrderDoc = _configuration.GetSection("FilePaths:LandTransfer:CopyOfOrderDoc").Value.ToString();
            IFormFile files = Request.Form.Files["file"];
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                copyOfOrderDoc = _configuration.GetSection("FilePaths:LandTransfer:CopyOfOrderDoc").Value.ToString();
                string FilePath = Path.Combine(copyOfOrderDoc, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(copyOfOrderDoc))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(copyOfOrderDoc);// Try to create the directory.
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
                                fullpath = _configuration.GetSection("FilePaths:LandTransfer:CopyOfOrderDoc").Value.ToString();
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




        public bool CheckMimeType(Landtransfer landtransfer)
        {
            bool Flag = true;
            string fullpath = string.Empty;
            string extension = string.Empty;
            copyOfOrderDoc = _configuration.GetSection("FilePaths:LandTransfer:CopyOfOrderDoc").Value.ToString();
            IFormFile files = landtransfer.CopyofOrder;
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                copyOfOrderDoc = _configuration.GetSection("FilePaths:LandTransfer:CopyOfOrderDoc").Value.ToString();
                string FilePath = Path.Combine(copyOfOrderDoc, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(copyOfOrderDoc))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(copyOfOrderDoc);// Try to create the directory.
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
                                fullpath = _configuration.GetSection("FilePaths:LandTransfer:CopyOfOrderDoc").Value.ToString();

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


        public bool CheckMimeType1(Landtransfer landtransfer)
        {
            bool Flag = true;
            string fullpath = string.Empty;
            string extension = string.Empty;
            takenOverFile = _configuration.GetSection("FilePaths:LandTransfer:TakenOverFile").Value.ToString();
            IFormFile files = landtransfer.TakenOverFile;
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                takenOverFile = _configuration.GetSection("FilePaths:LandTransfer:TakenOverFile").Value.ToString();
                string FilePath = Path.Combine(takenOverFile, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(takenOverFile))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(takenOverFile);// Try to create the directory.
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
                                fullpath = _configuration.GetSection("FilePaths:LandTransfer:TakenOverFile").Value.ToString();


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



        public bool CheckMimeType2(Landtransfer landtransfer)
        {
            bool Flag = true;
            string fullpath = string.Empty;
            string extension = string.Empty;
            handedOverFile = _configuration.GetSection("FilePaths:LandTransfer:HandedOverFile").Value.ToString();
            IFormFile files = landtransfer.HandedOverFiles;
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                handedOverFile = _configuration.GetSection("FilePaths:LandTransfer:HandedOverFile").Value.ToString();
                string FilePath = Path.Combine(handedOverFile, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(handedOverFile))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(handedOverFile);// Try to create the directory.
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
                                fullpath = _configuration.GetSection("FilePaths:LandTransfer:HandedOverFile").Value.ToString();


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