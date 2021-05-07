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
using Dto.Master;

namespace LeaseDetails.Controllers
{
    public class AllotteeEvidenceUploadController : BaseController
    {
        public readonly IAllotteeEvidenceUploadService _allotteeEvidenceUploadService;
        public readonly INoticeGenerationService _noticeGenerationService;
        public IConfiguration _configuration;
        private readonly IRequestforproceedingService _requestforproceedingService;
        private readonly ICancellationEntryService _cancellationEntryService;
        private readonly IHostingEnvironment _hostingEnvironment;

        string targetPathAllotteeEvidence = "";
        string targetPathNotice = "";
        string targetPathDemandLetter = "";
        string targetPathNOC = "";
        string targetPathCanellationOrder = "";
        public AllotteeEvidenceUploadController(INoticeGenerationService noticeGenerationService,
            IAllotteeEvidenceUploadService allotteeEvidenceUploadService,
            IConfiguration configuration, IRequestforproceedingService requestforproceedingService,
            IHostingEnvironment en,
            ICancellationEntryService cancellationEntryService)
        {
            _noticeGenerationService = noticeGenerationService;
            _allotteeEvidenceUploadService = allotteeEvidenceUploadService;
            _configuration = configuration;
            _requestforproceedingService = requestforproceedingService;
            _hostingEnvironment = en;
            _cancellationEntryService = cancellationEntryService;
            targetPathNotice = _configuration.GetSection("FilePaths:NoticeGeneration:NoticeGenerationPath").Value.ToString();
            targetPathAllotteeEvidence = _configuration.GetSection("FilePaths:AllotteEvidence:AllotteEvidencePath").Value.ToString();
            targetPathDemandLetter = _configuration.GetSection("FilePaths:CancellationEntry:DemandletterFilePath").Value.ToString();
            targetPathNOC = _configuration.GetSection("FilePaths:CancellationEntry:NOCFilePath").Value.ToString();
            targetPathCanellationOrder = _configuration.GetSection("FilePaths:CancellationEntry:CancellationOrderFilePath").Value.ToString();

        }


        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] AllotteeEvidenceSearchDto model)
        {
            var result = await _allotteeEvidenceUploadService.GetPagedRequestLetterDetails(model);
            return PartialView("_List", result);
        }


        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id)
        {
            Allotteeevidenceupload Data = new Allotteeevidenceupload();
            Data.AllotteeEvidenceUploadList = await _allotteeEvidenceUploadService.GetAllotteeEvidenceHistoryDetails(id);
            Data.RequestProceedingId = id;
            ViewBag.PrimaryId = 0;
            return View(Data);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id, Allotteeevidenceupload allotteeevidenceupload)
        {
            var result = false;
            ViewBag.PrimaryId = allotteeevidenceupload.Id;
            allotteeevidenceupload.AllotteeEvidenceUploadList = await _allotteeEvidenceUploadService.GetAllotteeEvidenceHistoryDetails(allotteeevidenceupload.RequestProceedingId);

            if (ModelState.IsValid)
            {
                FileHelper fileHelper = new FileHelper();
                if (allotteeevidenceupload.Document != null)
                {
                    allotteeevidenceupload.DocumentPatth = allotteeevidenceupload.Document != null ?
                                                                      fileHelper.SaveFile1(targetPathAllotteeEvidence, allotteeevidenceupload.Document) :
                                                                      allotteeevidenceupload.Document != null || allotteeevidenceupload.DocumentPatth != "" ?
                                                                      allotteeevidenceupload.DocumentPatth : string.Empty;
                }

                if (id == 0)
                {
                    allotteeevidenceupload.CreatedBy = SiteContext.UserId;
                    result = await _allotteeEvidenceUploadService.Create(allotteeevidenceupload);
                }
                else
                {
                    allotteeevidenceupload.ModifiedBy = SiteContext.UserId;
                    result = await _allotteeEvidenceUploadService.Update(id, allotteeevidenceupload);
                }


                if (result)
                {
                    Allotteeevidenceupload emptyData = new Allotteeevidenceupload();
                    emptyData.AllotteeEvidenceUploadList = await _allotteeEvidenceUploadService.GetAllotteeEvidenceHistoryDetails(allotteeevidenceupload.RequestProceedingId);
                    emptyData.RequestProceedingId = allotteeevidenceupload.RequestProceedingId;
                    if (id == 0)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        ViewBag.PrimaryId = 0;
                        return View("Create", emptyData);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        ViewBag.PrimaryId = 0;
                        return View("Create", emptyData);
                    }
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
                return View(allotteeevidenceupload);
            }
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

        #region Details Only For Notice Generation
        public async Task<PartialViewResult> NoticeGenerationDetails(int id)
        {
            var Data = await _noticeGenerationService.GetNoticeHistoryDetails(id);
            return PartialView("_ListNoticeGenertion", Data);
        }
        public async Task<FileResult> ViewNotice(int Id)
        {
            FileHelper file = new FileHelper();
            var Data = await _noticeGenerationService.FetchNoticeGenerationDetails(Id);
            string path = targetPathNotice + Data.NoticeFileName;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        #endregion

        public async Task<JsonResult> GetAllotteeEvidenceUploadDetails(int id)
        {
            var data = await _allotteeEvidenceUploadService.FetchAllotteeEvidenceUploadDetails(id);
            return Json(data);
        }

        public async Task<FileResult> ViewAllotteeEvidenceDoc(int Id)
        {
            FileHelper file = new FileHelper();
            var Data = await _allotteeEvidenceUploadService.FetchAllotteeEvidenceUploadDetails(Id);
            string path = targetPathAllotteeEvidence + Data.DocumentPatth;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        [AuthorizeContext(ViewAction.Download)]

        public async Task<IActionResult> AllotteeEvidenceList()
        {
            var result = await _allotteeEvidenceUploadService.GetAllotteeEvidenceDetails();
            List<AllotteeEvidenceListDto> data = new List<AllotteeEvidenceListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new AllotteeEvidenceListDto()
                    {
                        Id = result[i].Id,
                        ReferenceNo = result[i].Allotment == null ? "" : result[i].Allotment.Application.RefNo,
                        SocietyName = result[i].Allotment == null ? "" : result[i].Allotment.Application.Name,
                        AllotmentDate = Convert.ToDateTime(result[i].Allotment.AllotmentDate).ToString("dd-MMM-yyyy"),
                        Area = result[i].Allotment == null ? "" : result[i].Allotment.TotalArea.ToString(),
                        GroundOfViolations = result[i].GroundOfViolations,

                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }
    }
}

