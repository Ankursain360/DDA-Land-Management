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
using System;

namespace LeaseDetails.Controllers
{
    public class HearingDetailsController : BaseController
    {
        private readonly IHearingdetailsService _hearingdetailsService;
        public IConfiguration _configuration;
        string targetPathNotice = "";
        public HearingDetailsController(IHearingdetailsService hearingdetailsService, IConfiguration configuration)
        {
            _hearingdetailsService = hearingdetailsService;
            _configuration = configuration;
            targetPathNotice = _configuration.GetSection("FilePaths:NoticeGeneration:NoticeGenerationPath").Value.ToString();

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] RequestForProceedingSearchDto model)
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
                model.NoticeGenId = 3;
                model.EvidanceDocId = 1;
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
        //public async Task<IActionResult> DownloadJudgementFile(int Id)
        //{
        //    FileHelper file = new FileHelper();
        //    Hearingdetailsphotofiledetails Data = await _hearingdetailsService..FetchSingleResult(Id);
        //   // string filename = Data.FilePath;
        //  //  return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        //}
        public async Task<PartialViewResult> RequestForProceedingEvictionView(int id)
        {
            var Data = await _hearingdetailsService.FetchSingleReqDetails(id);
            Data.HonbleList = await _hearingdetailsService.GetAllHonble();
            Data.AllotmententryList = await _hearingdetailsService.GetAllAllotment();


            return PartialView("_RequestForProceedingEvictionView", Data);
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

            if (ModelState.IsValid)
            {
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
    }
}
