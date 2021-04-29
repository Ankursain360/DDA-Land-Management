using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;
using Microsoft.Extensions.Configuration;
using Utility.Helper;
using Dto.Common;
using EncroachmentDemolition.Filters;



using Core.Enum;


namespace EncroachmentDemolition.Controllers
{
    public class ComplaintController : BaseController
    {
        private readonly IOnlinecomplaintService _onlinecomplaintService;
        public IConfiguration _configuration;
        string targetPhotoPathLayout = string.Empty;
        string targetReportfilePathLayout = string.Empty;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;

        public ComplaintController(IOnlinecomplaintService onlinecomplaintService, IApprovalProccessService approvalproccessService,
            IWorkflowTemplateService workflowtemplateService, IConfiguration configuration)
        {
            _workflowtemplateService = workflowtemplateService;
            _onlinecomplaintService = onlinecomplaintService;
            _configuration = configuration;
            _approvalproccessService = approvalproccessService;
        }





        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] OnlinecomplaintSearchDto model)
        {
            var result = await _onlinecomplaintService.GetPagedOnlinecomplaint(model);

            return PartialView("_List", result);
        }



        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            ViewBag.Message = TempData["Message"] as string;
            Onlinecomplaint onlinecomplaint = new Onlinecomplaint();
            onlinecomplaint.IsActive = 1;
            onlinecomplaint.ComplaintList = await _onlinecomplaintService.GetAllComplaintType();
            onlinecomplaint.LocationList = await _onlinecomplaintService.GetAllLocation();
            return View(onlinecomplaint);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Onlinecomplaint onlinecomplaint)
        {
            try
            {
                var finalString = (DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond).ToUpper();
                onlinecomplaint.ReferenceNo = "TRN" + finalString;
                onlinecomplaint.ComplaintList = await _onlinecomplaintService.GetAllComplaintType();
                onlinecomplaint.LocationList = await _onlinecomplaintService.GetAllLocation();

                if (ModelState.IsValid)
                {
                    targetPhotoPathLayout = _configuration.GetSection("FilePaths:OnlineComplaint:Photo").Value.ToString();
                   
                    FileHelper file = new FileHelper();
                    if (onlinecomplaint.Photo != null)
                    {
                        onlinecomplaint.PhotoPath = file.SaveFile(targetPhotoPathLayout, onlinecomplaint.Photo);
                   
                    }


                    var result = await _onlinecomplaintService.Create(onlinecomplaint);


                    var DataFlow = await dataAsync();
                    for (int i = 0; i < DataFlow.Count; i++)
                    {
                        if (!DataFlow[i].parameterSkip)
                        {
                            onlinecomplaint.ApprovedStatus = 0;
                         //   onlinecomplaint.PendingAt = (DataFlow[i].parameterName);
                            result = await _onlinecomplaintService.UpdateBeforeApproval(onlinecomplaint.Id, onlinecomplaint);  //Update Table details 
                            if (result)
                            {
                                Approvalproccess approvalproccess = new Approvalproccess();
                                approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                              //  approvalproccess.ProccessID = Convert.ToInt32(_configuration.GetSection("workflowPreccessOnlineComplaintId").Value);
                                approvalproccess.ServiceId = onlinecomplaint.Id;
                            //    approvalproccess.SendFrom = SiteContext.UserId;
                             //   approvalproccess.SendTo = Convert.ToInt32(DataFlow[i].parameterName);
                                approvalproccess.PendingStatus = 1;   //1
                                approvalproccess.Status = null;   //1
                                approvalproccess.Remarks = "Record Added and Send for Approval";///May be Uncomment
                                result = await _approvalproccessService.Create(approvalproccess, SiteContext.UserId); //Create a row in approvalproccess Table
                            }

                            break;
                        }
                    }


                    if (result == true)
                    {
                        string DisplayName = onlinecomplaint.Name.ToString();
                        string EmailID = onlinecomplaint.Email.ToString();

                        string Action = "Dear Requester, <br> Your Request for <b>" + onlinecomplaint.ComplaintType.Name + "</b> has been successfully submitted.Please note your reference No for future reference.<br> Your Ref. number is : <b>" + onlinecomplaint.ReferenceNo + "</b> <br><br><br> Regards,<br>DDA";
                        String Mobile = onlinecomplaint.Contact;
                        SendMailDto mail = new SendMailDto();
                        SendSMSDto SMS = new SendSMSDto();
                        SMS.GenerateSendSMS(Action, Mobile);
                        try
                        {
                            mail.GenerateMailFormatForComplaint(DisplayName, EmailID, Action);


                            TempData["Message"] = Alert.Show(Messages.AddRecordSuccess + " Your Reference No is  " + onlinecomplaint.ReferenceNo, "", AlertType.Success);

                            return Redirect("/Complaint/Create");
                        }
                        catch (Exception ex)
                        {

                            TempData["Message"] = Alert.Show(Messages.AddRecordSuccess + " Your Reference No is " + onlinecomplaint.ReferenceNo + " system is unable to send the complaint details on mail.", "", AlertType.Success);
                            return Redirect("/Complaint/Create");

                        }

                        return View(onlinecomplaint);

                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(onlinecomplaint);
                    }
                }
                else
                {
                    return View(onlinecomplaint);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(onlinecomplaint);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _onlinecomplaintService.FetchSingleResult(id);
            Data.ComplaintList = await _onlinecomplaintService.GetAllComplaintType();
            Data.LocationList = await _onlinecomplaintService.GetAllLocation();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Onlinecomplaint onlinecomplaint)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _onlinecomplaintService.Update(id, onlinecomplaint);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _onlinecomplaintService.GetAllOnlinecomplaint();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(onlinecomplaint);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(onlinecomplaint);
                }
            }
            else
            {
                return View(onlinecomplaint);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _onlinecomplaintService.Delete(id);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            var list = await _onlinecomplaintService.GetAllOnlinecomplaint();
            return View("Index", list);
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _onlinecomplaintService.FetchSingleResult(id);
            Data.ComplaintList = await _onlinecomplaintService.GetAllComplaintType();
            Data.LocationList = await _onlinecomplaintService.GetAllLocation();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        private async Task<List<TemplateStructure>> dataAsync()
        {
            var Data = await _workflowtemplateService.FetchSingleResult(18);
            var template = Data.Template;
            List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
            return ObjList;
        }

    }
}