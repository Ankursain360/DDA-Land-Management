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
using System.Text.Json;

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
            var IsUser = await CheckThisisUser();
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
        public async Task<IActionResult> Create(int id, Damagepayeeregistertemp damagepayeeregistertemp)
        {
            var result = false;

            TransactionTemplateStructure obj = new TransactionTemplateStructure();
            obj.TaskRequestId = SiteContext.UserId;
            obj.ActionByUserId = SiteContext.UserId;
            obj.Remarks = damagepayeeregistertemp.ApprovalRemarks;
            obj.Status = damagepayeeregistertemp.ApprovalStatus;
            obj.Level = 1;
            string JsonTransactionTemplateData = JsonSerializer.Serialize(obj);

            var Count = ProccessWorkflowData();

            #region Approval Proccess At Further level start Added by Renu 28 Dec 2020
            var DataFlow = await DataAsync();
            for (int i = 0; i < DataFlow.Count; i++)
            {
                if (!DataFlow[i].parameterSkip)
                {
                    if ((DataFlow[i].parameterValue == "Role" && Convert.ToInt32(DataFlow[i].parameterName) == SiteContext.RoleId) || (DataFlow[i].parameterValue == "User" && Convert.ToInt32(DataFlow[i].parameterName) == SiteContext.UserId))
                    {
                        result = true;  ///May be comment
                        if (result)
                        {
                            Processworkflow proccess = new Processworkflow();
                            proccess.TransactionTemplate = JsonTransactionTemplateData;
                            proccess.WorkflowTemplateId = Convert.ToInt32(_configuration.GetSection("workflowTemplateIdDamagePayeeRegister").Value);
                            proccess.ActionId = SiteContext.UserId;
                            proccess.CreatedBy = SiteContext.UserId;
                          //  result = await _proccessWorkflowService.Create(proccess); //Create a row in ProccessWorkflow Table

                            if (result)
                            {
                                if (i == DataFlow.Count - 1)// Last Level 
                                {
                                    damagepayeeregistertemp.ApprovedStatus = 1;
                                   // result = await _damagepayeeregisterService.UpdateBeforeApproval(id, damagepayeeregistertemp);
                                    if (result)
                                    {

                                        if (damagepayeeregistertemp != null)
                                        {
                                            Damagepayeeregister model = new Damagepayeeregister();
                                            damagepayeeregistertemp = await _damagepayeeregisterService.FetchSingleResult(damagepayeeregistertemp.Id);
                                            result = await _damagepayeeregisterService.CreateApprovedDamagepayeeRegister(damagepayeeregistertemp, model);

                                            List<Damagepayeepersonelinfo> damagepayeepersonelinfo = new List<Damagepayeepersonelinfo>();
                                            var data = damagepayeeregistertemp.Damagepayeepersonelinfotemp.ToList();
                                            for (int j = 0; j < data.Count; j++)
                                            {
                                                damagepayeepersonelinfo.Add(new Damagepayeepersonelinfo
                                                {
                                                    Name = data[i].Name,
                                                    FatherName = data[i].FatherName,
                                                    Gender = data[i].Gender,
                                                    Address = data[i].Address,
                                                    MobileNo = data[i].MobileNo,
                                                    EmailId = data[i].EmailId,
                                                    DamagePayeeRegisterId = model.Id,
                                                    AadharNo = data[i].AadharNo,
                                                    PanNo = data[i].PanNo,
                                                    AadharNoFilePath = data[i].AadharNoFilePath,
                                                    PanNoFilePath = data[i].PanNoFilePath,
                                                    PhotographPath = data[i].PhotographPath,
                                                    SignaturePath = data[i].SignaturePath,
                                                    CreatedBy = SiteContext.UserId
                                                });
                                            }
                                            result = await _damagepayeeregisterService.SavePersonelInfo(damagepayeepersonelinfo);

                                            List<Allottetype> allottetype = new List<Allottetype>();
                                            var allottetmp = damagepayeeregistertemp.Allottetypetemp.ToList();
                                            for (int k = 0; k < allottetmp.Count; k++)
                                            {
                                                allottetype.Add(new Allottetype
                                                {
                                                    Name = allottetmp[k].Name,
                                                    FatherName = allottetmp[k].FatherName,
                                                    Date = allottetmp[k].Date,
                                                    DamagePayeeRegisterId = model.Id,
                                                    AtsgpadocumentPath = allottetmp[k].AtsgpadocumentPath,
                                                    CreatedBy = SiteContext.UserId
                                                });
                                            }
                                            result = await _damagepayeeregisterService.SaveAllotteType(allottetype);

                                            List<Damagepaymenthistory> damagepaymenthistory = new List<Damagepaymenthistory>();
                                            var historytmp = damagepayeeregistertemp.Damagepaymenthistorytemp.ToList();
                                            for (int m = 0; m < damagepayeeregistertemp.Name.Count; m++)
                                            {
                                                damagepaymenthistory.Add(new Damagepaymenthistory
                                                {
                                                    Name = historytmp[m].Name,
                                                    RecieptNo = historytmp[m].RecieptNo,
                                                    PaymentMode = historytmp[m].PaymentMode,
                                                    PaymentDate = historytmp[m].PaymentDate,
                                                    Amount = historytmp[m].Amount,
                                                    RecieptDocumentPath = historytmp[m].RecieptDocumentPath,
                                                    DamagePayeeRegisterId = model.Id,
                                                    CreatedBy = SiteContext.UserId
                                                });
                                            }

                                            result = await _damagepayeeregisterService.SavePaymentHistory(damagepaymenthistory);
                                        }
                                    }
                                }
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
                if (DataFlow[i].parameterValue == "Role" && Convert.ToInt32(DataFlow[i].parameterName) == SiteContext.RoleId)
                {
                    var dropdown = DataFlow[i].parameterAction;
                    return Json(dropdown);
                }
                else if (DataFlow[i].parameterValue == "User" && Convert.ToInt32(DataFlow[i].parameterName) == SiteContext.UserId)
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
                for (int i = 0; i < DataFlow.Count; i++)
                {
                    if (i > Count - 1 && i < DataFlow.Count)
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
