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
    public class AllotmentLetterController : BaseController
    {

        private readonly ILeaseApplicationFormService _leaseApplicationFormService;
        private readonly IAllotmentLetterService _allotmentLetterService;
        public IConfiguration _configuration;
        string targetPathAllotmentLetter = "";

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] AllotmentLetterSeearchDto model)
        {
            var result = await _allotmentLetterService.GetPagedAllotmentLetter(model);
            return PartialView("_List", result);
        }

        public AllotmentLetterController(ILeaseApplicationFormService leaseApplicationFormService, IAllotmentLetterService allotmentLetterService, IConfiguration configuration)
        {
            _leaseApplicationFormService = leaseApplicationFormService;
            _allotmentLetterService = allotmentLetterService;
            _configuration = configuration;
            targetPathAllotmentLetter = _configuration.GetSection("FilePaths:Allotmentletter:AllotmentletterFilePath").Value.ToString();
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Allotmentletter allotmentletter = new Allotmentletter();
            allotmentletter.RefNoList = await _leaseApplicationFormService.GetRefNoListforAllotmentLetterAtCreate();
            return View(allotmentletter);
        }

        public async Task<IActionResult> Receipt(int? ApplicationId)
        {
            // var Data = await _leaseApplicationFormService.FetchLeaseApplicationDetailsforAllotmentLetter(ApplicationId ?? 0);
            var Data = await _allotmentLetterService.FetchSingleAllotmentLetterDetails(ApplicationId??0);
            return PartialView("Receipt", Data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id, Allotmentletter model)
        {

            // Allotmentletter model= new Allotmentletter();
            model.RefNoList = await _leaseApplicationFormService.GetRefNoListforAllotmentLetterAtCreate();

            if (ModelState.IsValid)
            {
                model.CreatedBy = SiteContext.UserId;
                model.IsActive = 1;
                DateTime theDate = DateTime.Now;
                model.DemandPeriodStart = theDate;
                DateTime yearInTheFuture = theDate.AddYears(1);
                model.DemandPeriodEnd = yearInTheFuture;
                var result = await _allotmentLetterService.Create(model);

                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                   

                }
            }


            //  return View(model);
            var Data = await _allotmentLetterService.FetchAllotmentLetterDetails(model.AllotmentId);
            return PartialView("Receipt", Data);
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _allotmentLetterService.FetchSingleAllotmentLetterDetails(id);
            Data.RefNoList = await _allotmentLetterService.GetRefNoListforAllotmentLetter();
            ViewBag.ExistDocFile = Data.FilePath;
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Allotmentletter allotmentletter)
        {
            allotmentletter.RefNoList = await _allotmentLetterService.GetRefNoListforAllotmentLetter();

            if (ModelState.IsValid)
            {
                FileHelper fileHelper = new FileHelper();
                if (allotmentletter.DocFile != null)
                {
                    allotmentletter.FilePath = allotmentletter.DocFile != null ?
                                                                      fileHelper.SaveFile1(targetPathAllotmentLetter, allotmentletter.DocFile) :
                                                                      allotmentletter.DocFile != null || allotmentletter.FilePath != "" ?
                                                                      allotmentletter.FilePath : string.Empty;
                }
                try
                {

                    allotmentletter.ModifiedBy = SiteContext.UserId;
                    var result = await _allotmentLetterService.Update(id, allotmentletter);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        return RedirectToAction("Index", "AllotmentLetter");
                        //var list = await _possesionplanService.GetAllPossesionplan();
                        //return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(allotmentletter);

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(allotmentletter);

                }
            }
            return View(allotmentletter);
        }
        public async Task<IActionResult> UploadAllotmentLetter(int id, Allotmentletter allotmentletter)
        {
            ViewBag.ExistDocFile = allotmentletter.FilePath;

            var Data = await _allotmentLetterService.FetchSingleAllotmentLetterDetails(id);


            if (ModelState.IsValid)
            {
                FileHelper fileHelper = new FileHelper();
                if (allotmentletter.DocFile != null)
                {
                    allotmentletter.FilePath = allotmentletter.DocFile != null ?
                                                                      fileHelper.SaveFile1(targetPathAllotmentLetter, allotmentletter.DocFile) :
                                                                      allotmentletter.DocFile != null || allotmentletter.FilePath != "" ?
                                                                      allotmentletter.FilePath : string.Empty;
                }
                try
                {
                    allotmentletter.ModifiedBy = SiteContext.UserId;

                    var result = await _allotmentLetterService.Update(id, allotmentletter);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);


                    }

                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(allotmentletter);

                }
            }

            return RedirectToAction("Create", "AllotmentLetter");

        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        public async Task<IActionResult> Download(int Id)
        {
            FileHelper file = new FileHelper();
            var Data = await _allotmentLetterService.FetchSingleAllotmentLetterDetails(Id);
            string path = targetPathAllotmentLetter + "//" + Data.FilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
            //string filename = _allotmentLetterService.GetDownload(Id);
            //var memory = new MemoryStream();
            //using (var stream = new FileStream(filename, FileMode.Open))
            //{
            //    await stream.CopyToAsync(memory);
            //}
            //memory.Position = 0;
            //return File(memory, GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<FileResult> ViewAllotmentLetterDoc(int Id)
        {
            FileHelper file = new FileHelper();
            var Data = await _allotmentLetterService.FetchSingleAllotmentLetterDetails(Id);
            string path = targetPathAllotmentLetter + "//" + Data.FilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
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
    }
}
