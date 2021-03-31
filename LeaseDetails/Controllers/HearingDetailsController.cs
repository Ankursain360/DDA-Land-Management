using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace LeaseDetails.Controllers
{
    public class HearingDetailsController : BaseController
    {
        private readonly IHearingdetailsService _hearingdetailsService;
        public IConfiguration _configuration;
        string targetPathNotice = "";
        string targetPathHearingDetails = "";

        public readonly INoticeGenerationService _noticeGenerationService;
        public readonly ILeaseHearingDetailsService _leaseHearingDetailsService;
       
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;
        private readonly IRequestforproceedingService _requestforproceedingService;
        private readonly ICancellationEntryService _cancellationEntryService;
        private readonly IHostingEnvironment _hostingEnvironment;

        
        string targetPathDemandLetter = "";
        string targetPathNOC = "";
        string targetPathCanellationOrder = "";
        public HearingDetailsController(IHearingdetailsService hearingdetailsService, IConfiguration configuration, INoticeGenerationService noticeGenerationService,
            ILeaseHearingDetailsService leaseHearingDetailsService,
            IApprovalProccessService approvalproccessService, IWorkflowTemplateService workflowtemplateService,
            IRequestforproceedingService requestforproceedingService,
            IHostingEnvironment en,
            ICancellationEntryService cancellationEntryService)
        {
            _hearingdetailsService = hearingdetailsService;
            _configuration = configuration;
            targetPathNotice = _configuration.GetSection("FilePaths:NoticeGeneration:NoticeGenerationPath").Value.ToString();
            targetPathHearingDetails = _configuration.GetSection("FilePaths:HearingDetails:HearingDetailsPath").Value.ToString();
            _noticeGenerationService = noticeGenerationService;
            _leaseHearingDetailsService = leaseHearingDetailsService;
           
            _workflowtemplateService = workflowtemplateService;
            _approvalproccessService = approvalproccessService;
            _requestforproceedingService = requestforproceedingService;
            _hostingEnvironment = en;
            _cancellationEntryService = cancellationEntryService;
           
            targetPathDemandLetter = _configuration.GetSection("FilePaths:CancellationEntry:DemandletterFilePath").Value.ToString();
            targetPathNOC = _configuration.GetSection("FilePaths:CancellationEntry:NOCFilePath").Value.ToString();
            targetPathCanellationOrder = _configuration.GetSection("FilePaths:CancellationEntry:CancellationOrderFilePath").Value.ToString();

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] HearingdetailsSeachDto model)
        {
            var result = await _hearingdetailsService.GetPagedRequestForProceeding(model);

            return PartialView("_List", result);
        }
        public async Task<IActionResult> Create(int id)
        {
            var Data = await _hearingdetailsService.FetchSingleResult(id);

            if (Data == null)
            {
                Hearingdetails model = new Hearingdetails();

                model.ReqProcId = id;
                return View(model);
            }
            else
            {

                return View(Data);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id, Hearingdetails model)
        {
            // string Document = _configuration.GetSection("FilePaths:Judgement:Doc").Value.ToString();
            FileHelper fileHelper = new FileHelper();


            //if (ModelState.IsValid)
            //{
            var Data = await _hearingdetailsService.FetchSingleResult(id);
            if (Data == null)
            {
                //if (model.File != null)
                //{
                //    model.FilePath = fileHelper.SaveFile(Document, model.File);
                //}


                model.ReqProcId = id;
               
                model.Remark = "";
                model.Id = 0;
                model.CreatedBy = SiteContext.UserId;
                model.IsActive = 1;
                var result = await _hearingdetailsService.Create(model);

                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);

                    return RedirectToAction("Index", "HearingDetails");
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(model);

                }
            }
            else
            {
                //if (model.File != null)
                //{
                //    model.FilePath = fileHelper.SaveFile(Document, model.File);
                //}



                model.ModifiedBy = SiteContext.UserId;
                model.IsActive = 1;
                var result = await _hearingdetailsService.Update(id, model);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    return View(model);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(model);

                }
            }

            // }
            //else
            //{
            //    return View(model);
            //}

        }
        public IActionResult ViewLetter()
        {
            return View();
        }
        
        public async Task<PartialViewResult> NoticeGenerationView(int id)
        {
            var Data = await _hearingdetailsService.FetchNoticeGenerationDetails(id);

            return PartialView("_ListNoticeGenertion", Data);
        }

        public async Task<FileResult> ViewNotice(int Id)
        {
            FileHelper file = new FileHelper();
            var Data = await _hearingdetailsService.FetchSingleNotice(Id);
            string path = targetPathNotice + Data.NoticeFileName;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }


        public async Task<PartialViewResult> AllotteeEvidenceView(int id)
        {
            var Data = await _hearingdetailsService.FetchAllotteeEvidenceDetails(id);

            return PartialView("_AllotteeEvidence", Data);
        }
        public async Task<FileResult> ViewEvidence(int Id)
        {
            FileHelper file = new FileHelper();
            var Data = await _hearingdetailsService.FetchSingleEvidence(Id);
            string path = targetPathNotice + Data.DocumentPatth;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }


        [HttpPost]
        public async Task<PartialViewResult> ListHearingDetails([FromBody] HearingdetailsSeachDto model)
        {

            var result = await _hearingdetailsService.GetPagedHearingDetails(model);
            return PartialView("_ListHearingDetails", result);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _hearingdetailsService.FetchSingleResult(id);
            ViewBag.ExistDocFile = Data.DocumentPatth;
            if (Data == null)
            {
                return NotFound();
            }

            return View(Data);
        }

        [HttpPost]
        //  [ValidateAntiForgeryToken]
        //  [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Hearingdetails rate)
        {
            ViewBag.ExistDocFile = rate.DocumentPatth;

            if (ModelState.IsValid)
            {
                FileHelper fileHelper = new FileHelper();
                if (rate.Photo != null)
                {
                    rate.DocumentPatth = rate.Photo != null ?
                                                                      fileHelper.SaveFile1(targetPathHearingDetails, rate.Photo) :
                                                                      rate.Photo != null || rate.DocumentPatth != "" ?
                                                                      rate.DocumentPatth : string.Empty;
                }
                try
                {

                    rate.ModifiedBy = SiteContext.UserId;
                    var result = await _hearingdetailsService.Update(id, rate);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                        //var list = await _hearingdetailsService.GetAllInterestrate();
                        //return View("Index", list);
                        return RedirectToAction("Index", "HearingDetails");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(rate);

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(rate);

                }
            }
            return View(rate);
        }
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        public async Task<IActionResult> Download(int Id)
        {
            string filename = _hearingdetailsService.GetDownload(Id);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> View(int id)
        {
            var Data = await _hearingdetailsService.FetchSingleHearingdetailswithReqProc(id);
           
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
           // return PartialView("ViewLetter");
        }
        public async Task<FileResult> ViewHearingDetailsDoc(int Id)
        {
            FileHelper file = new FileHelper();
            var Data = await _hearingdetailsService.FetchSingleResult(Id);
            string path = targetPathHearingDetails +"//"+ Data.DocumentPatth;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
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
