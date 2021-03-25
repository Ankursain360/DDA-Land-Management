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
        public readonly ILeaseHearingDetailsService _leaseHearingDetailsService;      
        public IConfiguration _configuration;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;
        private readonly IRequestforproceedingService _undersection4PlotService;

        string targetPathDocument = "";
        public NoticeGenerationController(ILeaseHearingDetailsService leaseHearingDetailsService,
            IConfiguration configuration, 
            IApprovalProccessService approvalproccessService, IWorkflowTemplateService workflowtemplateService,
            IRequestforproceedingService undersection4PlotService)
        {
            _leaseHearingDetailsService = leaseHearingDetailsService;
            _configuration = configuration;
            _workflowtemplateService = workflowtemplateService;
            _approvalproccessService = approvalproccessService;
            _undersection4PlotService = undersection4PlotService;
        }


       // [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] LeaseHearingDetailsSearchDto model)
        {
            var result = await _leaseHearingDetailsService.GetPagedRequestLetterDetails(model, SiteContext.UserId);
            return PartialView("_List", result);
        }


        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id)
        {
            Requestforproceeding result = await _leaseHearingDetailsService.FetchRequestforproceedingData(id);
            //if (result == null)
            //{
            //    Requestforproceeding Data = new Requestforproceeding();
            //    Data.FixingDemolitionId = id;
            //    ViewBag.PrimaryId = 0;
            //    return View(Data);
            //}
            //else
            //{
            //    result.FixingDemolitionId = id;
            //    ViewBag.PrimaryId = result.Id;
                return View(result);
           // }
        }

        //[HttpPost]
        //[AuthorizeContext(ViewAction.Add)]
        //public async Task<IActionResult> Create(Demolitionpoliceassistenceletter demolitionpoliceassistenceletter)
        //{
        //    var result = false;
        //    ViewBag.PrimaryId = demolitionpoliceassistenceletter.Id;
        //    targetPathDocument = _configuration.GetSection("FilePaths:DemolitionPoliceAssistenceFiles:LetterFilePath").Value.ToString();
        //    if (ModelState.IsValid)
        //    {
        //        if (demolitionpoliceassistenceletter.GenerateUpload == 0)
        //        {
        //            if (demolitionpoliceassistenceletter.MeetingDate == null || demolitionpoliceassistenceletter.MeetingTime == null)
        //            {
        //                ViewBag.Message = Alert.Show("Meeting Date and Time is Mandatory", "", AlertType.Warning);
        //                return View(demolitionpoliceassistenceletter);
        //            }
        //        }
        //        else if (demolitionpoliceassistenceletter.GenerateUpload == 1)
        //        {
        //            if (demolitionpoliceassistenceletter.Document == null)
        //            {
        //                ViewBag.Message = Alert.Show("Document is Mandatory", "", AlertType.Warning);
        //                return View(demolitionpoliceassistenceletter);
        //            }
        //        }
        //        string LetterFileName = "";
        //        string LetterfilePath = "";
        //        if (demolitionpoliceassistenceletter.Document != null)
        //        {
        //            if (!Directory.Exists(targetPathDocument))
        //            {
        //                DirectoryInfo di = Directory.CreateDirectory(targetPathDocument);// Try to create the directory.
        //            }
        //            LetterFileName = Guid.NewGuid().ToString() + "_" + demolitionpoliceassistenceletter.Document.FileName;
        //            LetterfilePath = Path.Combine(targetPathDocument, LetterFileName);
        //            using (var stream = new FileStream(LetterfilePath, FileMode.Create))
        //            {
        //                demolitionpoliceassistenceletter.Document.CopyTo(stream);
        //            }
        //            demolitionpoliceassistenceletter.FilePath = LetterfilePath;
        //        }


        //        var demolitionpoliceData = await _demolitionPoliceAssistenceLetterService.FetchSingleResultButOnAneexureId(demolitionpoliceassistenceletter.FixingDemolitionId);
        //        if (demolitionpoliceData == null)
        //        {
        //            demolitionpoliceassistenceletter.CreatedBy = SiteContext.UserId;
        //            result = await _demolitionPoliceAssistenceLetterService.Create(demolitionpoliceassistenceletter);
        //        }
        //        else
        //        {
        //            demolitionpoliceassistenceletter.ModifiedBy = SiteContext.UserId;
        //            result = await _demolitionPoliceAssistenceLetterService.Update(demolitionpoliceData.Id, demolitionpoliceassistenceletter);
        //        }


        //        if (result)
        //        {
        //            var Data = await _demolitionPoliceAssistenceLetterService.FetchSingleResultButOnAneexureId(demolitionpoliceassistenceletter.FixingDemolitionId);
        //            demolitionpoliceassistenceletter.FilePath = Data.FilePath;
        //            string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "DemolitionLetter.html");
        //            var Body = PopulateBody(Data, path);
        //            if (demolitionpoliceassistenceletter.GenerateUpload == 0)
        //            {
        //                ViewBag.IsVisible = true;
        //                ViewBag.DataLetter = Body;
        //                ViewBag.Message = Alert.Show("Generate Letter Successfully", "", AlertType.Success);
        //            }
        //            else
        //            {
        //                ViewBag.IsVisible = false;
        //                ViewBag.Message = Alert.Show("File Uploaded Successfully", "", AlertType.Success);
        //            }
        //            return View(demolitionpoliceassistenceletter);
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
        //        return View(demolitionpoliceassistenceletter);
        //    }
        //}

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
        //public async Task<IActionResult> Edit(int id, Demolitionpoliceassistenceletter demolitionpoliceassistenceletter)
        //{
        //    targetPathDocument = _configuration.GetSection("FilePaths:DemolitionPoliceAssistenceFiles:LetterFilePath").Value.ToString();
        //    if (ModelState.IsValid)
        //    {

        //        if (demolitionpoliceassistenceletter.GenerateUpload == 0)
        //        {
        //            if (demolitionpoliceassistenceletter.MeetingDate == null || demolitionpoliceassistenceletter.MeetingTime == null)
        //            {
        //                ViewBag.Message = Alert.Show("Meeting Date and Time is Mandatory", "", AlertType.Warning);
        //                return View(demolitionpoliceassistenceletter);
        //            }
        //        }
        //        else if (demolitionpoliceassistenceletter.GenerateUpload == 1)
        //        {
        //            if (demolitionpoliceassistenceletter.Document == null)
        //            {
        //                ViewBag.Message = Alert.Show("Document is Mandatory", "", AlertType.Warning);
        //                return View(demolitionpoliceassistenceletter);
        //            }
        //        }
        //        string LetterFileName = "";
        //        string LetterfilePath = "";
        //        if (demolitionpoliceassistenceletter.Document != null)
        //        {
        //            if (!Directory.Exists(targetPathDocument))
        //            {
        //                DirectoryInfo di = Directory.CreateDirectory(targetPathDocument);// Try to create the directory.
        //            }
        //            LetterFileName = Guid.NewGuid().ToString() + "_" + demolitionpoliceassistenceletter.Document.FileName;
        //            LetterfilePath = Path.Combine(targetPathDocument, LetterFileName);
        //            using (var stream = new FileStream(LetterfilePath, FileMode.Create))
        //            {
        //                demolitionpoliceassistenceletter.Document.CopyTo(stream);
        //            }
        //            demolitionpoliceassistenceletter.FilePath = LetterfilePath;
        //        }

        //        demolitionpoliceassistenceletter.ModifiedBy = SiteContext.UserId;
        //        var result = await _demolitionPoliceAssistenceLetterService.Update(id, demolitionpoliceassistenceletter);

        //        if (result)
        //        {
        //            var Data = await _demolitionPoliceAssistenceLetterService.FetchSingleResult(id);
        //            demolitionpoliceassistenceletter.FilePath = Data.FilePath;
        //            string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "DemolitionLetter.html");
        //            var Body = PopulateBody(Data, path);
        //            ViewBag.IsVisible = true;
        //            ViewBag.DataLetter = Body;
        //            ViewBag.Message = Alert.Show("Generate Successfully", "", AlertType.Success);
        //            return View(demolitionpoliceassistenceletter);
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
        //        return View(demolitionpoliceassistenceletter);
        //    }

        //}
        //private string PopulateBody(Demolitionpoliceassistenceletter demolitionpoliceassistenceletter, string Path)
        //{
        //    string body = string.Empty;
        //    using (StreamReader reader = new StreamReader(Path))
        //    {
        //        body = reader.ReadToEnd();
        //    }
        //    body = body.Replace("{MeetingDate}", Convert.ToDateTime((demolitionpoliceassistenceletter.MeetingDate)).ToString("dd-MMM-yyyy"));
        //    body = body.Replace("{KhasraNo}", demolitionpoliceassistenceletter.FixingDemolition.Encroachment.KhasraNo);
        //    body = body.Replace("{Locality}", demolitionpoliceassistenceletter.FixingDemolition.Encroachment.Locality.Name);
        //    body = body.Replace("{MeetingTime}", demolitionpoliceassistenceletter.MeetingTime);

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
    }
}

