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
using NewLandAcquisition.Filters;
using Microsoft.Extensions.Configuration;
using System.IO;
using Core.Enum;
using Utility.Helper;
using Dto.Common;
using Dto.Master;
using Service.IApplicationService;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Configuration;

namespace NewLandAcquisition.Controllers
{
    public class RequestController : BaseController
    {
        private readonly IRequestService _requestService;
        public IConfiguration _configuration;
        string documentPhotoPathLayout = string.Empty;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;
        private readonly IUserProfileService _userProfileService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserNotificationService _userNotificationService;
        public object JsonRequestBehavior { get; private set; }
        string UploadFilePath = "";

        public RequestController(IRequestService requestService, IConfiguration configuration, 
            IApprovalProccessService approvalproccessService, IWorkflowTemplateService workflowtemplateService,
            IUserProfileService userProfileService, IHostingEnvironment hostingEnvironment,
            IUserNotificationService userNotificationService)
        {
            _workflowtemplateService = workflowtemplateService;
            _requestService = requestService;
            _configuration = configuration;
            _approvalproccessService = approvalproccessService;
            _userProfileService = userProfileService;
            _hostingEnvironment = hostingEnvironment;
            _userNotificationService = userNotificationService;
            documentPhotoPathLayout = _configuration.GetSection("FilePaths:RequestPhoto:Photo").Value.ToString();
        }

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] RequestSearchDto model)
        {
            var result = await _requestService.GetPagedRequest(model);
            var approvalstatusresult = await _approvalproccessService.GetStatusIdFromStatusCode((int)ApprovalActionStatus.Approved);
            ViewBag.ApprovedStatus = approvalstatusresult == null ? 0 : approvalstatusresult.Id;
            return PartialView("_List", result);
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Request request = new Request();
            request.IsActive = 1;
            return View(request);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Request request)
        {
            try
            {
                bool IsValidpdf = CheckMimeType(request);            
                request.IsActive = 1;
                var finalString = (DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond).ToUpper();
                request.ReferenceNo = "TRN" + finalString;

                documentPhotoPathLayout = _configuration.GetSection("FilePaths:RequestPhoto:Photo").Value.ToString();
                if (ModelState.IsValid)
                {
                    if (IsValidpdf == true)
                    {

                        FileHelper file = new FileHelper();
                        if (request.RequestPhotos != null)
                        {
                            request.LayoutPlan = file.SaveFile1(documentPhotoPathLayout, request.RequestPhotos);
                        }


                        #region Approval Proccess At 1st level Check Initial Before Creating Record  Added by Renu 04 May 2021

                        Approvalproccess approvalproccess = new Approvalproccess();
                        var DataFlow = await dataAsync();
                        for (int i = 0; i < DataFlow.Count; i++)
                        {
                            if (!DataFlow[i].parameterSkip)
                            {
                                if (DataFlow[i].parameterConditional == (_configuration.GetSection("ApprovalZoneWise").Value))
                                {
                                    if (SiteContext.ZoneId == null)
                                    {
                                        ViewBag.Message = Alert.Show("Your Zone is not available , Without zone application cannot be processed further, Please contact system administrator", "", AlertType.Warning);
                                        return View(request);
                                    }

                                    request.ApprovalZoneId = SiteContext.ZoneId;
                                }
                                if (DataFlow[i].parameterValue == (_configuration.GetSection("ApprovalRoleType").Value))
                                {
                                    for (int j = 0; j < DataFlow[i].parameterName.Count; j++)
                                    {
                                        List<UserProfileDto> UserListRoleBasis = null;
                                        if (DataFlow[i].parameterConditional == (_configuration.GetSection("ApprovalZoneWise").Value))
                                            UserListRoleBasis = await _userProfileService.GetUserOnRoleZoneBasis(Convert.ToInt32(DataFlow[i].parameterName[j]), SiteContext.ZoneId ?? 0);
                                        else
                                            UserListRoleBasis = await _userProfileService.GetUserOnRoleBasis(Convert.ToInt32(DataFlow[i].parameterName[j]));

                                        StringBuilder multouserszonewise = new StringBuilder();
                                        int col = 0;
                                        if (UserListRoleBasis != null)
                                        {
                                            for (int h = 0; h < UserListRoleBasis.Count; h++)
                                            {
                                                if (col > 0)
                                                    multouserszonewise.Append(",");
                                                multouserszonewise.Append(UserListRoleBasis[h].UserId);
                                                col++;
                                            }
                                            approvalproccess.SendTo = multouserszonewise.ToString();
                                        }

                                    }
                                }
                                else
                                {
                                    approvalproccess.SendTo = String.Join(",", (DataFlow[i].parameterName));
                                    if (DataFlow[i].parameterConditional == (_configuration.GetSection("ApprovalZoneWise").Value))
                                    {
                                        StringBuilder multouserszonewise = new StringBuilder();
                                        int col = 0;
                                        if (approvalproccess.SendTo != null)
                                        {
                                            string[] multiTo = approvalproccess.SendTo.Split(',');
                                            foreach (string MultiUserId in multiTo)
                                            {
                                                if (col > 0)
                                                    multouserszonewise.Append(",");
                                                var UserProfile = await _userProfileService.GetUserByIdZone(Convert.ToInt32(MultiUserId), SiteContext.ZoneId ?? 0);
                                                multouserszonewise.Append(UserProfile.UserId);
                                                col++;
                                            }
                                            approvalproccess.SendTo = multouserszonewise.ToString();
                                        }
                                    }
                                }


                                break;
                            }
                        }
                        #endregion


                        var result = await _requestService.Create(request);



                        if (result == true)
                        {
                            #region Approval Proccess At 1st level start Added by Renu 04 May 2021
                            var workflowtemplatedata = await _workflowtemplateService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowProcessGuidRequestApproval").Value));
                            var ApprovalStatus = await _approvalproccessService.GetStatusIdFromStatusCode((int)ApprovalActionStatus.Forward);

                            for (int i = 0; i < DataFlow.Count; i++)
                            {
                                if (!DataFlow[i].parameterSkip)
                                {
                                    request.ApprovedStatus = ApprovalStatus.Id;
                                    request.PendingAt = approvalproccess.SendTo;
                                    result = await _requestService.UpdateBeforeApproval(request.Id, request);  //Update Table details 
                                    if (result)
                                    {
                                        approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                                        approvalproccess.ProcessGuid = (_configuration.GetSection("workflowProcessGuidRequestApproval").Value);
                                        approvalproccess.ServiceId = request.Id;
                                        approvalproccess.SendFrom = SiteContext.UserId.ToString();
                                        approvalproccess.SendFromProfileId = SiteContext.ProfileId.ToString();
                                        #region set sendto and sendtoprofileid 
                                        StringBuilder multouserprofileid = new StringBuilder();
                                        int col = 0;
                                        if (approvalproccess.SendTo != null)
                                        {
                                            string[] multiTo = approvalproccess.SendTo.Split(',');
                                            foreach (string MultiUserId in multiTo)
                                            {
                                                if (col > 0)
                                                    multouserprofileid.Append(",");
                                                var UserProfile = await _userProfileService.GetUserById(Convert.ToInt32(MultiUserId));
                                                multouserprofileid.Append(UserProfile.Id);
                                                col++;
                                            }
                                            approvalproccess.SendToProfileId = multouserprofileid.ToString();
                                        }
                                        #endregion
                                        approvalproccess.PendingStatus = 1;   //1
                                        approvalproccess.Status = ApprovalStatus.Id;   //1
                                        approvalproccess.Level = i + 1;
                                        approvalproccess.Version = workflowtemplatedata.Version;
                                        approvalproccess.Remarks = "Record Added and Send for Approval";///May be Uncomment
                                        result = await _approvalproccessService.Create(approvalproccess, SiteContext.UserId); //Create a row in approvalproccess Table

                                        #region Insert Into usernotification table Added By Renu 18 June 2021
                                        if (result == true && approvalproccess.SendTo != null)
                                        {
                                            var notificationtemplate = await _approvalproccessService.FetchSingleNotificationTemplate(_configuration.GetSection("userNotificationGuidRequestService").Value);
                                            var user = await _userProfileService.GetUserById(SiteContext.UserId);
                                            Usernotification usernotification = new Usernotification();
                                            var replacement = notificationtemplate.Template.Replace("{proccess name}", "Identification of land").Replace("{from user}", user.User.UserName).Replace("{datetime}", DateTime.Now.ToString());
                                            usernotification.Message = replacement;
                                            usernotification.UserNotificationGuid = (_configuration.GetSection("userNotificationGuidRequestService").Value);
                                            usernotification.ProcessGuid = approvalproccess.ProcessGuid;
                                            usernotification.ServiceId = approvalproccess.ServiceId;
                                            usernotification.SendFrom = approvalproccess.SendFrom;
                                            usernotification.SendTo = approvalproccess.SendTo;
                                            result = await _userNotificationService.Create(usernotification, SiteContext.UserId);
                                        }
                                        #endregion

                                    }

                                    break;
                                }
                            }

                            #endregion

                            #region Approval Proccess  Mail Generation Added by Renu 07 May 2021
                            var sendMailResult = false;
                            var DataApprovalSatatusMsg = await _approvalproccessService.FetchSingleApprovalStatus(Convert.ToInt32(ApprovalStatus.Id));
                            if (approvalproccess.SendTo != null)
                            {
                                #region Mail Generate
                                //At successfull completion send mail and sms
                                Uri uri = new Uri("https://master.managemybusinessess.com/ApprovalProcess/Index");
                                string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "ApprovalMailDetailsContent.html");
                                string link = "<a target=\"_blank\" href=\"" + uri + "\">Click Here</a>";

                                var senderUser = await _userProfileService.GetUserById(SiteContext.UserId);
                                StringBuilder multousermailId = new StringBuilder();
                                if (approvalproccess.SendTo != null)
                                {
                                    int col = 0;
                                    string[] multiTo = approvalproccess.SendTo.Split(',');
                                    foreach (string MultiUserId in multiTo)
                                    {
                                        if (col > 0)
                                            multousermailId.Append(",");
                                        var RecevierUsers = await _userProfileService.GetUserById(Convert.ToInt32(MultiUserId));
                                        multousermailId.Append(RecevierUsers.User.Email);
                                        col++;
                                    }
                                }

                                #region Mail Generation Added By Renu

                                MailSMSHelper mailG = new MailSMSHelper();

                                #region HTML Body Generation
                                ApprovalMailBodyDto bodyDTO = new ApprovalMailBodyDto();
                                bodyDTO.ApplicationName = "Request Application";
                                bodyDTO.Status = DataApprovalSatatusMsg.SentStatusName;
                                bodyDTO.SenderName = senderUser.User.Name;
                                bodyDTO.Link = link;
                                bodyDTO.AppRefNo = request.ReferenceNo;
                                bodyDTO.SubmitDate = DateTime.Now.ToString("dd-MMM-yyyy");
                                bodyDTO.Remarks = approvalproccess.Remarks;
                                bodyDTO.path = path;
                                string strBodyMsg = mailG.PopulateBodyApprovalMailDetails(bodyDTO);
                                #endregion

                                // sendMailResult = mailG.SendMailWithAttachment(strMailSubject, strBodyMsg, multousermailId.ToString(), strMailCC, strMailBCC, strAttachPath);
                                #region Common Mail Genration
                                SentMailGenerationDto maildto = new SentMailGenerationDto();
                                maildto.strMailSubject = "Pending Request Application Approval Request Details ";
                                maildto.strMailCC = ""; maildto.strMailBCC = ""; maildto.strAttachPath = "";
                                maildto.strBodyMsg = strBodyMsg;
                                maildto.defaultPswd = (_configuration.GetSection("EmailConfiguration:defaultPswd").Value).ToString();
                                maildto.fromMail = (_configuration.GetSection("EmailConfiguration:fromMail").Value).ToString();
                                maildto.fromMailPwd = (_configuration.GetSection("EmailConfiguration:fromMailPwd").Value).ToString();
                                maildto.mailHost = (_configuration.GetSection("EmailConfiguration:mailHost").Value).ToString();
                                maildto.port = Convert.ToInt32(_configuration.GetSection("EmailConfiguration:port").Value);

                                maildto.strMailTo = multousermailId.ToString();
                                sendMailResult = mailG.SendMailWithAttachment(maildto);
                                #endregion
                                #endregion


                                #endregion
                            }
                            #endregion
                            ViewBag.Message = Alert.Show(Messages.AddAndApprovalRecordSuccess, "", AlertType.Success);
                           // var list = await _requestService.GetAllRequest();
                            return View("Index");
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            return View("Index");
                        }
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                        return View(request);

                    }
                }
                else
                {
                    return View(request);
                }
            }
            catch (Exception ex)
            {
                #region Roll Back of Transaction Added by Renu 26 April  2021 
                var deleteResult = false;
                if (request.Id != 0)
                {
                    deleteResult = await _userNotificationService.RollBackEntry((_configuration.GetSection("workflowProcessGuidRequestApproval").Value), request.Id);
                    deleteResult = await _approvalproccessService.RollBackEntry((_configuration.GetSection("workflowProcessGuidRequestApproval").Value), request.Id);
                    deleteResult = await _requestService.RollBackEntry(request.Id);
                }
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(request);
                #endregion
            }
        }

      
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _requestService.FetchSingleResult(id);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
       [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Request scheme)
        {

            bool IsValidpdf = CheckMimeType(scheme);

            documentPhotoPathLayout = _configuration.GetSection("FilePaths:RequestPhoto:Photo").Value.ToString();
            if (ModelState.IsValid)
            {
                if (IsValidpdf == true)
                {
                    FileHelper file = new FileHelper();
                    if (scheme.RequestPhotos != null)
                    {
                        scheme.LayoutPlan = file.SaveFile1(documentPhotoPathLayout, scheme.RequestPhotos);
                    }

                    try
                    {
                        var result = await _requestService.Update(id, scheme);
                        if (result == true)
                        {
                            ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                            var list = await _requestService.GetAllRequest();
                            return View("Index", list);
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            return View(scheme);
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(scheme);
                    }
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                    return View(scheme);

                }
            }
            else
            {
                return View(scheme);
            }
        }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _requestService.Delete(id);
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
            var list = await _requestService.GetAllRequest();
            return View("Index", list);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _requestService.FetchSingleResult(id);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        public async Task<FileResult> ViewLetter(int Id)
        {
            try
            {
                FileHelper file = new FileHelper();
                Request Data = await _requestService.FetchSingleResult(Id);
                string targetPhotoPathLayout = documentPhotoPathLayout + Data.LayoutPlan.Trim();
               // string path =  Data.LayoutPlan;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
            }
            catch (Exception ex)
            {

                FileHelper file = new FileHelper();
                var Data = await _requestService.FetchSingleResult(Id);
                string targetPhotoPathLayout = Data.LayoutPlan;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));

            }
        }

        public async Task<IActionResult> RequestList([FromBody] RequestSearchDto model)
        {
            var result = await _requestService.GetAllRequestList(model);
            List<RequestListDto> data = new List<RequestListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new RequestListDto()
                    {
                        Id = result[i].Id,
                        ProposalName = result[i].PproposalName,
                        Fileno = result[i].PfileNo ,
                        RequiringBody = result[i].RequiringBody,
                        Area = result[i].AreaLocality,

                        IsActive = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            TempData["file"] = memory;
            return Ok();

        }
        [HttpGet]
        public virtual ActionResult download()
        {
            byte[] data = TempData["file"] as byte[];
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        #region Fetch workflow data for approval prrocess Added by Renu 04 May 2021
        private async Task<List<TemplateStructure>> dataAsync()
        {
            var Data = await _workflowtemplateService.FetchSingleResultOnProcessGuid((_configuration.GetSection("workflowProcessGuidRequestApproval").Value));
            var template = Data.Template;
            List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
            return ObjList;
        }
        #endregion





        [HttpPost]
        public JsonResult CheckFile()
        {
            bool IsImg = true;
            string fullpath = string.Empty;
            //   string fullpath = string.Empty;
            string extension = string.Empty;
            documentPhotoPathLayout = _configuration.GetSection("FilePaths:RequestPhoto:Photo").Value.ToString();
            IFormFile files = Request.Form.Files["file"];
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;

                documentPhotoPathLayout = _configuration.GetSection("FilePaths:RequestPhoto:Photo").Value.ToString();
                string FilePath = Path.Combine(documentPhotoPathLayout, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(documentPhotoPathLayout))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(documentPhotoPathLayout);// Try to create the directory.
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
                                fullpath = _configuration.GetSection("FilePaths:RequestPhoto:Photo").Value.ToString();
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


        public bool CheckMimeType(Request request)
        {
            bool Flag = true;
            string fullpath = string.Empty;
            //   string fullpath = string.Empty;
            string extension = string.Empty;
            documentPhotoPathLayout = _configuration.GetSection("FilePaths:RequestPhoto:Photo").Value.ToString();
            IFormFile files = request.RequestPhotos;
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;

                documentPhotoPathLayout = _configuration.GetSection("FilePaths:RequestPhoto:Photo").Value.ToString();
                string FilePath = Path.Combine(documentPhotoPathLayout, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(documentPhotoPathLayout))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(documentPhotoPathLayout);// Try to create the directory.
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
                                fullpath = _configuration.GetSection("FilePaths:RequestPhoto:Photo").Value.ToString();
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
