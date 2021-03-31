using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using LeaseDetails.Filters;
using Utility.Helper;
using System.Threading.Tasks;
using Dto.Search;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace LeaseDetails.Controllers
{
    public class CaseStatusHistoryController : Controller
    {
        private readonly IJudgementService _judgementService;
        public IConfiguration _configuration;
        string targetPathNotice = "";
        public CaseStatusHistoryController(IJudgementService judgementService, IConfiguration configuration)
        {
            _judgementService = judgementService;
            _configuration = configuration;
            targetPathNotice = _configuration.GetSection("FilePaths:NoticeGeneration:NoticeGenerationPath").Value.ToString();

        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create(int id)
        {
            var Data = await _judgementService.FetchSingleResult(id);

            if (Data == null)
            {
                //Judgement model = new Judgement();
                //model.UserNameList = await _judgementService.BindUsernameNameList();
                //model.JudgementStatusList = await _judgementService.GetJudgementStatusList();
                //model.RequestForProceedingId = id;
                return View();
            }
            else
            {
                Data.UserNameList = await _judgementService.BindUsernameNameList();
                Data.JudgementStatusList = await _judgementService.GetJudgementStatusList();
                return View(Data);
            }

        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] RequestForProceedingSearchDto model)
        {
            var result = await _judgementService.GetPagedRequestForProceeding(model);

            return PartialView("_List", result);
        }
        public async Task<PartialViewResult> RequestForProceedingEvictionView(int id)
        {
            var Data = await _judgementService.FetchSingleReqDetails(id);
            Data.HonbleList = await _judgementService.GetAllHonble();
            Data.AllotmententryList = await _judgementService.GetAllAllotment();
            Data.UserNameList = await _judgementService.BindUsernameNameList();

            return PartialView("_RequestForProceedingEvictionView", Data);
        }



        public async Task<PartialViewResult> NoticeGenerationView(int id)
        {
            var Data = await _judgementService.FetchNoticeGenerationDetails(id);

            return PartialView("_ListNoticeGenertion", Data);
        }

        public async Task<FileResult> ViewNotice(int Id)
        {
            FileHelper file = new FileHelper();
            var Data = await _judgementService.FetchSingleNotice(Id);
            string path = targetPathNotice + Data.NoticeFileName;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }


        public async Task<PartialViewResult> AllotteeEvidenceView(int id)
        {
            var Data = await _judgementService.FetchAllotteeEvidenceDetails(id);

            return PartialView("_AllotteeEvidence", Data);
        }
        public async Task<FileResult> ViewEvidence(int Id)
        {
            FileHelper file = new FileHelper();
            var Data = await _judgementService.FetchSingleEvidence(Id);
            string path = targetPathNotice + Data.DocumentPatth;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        public async Task<PartialViewResult> HearingDetailsView(int id)
        {
            var Data = await _judgementService.FetchHearingDetails(id);

            return PartialView("_HearingDetails", Data);
        }
    }
}
