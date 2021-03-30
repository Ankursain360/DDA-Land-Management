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
        private readonly IRequestforproceedingService _requestforproceedingService;
        private readonly IHostingEnvironment _hostingEnvironment;

        string targetPathNotice = "";
        public NoticeGenerationController(INoticeGenerationService noticeGenerationService,
            ILeaseHearingDetailsService leaseHearingDetailsService,
            IConfiguration configuration,
            IApprovalProccessService approvalproccessService, IWorkflowTemplateService workflowtemplateService,
            IRequestforproceedingService requestforproceedingService,
            IHostingEnvironment en)
        {
            _noticeGenerationService = noticeGenerationService;
            _leaseHearingDetailsService = leaseHearingDetailsService;
            _configuration = configuration;
            _workflowtemplateService = workflowtemplateService;
            _approvalproccessService = approvalproccessService;
            _requestforproceedingService = requestforproceedingService;
            _hostingEnvironment = en;
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
            Data.LeaseNoticeGenerationList = await _noticeGenerationService.GetNoticeHistoryDetails(id);
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
                leasenoticegeneration.LeaseNoticeGenerationList = await _noticeGenerationService.GetNoticeHistoryDetails(leasenoticegeneration.RequestProceedingId);
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
                    var finalString = (DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond).ToUpper();
                    leasenoticegeneration.NoticeReferenceNo = leasenoticegeneration.RequestProceedingId + finalString;
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
                    var Data = await _noticeGenerationService.FetchNoticeGenerationDetails(leasenoticegeneration.Id);
                    string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "NoticeDetails.html");
                    var Body = PopulateBody(Data, path);
                    Leasenoticegeneration emptyData = new Leasenoticegeneration();
                    emptyData.LeaseNoticeGenerationList = await _noticeGenerationService.GetNoticeHistoryDetails(leasenoticegeneration.RequestProceedingId);
                    emptyData.RequestProceedingId = leasenoticegeneration.RequestProceedingId;
                    if (leasenoticegeneration.GenerateUpload == 0)
                    {
                        ViewBag.IsVisible = true;
                        ViewBag.DataLetter = Body;
                        ViewBag.Message = Alert.Show("Generate Letter Successfully", "", AlertType.Success);
                        ViewBag.PrimaryId = 0;
                        return View("Create",emptyData);
                    }
                    else
                    {
                        ViewBag.IsVisible = false;
                        ViewBag.Message = Alert.Show("File Uploaded Successfully", "", AlertType.Success);
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
                return View(leasenoticegeneration);
            }
        }
        private string PopulateBody(Leasenoticegeneration leasenoticegeneration, string Path)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Path))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{LetterReferenceNo}", (leasenoticegeneration.NoticeReferenceNo == null ? "" : leasenoticegeneration.NoticeReferenceNo));
            body = body.Replace("{TodayDatetime}", Convert.ToDateTime((DateTime.Now)).ToString("dd-MMM-yyyy"));
            body = body.Replace("{SocietyName}", (leasenoticegeneration.RequestProceeding == null ? "" :
                                                    leasenoticegeneration.RequestProceeding.Allotment == null ? "" :
                                                    leasenoticegeneration.RequestProceeding.Allotment.Application == null ? "" :
                                                    leasenoticegeneration.RequestProceeding.Allotment.Application.Name == null ? "" :
                                                    leasenoticegeneration.RequestProceeding.Allotment.Application.Name));
            body = body.Replace("{Address}", (leasenoticegeneration.RequestProceeding == null ? "" :
                                                    leasenoticegeneration.RequestProceeding.Allotment == null ? "" :
                                                    leasenoticegeneration.RequestProceeding.Allotment.Application == null ? "" :
                                                    leasenoticegeneration.RequestProceeding.Allotment.Application.Address == null ? "" :
                                                    leasenoticegeneration.RequestProceeding.Allotment.Application.Address));
            body = body.Replace("{MeetingDate}", (leasenoticegeneration.MeetingDate == null ? "" : Convert.ToDateTime((leasenoticegeneration.MeetingDate)).ToString("dd-MMM-yyyy")));
            body = body.Replace("{MeetingTime}", (leasenoticegeneration.MeetingTime == null ? "" : leasenoticegeneration.MeetingTime));
            body = body.Replace("{MeetingPlace}", (leasenoticegeneration.MeetingPlace == null ? "" : leasenoticegeneration.MeetingPlace));


            return body;
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

        #region RequestForProceedingEviction Details
        public async Task<PartialViewResult> RequestForProceedingEvictionView(int id)
        {
            var Data = await _requestforproceedingService.FetchSingleResult(id);
            Data.HonbleList = await _requestforproceedingService.GetAllHonble();
            Data.AllotmententryList = await _requestforproceedingService.GetAllAllotment();
            Data.UserNameList = await _requestforproceedingService.BindUsernameNameList();

            return PartialView("_RequestForProceedingEvictionView", Data);
        }
        public async Task<FileResult> ViewLetter(int Id)
        {
            FileHelper file = new FileHelper();
            var Data = await _requestforproceedingService.FetchSingleResult(Id);
            string targetPhotoPathLayout = Data.DemandLetter;
            byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
            return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
        }


        public async Task<FileResult> ViewLetter1(int Id)
        {
            try
            {
                FileHelper file = new FileHelper();
                var Data = await _requestforproceedingService.FetchSingleResult(Id);
                string targetPhotoPathLayout = Data.Noc;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
            }
            catch (Exception ex)
            {

                FileHelper file = new FileHelper();
                var Data = await _requestforproceedingService.FetchSingleResult(Id);
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
                var Data = await _requestforproceedingService.FetchSingleResult(Id);
                string targetPhotoPathLayout = Data.CancellationOrder;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
            }
            catch (Exception ex)
            {
                FileHelper file = new FileHelper();
                var Data = await _requestforproceedingService.FetchSingleResult(Id);
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

        public async Task<FileResult> ViewNotice(int Id)
        {
            FileHelper file = new FileHelper();
            var Data = await _noticeGenerationService.FetchNoticeGenerationDetails(Id);
            string path = targetPathNotice + Data.NoticeFileName;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
    }
}

