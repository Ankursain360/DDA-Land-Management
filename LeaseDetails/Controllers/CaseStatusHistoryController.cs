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
        string targetPathDemandLetter = "";
        string targetPathNOC = "";
        string targetPathCanellationOrder = "";
        private readonly IRequestforproceedingService _requestforproceedingService;
        private readonly ICancellationEntryService _cancellationEntryService;
        public CaseStatusHistoryController(IJudgementService judgementService,
            IConfiguration configuration,
            IRequestforproceedingService requestforproceedingService,
            ICancellationEntryService cancellationEntryService)
        {
            _judgementService = judgementService;
            _configuration = configuration;
            _requestforproceedingService = requestforproceedingService;
            _cancellationEntryService = cancellationEntryService;

            targetPathNotice = _configuration.GetSection("FilePaths:NoticeGeneration:NoticeGenerationPath").Value.ToString();
            targetPathDemandLetter = _configuration.GetSection("FilePaths:CancellationEntry:DemandletterFilePath").Value.ToString();
            targetPathNOC = _configuration.GetSection("FilePaths:CancellationEntry:NOCFilePath").Value.ToString();
            targetPathCanellationOrder = _configuration.GetSection("FilePaths:CancellationEntry:CancellationOrderFilePath").Value.ToString();

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

        public async Task<IActionResult> DownloadJudgementFile(int Id)
        {
            FileHelper file = new FileHelper();
            Judgement Data = await _judgementService.FetchSingleResult(Id);
            string filename = Data.FilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        //public async Task<PartialViewResult> RequestForProceedingEvictionView(int id)
        //{
        //    var Data = await _judgementService.FetchSingleReqDetails(id);
        //    Data.HonbleList = await _judgementService.GetAllHonble();
        //    Data.AllotmententryList = await _judgementService.GetAllAllotment();
        //    Data.UserNameList = await _judgementService.BindUsernameNameList();

        //    return PartialView("_RequestForProceedingEvictionView", Data);
        //}



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
        public async Task<PartialViewResult> ActionTakenByDDAView(int id)
        {
            var Data = await _judgementService.FetchActionTakenByDDADetails(id);

            return PartialView("_ActiontakenbyDDA", Data);
        }
        public async Task<IActionResult> DownloadActionFile(int Id)
        {
            FileHelper file = new FileHelper();
            Actiontakenbydda Data = await _judgementService.FetchActionTakenByDDADetails(Id);
            string filename = Data.Document;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }



        #region RequestForProceedingEviction Details
        public async Task<PartialViewResult> RequestForProceedingEvictionView(int id)
        {
            var Data = await _requestforproceedingService.FetchSingleResult(id);
            Data.HonbleList = await _requestforproceedingService.GetAllHonble();
            Data.CancellationList = await _requestforproceedingService.GetCancellationListData();
            Data.UserNameList = await _requestforproceedingService.BindUsernameNameList();

            return PartialView("_RequestForProceedingEvictionView", Data);
        }
        public async Task<FileResult> ViewLetter(int Id)
        {
            FileHelper file = new FileHelper();
            var Data = await _cancellationEntryService.FetchSingleResult(Id);
            string targetPhotoPathLayout = targetPathDemandLetter + Data.DemandLetter;
            byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
            return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
        }


        public async Task<FileResult> ViewLetter1(int Id)
        {
            FileHelper file = new FileHelper();
            var Data = await _cancellationEntryService.FetchSingleResult(Id);
            string targetPhotoPathLayout = targetPathNOC + Data.Noc;
            byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
            return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
        }
        public async Task<FileResult> ViewLetter2(int Id)
        {
            FileHelper file = new FileHelper();
            var Data = await _cancellationEntryService.FetchSingleResult(Id);
            string targetPhotoPathLayout = targetPathCanellationOrder + Data.CancellationOrder;
            byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
            return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
        }
        #endregion
    }
}
