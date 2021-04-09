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
    public class ExtensionApprovalController  : BaseController
    {
        public readonly IExtensionApprovalService _extensionApprovalService;
        private readonly IExtensionService _extensionService;
        public IConfiguration _configuration;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;
        string LeaseFilePath = "";
        string ApprovalDocumentPath = "";
        public ExtensionApprovalController(IExtensionApprovalService extensionApprovalService,
        IExtensionService extensionService,
           IConfiguration configuration,
           IApprovalProccessService approvalproccessService, IWorkflowTemplateService workflowtemplateService)
        {
            _extensionApprovalService = extensionApprovalService;
            _extensionService = extensionService;
            _configuration = configuration;
            _approvalproccessService = approvalproccessService;
            _workflowtemplateService = workflowtemplateService;
            LeaseFilePath = _configuration.GetSection("FilePaths:LeaseApplicationForm:DocumentFilePath").Value.ToString();
            ApprovalDocumentPath = _configuration.GetSection("FilePaths:LeaseApplicationForm:ApprovalDocumentPath").Value.ToString();

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] ExtensionApprovalSearchDto model)
        {
            var result = await _extensionApprovalService.GetPagedExtensionDetails(model, SiteContext.UserId);
            ViewBag.IsApproved = model.StatusId;
            return PartialView("_ListExtensionApproval", result);
        }
      
        public async Task<IActionResult> Create(int id)
        {
            var Data = await _extensionApprovalService.FetchSingleResult(id);
            var dropdownValue = await GetApprovalStatusDropdownList();
            List<int> dropdownValue1 = ConvertStringListToIntList(dropdownValue);
            Data.ApprovalStatusList = await _approvalproccessService.BindDropdownApprovalStatus(dropdownValue1.ToArray());
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        public List<int> ConvertStringListToIntList(List<string> list)
        {
            List<int> resultList = new List<int>();
            for (int i = 0; i < list.Count; i++)
                resultList.Add(Convert.ToInt32(list[i]));

            return resultList;
        }
        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id, Extension extension)
        {
            var result = false;
            var Data = await _extensionApprovalService.FetchSingleResult(id);
            FileHelper fileHelper = new FileHelper();
            var Msgddl = extension.ApprovalStatus;

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
                            approvalproccess.ProccessID = Convert.ToInt32(_configuration.GetSection("workflowProcessIdExtensionService").Value);
                            approvalproccess.ServiceId = extension.Id;
                            approvalproccess.SendFrom = SiteContext.UserId;
                            approvalproccess.PendingStatus = 1;
                            approvalproccess.Remarks = extension.ApprovalRemarks; ///May be comment
                            approvalproccess.Status = Convert.ToInt32(extension.ApprovalStatus);
                            approvalproccess.DocumentName = extension.ApprovalDocument == null ? null : fileHelper.SaveFile1(ApprovalDocumentPath, extension.ApprovalDocument);

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
                                    extension.ApprovedStatus = 1;
                                    extension.PendingAt = "0";
                                }
                                else
                                {
                                    extension.ApprovedStatus = 0;
                                    extension.PendingAt = Convert.ToString(DataFlow[i + 1].parameterName);
                                }
                                result = await _extensionService.UpdateBeforeApproval(extension.Id, extension);  //Update Table details 
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


        #region Fetch workflow data for approval prrocess Added by Renu 16 march 2021
        private async Task<List<TemplateStructure>> DataAsync()
        {
            var Data = await _workflowtemplateService.FetchSingleResult(Convert.ToInt32(_configuration.GetSection("workflowProcessIdExtensionService").Value));
            var template = Data.Template;
            List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
            return ObjList;
        }

        [HttpGet]
        //public async Task<JsonResult> GetApprovalDropdownList()  //Bind Dropdown of Approval Status
        //{
        //    var DataFlow = await DataAsync();

        //    for (int i = 0; i < DataFlow.Count; i++)
        //    {
        //        if (Convert.ToInt32(DataFlow[i].parameterName) == SiteContext.UserId)
        //        {
        //            var dropdown = DataFlow[i].parameterAction;
        //            return Json(dropdown);
        //            break;
        //        }

        //    }
        //    return Json(DataFlow);
        //}

        public async Task<List<string>> GetApprovalStatusDropdownList()  //Bind Dropdown of Approval Status
        {
            var DataFlow = await DataAsync();
            List<string> dropdown = null;

            for (int i = 0; i < DataFlow.Count; i++)
            {
                if (Convert.ToInt32(DataFlow[i].parameterName) == SiteContext.UserId)
                {
                    dropdown = (List<string>)DataFlow[i].parameterAction;
                    return (dropdown);
                    break;
                }

            }
            return (List<string>)dropdown;
        }

        #region History Details Only For Approval Page
        public async Task<PartialViewResult> HistoryDetails(int id)
        {
            var Data = await _approvalproccessService.GetHistoryDetails(Convert.ToInt32(_configuration.GetSection("workflowProcessIdExtensionService").Value), id);

            return PartialView("_HistoryDetails", Data);
        }
        #endregion

        #region Extension Details
        public async Task<PartialViewResult> ExtensionView(int id)
        {
            var Data = await _extensionService.FetchSingleResult(id);
          //  Data.Leasedocuments = await _leaseApplicationFormService.LeaseApplicationDocumentDetails(id);
            return PartialView("_LeaseApplicationFormView", Data);
        }

        //public async Task<IActionResult> ViewDocumentLeaseApplicationForm(int Id)
        //{
        //    FileHelper file = new FileHelper();
        //    Leaseapplicationdocuments Data = await _leaseApplicationFormService.FetchLeaseApplicationDocumentDetails(Id);
        //    string filename = LeaseFilePath + Data.DocumentFileName;
        //    byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
        //    return File(FileBytes, file.GetContentType(filename));
        //    //string filename = Data.DocumentFileName;
        //    //return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        //}
        #endregion
        public async Task<IActionResult> ViewDocumentApprovalProccess(int Id)
        {
            FileHelper file = new FileHelper();
            Approvalproccess Data = await _approvalproccessService.FetchApprovalProcessDocumentDetails(Id);
            string filename = ApprovalDocumentPath + Data.DocumentName;
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, file.GetContentType(filename));
            //string filename = Data.DocumentFileName;
            //return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        #endregion
    }
}
