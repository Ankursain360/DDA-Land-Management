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
using AcquiredLandInformationManagement.Filters;
using Core.Enum;
using Utility.Helper;
using Dto.Master;
using Microsoft.Extensions.Configuration;




using Microsoft.AspNetCore.Http;

using System.IO;





namespace AcquiredLandInformationManagement.Controllers
{
    public class UnderSection17DetailsController : BaseController
    {
        private readonly IUndersection17Service _undersection17Service;
        public IConfiguration _configuration;
        string DocumentFilePath = "";
        public object JsonRequestBehavior { get; private set; }



        public UnderSection17DetailsController(IUndersection17Service undersection17Service, IConfiguration configuration)
        {
          _undersection17Service = undersection17Service;
            _configuration = configuration;
            DocumentFilePath = _configuration.GetSection("FilePaths:US17:DocumentFIlePath").Value.ToString();
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            var list = await _undersection17Service.GetAllUndersection17();
            return View(list);
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] UnderSection17SearchDto model)
        {
            var result = await _undersection17Service.GetPagedUndersection17(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()

        {
            Undersection17 undersection17 = new Undersection17();
            undersection17.IsActive = 1;


            undersection17.Undersection6List = await _undersection17Service.GetAllUndersection6List();
            return View(undersection17);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Undersection17 undersection17)
        {
            bool IsValidpdf = CheckMimeType(undersection17);
            try
            {
                undersection17.Undersection6List = await _undersection17Service.GetAllUndersection6List();

                if (ModelState.IsValid)
                {
                    if (IsValidpdf == true)
                    {
                        FileHelper fileHelper = new FileHelper();
                        undersection17.DocumentName = undersection17.DocumentIFormFile == null ? undersection17.DocumentName : fileHelper.SaveFile1(DocumentFilePath, undersection17.DocumentIFormFile);
                        undersection17.CreatedBy = SiteContext.UserId;
                        var result = await _undersection17Service.Create(undersection17);

                        if (result == true)
                        {
                            ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                            var list = await _undersection17Service.GetAllUndersection17();
                            return View("Index", list);
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            return View(undersection17);
                        }
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                        return View(undersection17);

                    }
                }
                else
                {
                    return View(undersection17);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(undersection17);
            }
        }



        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _undersection17Service.FetchSingleResult(id);
           

            Data.Undersection6List = await _undersection17Service.GetAllUndersection6List();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Undersection17 undersection17)
        {
            bool IsValidpdf = CheckMimeType(undersection17);
            undersection17.Undersection6List = await _undersection17Service.GetAllUndersection6List();
            if (ModelState.IsValid)
            {
                if (IsValidpdf == true)
                {
                    try
                    {
                        FileHelper fileHelper = new FileHelper();
                        undersection17.DocumentName = undersection17.DocumentIFormFile == null ? undersection17.DocumentName : fileHelper.SaveFile1(DocumentFilePath, undersection17.DocumentIFormFile);
                        undersection17.ModifiedBy = SiteContext.UserId;

                        var result = await _undersection17Service.Update(id, undersection17);
                        if (result == true)
                        {
                            ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                            var list = await _undersection17Service.GetAllUndersection17();
                            return View("Index", list);
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            return View(undersection17);
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(undersection17);
                    }
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                    return View(undersection17);

                }
            }
            else
            {
                return View(undersection17);
            }
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _undersection17Service.Delete(id);
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
            var list = await _undersection17Service.GetAllUndersection17();
            return View("Index", list);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _undersection17Service.FetchSingleResult(id);

         
            Data.Undersection6List = await _undersection17Service.GetAllUndersection6List();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Undersection17List()
        {
            var result = await _undersection17Service.GetAllUndersection17();
            List<Undersection17ListDto> data = new List<Undersection17ListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new Undersection17ListDto()
                    {
                        Id = result[i].Id,
                        NotificationNo = result[i].Number,
                        NotificationDate = Convert.ToDateTime(result[i].NotificationDate).ToString("dd-MMM-yyyy"),
                        NotificationUS6 = result[i].UnderSection6 == null ? "" : result[i].UnderSection6.Number,
                        
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
            Undersection17 Data = await _undersection17Service.FetchSingleResult(Id);
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
            DocumentFilePath = _configuration.GetSection("FilePaths:US17:DocumentFIlePath").Value.ToString();
            IFormFile files = Request.Form.Files["file"];
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                DocumentFilePath = _configuration.GetSection("FilePaths:US17:DocumentFIlePath").Value.ToString();
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
                                fullpath = _configuration.GetSection("FilePaths:US17:DocumentFIlePath").Value.ToString();
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



        public bool CheckMimeType(Undersection17 undersection17)
        {
            bool Flag = true;
            string fullpath = string.Empty;          
            string extension = string.Empty;
            DocumentFilePath = _configuration.GetSection("FilePaths:US17:DocumentFIlePath").Value.ToString();
            IFormFile files = undersection17.DocumentIFormFile;
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                DocumentFilePath = _configuration.GetSection("FilePaths:US17:DocumentFIlePath").Value.ToString();
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
                                fullpath = _configuration.GetSection("FilePaths:US17:DocumentFIlePath").Value.ToString();
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
                        // Image.FromFile will throw this if file is invalid.  
                    }

                }
            }

            return Flag;
        }
    }
}

