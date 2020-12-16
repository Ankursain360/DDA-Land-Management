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

namespace EncroachmentDemolition.Controllers
{
    public class DemolitionPoliceAssistenceLetterController : BaseController
    {
        public readonly IDemolitionPoliceAssistenceLetterService _demolitionPoliceAssistenceLetterService;
        public readonly IAnnexureAApprovalService _annexureAApprovalService;
        public readonly IAnnexureAService _annexureAService;
        public readonly IEncroachmentRegisterationApprovalService _encroachmentRegisterationApprovalService;
        public IConfiguration _configuration;
        public readonly IEncroachmentRegisterationService _encroachmentRegisterationService;
        private readonly IWatchandwardService _watchandwardService;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;
        private readonly IHostingEnvironment _hostingEnvironment;

        string targetPathDocument = "";
        public DemolitionPoliceAssistenceLetterController(IDemolitionPoliceAssistenceLetterService demolitionPoliceAssistenceLetterService,
            IEncroachmentRegisterationApprovalService encroachmentRegisterationApprovalService, 
            IEncroachmentRegisterationService encroachmentRegisterationService,
            IConfiguration configuration, IWatchandwardService watchandwardService,
            IApprovalProccessService approvalproccessService, IWorkflowTemplateService workflowtemplateService,
            IAnnexureAService annexureAService, IAnnexureAApprovalService annexureAApprovalService,
            IHostingEnvironment en)
        {
            _demolitionPoliceAssistenceLetterService = demolitionPoliceAssistenceLetterService;
            _encroachmentRegisterationApprovalService = encroachmentRegisterationApprovalService;
            _encroachmentRegisterationService = encroachmentRegisterationService;
            _configuration = configuration;
            _watchandwardService = watchandwardService;
            _workflowtemplateService = workflowtemplateService;
            _approvalproccessService = approvalproccessService;
            _annexureAService = annexureAService;
            _annexureAApprovalService = annexureAApprovalService;
            _hostingEnvironment = en;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DemolitionPoliceAssistenceLetterSearchDto model)
        {
            if(model.StatusId == 1)
            {
                var result = await _demolitionPoliceAssistenceLetterService.GetPagedApprovedAnnexureA(model, SiteContext.UserId);
                ViewBag.IsApproved = model.StatusId;
                return PartialView("_List", result);
            }
            else
            {
                var result = await _demolitionPoliceAssistenceLetterService.GetPagedApprovedAnnexureAListedit(model, SiteContext.UserId);
                ViewBag.IsApproved = model.StatusId;
                return PartialView("_List2", result);
            }
           
        }

        public async Task<IActionResult> Create(int id)
        {
            Demolitionpoliceassistenceletter Data = new Demolitionpoliceassistenceletter();
            Data.FixingDemolitionId = id;
            ViewBag.PrimaryId = 0;
            return View(Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Demolitionpoliceassistenceletter demolitionpoliceassistenceletter)
        {
            var result = false;
            targetPathDocument = _configuration.GetSection("FilePaths:DemolitionPoliceAssistenceFiles:LetterFilePath").Value.ToString();
            if(ModelState.IsValid)
            {
                string LetterFileName = "";
                string LetterfilePath = "";
                if (demolitionpoliceassistenceletter.Document != null)
                {
                    if (!Directory.Exists(targetPathDocument))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(targetPathDocument);// Try to create the directory.
                    }
                    LetterFileName = Guid.NewGuid().ToString() + "_" + demolitionpoliceassistenceletter.Document.FileName;
                    LetterfilePath = Path.Combine(targetPathDocument, LetterFileName);
                    using (var stream = new FileStream(LetterfilePath, FileMode.Create))
                    {
                        demolitionpoliceassistenceletter.Document.CopyTo(stream);
                    }
                    demolitionpoliceassistenceletter.FilePath = LetterfilePath;
                }


                var demolitionpoliceData =await _demolitionPoliceAssistenceLetterService.FetchSingleResultButOnAneexureId(demolitionpoliceassistenceletter.FixingDemolitionId);
                if (demolitionpoliceData == null)
                {
                    demolitionpoliceassistenceletter.CreatedBy = SiteContext.UserId;
                    result = await _demolitionPoliceAssistenceLetterService.Create(demolitionpoliceassistenceletter);
                }
                else
                {
                    demolitionpoliceassistenceletter.ModifiedBy = SiteContext.UserId;
                    result = await _demolitionPoliceAssistenceLetterService.Update(demolitionpoliceData.Id, demolitionpoliceassistenceletter);
                }
              

                if(result)
                {
                    var Data = await _demolitionPoliceAssistenceLetterService.FetchSingleResult(demolitionpoliceData.Id);
                    demolitionpoliceassistenceletter.FilePath = Data.FilePath;
                    string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "DemolitionLetter.html");
                    var Body = PopulateBody(Data, path);
                    ViewBag.IsVisible = true;
                    ViewBag.DataLetter = Body;
                    ViewBag.Message = Alert.Show("Generate Successfully", "", AlertType.Success);
                    return View(demolitionpoliceassistenceletter);
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
                return View(demolitionpoliceassistenceletter);
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _demolitionPoliceAssistenceLetterService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Demolitionpoliceassistenceletter demolitionpoliceassistenceletter)
        {
            targetPathDocument = _configuration.GetSection("FilePaths:DemolitionPoliceAssistenceFiles:LetterFilePath").Value.ToString();
            if (ModelState.IsValid)
            {
               
                if(demolitionpoliceassistenceletter.GenerateUpload == 0 )
                {
                    if(demolitionpoliceassistenceletter.MeetingDate == null || demolitionpoliceassistenceletter.MeetingTime == null)
                    {
                        ViewBag.Message = Alert.Show("Meeting Date and Time is Mandatory", "", AlertType.Warning);
                        return View(demolitionpoliceassistenceletter);
                    }
                }
                else if (demolitionpoliceassistenceletter.GenerateUpload == 1)
                {
                    if (demolitionpoliceassistenceletter.Document == null )
                    {
                        ViewBag.Message = Alert.Show("Document is Mandatory", "", AlertType.Warning);
                        return View(demolitionpoliceassistenceletter);
                    }
                }
                string LetterFileName = "";
                string LetterfilePath = "";
                if (demolitionpoliceassistenceletter.Document != null)
                {
                    if (!Directory.Exists(targetPathDocument))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(targetPathDocument);// Try to create the directory.
                    }
                    LetterFileName = Guid.NewGuid().ToString() + "_" + demolitionpoliceassistenceletter.Document.FileName;
                    LetterfilePath = Path.Combine(targetPathDocument, LetterFileName);
                    using (var stream = new FileStream(LetterfilePath, FileMode.Create))
                    {
                        demolitionpoliceassistenceletter.Document.CopyTo(stream);
                    }
                    demolitionpoliceassistenceletter.FilePath = LetterfilePath;
                }

                demolitionpoliceassistenceletter.ModifiedBy = SiteContext.UserId;
                var result = await _demolitionPoliceAssistenceLetterService.Update(id, demolitionpoliceassistenceletter);

                if (result)
                {
                    var Data = await _demolitionPoliceAssistenceLetterService.FetchSingleResult(id);
                    demolitionpoliceassistenceletter.FilePath = Data.FilePath;
                    string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "DemolitionLetter.html");
                    var Body = PopulateBody(Data, path);
                    ViewBag.IsVisible = true;
                    ViewBag.DataLetter = Body;
                    ViewBag.Message = Alert.Show("Generate Successfully", "", AlertType.Success);
                    return View(demolitionpoliceassistenceletter);
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
                return View(demolitionpoliceassistenceletter);
            }

        }
        private string PopulateBody(Demolitionpoliceassistenceletter demolitionpoliceassistenceletter, string Path)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Path))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{MeetingDate}", Convert.ToDateTime((demolitionpoliceassistenceletter.MeetingDate)).ToString("dd-MMM-yyyy"));
            body = body.Replace("{KhasraNo}", demolitionpoliceassistenceletter.FixingDemolition.Encroachment.KhasraNo);
            body = body.Replace("{Locality}", demolitionpoliceassistenceletter.FixingDemolition.Encroachment.Locality.Name);
            body = body.Replace("{MeetingTime}", demolitionpoliceassistenceletter.MeetingTime);

            return body;
        }

        public async Task<FileResult> ViewLetter(int Id)
        {
            FileHelper file = new FileHelper();
            var Data = await _demolitionPoliceAssistenceLetterService.FetchSingleResult(Id);
            //Watchandwardphotofiledetails Data = await _watchandwardService.GetWatchandwardphotofiledetails(Id);
            string path = Data.FilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

    }
}
