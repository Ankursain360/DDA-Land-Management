using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Service.IApplicationService;
using Libraries.Model.Entity;
using Utility.Helper;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.Extensions.Configuration;
using Dto.Search;
using System.IO;
using System.Linq;
using EncroachmentDemolition.Filters;
using Core.Enum;
using System.Data;
using Newtonsoft.Json;


namespace EncroachmentDemolition.Controllers
{
    public class AnnexureAApprovalController : BaseController
    {
        public readonly IAnnexureAApprovalService _annexureAApprovalService;
        public readonly IAnnexureAService _annexureAService;
        public readonly IEncroachmentRegisterationApprovalService _encroachmentRegisterationApprovalService;
        public IConfiguration _configuration;
        public readonly IEncroachmentRegisterationService _encroachmentRegisterationService;
        private readonly IWatchandwardService _watchandwardService;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;

        public AnnexureAApprovalController(IEncroachmentRegisterationApprovalService encroachmentRegisterationApprovalService, IEncroachmentRegisterationService encroachmentRegisterationService,
            IConfiguration configuration, IWatchandwardService watchandwardService,
            IApprovalProccessService approvalproccessService, IWorkflowTemplateService workflowtemplateService,
            IAnnexureAService annexureAService, IAnnexureAApprovalService annexureAApprovalService)
        {
            _encroachmentRegisterationApprovalService = encroachmentRegisterationApprovalService;
            _encroachmentRegisterationService = encroachmentRegisterationService;
            _configuration = configuration;
            _watchandwardService = watchandwardService;
            _workflowtemplateService = workflowtemplateService;
            _approvalproccessService = approvalproccessService;
            _annexureAService = annexureAService;
            _annexureAApprovalService = annexureAApprovalService;
        }

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] AnnexureAApprovalSearchDto model)
        {
            var result = await _annexureAApprovalService.GetPagedAnnexureA(model, SiteContext.UserId);
            ViewBag.IsApproved = model.StatusId;
            return PartialView("_List", result);
        }



        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id)
        {
            var Data = await _annexureAApprovalService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id, Fixingdemolition fixingdemolition)
        {
            var result = false;
           
            #region Approval Proccess At Further level start Added by Renu 9 Dec 2020
            var DataFlow = await DataAsync();
            for (int i = 0; i < DataFlow.Count; i++)
            {
                if (!DataFlow[i].parameterSkip)
                {
                    if (Convert.ToInt32(DataFlow[i].parameterName) == SiteContext.UserId)
                    {
                        result = true;
                        if (result)
                        {
                            Approvalproccess approvalproccess = new Approvalproccess();
                            approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                            approvalproccess.ProccessID = Convert.ToInt32(_configuration.GetSection("workflowPreccessIdRequestDemolition").Value);
                            approvalproccess.ServiceId = fixingdemolition.Id;
                            approvalproccess.SendFrom = SiteContext.UserId;
                            approvalproccess.PendingStatus = 1;
                            approvalproccess.Remarks = fixingdemolition.ApprovalRemarks; ///May be comment
                            approvalproccess.Status = Convert.ToInt32(fixingdemolition.ApprovalStatus);
                            if (i == DataFlow.Count - 1)
                                approvalproccess.SendTo = null;
                            else
                            {
                                approvalproccess.SendTo = Convert.ToInt32(DataFlow[i + 1].parameterName);
                            }
                            result = await _approvalproccessService.Create(approvalproccess, SiteContext.UserId); //Create a row in approvalproccess Table

                            if (result)
                            {
                                if (i == DataFlow.Count - 1)
                                {
                                    fixingdemolition.ApprovedStatus = 1;
                                    fixingdemolition.PendingAt = 0;
                                }
                                else
                                {
                                    fixingdemolition.ApprovedStatus = 0;
                                    fixingdemolition.PendingAt = Convert.ToInt32(DataFlow[i + 1].parameterName);
                                }
                                result = await _annexureAService.UpdateBeforeApproval(id, fixingdemolition);
                            }
                        }
                        break;
                    }

                }
            }

            #endregion

            ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
            return View("Index");
        }

        #region History Details Only For Approval Page
        public async Task<PartialViewResult> HistoryDetails(int id)
        {
            var Data = await _approvalproccessService.GetHistoryDetails(Convert.ToInt32(_configuration.GetSection("workflowPreccessIdRequestDemolition").Value), id);

            return PartialView("_HistoryDetails", Data);
        }
        #endregion

        #region Watch & Ward  Details
        public async Task<PartialViewResult> WatchWardView(int id)
        {
            var Data = await _watchandwardService.FetchSingleResult(id);
            if (Data != null)
                Data.PrimaryListNoList = await _watchandwardService.GetAllPrimaryList();

            return PartialView("_WatchWard", Data);
        }



        public async Task<FileResult> ViewDocument(int Id)
        {
            FileHelper file = new FileHelper();
            Watchandwardphotofiledetails Data = await _watchandwardService.GetWatchandwardphotofiledetails(Id);
            string path = Data.PhotoFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        #endregion

        #region EncroachmentRegisteration Details
        public async Task<PartialViewResult> EncroachmentRegisterView(int id)
        {
            var encroachmentRegisterations = await _encroachmentRegisterationService.FetchSingleResult(id);
            encroachmentRegisterations.DepartmentList = await _encroachmentRegisterationService.GetAllDepartment();
            encroachmentRegisterations.ZoneList = await _encroachmentRegisterationService.GetAllZone(encroachmentRegisterations.DepartmentId);
            encroachmentRegisterations.DivisionList = await _encroachmentRegisterationService.GetAllDivisionList(encroachmentRegisterations.ZoneId);
            encroachmentRegisterations.LocalityList = await _encroachmentRegisterationService.GetAllLocalityList(encroachmentRegisterations.DivisionId);
            encroachmentRegisterations.KhasraList = await _encroachmentRegisterationService.GetAllKhasraList(encroachmentRegisterations.LocalityId);

            return PartialView("_EncroachmentRegisterView", encroachmentRegisterations);
        }




        public async Task<IActionResult> DownloadPhotoFile(int Id)
        {
            FileHelper file = new FileHelper();
            EncroachmentPhotoFileDetails Data = await _encroachmentRegisterationService.GetEncroachmentPhotoFileDetails(Id);
            string filename = Data.PhotoFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }

        public async Task<JsonResult> DetailsOfRepeater(int? Id)
        {
            Id = Id ?? 0;
            var data = await _encroachmentRegisterationService.GetDetailsOfEncroachment(Convert.ToInt32(Id));
            return Json(data.Select(x => new { x.CountOfStructure, x.DateOfEncroachment, x.Area, x.NameOfStructure, x.ReferenceNoOnLocation, x.Type, x.ConstructionStatus, x.ReligiousStructure }));
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
        #endregion

        #region AnnexureA Details
        public async Task<PartialViewResult> AnnexureADetails(int id)
        {
            var Data = await _annexureAService.FetchSingleResult(id);
            Data.Demolitionchecklist = await _annexureAService.GetDemolitionchecklist();
            Data.Demolitionprogram = await _annexureAService.GetDemolitionprogram();
            Data.Demolitiondocument = await _annexureAService.GetDemolitiondocument();
            return PartialView("_AnnexureAView", Data);
        }

        public async Task<FileResult> ViewDocumentAnnexureA(int Id)
        {
            FileHelper file = new FileHelper();
            Fixingdocument Data = await _annexureAService.GetAnnexureAfiledetails(Id);
            string path = Data.DocumentDetails;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        #endregion

        #region Fetch workflow data for approval prrocess Added by Renu 9 Dec 2020
        private async Task<List<TemplateStructure>> DataAsync()
        {
            var Data = await _workflowtemplateService.FetchSingleResult(2);
            var template = Data.Template;
            List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
            return ObjList;
        }

        [HttpGet]
        public async Task<JsonResult> GetApprovalDropdownList()  //Bind Dropdown of Approval Status
        {
            var DataFlow = await DataAsync();

            for (int i = 0; i < DataFlow.Count; i++)
            {
                if (Convert.ToInt32(DataFlow[i].parameterName) == SiteContext.UserId)
                {
                    var dropdown = DataFlow[i].parameterAction;
                    return Json(dropdown);
                    break;
                }

            }
            return Json(DataFlow);
        }
        #endregion
    }
}
