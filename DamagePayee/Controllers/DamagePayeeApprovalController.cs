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
using DamagePayee.Filters;
using Core.Enum;
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
        private readonly IApprovalProccessService _approvalproccessService;
        public DamagePayeeApprovalController(IDamagePayeeApprovalService damagePayeeApprovalService,
            IDamagepayeeregisterService damagepayeeregisterService, IConfiguration configuration,
            IWorkflowTemplateService workflowtemplateService, ISelfAssessmentDamageService selfAssessmentDamageService,
            IProccessWorkflowService proccessWorkflowService, IApprovalProccessService approvalproccessService)
        {
            _configuration = configuration;
            _damagePayeeApprovalService = damagePayeeApprovalService;
            _damagepayeeregisterService = damagepayeeregisterService;
            _workflowtemplateService = workflowtemplateService;
            _selfAssessmentDamageService = selfAssessmentDamageService;
            _proccessWorkflowService = proccessWorkflowService;
            _approvalproccessService = approvalproccessService;
        }


        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }
       
        async Task BindDropDown(Damagepayeeregister damagepayeeregistertemp)
        {
            damagepayeeregistertemp.LocalityList = await _damagepayeeregisterService.GetLocalityList();
            damagepayeeregistertemp.DistrictList = await _damagepayeeregisterService.GetDistrictList();
        }

        [AuthorizeContext(ViewAction.Add)]
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

       
        private async Task<bool> UpdateTaskRequestedByTable(int id, Damagepayeeregister damagepayeeregistertemp)
        {
            damagepayeeregistertemp.ApprovedStatus = 1;
            var result = await _damagepayeeregisterService.UpdateBeforeApproval(id, damagepayeeregistertemp);
            return result;
        }
        private async Task<bool> CreateAprrovedRecordsinActualTable(Damagepayeeregister damagepayeeregistertemp)
        {
            var result = false;
            if (damagepayeeregistertemp != null)
            {
                Damagepayeeregister model = new Damagepayeeregister();
                damagepayeeregistertemp = await _damagepayeeregisterService.FetchSingleResult(damagepayeeregistertemp.Id);
                result = await _damagepayeeregisterService.CreateApprovedDamagepayeeRegister(damagepayeeregistertemp, model);

                List<Damagepayeepersonelinfo> damagepayeepersonelinfo = new List<Damagepayeepersonelinfo>();
                var data = damagepayeeregistertemp.Damagepayeepersonelinfo.ToList();
                for (int j = 0; j < data.Count; j++)
                {
                    damagepayeepersonelinfo.Add(new Damagepayeepersonelinfo
                    {
                        Name = data[j].Name,
                        FatherName = data[j].FatherName,
                        Gender = data[j].Gender,
                        Address = data[j].Address,
                        MobileNo = data[j].MobileNo,
                        EmailId = data[j].EmailId,
                        AadharNo = data[j].AadharNo,
                        PanNo = data[j].PanNo,
                        AadharNoFilePath = data[j].AadharNoFilePath,
                        PanNoFilePath = data[j].PanNoFilePath,
                        PhotographPath = data[j].PhotographPath,
                        SignaturePath = data[j].SignaturePath,
                        DamagePayeeRegisterTempId = model.Id,
                        CreatedBy = SiteContext.UserId
                    });
                }
                result = await _damagepayeeregisterService.SavePersonelInfo(damagepayeepersonelinfo);

                List<Allottetype> allottetype = new List<Allottetype>();
                var allottetmp = damagepayeeregistertemp.Allottetype.ToList();
                for (int k = 0; k < allottetmp.Count; k++)
                {
                    allottetype.Add(new Allottetype
                    {
                        Name = allottetmp[k].Name,
                        FatherName = allottetmp[k].FatherName,
                        Date = allottetmp[k].Date,
                        AtsgpadocumentPath = allottetmp[k].AtsgpadocumentPath,
                        DamagePayeeRegisterTempId = model.Id,
                        CreatedBy = SiteContext.UserId
                    });
                }
                result = await _damagepayeeregisterService.SaveAllotteType(allottetype);

                List<Damagepaymenthistory> damagepaymenthistory = new List<Damagepaymenthistory>();
                var historytmp = damagepayeeregistertemp.Damagepaymenthistory.ToList();
                for (int m = 0; m < historytmp.Count; m++)
                {
                    damagepaymenthistory.Add(new Damagepaymenthistory
                    {
                        Name = historytmp[m].Name,
                        RecieptNo = historytmp[m].RecieptNo,
                        PaymentMode = historytmp[m].PaymentMode,
                        PaymentDate = historytmp[m].PaymentDate,
                        Amount = historytmp[m].Amount,
                        RecieptDocumentPath = historytmp[m].RecieptDocumentPath,
                        DamagePayeeRegisterTempId = model.Id,
                        CreatedBy = SiteContext.UserId
                    });
                }

                result = await _damagepayeeregisterService.SavePaymentHistory(damagepaymenthistory);
            }
            return result;
        }
        public async Task<PartialViewResult> HistoryDetails(int id)
        {
            var Data = await _proccessWorkflowService.GetHistoryDetails((_configuration.GetSection("workflowPreccessIdDamagePayee").Value), id);

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
            Damagepayeepersonelinfo Data = await _selfAssessmentDamageService.GetPersonelInfoFilePath(Id);
            string path = Data.AadharNoFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewPersonelInfoPanFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _selfAssessmentDamageService.GetPersonelInfoFilePath(Id);
            string path = Data.PanNoFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewPersonelInfoPhotoFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _selfAssessmentDamageService.GetPersonelInfoFilePath(Id);
            string path = Data.PhotographPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewPersonelInfoSignautreFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _selfAssessmentDamageService.GetPersonelInfoFilePath(Id);
            string path = Data.SignaturePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewATSGPAFile(int Id)
        {
            FileHelper file = new FileHelper();
            Allottetype Data = await _selfAssessmentDamageService.GetAllotteTypeSingleResult(Id);
            string path = Data.AtsgpadocumentPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewRecieptFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepaymenthistory Data = await _selfAssessmentDamageService.GetPaymentHistorySingleResult(Id);
            string path = Data.RecieptDocumentPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        //******************  download files **************************
        public async Task<IActionResult> DownloadPropertyPhoto(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _selfAssessmentDamageService.FetchSingleResult(Id);
            string path = Data.PropertyPhotoPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<IActionResult> DownloadShowCauseNotice(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _selfAssessmentDamageService.FetchSingleResult(Id);
            string path = Data.ShowCauseNoticePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<IActionResult> DownloadFgform(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _selfAssessmentDamageService.FetchSingleResult(Id);
            string path = Data.FgformPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<IActionResult> DownloadBillfile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _selfAssessmentDamageService.FetchSingleResult(Id);
            string path = Data.DocumentForFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        #endregion

       
      

        #region Fetch ProccessWorkflow Data Added by Renu 28 Dec 2020
        private int ProccessWorkflowData()
        {
            var Count = _proccessWorkflowService.FetchCountResultForProccessWorkflow(Convert.ToInt32(_configuration.GetSection("workflowPreccessIdDamagePayee").Value));
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
                for (int i = Count; i < DataFlow.Count; i++)
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
            return Result;
        }

        #endregion





        #region Renu Approval Proccess
        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id, Damagepayeeregister damagepayeeregister)
        {
            var result = false;
            var Data = await _damagepayeeregisterService.FetchSingleResult(id);
            await BindDropDown(Data);
            var value = await _selfAssessmentDamageService.GetRebateValue();
            if (value == null)
                ViewBag.RebateValue = 0;
            else
                ViewBag.RebateValue = Math.Round((value.RebatePercentage), 2);

            //#region Approval Proccess At Further level start Added by Renu 23 jan 2021
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
            //                approvalproccess.ProccessID = Convert.ToInt32(_configuration.GetSection("workflowPreccessIdDamagePayee").Value);
            //                approvalproccess.ServiceId = damagepayeeregister.Id;
            //                approvalproccess.SendFrom = SiteContext.UserId;
            //                approvalproccess.PendingStatus = 1;
            //                approvalproccess.Remarks = damagepayeeregister.ApprovalRemarks; ///May be comment
            //                approvalproccess.Status = Convert.ToInt32(damagepayeeregister.ApprovalStatus);
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
            //                        damagepayeeregister.ApprovedStatus = 1;
            //                        damagepayeeregister.PendingAt = 0;
            //                    }
            //                    else
            //                    {
            //                        damagepayeeregister.ApprovedStatus = 0;
            //                        damagepayeeregister.PendingAt = Convert.ToInt32(DataFlow[i + 1].parameterName);
            //                    }
            //                    result = await _damagepayeeregisterService.UpdateBeforeApproval(id, damagepayeeregister);
            //                }
            //            }
            //            break;
            //        }

            //    }
            //}

            //#endregion
            var Msgddl = damagepayeeregister.ApprovalStatus;
            if (Msgddl == "3")
            {
                ViewBag.Message = Alert.Show(Messages.Approvedsuccesfuly, "", AlertType.Success);
            }
            else if (Msgddl == "2")
            {
                ViewBag.Message = Alert.Show(Messages.Approvedsuccesfuly, "", AlertType.Success);
            }
            else if (Msgddl == "1")
            {

                ViewBag.Message = Alert.Show(Messages.Forwardsuccesfuly, "", AlertType.Success);
            }
            return View("Index");
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DamagepayeeRegisterApprovalDto model)
        {
            var result = await _damagePayeeApprovalService.GetPagedDamageForApproval(model, SiteContext.UserId);
            ViewBag.IsApproved = model.StatusId;
            return PartialView("_List", result);
        }
        #endregion

        #region Fetch workflow data for approval prrocess Added by Renu 26 Nov 2020
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
