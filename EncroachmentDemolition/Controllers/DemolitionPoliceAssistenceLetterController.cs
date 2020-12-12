using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dto.Search;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;

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
            var Data = await _annexureAApprovalService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
    }
}
