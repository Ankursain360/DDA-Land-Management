using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model.Entity;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Utility.Helper;

namespace EncroachmentDemolition.Controllers
{
    public class EncroachmentRegisterController : BaseController
    {
        public IConfiguration _configuration;
        public readonly IEncroachmentRegisterationService _encroachmentRegisterationService;
        private readonly IWatchandwardService _watchandwardService;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;
        public EncroachmentRegisterController(IEncroachmentRegisterationService encroachmentRegisterationService, 
            IConfiguration configuration, IWatchandwardService watchandwardService, 
            IApprovalProccessService approvalproccessService, IWorkflowTemplateService workflowtemplateService)
        {
            _encroachmentRegisterationService = encroachmentRegisterationService;
            _configuration = configuration;
            _watchandwardService = watchandwardService;
            _workflowtemplateService = workflowtemplateService;
            _approvalproccessService = approvalproccessService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] EncroachmentRegisterationDto model)
        {
            var result = await _encroachmentRegisterationService.GetPagedEncroachmentRegisteration(model);
            return PartialView("_List", result);
        }
        public async Task<IActionResult> Create(int id)
        {
            EncroachmentRegisteration encroachmentRegisterations = new EncroachmentRegisteration();
            encroachmentRegisterations.WatchWardId = id;
            ViewBag.PrimaryId = 0;
            encroachmentRegisterations.Id = 0;
            encroachmentRegisterations.DepartmentList = await _encroachmentRegisterationService.GetAllDepartment();
            encroachmentRegisterations.ZoneList = await _encroachmentRegisterationService.GetAllZone(encroachmentRegisterations.DepartmentId);
            encroachmentRegisterations.DivisionList = await _encroachmentRegisterationService.GetAllDivisionList(encroachmentRegisterations.ZoneId);
            encroachmentRegisterations.LocalityList = await _encroachmentRegisterationService.GetAllLocalityList(encroachmentRegisterations.DivisionId);
            encroachmentRegisterations.KhasraList = await _encroachmentRegisterationService.GetAllKhasraList(encroachmentRegisterations.LocalityId);
            return View(encroachmentRegisterations);
        }

        public async Task<PartialViewResult> WatchWardView(int id)
        {
            var Data = await _watchandwardService.FetchSingleResult(id);
            if(Data!=null)
            Data.PrimaryListNoList = await _watchandwardService.GetAllPrimaryList();

            return PartialView("_WatchWard", Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EncroachmentRegisteration encroachmentRegisterations)
        {
            encroachmentRegisterations.DepartmentList = await _encroachmentRegisterationService.GetAllDepartment();
            encroachmentRegisterations.ZoneList = await _encroachmentRegisterationService.GetAllZone(encroachmentRegisterations.DepartmentId);
            encroachmentRegisterations.DivisionList = await _encroachmentRegisterationService.GetAllDivisionList(encroachmentRegisterations.ZoneId);
            encroachmentRegisterations.LocalityList = await _encroachmentRegisterationService.GetAllLocalityList(encroachmentRegisterations.DivisionId);
            encroachmentRegisterations.KhasraList = await _encroachmentRegisterationService.GetAllKhasraList(encroachmentRegisterations.LocalityId);
            string PhotoFilePath = _configuration.GetSection("FilePaths:EncroachmentRegisterationFiles:PhotoFilePath").Value.ToString();
            string LocationMapFilePath = _configuration.GetSection("FilePaths:EncroachmentRegisterationFiles:LocationMapFilePath").Value.ToString();
            string FirfilePath = _configuration.GetSection("FilePaths:EncroachmentRegisterationFiles:FIRFilePath").Value.ToString();
            if (ModelState.IsValid)
            {
                var result = await _encroachmentRegisterationService.Create(encroachmentRegisterations);
                if (result)
                {
                    FileHelper fileHelper = new FileHelper();
                    if (encroachmentRegisterations.NameOfStructure != null && encroachmentRegisterations.AreaApprox != null && encroachmentRegisterations.Type != null && encroachmentRegisterations.DateOfEncroachment != null && encroachmentRegisterations.CountOfStructure != null && encroachmentRegisterations.ReferenceNoOnLocation != null && encroachmentRegisterations.NameOfStructure.Count > 0 && encroachmentRegisterations.AreaApprox.Count > 0 && encroachmentRegisterations.Type.Count > 0 && encroachmentRegisterations.DateOfEncroachment.Count > 0 && encroachmentRegisterations.CountOfStructure.Count > 0 && encroachmentRegisterations.ReferenceNoOnLocation.Count > 0)
                    {
                        List<DetailsOfEncroachment> detailsOfEncroachment = new List<DetailsOfEncroachment>();
                        for (int i = 0; i < encroachmentRegisterations.NameOfStructure.Count; i++)
                        {
                            detailsOfEncroachment.Add(new DetailsOfEncroachment
                            {
                                Area = encroachmentRegisterations.AreaApprox[i],
                                CountOfStructure = encroachmentRegisterations.CountOfStructure[i],
                                DateOfEncroachment = encroachmentRegisterations.DateOfEncroachment[i],
                                ReligiousStructure = encroachmentRegisterations.ReligiousStructure[i],
                                ConstructionStatus = encroachmentRegisterations.ConstructionStatus[i],
                                NameOfStructure = encroachmentRegisterations.NameOfStructure[i],
                                ReferenceNoOnLocation = encroachmentRegisterations.ReferenceNoOnLocation[i],
                                Type = encroachmentRegisterations.Type[i],
                                EncroachmentRegisterationId = encroachmentRegisterations.Id
                            });
                        }
                        foreach (var item in detailsOfEncroachment)
                        {
                            result = await _encroachmentRegisterationService.SaveDetailsOfEncroachment(item);
                        }
                    }
                    if (encroachmentRegisterations.Firfile != null && encroachmentRegisterations.Firfile.Count > 0)
                    {
                        List<EncroachmentFirFileDetails> encroachmentFirFileDetails = new List<EncroachmentFirFileDetails>();
                        for (int i = 0; i < encroachmentRegisterations.Firfile.Count; i++)
                        {
                            string FilePath = fileHelper.SaveFile(FirfilePath, encroachmentRegisterations.Firfile[i]);
                            encroachmentFirFileDetails.Add(new EncroachmentFirFileDetails
                            {
                                EncroachmentRegistrationId = encroachmentRegisterations.Id,
                                FirFilePath = FilePath
                            });
                        }
                        foreach (var item in encroachmentFirFileDetails)
                        {
                            result = await _encroachmentRegisterationService.SaveEncroachmentFirFileDetails(item);
                        }
                    }
                    if (encroachmentRegisterations.PhotoFile != null && encroachmentRegisterations.PhotoFile.Count > 0)
                    {
                        List<EncroachmentPhotoFileDetails> encroachmentPhotoFileDetails = new List<EncroachmentPhotoFileDetails>();
                        for (int i = 0; i < encroachmentRegisterations.PhotoFile.Count; i++)
                        {
                            string FilePath = fileHelper.SaveFile(PhotoFilePath, encroachmentRegisterations.PhotoFile[i]);
                            encroachmentPhotoFileDetails.Add(new EncroachmentPhotoFileDetails
                            {
                                EncroachmentRegistrationId = encroachmentRegisterations.Id,
                                PhotoFilePath = FilePath
                            });
                        }
                        foreach (var item in encroachmentPhotoFileDetails)
                        {
                            result = await _encroachmentRegisterationService.SaveEncroachmentPhotoFileDetails(item);
                        }
                    }
                    if (encroachmentRegisterations.LocationMapFile != null && encroachmentRegisterations.LocationMapFile.Count > 0)
                    {
                        List<EncroachmentLocationMapFileDetails> encroachmentLocationMapFileDetails = new List<EncroachmentLocationMapFileDetails>();
                        for (int i = 0; i < encroachmentRegisterations.LocationMapFile.Count; i++)
                        {
                            string FilePath = fileHelper.SaveFile(LocationMapFilePath, encroachmentRegisterations.LocationMapFile[i]);
                            encroachmentLocationMapFileDetails.Add(new EncroachmentLocationMapFileDetails
                            {
                                EncroachmentRegistrationId = encroachmentRegisterations.Id,
                                LocationMapFilePath = FilePath
                            });
                        }
                        foreach (var item in encroachmentLocationMapFileDetails)
                        {
                            result = await _encroachmentRegisterationService.SaveEncroachmentLocationMapFileDetails(item);
                        }
                    }
                    if (result)
                    {
                        #region Approval Proccess At 1st level start Added by Renu 26 Nov 2020
                        var DataFlow = await dataAsync();
                        for (int i = 0; i < DataFlow.Count; i++)
                        {
                            if (!DataFlow[i].parameterSkip)
                            {
                                encroachmentRegisterations.ApprovedStatus = 0;
                                encroachmentRegisterations.PendingAt = Convert.ToInt32(DataFlow[i].parameterName);
                               // result = await _encroachmentRegisterationService.UpdateBeforeApproval(encroachmentRegisterations.Id, encroachmentRegisterations);  //Update Table details 
                                if (result)
                                {
                                    Approvalproccess approvalproccess = new Approvalproccess();
                                    approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                                    approvalproccess.ProccessID = Convert.ToInt32(_configuration.GetSection("workflowPreccessIdInspection").Value);
                                    approvalproccess.ServiceId = encroachmentRegisterations.Id;
                                    approvalproccess.SendFrom = SiteContext.UserId;
                                    approvalproccess.SendTo = Convert.ToInt32(DataFlow[i].parameterName);
                                    approvalproccess.PendingStatus = 1;   //1
                                    approvalproccess.Status = null;   //1
                                    approvalproccess.Remarks = "Record Added and Send for Approval";///May be Uncomment
                                    result = await _approvalproccessService.Create(approvalproccess, SiteContext.UserId); //Create a row in approvalproccess Table
                                }

                                break;
                            }
                        }

                        #endregion 
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var result1 = await _encroachmentRegisterationService.GetAllEncroachmentRegisteration();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(encroachmentRegisterations);
                    }
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(encroachmentRegisterations);
                }
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(encroachmentRegisterations);
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            var encroachmentRegisterations = await _encroachmentRegisterationService.FetchSingleResult(id);
            encroachmentRegisterations.DepartmentList = await _encroachmentRegisterationService.GetAllDepartment();
            encroachmentRegisterations.ZoneList = await _encroachmentRegisterationService.GetAllZone(encroachmentRegisterations.DepartmentId);
            encroachmentRegisterations.DivisionList = await _encroachmentRegisterationService.GetAllDivisionList(encroachmentRegisterations.ZoneId);
            encroachmentRegisterations.LocalityList = await _encroachmentRegisterationService.GetAllLocalityList(encroachmentRegisterations.DivisionId);
            encroachmentRegisterations.KhasraList = await _encroachmentRegisterationService.GetAllKhasraList(encroachmentRegisterations.LocalityId);
            if (encroachmentRegisterations == null)
            {
                return NotFound();
            }
            return View(encroachmentRegisterations);
        }
        public async Task<IActionResult> View(int id)
        {
            var encroachmentRegisterations = await _encroachmentRegisterationService.FetchSingleResult(id);
            encroachmentRegisterations.DepartmentList = await _encroachmentRegisterationService.GetAllDepartment();
            encroachmentRegisterations.ZoneList = await _encroachmentRegisterationService.GetAllZone(encroachmentRegisterations.DepartmentId);
            encroachmentRegisterations.DivisionList = await _encroachmentRegisterationService.GetAllDivisionList(encroachmentRegisterations.ZoneId);
            encroachmentRegisterations.LocalityList = await _encroachmentRegisterationService.GetAllLocalityList(encroachmentRegisterations.DivisionId);
            encroachmentRegisterations.KhasraList = await _encroachmentRegisterationService.GetAllKhasraList(encroachmentRegisterations.LocalityId);
            if (encroachmentRegisterations == null)
            {
                return NotFound();
            }
            return View(encroachmentRegisterations);
        }
        public async Task<JsonResult> DetailsOfRepeater(int? Id)
        {
            Id = Id ?? 0;
            var data = await _encroachmentRegisterationService.GetDetailsOfEncroachment(Convert.ToInt32(Id));
            //return Json(data.Select(x => new { x.CountOfStructure, DateOfEncroachment = Convert.ToDateTime(x.DateOfEncroachment).ToString("yyyy-MM-dd"), x.Area, x.NameOfStructure, x.ReferenceNoOnLocation, x.Type, x.ConstructionStatus }));
            return Json(data.Select(x => new { x.CountOfStructure,  x.DateOfEncroachment, x.Area, x.NameOfStructure, x.ReferenceNoOnLocation, x.Type, x.ConstructionStatus , x.ReligiousStructure}));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EncroachmentRegisteration encroachmentRegisterations)
        {
            var Data = await _encroachmentRegisterationService.FetchSingleResult(id);
            Data.DepartmentList = await _encroachmentRegisterationService.GetAllDepartment();
            Data.ZoneList = await _encroachmentRegisterationService.GetAllZone(Data.DepartmentId);
            Data.DivisionList = await _encroachmentRegisterationService.GetAllDivisionList(Data.ZoneId);
            Data.LocalityList = await _encroachmentRegisterationService.GetAllLocalityList(Data.DivisionId);
            Data.KhasraList = await _encroachmentRegisterationService.GetAllKhasraList(Data.LocalityId);
            string PhotoFilePath = _configuration.GetSection("FilePaths:EncroachmentRegisterationFiles:PhotoFilePath").Value.ToString();
            string LocationMapFilePath = _configuration.GetSection("FilePaths:EncroachmentRegisterationFiles:LocationMapFilePath").Value.ToString();
            string FirfilePath = _configuration.GetSection("FilePaths:EncroachmentRegisterationFiles:FIRFilePath").Value.ToString();
            if (ModelState.IsValid)
            {
                var result = await _encroachmentRegisterationService.Update(id, encroachmentRegisterations);
                if (result)
                {
                    FileHelper fileHelper = new FileHelper();
                    if (encroachmentRegisterations.NameOfStructure != null && encroachmentRegisterations.AreaApprox != null && encroachmentRegisterations.Type != null && encroachmentRegisterations.DateOfEncroachment != null && encroachmentRegisterations.CountOfStructure != null && encroachmentRegisterations.ReferenceNoOnLocation != null && encroachmentRegisterations.NameOfStructure.Count > 0 && encroachmentRegisterations.AreaApprox.Count > 0 && encroachmentRegisterations.Type.Count > 0 && encroachmentRegisterations.DateOfEncroachment.Count > 0 && encroachmentRegisterations.CountOfStructure.Count > 0 && encroachmentRegisterations.ReferenceNoOnLocation.Count > 0)
                    {
                        List<DetailsOfEncroachment> detailsOfEncroachment = new List<DetailsOfEncroachment>();
                        result =await _encroachmentRegisterationService.DeleteDetailsOfEncroachment(id);
                        for (int i = 0; i < encroachmentRegisterations.NameOfStructure.Count; i++)
                        {
                            detailsOfEncroachment.Add(new DetailsOfEncroachment
                            {
                                Area = encroachmentRegisterations.AreaApprox[i],
                                CountOfStructure = encroachmentRegisterations.CountOfStructure[i],
                                ReligiousStructure = encroachmentRegisterations.ReligiousStructure[i],
                                DateOfEncroachment = encroachmentRegisterations.DateOfEncroachment[i],
                                ConstructionStatus = encroachmentRegisterations.ConstructionStatus[i],
                                NameOfStructure = encroachmentRegisterations.NameOfStructure[i],
                                ReferenceNoOnLocation = encroachmentRegisterations.ReferenceNoOnLocation[i],
                                Type = encroachmentRegisterations.Type[i],
                                EncroachmentRegisterationId = encroachmentRegisterations.Id
                            });
                        }
                        foreach (var item in detailsOfEncroachment)
                        {
                            result = await _encroachmentRegisterationService.SaveDetailsOfEncroachment(item);
                        }
                    }
                    if (encroachmentRegisterations.Firfile != null && encroachmentRegisterations.Firfile.Count > 0)
                    {
                        List<EncroachmentFirFileDetails> encroachmentFirFileDetails = new List<EncroachmentFirFileDetails>();
                        result = await _encroachmentRegisterationService.DeleteEncroachmentFirFileDetails(id);
                        for (int i = 0; i < encroachmentRegisterations.Firfile.Count; i++)
                        {
                            string FilePath = fileHelper.SaveFile(FirfilePath, encroachmentRegisterations.Firfile[i]);
                            encroachmentFirFileDetails.Add(new EncroachmentFirFileDetails
                            {
                                EncroachmentRegistrationId = encroachmentRegisterations.Id,
                                FirFilePath = FilePath
                            });
                        }
                        foreach (var item in encroachmentFirFileDetails)
                        {
                            result = await _encroachmentRegisterationService.SaveEncroachmentFirFileDetails(item);
                        }
                    }
                    if (encroachmentRegisterations.PhotoFile != null && encroachmentRegisterations.PhotoFile.Count > 0)
                    {
                        List<EncroachmentPhotoFileDetails> encroachmentPhotoFileDetails = new List<EncroachmentPhotoFileDetails>();
                        result = await _encroachmentRegisterationService.DeleteEncroachmentPhotoFileDetails(id);
                        for (int i = 0; i < encroachmentRegisterations.PhotoFile.Count; i++)
                        {
                            string FilePath = fileHelper.SaveFile(PhotoFilePath, encroachmentRegisterations.PhotoFile[i]);
                            encroachmentPhotoFileDetails.Add(new EncroachmentPhotoFileDetails
                            {
                                EncroachmentRegistrationId = encroachmentRegisterations.Id,
                                PhotoFilePath = FilePath
                            });
                        }
                        foreach (var item in encroachmentPhotoFileDetails)
                        {
                            result = await _encroachmentRegisterationService.SaveEncroachmentPhotoFileDetails(item);
                        }
                    }
                    if (encroachmentRegisterations.LocationMapFile != null && encroachmentRegisterations.LocationMapFile.Count > 0)
                    {
                        List<EncroachmentLocationMapFileDetails> encroachmentLocationMapFileDetails = new List<EncroachmentLocationMapFileDetails>();
                        result = await _encroachmentRegisterationService.DeleteEncroachmentLocationMapFileDetails(id);
                        for (int i = 0; i < encroachmentRegisterations.LocationMapFile.Count; i++)
                        {
                            string FilePath = fileHelper.SaveFile(LocationMapFilePath, encroachmentRegisterations.LocationMapFile[i]);
                            encroachmentLocationMapFileDetails.Add(new EncroachmentLocationMapFileDetails
                            {
                                EncroachmentRegistrationId = encroachmentRegisterations.Id,
                                LocationMapFilePath = FilePath
                            });
                        }
                        foreach (var item in encroachmentLocationMapFileDetails)
                        {
                            result = await _encroachmentRegisterationService.SaveEncroachmentLocationMapFileDetails(item);
                        }
                    }
                    if (result)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var result1 = await _encroachmentRegisterationService.GetAllEncroachmentRegisteration();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(encroachmentRegisterations);
                    }

                }
                else
                {
                    return View(encroachmentRegisterations);
                }
            }
            else
            {
                return View(encroachmentRegisterations);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _encroachmentRegisterationService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            var result1 = await _encroachmentRegisterationService.GetAllEncroachmentRegisteration();
            return View("Index", result1);
        }

        [HttpGet]
        public async Task<JsonResult> GetZoneList(int? DepartmentId)
        {
            DepartmentId = DepartmentId ?? 0;
            return Json(await _encroachmentRegisterationService.GetAllZone(Convert.ToInt32(DepartmentId)));
        }
        [HttpGet]
        public async Task<JsonResult> GetDivisionList(int? ZoneId)
        {
            ZoneId = ZoneId ?? 0;
            return Json(await _encroachmentRegisterationService.GetAllDivisionList(Convert.ToInt32(ZoneId)));
        }
        [HttpGet]
        public async Task<JsonResult> GetLocalityList(int? DivisionId)
        {
            DivisionId = DivisionId ?? 0;
            return Json(await _encroachmentRegisterationService.GetAllLocalityList(Convert.ToInt32(DivisionId)));
        }
        public async Task<IActionResult> DownloadFirfile(int Id)
        {
            FileHelper file = new FileHelper();
            EncroachmentFirFileDetails Data = await _encroachmentRegisterationService.GetEncroachmentFirFileDetails(Id);
            string filename = Data.FirFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadLocationMapFile(int Id)
        {
            FileHelper file = new FileHelper();
            EncroachmentLocationMapFileDetails Data = await _encroachmentRegisterationService.GetEncroachmentLocationMapFileDetails(Id);
            string filename = Data.LocationMapFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadPhotoFile(int Id)
        {
            FileHelper file = new FileHelper();
            EncroachmentPhotoFileDetails Data = await _encroachmentRegisterationService.GetEncroachmentPhotoFileDetails(Id);
            string filename = Data.PhotoFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }

        public IActionResult EncroachmentRegisterApproval()
        {
            return View();
        }

        public IActionResult EncroachmentRegisterApprovalCreate()
        {
            return View();
        }

        #region Fetch workflow data for approval prrocess Added by Renu 26 Nov 2020
        private async Task<List<TemplateStructure>> dataAsync()
        {
            var Data = await _workflowtemplateService.FetchSingleResult(2);
            var template = Data.Template;
            List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
            return ObjList;
        }
        #endregion
    }
}
