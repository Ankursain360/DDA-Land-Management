using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Hosting;
using LeaseDetails.Filters;
using Core.Enum;
using System.Data;

namespace LeaseDetails.Controllers
{
    public class NoticeGenerationController : BaseController
    {
        public readonly INoticeGenerationService _noticeGenerationService;
        public readonly ILeaseHearingDetailsService _leaseHearingDetailsService;
        public IConfiguration _configuration;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;
        private readonly IRequestforproceedingService _undersection4PlotService;

        string targetPathNotice = "";
        public NoticeGenerationController(INoticeGenerationService noticeGenerationService,
            ILeaseHearingDetailsService leaseHearingDetailsService,
            IConfiguration configuration,
            IApprovalProccessService approvalproccessService, IWorkflowTemplateService workflowtemplateService,
            IRequestforproceedingService undersection4PlotService)
        {
            _noticeGenerationService = noticeGenerationService;
            _leaseHearingDetailsService = leaseHearingDetailsService;
            _configuration = configuration;
            _workflowtemplateService = workflowtemplateService;
            _approvalproccessService = approvalproccessService;
            _undersection4PlotService = undersection4PlotService;
            targetPathNotice = _configuration.GetSection("FilePaths:NoticeGeneration:NoticeGenerationPath").Value.ToString();
        }


        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] LeaseHearingDetailsSearchDto model)
        {
            var result = await _noticeGenerationService.GetPagedRequestLetterDetails(model);
            return PartialView("_List", result);
        }


        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id)
        {
            //if (id == 0)
            //{
                Leasenoticegeneration Data = new Leasenoticegeneration();
                Data.RequestProceedingId = id;
                ViewBag.PrimaryId = 0;
                return View(Data);
            //}
            //else
            //{
            //    Leasenoticegeneration result = await _noticeGenerationService.FetchNoticeGenerationDetails(id);
            //    result.RequestProceedingId = id;
            //    ViewBag.PrimaryId = result.Id;
            //    return View(result);
            //}
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id, Leasenoticegeneration leasenoticegeneration)
        {
            var result = false;
            ViewBag.PrimaryId = leasenoticegeneration.Id;
            if (ModelState.IsValid)
            {
                if (leasenoticegeneration.GenerateUpload == 0)
                {
                    if (leasenoticegeneration.MeetingDate == null || leasenoticegeneration.MeetingTime == null || leasenoticegeneration.MeetingPlace == null)
                    {
                        ViewBag.Message = Alert.Show("Meeting Date , Time and Place is Mandatory", "", AlertType.Warning);
                        return View(leasenoticegeneration);
                    }
                }
                else if (leasenoticegeneration.GenerateUpload == 1)
                {
                    if (leasenoticegeneration.Document == null)
                    {
                        ViewBag.Message = Alert.Show("Document is Mandatory", "", AlertType.Warning);
                        return View(leasenoticegeneration);
                    }
                }
                FileHelper fileHelper = new FileHelper();
                if (leasenoticegeneration.Document != null)
                {
                    leasenoticegeneration.NoticeFileName = leasenoticegeneration.Document != null ?
                                                                      fileHelper.SaveFile1(targetPathNotice, leasenoticegeneration.Document) :
                                                                      leasenoticegeneration.Document != null || leasenoticegeneration.NoticeFileName != "" ?
                                                                      leasenoticegeneration.NoticeFileName : string.Empty;
                }


               // var demolitionpoliceData = await _noticeGenerationService.FetchSingleResultButOnAneexureId(leasenoticegeneration.FixingDemolitionId);
                if (id == 0)
                {
                    leasenoticegeneration.CreatedBy = SiteContext.UserId;
                    result = await _noticeGenerationService.Create(leasenoticegeneration);
                }
                else
                {
                    leasenoticegeneration.ModifiedBy = SiteContext.UserId;
                    result = await _noticeGenerationService.Update(id, leasenoticegeneration);
                }


                if (result)
                {
                    //var Data = await _noticeGenerationService.FetchSingleResultButOnAneexureId(leasenoticegeneration.FixingDemolitionId);
                    //leasenoticegeneration.FilePath = Data.FilePath;
                    //string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "DemolitionLetter.html");
                    //var Body = PopulateBody(Data, path);
                    if (leasenoticegeneration.GenerateUpload == 0)
                    {
                        ViewBag.IsVisible = true;
                       // ViewBag.DataLetter = Body;
                        ViewBag.Message = Alert.Show("Generate Letter Successfully", "", AlertType.Success);
                    }
                    else
                    {
                        ViewBag.IsVisible = false;
                        ViewBag.Message = Alert.Show("File Uploaded Successfully", "", AlertType.Success);
                    }
                    return View(leasenoticegeneration);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Success);
                    return View("Index");
                }

            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(leasenoticegeneration);
            }
        }

        [HttpPost]
        public async Task<PartialViewResult> ViewNotice([FromBody] ProceedingEvictionLetterSearchDto model)
        {
            //var result = false;
            //if (model != null)
            //{
            //    result = await _proceedingEvictionLetterService.UpdateRequestProceeding(model, SiteContext.UserId);
            //}
            //if (result)
            //{
            //    ProceedingEvictionLetterViewLetterDataDto data = new ProceedingEvictionLetterViewLetterDataDto();
            //    data = await _proceedingEvictionLetterService.BindProceedingConvictionLetterData(model.RefNoNameId);
            //    ViewBag.VisibleLetter = 1;
            //    return PartialView("_ViewNotice", data);
            //}
            //else
            //{
            //    Requestforproceeding data = new Requestforproceeding();
            //    ViewBag.Message = Alert.Show("No data Found", "", AlertType.Info);
            //    return PartialView("_ViewNotice", data);
            //}
            return PartialView("_ViewNotice");
        }

        //[AuthorizeContext(ViewAction.Edit)]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var Data = await _demolitionPoliceAssistenceLetterService.FetchSingleResult(id);
        //    if (Data == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(Data);
        //}


        //[HttpPost]
        //[AuthorizeContext(ViewAction.Edit)]
        //public async Task<IActionResult> Edit(int id, Demolitionpoliceassistenceletter leasenoticegeneration)
        //{
        //    targetPathDocument = _configuration.GetSection("FilePaths:DemolitionPoliceAssistenceFiles:LetterFilePath").Value.ToString();
        //    if (ModelState.IsValid)
        //    {

        //        if (leasenoticegeneration.GenerateUpload == 0)
        //        {
        //            if (leasenoticegeneration.MeetingDate == null || leasenoticegeneration.MeetingTime == null)
        //            {
        //                ViewBag.Message = Alert.Show("Meeting Date and Time is Mandatory", "", AlertType.Warning);
        //                return View(leasenoticegeneration);
        //            }
        //        }
        //        else if (leasenoticegeneration.GenerateUpload == 1)
        //        {
        //            if (leasenoticegeneration.Document == null)
        //            {
        //                ViewBag.Message = Alert.Show("Document is Mandatory", "", AlertType.Warning);
        //                return View(leasenoticegeneration);
        //            }
        //        }
        //        string LetterFileName = "";
        //        string LetterfilePath = "";
        //        if (leasenoticegeneration.Document != null)
        //        {
        //            if (!Directory.Exists(targetPathDocument))
        //            {
        //                DirectoryInfo di = Directory.CreateDirectory(targetPathDocument);// Try to create the directory.
        //            }
        //            LetterFileName = Guid.NewGuid().ToString() + "_" + leasenoticegeneration.Document.FileName;
        //            LetterfilePath = Path.Combine(targetPathDocument, LetterFileName);
        //            using (var stream = new FileStream(LetterfilePath, FileMode.Create))
        //            {
        //                leasenoticegeneration.Document.CopyTo(stream);
        //            }
        //            leasenoticegeneration.FilePath = LetterfilePath;
        //        }

        //        leasenoticegeneration.ModifiedBy = SiteContext.UserId;
        //        var result = await _demolitionPoliceAssistenceLetterService.Update(id, leasenoticegeneration);

        //        if (result)
        //        {
        //            var Data = await _demolitionPoliceAssistenceLetterService.FetchSingleResult(id);
        //            leasenoticegeneration.FilePath = Data.FilePath;
        //            string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "DemolitionLetter.html");
        //            var Body = PopulateBody(Data, path);
        //            ViewBag.IsVisible = true;
        //            ViewBag.DataLetter = Body;
        //            ViewBag.Message = Alert.Show("Generate Successfully", "", AlertType.Success);
        //            return View(leasenoticegeneration);
        //        }
        //        else
        //        {
        //            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Success);
        //            return View("Index");
        //        }

        //    }
        //    else
        //    {
        //        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //        return View(leasenoticegeneration);
        //    }

        //}
        //private string PopulateBody(Demolitionpoliceassistenceletter leasenoticegeneration, string Path)
        //{
        //    string body = string.Empty;
        //    using (StreamReader reader = new StreamReader(Path))
        //    {
        //        body = reader.ReadToEnd();
        //    }
        //    body = body.Replace("{MeetingDate}", Convert.ToDateTime((leasenoticegeneration.MeetingDate)).ToString("dd-MMM-yyyy"));
        //    body = body.Replace("{KhasraNo}", leasenoticegeneration.FixingDemolition.Encroachment.KhasraNo);
        //    body = body.Replace("{Locality}", leasenoticegeneration.FixingDemolition.Encroachment.Locality.Name);
        //    body = body.Replace("{MeetingTime}", leasenoticegeneration.MeetingTime);

        //    return body;
        //}

        //public async Task<FileResult> ViewLetter(int Id)
        //{
        //    FileHelper file = new FileHelper();
        //    var Data = await _demolitionPoliceAssistenceLetterService.FetchSingleResult(Id);
        //    string path = Data.FilePath;
        //    byte[] FileBytes = System.IO.File.ReadAllBytes(path);
        //    return File(FileBytes, file.GetContentType(path));
        //}
        #region RequestForProceedingEviction Details
        public async Task<PartialViewResult> RequestForProceedingEvictionView(int id)
        {
            var Data = await _undersection4PlotService.FetchSingleResult(id);
            Data.HonbleList = await _undersection4PlotService.GetAllHonble();
            Data.AllotmententryList = await _undersection4PlotService.GetAllAllotment();
            Data.UserNameList = await _undersection4PlotService.BindUsernameNameList();

            return PartialView("_RequestForProceedingEvictionView", Data);
        }
        public async Task<FileResult> ViewLetter(int Id)
        {
            FileHelper file = new FileHelper();
            var Data = await _undersection4PlotService.FetchSingleResult(Id);
            string targetPhotoPathLayout = Data.DemandLetter;
            byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
            return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
        }


        public async Task<FileResult> ViewLetter1(int Id)
        {
            try
            {
                FileHelper file = new FileHelper();
                var Data = await _undersection4PlotService.FetchSingleResult(Id);
                string targetPhotoPathLayout = Data.Noc;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
            }
            catch (Exception ex)
            {

                FileHelper file = new FileHelper();
                var Data = await _undersection4PlotService.FetchSingleResult(Id);
                string targetPhotoPathLayout = Data.Noc;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
            }
        }
        public async Task<FileResult> ViewLetter2(int Id)
        {
            try
            {
                FileHelper file = new FileHelper();
                var Data = await _undersection4PlotService.FetchSingleResult(Id);
                string targetPhotoPathLayout = Data.CancellationOrder;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
            }
            catch (Exception ex)
            {
                FileHelper file = new FileHelper();
                var Data = await _undersection4PlotService.FetchSingleResult(Id);
                string targetPhotoPathLayout = Data.CancellationOrder;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
            }
        }
        #endregion

        #region History Details Only For Notice Generation
        public async Task<PartialViewResult> NoticeHistoryDetails(int id)
        {
            var Data = await _noticeGenerationService.GetNoticeHistoryDetails(id);
            return PartialView("_ListNoticeGenertion", Data);
        }
        #endregion

        public async Task<JsonResult> GetNoticeGenerationDetails(int id)
        {
            var data = await _noticeGenerationService.FetchNoticeGenerationDetails(id);
            return Json(data);
        }

    }
}

