using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dto.Search;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
using System.IO;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;

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
        string targetPathDocument = "";
        public DemolitionPoliceAssistenceLetterController(IDemolitionPoliceAssistenceLetterService demolitionPoliceAssistenceLetterService,
            IEncroachmentRegisterationApprovalService encroachmentRegisterationApprovalService, 
            IEncroachmentRegisterationService encroachmentRegisterationService,
            IConfiguration configuration, IWatchandwardService watchandwardService,
            IApprovalProccessService approvalproccessService, IWorkflowTemplateService workflowtemplateService,
            IAnnexureAService annexureAService, IAnnexureAApprovalService annexureAApprovalService)
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
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DemolitionPoliceAssistenceLetterSearchDto model)
        {
            var result = await _demolitionPoliceAssistenceLetterService.GetPagedApprovedAnnexureA(model, SiteContext.UserId);
            ViewBag.IsApproved = model.StatusId;
            return PartialView("_List", result);
        }

        public async Task<IActionResult> Create(int id)
        {
            Demolitionpoliceassistenceletter Data = new Demolitionpoliceassistenceletter();
            Data.FixingDemolitionId = id;
            return View(Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Demolitionpoliceassistenceletter demolitionpoliceassistenceletter)
        {
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

                demolitionpoliceassistenceletter.CreatedBy = SiteContext.UserId;
                var result = await _demolitionPoliceAssistenceLetterService.Create(demolitionpoliceassistenceletter);

                if(result)
                {
                    ViewBag.Message = Alert.Show("Generate Successfull", "", AlertType.Success);
                    return View("Index");
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
    }
}
