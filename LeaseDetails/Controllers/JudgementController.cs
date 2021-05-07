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
using Core.Enum;
using Dto.Master;
using System.Collections.Generic;
using System;

namespace LeaseDetails.Controllers
{
    public class JudgementController : BaseController
    {
        private readonly IJudgementService _judgementService;
        public IConfiguration _configuration;
        private readonly IRequestforproceedingService _requestforproceedingService;
        private readonly ICancellationEntryService _cancellationEntryService;
        string targetPathNotice = "";
        string targetPathDemandLetter = "";
        string targetPathNOC = "";
        string targetPathCanellationOrder = "";
        public JudgementController(IJudgementService judgementService,
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
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] RequestForProceedingSearchDto model)
        {
            var result = await _judgementService.GetPagedRequestForProceeding(model);

            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id)
        {
            var Data = await _judgementService.FetchSingleResult(id);
            
            if (Data == null)
            {
                Judgement model = new Judgement();
                model.UserNameList = await _judgementService.BindUsernameNameList();
                model.JudgementStatusList = await _judgementService.GetJudgementStatusList();
                model.RequestForProceedingId = id;
                return View(model);
            }
            else 
            { 
                Data.UserNameList = await _judgementService.BindUsernameNameList();
                Data.JudgementStatusList = await _judgementService.GetJudgementStatusList();
                return View(Data);
            }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id,Judgement model)
        {
            string Document = _configuration.GetSection("FilePaths:Judgement:Doc").Value.ToString();
            FileHelper fileHelper = new FileHelper();


            if (ModelState.IsValid)
                {
                var Data = await _judgementService.FetchSingleResult(id);
                if (Data == null)
                {
                    if (model.File != null)
                    {
                        model.FilePath = fileHelper.SaveFile(Document, model.File);
                    }
                    model.UserNameList = await _judgementService.BindUsernameNameList();
                    model.JudgementStatusList = await _judgementService.GetJudgementStatusList();
                    model.RequestForProceedingId = id;
                    model.Id = 0;
                    model.CreatedBy = SiteContext.UserId;
                    model.IsActive = 1;
                    var result = await _judgementService.Create(model);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);

                        return View(model);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(model);

                    }
                }
                else 
                {
                    if (model.File != null)
                    {
                        model.FilePath = fileHelper.SaveFile(Document, model.File);
                    }
                    model.UserNameList = await _judgementService.BindUsernameNameList();
                    model.JudgementStatusList = await _judgementService.GetJudgementStatusList();

                    model.ModifiedBy = SiteContext.UserId;
                    model.IsActive = 1;
                    var result = await _judgementService.Update(id, model);
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
                    
                }
                else
                {
                    return View(model);
                }
           
        }
        public IActionResult ViewLetter()
        {
            return View();
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


        [AuthorizeContext(ViewAction.Download)]

        public async Task<IActionResult> JudgementList()
        {
            var result = await _judgementService.GetAllJudgementIndex();
            List<JudgementListDto> data = new List<JudgementListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new JudgementListDto()
                    {
                        Id = result[i].Id,
                        ReferenceNo = result[i].Allotment == null ? "" : result[i].Allotment.Application.RefNo,
                        SocietyName = result[i].Allotment == null ? "" : result[i].Allotment.Application.Name,
                        AllotmentDate = Convert.ToDateTime(result[i].Allotment.AllotmentDate).ToString("dd-MMM-yyyy"),
                        Area = result[i].Allotment == null ? "" : result[i].Allotment.TotalArea.ToString(),
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",

                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }
    }
}
