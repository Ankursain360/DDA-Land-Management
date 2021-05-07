using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;
using Microsoft.Extensions.Configuration;
using Utility.Helper;
using Microsoft.AspNetCore.Http;
using System.IO;
using LeaseDetails.Filters;
using Core.Enum;
using Dto.Master;

namespace LeaseDetails.Controllers
{
    public class RequestForProceedingEviction : BaseController
    {
        private readonly IRequestforproceedingService _requestforproceedingService;
        private readonly ICancellationEntryService _cancellationEntryService;
        public IConfiguration _configuration;

        string targetPathDemandLetter = "";
        string targetPathNOC = "";
        string targetPathCanellationOrder = "";
        public RequestForProceedingEviction(IRequestforproceedingService requestforproceedingService,
            ICancellationEntryService cancellationEntryService, 
            IConfiguration configuration)
        {
            _configuration = configuration;
            _cancellationEntryService = cancellationEntryService;
            _requestforproceedingService = requestforproceedingService;
            targetPathDemandLetter = _configuration.GetSection("FilePaths:CancellationEntry:DemandletterFilePath").Value.ToString();
            targetPathNOC = _configuration.GetSection("FilePaths:CancellationEntry:NOCFilePath").Value.ToString();
            targetPathCanellationOrder = _configuration.GetSection("FilePaths:CancellationEntry:CancellationOrderFilePath").Value.ToString();
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            var Msg = TempData.Peek("Message");
            if (Msg != null)
                ViewBag.Message = Msg;
            var list = await _requestforproceedingService.GetAllRequestForProceeding();
            return View(list);
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] RequestForProceedingSearchDto model)
        {
            var result = await _requestforproceedingService.GetPagedRequestForProceeding(model);

            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Requestforproceeding requestforproceeding = new Requestforproceeding();
            requestforproceeding.IsActive = 1;
            requestforproceeding.HonbleList = await _requestforproceedingService.GetAllHonble();
            requestforproceeding.CancellationList = await _requestforproceedingService.GetCancellationListData();
            requestforproceeding.UserNameList = await _requestforproceedingService.BindUsernameNameList();
            return View(requestforproceeding);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Requestforproceeding requestforproceeding)
        {
            try
            {
                requestforproceeding.CancellationList = await _requestforproceedingService.GetCancellationListData();
                requestforproceeding.UserNameList = await _requestforproceedingService.BindUsernameNameList();
                requestforproceeding.HonbleList = await _requestforproceedingService.GetAllHonble();

                if (ModelState.IsValid)
                {
                    FileHelper fileHelper = new FileHelper();
                    if (requestforproceeding.DemandLetterPhoto != null)
                    {
                        requestforproceeding.DemandLetter = fileHelper.SaveFile1(targetPathDemandLetter, requestforproceeding.DemandLetterPhoto);
                    }
                    if (requestforproceeding.NocPhoto != null)
                    {
                        requestforproceeding.Noc = fileHelper.SaveFile1(targetPathNOC, requestforproceeding.NocPhoto);
                    }
                    if (requestforproceeding.CancellationPhoto != null)
                    {
                        requestforproceeding.CancellationOrder = fileHelper.SaveFile1(targetPathCanellationOrder, requestforproceeding.CancellationPhoto);
                    }
                    requestforproceeding.CreatedBy = SiteContext.UserId;

                    var result = await _requestforproceedingService.Create(requestforproceeding);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _requestforproceedingService.GetAllRequestForProceeding();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(requestforproceeding);
                    }
                }
                else
                {
                    return View(requestforproceeding);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(requestforproceeding);
            }
        }

        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _requestforproceedingService.FetchSingleResult(id);
            Data.HonbleList = await _requestforproceedingService.GetAllHonble();
            Data.CancellationList = await _requestforproceedingService.GetCancellationListData();
            Data.UserNameList = await _requestforproceedingService.BindUsernameNameList();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Requestforproceeding requestforproceeding)
        {
            requestforproceeding.CancellationList = await _requestforproceedingService.GetCancellationListData();
            requestforproceeding.UserNameList = await _requestforproceedingService.BindUsernameNameList();
            requestforproceeding.HonbleList = await _requestforproceedingService.GetAllHonble();

            FileHelper fileHelper = new FileHelper();
            if (requestforproceeding.DemandLetterPhoto != null)
            {
                requestforproceeding.DemandLetter = fileHelper.SaveFile1(targetPathDemandLetter, requestforproceeding.DemandLetterPhoto);
            }
            if (requestforproceeding.NocPhoto != null)
            {
                requestforproceeding.Noc = fileHelper.SaveFile1(targetPathNOC, requestforproceeding.NocPhoto);
            }
            if (requestforproceeding.CancellationPhoto != null)
            {
                requestforproceeding.CancellationOrder = fileHelper.SaveFile1(targetPathCanellationOrder, requestforproceeding.CancellationPhoto);
            }

            try
            {
                var result = await _requestforproceedingService.Update(id, requestforproceeding);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    var list = await _requestforproceedingService.GetAllRequestForProceeding();
                    return View("Index", list);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(requestforproceeding);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(requestforproceeding);
            }
            //}
            //    else
            //    {
            return View(requestforproceeding);
            //  }
        }



        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _requestforproceedingService.FetchSingleResult(id);
            Data.HonbleList = await _requestforproceedingService.GetAllHonble();
            Data.CancellationList = await _requestforproceedingService.GetCancellationListData();
            Data.UserNameList = await _requestforproceedingService.BindUsernameNameList();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _requestforproceedingService.Delete(id);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            var list = await _requestforproceedingService.GetAllRequestForProceeding();
            return View("Index", list);
        }

        public async Task<FileResult> ViewLetter(int Id)
        {
            try
            {
                FileHelper file = new FileHelper();
                var Data = await _cancellationEntryService.FetchSingleResult(Id);
                string targetPhotoPathLayout = targetPathDemandLetter + Data.DemandLetter;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
            }
            catch (Exception ex)
            {

                FileHelper file = new FileHelper();
                var Data = await _cancellationEntryService.FetchSingleResult(Id);
                string targetPhotoPathLayout = targetPathDemandLetter + Data.DemandLetter;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));

            }
        }

        public async Task<FileResult> ViewLetter1(int Id)
        {
            try
            {
                FileHelper file = new FileHelper();
                var Data = await _cancellationEntryService.FetchSingleResult(Id);
                string targetPhotoPathLayout = targetPathNOC + Data.Noc;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
            }
            catch (Exception ex)
            {

                FileHelper file = new FileHelper();
                var Data = await _cancellationEntryService.FetchSingleResult(Id);
                string targetPhotoPathLayout = targetPathNOC + Data.Noc;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));

            }
        }
        public async Task<FileResult> ViewLetter2(int Id)
        {
            try
            {
                FileHelper file = new FileHelper();
                var Data = await _cancellationEntryService.FetchSingleResult(Id);
                string targetPhotoPathLayout = targetPathCanellationOrder + Data.CancellationOrder;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
            }
            catch (Exception ex)
            {

                FileHelper file = new FileHelper();
                var Data = await _cancellationEntryService.FetchSingleResult(Id);
                string targetPhotoPathLayout = targetPathCanellationOrder + Data.CancellationOrder;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));

            }
        }

        [HttpGet]
        public async Task<JsonResult> GetRelativeData(int? CancellationId)
        {
            CancellationId = CancellationId ?? 0;
            return Json(await _requestforproceedingService.FetchCancellationDetailsDetails(Convert.ToInt32(CancellationId)));
        }


        [AuthorizeContext(ViewAction.Download)]

        public async Task<IActionResult> RequestforproceedingList()
        {
            var result = await _requestforproceedingService.GetAllRequestForProceeding();
            List<RequestforproceedingListDto> data = new List<RequestforproceedingListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new RequestforproceedingListDto()
                    {
                        Id = result[i].Id,
                        ReferenceNo = result[i].Allotment == null ? "" : result[i].Allotment.Application.RefNo,
                        SocietyName = result[i].Allotment == null ? "" : result[i].Allotment.Application.Name,
                        AllotmentDate = Convert.ToDateTime(result[i].Allotment.AllotmentDate).ToString("dd-MMM-yyyy"),
                        Area = result[i].Allotment == null ? "" : result[i].Allotment.TotalArea.ToString(),
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                        LetterStatus = result[i].Status.ToString() == "1" ? "Generated":
                                       result[i].Status.ToString() == "2" ?  "Uploaded":
                                       result[i].Status.ToString() =="3" ? "Sent" : "Not Generated"

                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }

   
}
}
