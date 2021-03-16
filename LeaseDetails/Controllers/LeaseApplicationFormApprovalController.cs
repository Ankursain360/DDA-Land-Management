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
using LeaseDetails.Filters;
using Core.Enum;
namespace LeaseDetails.Controllers
{
    public class LeaseApplicationFormApprovalController : BaseController
    {
        public readonly ILeaseApplicationFormApprovalService _leaseApplicationFormApprovalService;
        private readonly ILeaseApplicationFormService _leaseApplicationFormService;
        public IConfiguration _configuration;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;

        public LeaseApplicationFormApprovalController(ILeaseApplicationFormApprovalService leaseApplicationFormApprovalService,
            ILeaseApplicationFormService leaseApplicationFormService,
            IConfiguration configuration, IWatchandwardService watchandwardService,
            IApprovalProccessService approvalproccessService, IWorkflowTemplateService workflowtemplateService)
        {
            _leaseApplicationFormApprovalService = leaseApplicationFormApprovalService;
            _leaseApplicationFormService = leaseApplicationFormService;
            _configuration = configuration;
            _approvalproccessService = approvalproccessService;
            _workflowtemplateService = workflowtemplateService;
        }


        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] LeaseApplicationFormApprovalSearchDto model)
        {
            var result = await _leaseApplicationFormApprovalService.GetPagedLeaseApplicationFormDetails(model, SiteContext.UserId);
            ViewBag.IsApproved = model.StatusId;
            return PartialView("_ListLeaseApplicationFormApproval", result);
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id)
        {
            var Data = await _leaseApplicationFormApprovalService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        [HttpPost]

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id, Leaseapplication leaseapplication)
        {
            var result = false;
            var Data = await _leaseApplicationFormApprovalService.FetchSingleResult(id);
            string ApprovalDocumentPath = _configuration.GetSection("FilePaths:LeaseApplicationForm:ApprovalDocumentPath").Value.ToString();
            FileHelper fileHelper = new FileHelper();
            var Msgddl = leaseapplication.ApprovalStatus;
            #region Approval Proccess At Further level start Added by Renu 16 march 2021
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
                            approvalproccess.ProccessID = Convert.ToInt32(_configuration.GetSection("workflowPreccessIdLeaseApplicationForm").Value);
                            approvalproccess.ServiceId = leaseapplication.Id;
                            approvalproccess.SendFrom = SiteContext.UserId;
                            approvalproccess.PendingStatus = 1;
                            approvalproccess.Remarks = leaseapplication.ApprovalRemarks; ///May be comment
                            approvalproccess.Status = Convert.ToInt32(leaseapplication.ApprovalStatus);
                            approvalproccess.DocumentName = leaseapplication.ApprovalDocument == null ? null : fileHelper.SaveFile(ApprovalDocumentPath, leaseapplication.ApprovalDocument);

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
                                    leaseapplication.ApprovedStatus = 1;
                                    leaseapplication.PendingAt = 0;
                                }
                                else
                                {
                                    leaseapplication.ApprovedStatus = 0;
                                    leaseapplication.PendingAt = Convert.ToInt32(DataFlow[i + 1].parameterName);
                                }
                                result = await _leaseApplicationFormService.UpdateBeforeApproval(leaseapplication.Id, leaseapplication);  //Update Table details 
                            }
                        }
                        break;
                    }

                }
            }

            #endregion


            if (Msgddl == "3")
            {
                ViewBag.Message = Alert.Show(Messages.Approvedsuccesfuly, "", AlertType.Success);
            }
            else
            {

                ViewBag.Message = Alert.Show(Messages.Forwardsuccesfuly, "", AlertType.Success);
            }
            return View("Index");
        }

        #region History Details Only For Approval Page
        public async Task<PartialViewResult> HistoryDetails(int id)
        {
            var Data = await _approvalproccessService.GetHistoryDetails(Convert.ToInt32(_configuration.GetSection("workflowPreccessId").Value), id);

            return PartialView("_HistoryDetails", Data);
        }
        #endregion
                

        #region EncroachmentRegisteration Details
        public async Task<PartialViewResult> EncroachmentRegisterView(int id)
        {
            var Data = await _leaseApplicationFormService.FetchLeaseApplicationDetails(id);
            Data.Leasedocuments = await _leaseApplicationFormService.LeaseApplicationDocumentDetails(id);
            return PartialView("_LeaseApplicationFormView", Data);
        }
        //public async Task<IActionResult> DownloadFirfile(int Id)
        //{
        //    FileHelper file = new FileHelper();
        //    EncroachmentFirFileDetails Data = await _encroachmentRegisterationService.GetEncroachmentFirFileDetails(Id);
        //    string filename = Data.FirFilePath;
        //    return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        //}
        #endregion


        #region Fetch workflow data for approval prrocess Added by Renu 16 march 2021
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

        //public async Task<FileResult> ViewDocument(int Id)
        //{
        //    FileHelper file = new FileHelper();
        //    Watchandwardphotofiledetails Data = await _watchandwardService.GetWatchandwardphotofiledetails(Id);
        //    string path = Data.PhotoFilePath;
        //    byte[] FileBytes = System.IO.File.ReadAllBytes(path);
        //    return File(FileBytes, file.GetContentType(path));
        //}
    }
}
