using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using EncroachmentDemolition.Models;
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
using System.Drawing;
using System.Drawing.Imaging;
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




        [AuthorizeContext(ViewAction.View)]
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
                    string targetPhotoPathLayout = _configuration.GetSection("FilePaths:OnlineComplaint:Photo").Value.ToString();
                    // targetReportfilePathLayout = _configuration.GetSection("FilePaths:WatchAndWard:ReportFile").Value.ToString();
                    FileHelper file = new FileHelper();
                    if (onlinecomplaint.Photo != null)
                    {
                        string PhotoPath = file.SaveFile(targetPhotoPathLayout, onlinecomplaint.Photo);
                        GetLattLongDetails(PhotoPath, onlinecomplaint.Lattitude, onlinecomplaint.Longitude);
                        var LattitudeValue = TempData["LattitudeValue"] as string;
                        onlinecomplaint.Lattitude = LattitudeValue;
                        var LongitudeValue = TempData["LongitudeValue"] as string;
                        onlinecomplaint.Longitude = LongitudeValue;
                        // var lattlongurlvalue = TempData["url"] as string;
                    }


                    var result = await _onlinecomplaintService.Create(onlinecomplaint);


                    var DataFlow = await dataAsync();
                    for (int i = 0; i < DataFlow.Count; i++)
                    {
                        if (!DataFlow[i].parameterSkip)
                        {
                            onlinecomplaint.ApprovedStatus = 0;
                            onlinecomplaint.PendingAt = Convert.ToInt32(DataFlow[i].parameterName);
                            result = await _onlinecomplaintService.UpdateBeforeApproval(onlinecomplaint.Id, onlinecomplaint);  //Update Table details 
                            if (result)
                            {
                                Approvalproccess approvalproccess = new Approvalproccess();
                                approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                                approvalproccess.ProccessID = Convert.ToInt32(_configuration.GetSection("workflowPreccessOnlineComplaintId").Value);
                                approvalproccess.ServiceId = onlinecomplaint.Id;
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


                    if (result == true)
                    {
                        string DisplayName = onlinecomplaint.Name.ToString();
                        string EmailID = onlinecomplaint.Email.ToString();

                        string Action = "Dear Requester, <br> Your Request for <b>" + onlinecomplaint.ComplaintType.Name + "</b> has been successfully submitted.Please note your reference No for future reference.<br> Your Ref. number is : <b>" + onlinecomplaint.ReferenceNo + "</b> <br><br><br> Regards,<br>DDA";
                        String Mobile = onlinecomplaint.Contact;

                        #region Mail Generation Added By Renu
                        MailSMSHelper mailG = new MailSMSHelper();

                        #region HTML Body Generation
                        string strBodyMsg = Action;
                        #endregion

                        string strMailSubject = "DDA Alert-Your Complaint has been successfully submitted.";
                        string strMailCC = "", strMailBCC = "", strAttachPath = "";
                        var sendMailResult = mailG.SendMailWithAttachment(strMailSubject, strBodyMsg, EmailID, strMailCC, strMailBCC, strAttachPath);
                        #endregion

                        mailG.SendSMS(Action, Mobile);
                        if (sendMailResult)
                            TempData["Message"] = Alert.Show(Messages.AddRecordSuccess + " Your Reference No is  " + onlinecomplaint.ReferenceNo + " and These Details send on your Registered email and Mobile No", "", AlertType.Success);
                        else
                            TempData["Message"] = Alert.Show(Messages.AddRecordSuccess + " Your Reference No is " + onlinecomplaint.ReferenceNo + " system is unable to send the complaint details on mail.", "", AlertType.Info);
                        return Redirect("/Complaint/Create");

                       // return View(onlinecomplaint);

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


        [AuthorizeContext(ViewAction.Edit)]
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
        [AuthorizeContext(ViewAction.Edit)]
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



        [AuthorizeContext(ViewAction.Delete)]
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


        [AuthorizeContext(ViewAction.View)]
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


        [HttpGet]
        public void GetLattLongDetails(string path, string Latt, string Long)
        {
            double? latitdue = null;
            double? longitude = null;
            string url = null;
            if (path != null)
            {
                Bitmap bmp = new Bitmap(path);
                foreach (PropertyItem propItem in bmp.PropertyItems)
                {
                    switch (propItem.Type)
                    {
                        case 5:
                            if (propItem.Id == 2) // Latitude Array
                            {
                                latitdue = GetLatitudeAndLongitude(propItem);
                            }
                            if (propItem.Id == 4) //Longitude Array
                            {
                                longitude = GetLatitudeAndLongitude(propItem);
                            }
                            break;
                    }
                }

                if (!string.IsNullOrEmpty(latitdue.ToString()) && !string.IsNullOrEmpty(longitude.ToString()))
                {
                    url = $"https://www.google.com/maps/place/{latitdue},{longitude}";
                }
                else
                {
                    ViewBag.Message = Alert.Show("Uploaded Image does not contain any geo location.please enter your request location in below textbox", "", AlertType.Error);

                }
                bmp.Dispose();
            }
            TempData["LattitudeValue"] = latitdue.ToString();
            TempData["LongitudeValue"] = longitude.ToString();
            TempData["url"] = url;
        }
        private static double? GetLatitudeAndLongitude(PropertyItem propItem)
        {
            try
            {
                uint degreesNumerator = BitConverter.ToUInt32(propItem.Value, 0);
                uint degreesDenominator = BitConverter.ToUInt32(propItem.Value, 4);
                uint minutesNumerator = BitConverter.ToUInt32(propItem.Value, 8);
                uint minutesDenominator = BitConverter.ToUInt32(propItem.Value, 12);
                uint secondsNumerator = BitConverter.ToUInt32(propItem.Value, 16);
                uint secondsDenominator = BitConverter.ToUInt32(propItem.Value, 20);
                return (Convert.ToDouble(degreesNumerator) / Convert.ToDouble(degreesDenominator)) + (Convert.ToDouble(Convert.ToDouble(minutesNumerator) / Convert.ToDouble(minutesDenominator)) / 60) +
                       (Convert.ToDouble((Convert.ToDouble(secondsNumerator) / Convert.ToDouble(secondsDenominator)) / 3600));
            }
            catch (Exception)
            {

                return null;
            }
        }


    }
}