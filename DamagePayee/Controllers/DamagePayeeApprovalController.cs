using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dto.Search;
using Dto.Master;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Utility.Helper;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System.IO;
namespace DamagePayee.Controllers
{
    //This is right user yes : 1, This is right user No : 0
    public class DamagePayeeApprovalController : BaseController
    {
        private readonly IDamagepayeeregisterService _damagepayeeregisterService;
        private readonly IDamagePayeeApprovalService _damagePayeeApprovalService;
        public IConfiguration _configuration;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly ISelfAssessmentDamageService _selfAssessmentDamageService;
        private readonly IProccessWorkflowService _proccessWorkflowService;
        public DamagePayeeApprovalController(IDamagePayeeApprovalService damagePayeeApprovalService,
            IDamagepayeeregisterService damagepayeeregisterService, IConfiguration configuration,
            IWorkflowTemplateService workflowtemplateService, ISelfAssessmentDamageService selfAssessmentDamageService,
            IProccessWorkflowService proccessWorkflowService)
        {
            _configuration = configuration;
            _damagePayeeApprovalService = damagePayeeApprovalService;
            _damagepayeeregisterService = damagepayeeregisterService;
            _workflowtemplateService = workflowtemplateService;
            _selfAssessmentDamageService = selfAssessmentDamageService;
            _proccessWorkflowService = proccessWorkflowService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DamagepayeeRegisterApprovalDto model)
        {
            var IsUser =await CheckThisisUser();
            var result = await _damagePayeeApprovalService.GetPagedDamagePayeeRegisterForApproval(model, IsUser);
            return PartialView("_List", result);
        }
        async Task BindDropDown(Damagepayeeregistertemp damagepayeeregistertemp)
        {
            damagepayeeregistertemp.LocalityList = await _damagepayeeregisterService.GetLocalityList();
            damagepayeeregistertemp.DistrictList = await _damagepayeeregisterService.GetDistrictList();
        }
        public async Task<IActionResult> Create(int id)
        {
            var Data = await _damagepayeeregisterService.FetchSingleResult(id);
            await BindDropDown(Data);
            var value = await _selfAssessmentDamageService.GetRebateValue();
            if (value == null)
                ViewBag.RebateValue = 0;
            else
                ViewBag.RebateValue = Math.Round((value.RebatePercentage), 2);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DamagePayeeApprovalCreateDto damagepayeeapprovalcreatedto)
        {
            Processworkflow processworkflow = new Processworkflow();
            WorkflowTemplate model = new WorkflowTemplate();
            //model.Name = workflowtemplatecreatedto.name;
            //model.Description = workflowtemplatecreatedto.description;
            //model.ModuleId = workflowtemplatecreatedto.moduleId;
            //model.UserType = workflowtemplatecreatedto.usertype;
            //model.IsActive = workflowtemplatecreatedto.isActive;
            //model.Template = workflowtemplatecreatedto.template;

            if (ModelState.IsValid)
            {
                if (model.Template != null)
                {
                    var result = await _workflowtemplateService.Create(model);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        return Json(Url.Action("Index", "DamagePayeeApproval"));
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return Json(Url.Action("Create", "DamagePayeeApproval"));

                    }
                }
                else
                {
                    return Json(Url.Action("Create", "DamagePayeeApproval"));
                }

            }
            else
            {
                return Json(Url.Action("Create", "DamagePayeeApproval"));
            }
            //var result = false;
            //var Data = await _watchandwardService.FetchSingleResult(id);
            //Data.LocalityList = await _watchandwardService.GetAllLocality();
            //Data.KhasraList = await _watchandwardService.GetAllKhasra();
            //Data.PrimaryListNoList = await _watchandwardService.GetAllPrimaryList();

            //#region Approval Proccess At Further level start Added by Renu 27 Nov 2020
            //var DataFlow = await DataAsync();
            //for (int i = 0; i < DataFlow.Count; i++)
            //{
            //    if (!DataFlow[i].parameterSkip)
            //    {
            //        if (Convert.ToInt32(DataFlow[i].parameterName) == SiteContext.UserId)
            //        {
            //            result = true;  ///May be comment
            //            if (result)
            //            {
            //                Approvalproccess approvalproccess = new Approvalproccess();
            //                approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
            //                approvalproccess.ProccessID = Convert.ToInt32(_configuration.GetSection("workflowTemplateIdDamagePayeeRegister").Value);
            //                approvalproccess.ServiceId = watchandward.Id;
            //                approvalproccess.SendFrom = SiteContext.UserId;
            //                approvalproccess.PendingStatus = 1;
            //                approvalproccess.Remarks = watchandward.ApprovalRemarks; ///May be comment
            //                approvalproccess.Status = Convert.ToInt32(watchandward.ApprovalStatus);
            //                if (i == DataFlow.Count - 1)
            //                    approvalproccess.SendTo = null;
            //                else
            //                {
            //                    approvalproccess.SendTo = Convert.ToInt32(DataFlow[i + 1].parameterName);
            //                }
            //                // if (i != DataFlow.Count - 1)  ///May be Uncomment
            //                result = await _approvalproccessService.Create(approvalproccess, SiteContext.UserId); //Create a row in approvalproccess Table

            //                if (result)
            //                {
            //                    if (i == DataFlow.Count - 1)
            //                    {
            //                        watchandward.ApprovedStatus = 1;
            //                        watchandward.PendingAt = 0;
            //                    }
            //                    else
            //                    {
            //                        watchandward.ApprovedStatus = 0;
            //                        watchandward.PendingAt = Convert.ToInt32(DataFlow[i + 1].parameterName);
            //                    }
            //                    result = await _watchandwardService.UpdateBeforeApproval(id, watchandward);
            //                }
            //            }
            //            break;
            //        }

            //    }
            //}

            //#endregion

            ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
            return View("Index");
        }
        public async Task<PartialViewResult> HistoryDetails(int id)
        {
            var Data = await _proccessWorkflowService.GetHistoryDetails(Convert.ToInt32(_configuration.GetSection("workflowTemplateIdDamagePayeeRegister").Value), id);

            return PartialView("_HistoryDetails", Data);
        }

        #region Damage Payee Register  Details
        public async Task<PartialViewResult> DamagePayeeRegisterView(int id)
        {
            var Data = await _damagepayeeregisterService.FetchSingleResult(id);
            await BindDropDown(Data);

            return PartialView("_DamagePayeeRegister", Data);
        }
        public async Task<JsonResult> GetDetailspersonelinfotemp(int? Id)
        {
            Id = Id ?? 0;
            var data = await _selfAssessmentDamageService.GetPersonalInfoTemp(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                x.Id,
                x.Name,
                x.FatherName,
                x.Gender,
                x.Address,
                x.MobileNo,
                x.EmailId,
                x.AadharNoFilePath,
                x.PanNoFilePath,
                x.PhotographPath,
                x.SignaturePath,
                x.AadharNo,
                x.PanNo
            }));
        }
        public async Task<JsonResult> GetDetailsAllottetypetemp(int? Id)
        {
            Id = Id ?? 0;
            var data = await _selfAssessmentDamageService.GetAllottetypeTemp(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                x.Id,
                x.Name,
                x.FatherName,
                Date = Convert.ToDateTime(x.Date).ToString("yyyy-MM-dd"),
                x.AtsgpadocumentPath
            }));
        }
        public async Task<JsonResult> GetDetailspaymenthistorytemp(int? Id)
        {
            Id = Id ?? 0;
            var data = await _selfAssessmentDamageService.GetPaymentHistoryTemp(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                x.Id,
                x.Name,
                x.RecieptNo,
                x.PaymentMode,
                PaymentDate = Convert.ToDateTime(x.PaymentDate).ToString("yyyy-MM-dd"),
                x.Amount,
                x.RecieptDocumentPath
            }));
        }

        public async Task<FileResult> ViewPersonelInfoAadharFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfotemp Data = await _selfAssessmentDamageService.GetPersonelInfoFilePath(Id);
            string path = Data.AadharNoFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewPersonelInfoPanFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfotemp Data = await _selfAssessmentDamageService.GetPersonelInfoFilePath(Id);
            string path = Data.PanNoFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewPersonelInfoPhotoFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfotemp Data = await _selfAssessmentDamageService.GetPersonelInfoFilePath(Id);
            string path = Data.PhotographPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewPersonelInfoSignautreFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfotemp Data = await _selfAssessmentDamageService.GetPersonelInfoFilePath(Id);
            string path = Data.SignaturePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewATSGPAFile(int Id)
        {
            FileHelper file = new FileHelper();
            Allottetypetemp Data = await _selfAssessmentDamageService.GetAllotteTypeSingleResult(Id);
            string path = Data.AtsgpadocumentPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewRecieptFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepaymenthistorytemp Data = await _selfAssessmentDamageService.GetPaymentHistorySingleResult(Id);
            string path = Data.RecieptDocumentPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        //******************  download files **************************
        public async Task<IActionResult> DownloadPropertyPhoto(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregistertemp Data = await _selfAssessmentDamageService.FetchSingleResult(Id);
            string path = Data.PropertyPhotoPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<IActionResult> DownloadShowCauseNotice(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregistertemp Data = await _selfAssessmentDamageService.FetchSingleResult(Id);
            string path = Data.ShowCauseNoticePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<IActionResult> DownloadFgform(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregistertemp Data = await _selfAssessmentDamageService.FetchSingleResult(Id);
            string path = Data.FgformPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<IActionResult> DownloadBillfile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregistertemp Data = await _selfAssessmentDamageService.FetchSingleResult(Id);
            string path = Data.DocumentForFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        #endregion

        #region Fetch workflow data for approval prrocess Added by Renu 28 Dec 2020
        private async Task<List<TemplateStructure>> DataAsync()
        {
            var Data = await _workflowtemplateService.FetchSingleResult(1);
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
                }

            }
            return Json(DataFlow);
        }
        #endregion

        #region Fetch ProccessWorkflow Data Added by Renu 28 Dec 2020
        private int ProccessWorkflowData()
        {
            var Count = _proccessWorkflowService.FetchCountResultForProccessWorkflow(1);
            return Count;
        }
        private async Task<bool> CheckThisisUser()
        {
            bool Result = false;
            var Count = ProccessWorkflowData();
            if (Count == 0)
            {
                var DataFlow = await DataAsync();
                for (int i = 0; i < DataFlow.Count; i++)
                {
                    if (!DataFlow[i].parameterSkip)
                    {
                        if (DataFlow[i].parameterValue == "Role" && Convert.ToInt32(DataFlow[i].parameterName) == SiteContext.RoleId)
                        {
                            Result = true;
                            break;
                        }
                        else if (DataFlow[i].parameterValue == "User" && Convert.ToInt32(DataFlow[i].parameterName) == SiteContext.UserId)
                        {
                            Result = true;
                            break;
                        }
                        else
                        {
                            Result = false;
                            break;
                        }

                    }
                }
            }
            else
            {
                var DataFlow = await DataAsync();
                for (int i = 1; i < DataFlow.Count; i++)
                {
                    if(i > Count && i < DataFlow.Count)
                    {

                        if (!DataFlow[i].parameterSkip)
                        {
                            if (DataFlow[i].parameterValue == "Role" && Convert.ToInt32(DataFlow[i].parameterName) == SiteContext.RoleId)
                            {
                                Result = true;
                                break;
                            }
                            else if (DataFlow[i].parameterValue == "User" && Convert.ToInt32(DataFlow[i].parameterName) == SiteContext.UserId)
                            {
                                Result = true;
                                break;
                            }
                            else
                            {
                                Result = false;
                                break;
                            }

                        }
                    }
                }

            }
            return Result;
        }

        #endregion
    }
}
