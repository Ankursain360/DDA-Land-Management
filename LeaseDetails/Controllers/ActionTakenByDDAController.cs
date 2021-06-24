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
using System;
using Dto.Master;
using System.Collections.Generic;

namespace LeaseDetails.Controllers
{
    public class ActionTakenByDDAController : BaseController
    {
        private readonly IActiontakenbyddaService _actiontakenbyddaService;
        private readonly IRequestforproceedingService _requestforproceedingService;
        private readonly ICancellationEntryService _cancellationEntryService;
        public IConfiguration _configuration;
        string targetPathNotice = "";
       
        string targetPathDemandLetter = "";
        string targetPathNOC = "";
        string targetPathCanellationOrder = "";
        public ActionTakenByDDAController(IActiontakenbyddaService actiontakenbyddaService, IConfiguration configuration, IRequestforproceedingService requestforproceedingService, ICancellationEntryService cancellationEntryService)
        {
            _requestforproceedingService = requestforproceedingService;
            _actiontakenbyddaService = actiontakenbyddaService;
            _configuration = configuration;
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
        public async Task<PartialViewResult> List([FromBody] ActionTakenByDDASearchDto model)
        {
            var result = await _actiontakenbyddaService.GetPagedRequestForProceeding(model);

            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id)
        {
            var Data = await _actiontakenbyddaService.FetchSingleResult(id);

            if (Data == null)
            {
                Actiontakenbydda model = new Actiontakenbydda();
                model.UserNameList = await _actiontakenbyddaService.BindUsernameNameList();
                model.RequestForProceedingId = id;
                return View(model);
            }
            else
            {
                Data.UserNameList = await _actiontakenbyddaService.BindUsernameNameList();
                return View(Data);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id, Actiontakenbydda model)
        {
            string Documentt = _configuration.GetSection("FilePaths:ActionTakenByDDA:Doc").Value.ToString();
            FileHelper fileHelper = new FileHelper();


            if (ModelState.IsValid)
            {
                var Data = await _actiontakenbyddaService.FetchSingleResult(id);
                if (Data == null)
                {
                    if (model.File != null)
                    {
                        model.Document = fileHelper.SaveFile(Documentt, model.File);
                    }
                    model.UserNameList = await _actiontakenbyddaService.BindUsernameNameList();
                    model.RequestForProceedingId = id;
                    model.Id = 0;
                    model.CreatedBy = SiteContext.UserId;
                    model.IsActive = 1;
                    var result = await _actiontakenbyddaService.Create(model);

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
                        model.Document = fileHelper.SaveFile(Documentt, model.File);
                    }
                    model.UserNameList = await _actiontakenbyddaService.BindUsernameNameList();


                    model.ModifiedBy = SiteContext.UserId;
                    model.IsActive = 1;
                    var result = await _actiontakenbyddaService.Update(id, model);
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
        //public IActionResult ViewLetter()
        //{
        //    return View();
        //}
        public async Task<IActionResult> DownloadJudgementFile(int Id)
        {
            FileHelper file = new FileHelper();
            Actiontakenbydda Data = await _actiontakenbyddaService.FetchSingleResult(Id);
            string filename = Data.Document;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        //public async Task<PartialViewResult> RequestForProceedingEvictionView(int id)
        //{
        //    var Data = await _actiontakenbyddaService.FetchSingleReqDetails(id);
        //    Data.HonbleList = await _actiontakenbyddaService.GetAllHonble();
        //    Data.AllotmententryList = await _actiontakenbyddaService.GetAllAllotment();
        //    Data.UserNameList = await _actiontakenbyddaService.BindUsernameNameList();

        //    return PartialView("_RequestForProceedingEvictionView", Data);
        //}



        public async Task<PartialViewResult> NoticeGenerationView(int id)
        {
            var Data = await _actiontakenbyddaService.FetchNoticeGenerationDetails(id);

            return PartialView("_ListNoticeGenertion", Data);
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
        public async Task<FileResult> ViewNotice(int Id)
        {
            FileHelper file = new FileHelper();
            var Data = await _actiontakenbyddaService.FetchSingleNotice(Id);
            string path = targetPathNotice + Data.NoticeFileName;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }


        public async Task<PartialViewResult> AllotteeEvidenceView(int id)
        {
            var Data = await _actiontakenbyddaService.FetchAllotteeEvidenceDetails(id);

            return PartialView("_AllotteeEvidence", Data);
        }
        public async Task<FileResult> ViewEvidence(int Id)
        {
            FileHelper file = new FileHelper();
            var Data = await _actiontakenbyddaService.FetchSingleEvidence(Id);
            string path = targetPathNotice + Data.DocumentPatth;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        public async Task<PartialViewResult> HearingDetailsView(int id)
        {
            var Data = await _actiontakenbyddaService.FetchHearingDetails(id);

            return PartialView("_HearingDetails", Data);
        }
      //  [AuthorizeContext(ViewAction.Download)]

        public async Task<IActionResult> ActionTakenByDDAList()
        {
            var result = await _actiontakenbyddaService.GetAllActionIndex();
            List<ActionTakenByDDAListDto> data = new List<ActionTakenByDDAListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new ActionTakenByDDAListDto()
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
