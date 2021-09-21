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
using Core.Enum;
using AcquiredLandInformationManagement.Filters;
using Dto.Master;
using Utility.Helper;
using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Http;

using System.IO;


namespace AcquiredLandInformationManagement.Controllers
{
    public class UnderSection6Master : BaseController
    {
        private readonly IUnderSection6Service _undersection4service;
        public IConfiguration _configuration;
        string DocumentFilePath = "";
        public object JsonRequestBehavior { get; private set; }

        public UnderSection6Master(IUnderSection6Service undersection4service, IConfiguration configuration)
        {
            _undersection4service = undersection4service;
            _configuration = configuration;
            DocumentFilePath = _configuration.GetSection("FilePaths:US6:DocumentFIlePath").Value.ToString();
        }

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {

            return View();
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] Undersection6SearchDto model)
        {
            var result = await _undersection4service.GetPagedUndersection6details(model);

            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Undersection6 undersection4 = new Undersection6();
            undersection4.IsActive = 1;
            undersection4.NotificationList = await _undersection4service.GetAllundersection4();

            return View(undersection4);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Undersection6 undersection6)
        {
            bool IsValidpdf = CheckMimeType(undersection6);
            try
            {
                undersection6.NotificationList = await _undersection4service.GetAllundersection4();

                if (ModelState.IsValid)
                {
                    if (IsValidpdf == true)
                        {
                        FileHelper fileHelper = new FileHelper();
                       undersection6.DocumentName = undersection6.DocumentIFormFile == null ? undersection6.DocumentName : fileHelper.SaveFile1(DocumentFilePath, undersection6.DocumentIFormFile);
                    undersection6.CreatedBy = SiteContext.UserId;
                    var result = await _undersection4service.Create(undersection6);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _undersection4service.GetAllUndersection6();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(undersection6);
                    }
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                    return View(undersection6);
                }
            }
                else
                {
                    return View(undersection6);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(undersection6);
            }
        }

        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _undersection4service.FetchSingleResult(id);

            Data.NotificationList = await _undersection4service.GetAllundersection4();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Undersection6 undersection6)
        {
            bool IsValidpdf = CheckMimeType(undersection6);
            undersection6.NotificationList = await _undersection4service.GetAllundersection4();

            if (ModelState.IsValid)
            {
                if (IsValidpdf == true)
                {
                    try
                    {
                        FileHelper fileHelper = new FileHelper();
                        undersection6.DocumentName = undersection6.DocumentIFormFile == null ? undersection6.DocumentName : fileHelper.SaveFile1(DocumentFilePath, undersection6.DocumentIFormFile);
                        undersection6.ModifiedBy = SiteContext.UserId;

                        var result = await _undersection4service.Update(id, undersection6);
                        if (result == true)
                        {
                            ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                            var list = await _undersection4service.GetAllUndersection6();
                            return View("Index", list);
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            return View(undersection6);
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(undersection6);
                    }
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                    return View(undersection6);
                }
            }
            else
            {
                return View(undersection6);
            }
        }


        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _undersection4service.Delete(id);
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
            var list = await _undersection4service.GetAllUndersection6();
            return View("Index", list);
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _undersection4service.FetchSingleResult(id);
            Data.NotificationList = await _undersection4service.GetAllundersection4();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.Download)]


        public async Task<IActionResult> Undersection6List()
        {
            var result = await _undersection4service.GetAllUndersection6();
            List<Undersection6ListDto> data = new List<Undersection6ListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new Undersection6ListDto()
                    {
                        Id = result[i].Id,
                        UnderSection4No = result[i].Undersection4 == null ? "" : result[i].Undersection4.Number,
                        NotificationNo = result[i].Number,
                        NotificationDate = Convert.ToDateTime(result[i].Ndate).ToString("dd-MMM-yyyy"),
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }


        public async Task<IActionResult> ViewUploadedDocument(int Id)
        {
            FileHelper file = new FileHelper();
            Undersection6 Data = await _undersection4service.FetchSingleResult(Id);
            string filename = DocumentFilePath + Data.DocumentName;
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, file.GetContentType(filename));
        }

        [HttpPost]
        public JsonResult CheckFile()
        {
            bool IsImg = true;
            string fullpath = string.Empty;
            //   string fullpath = string.Empty;
            string extension = string.Empty;
            DocumentFilePath = _configuration.GetSection("FilePaths:US6:DocumentFIlePath").Value.ToString();
            IFormFile files = Request.Form.Files["file"];
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                DocumentFilePath = _configuration.GetSection("FilePaths:US6:DocumentFIlePath").Value.ToString();
                string FilePath = Path.Combine(DocumentFilePath, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(DocumentFilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(DocumentFilePath);// Try to create the directory.
                    }
                    try
                    {
                        if (extension.ToLower() == ".pdf")
                        {
                            try
                            {
                                using (var stream = new FileStream(FilePath, FileMode.Create))
                                {
                                    files.CopyTo(stream);

                                }

                                iTextSharp.text.pdf.PdfReader oPdfReader = new iTextSharp.text.pdf.PdfReader(FilePath);
                                oPdfReader.Close();
                                fullpath = _configuration.GetSection("FilePaths:US6:DocumentFIlePath").Value.ToString();
                                FileInfo doc = new FileInfo(fullpath);
                                if (doc.Exists)
                                {
                                    doc.Delete();
                                }
                            }
                            catch (iTextSharp.text.exceptions.InvalidPdfException)
                            {
                                IsImg = false;
                            }

                        }
                    }
                    catch (OutOfMemoryException ex)
                    {
                        IsImg = false;

                        if (System.IO.File.Exists(fullpath))
                        {
                            try
                            {
                                System.IO.File.Delete(fullpath);
                            }
                            catch (Exception exs)
                            {
                            }
                        }
                        // Image.FromFile will throw this if file is invalid.  
                    }

                }
            }

            return Json(IsImg, JsonRequestBehavior);
        }


        public bool CheckMimeType(Undersection6 undersection6)
        {
            bool Flag = true;
            string fullpath = string.Empty;        
            string extension = string.Empty;
            DocumentFilePath = _configuration.GetSection("FilePaths:US6:DocumentFIlePath").Value.ToString();
            IFormFile files = undersection6.DocumentIFormFile;
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                DocumentFilePath = _configuration.GetSection("FilePaths:US6:DocumentFIlePath").Value.ToString();
                string FilePath = Path.Combine(DocumentFilePath, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(DocumentFilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(DocumentFilePath);// Try to create the directory.
                    }
                    try
                    {
                        if (extension.ToLower() == ".pdf")
                        {
                            try
                            {
                                using (var stream = new FileStream(FilePath, FileMode.Create))
                                {
                                    files.CopyTo(stream);

                                }

                                iTextSharp.text.pdf.PdfReader oPdfReader = new iTextSharp.text.pdf.PdfReader(FilePath);
                                oPdfReader.Close();
                                fullpath = _configuration.GetSection("FilePaths:US6:DocumentFIlePath").Value.ToString();
                                FileInfo doc = new FileInfo(fullpath);
                                if (doc.Exists)
                                {
                                    doc.Delete();
                                }
                            }
                            catch (iTextSharp.text.exceptions.InvalidPdfException)
                            {
                                Flag = false;
                            }

                        }
                    }
                    catch (OutOfMemoryException ex)
                    {
                        Flag = false;

                        if (System.IO.File.Exists(fullpath))
                        {
                            try
                            {
                                System.IO.File.Delete(fullpath);
                            }
                            catch (Exception exs)
                            {
                            }
                        }
                       
                    }

                }
            }

            return Flag;
        }




    }
}
