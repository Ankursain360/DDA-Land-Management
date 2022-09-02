using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using AcquiredLandInformationManagement.Models;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
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
   
    public class NazulController : Controller
    {
        public object JsonRequestBehavior { get; private set; }

        public IConfiguration _configuration;

        private readonly INazulService _nazulService;
        string DocumentFilePath = "";
        string DocumentSizraFilePath = "";
        public NazulController(INazulService nazulService, IConfiguration configuration)
        {
            _nazulService = nazulService;
            _configuration = configuration;
            DocumentFilePath = _configuration.GetSection("FilePaths:Nazul:DocumentFIlePath").Value.ToString();
            DocumentSizraFilePath = _configuration.GetSection("FilePaths:SizraNazul:DocumentFIlesPath").Value.ToString();
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
          //  var result = await _nazulService.GetAllNazul();
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NazulSearchDto model)
        {
            var result = await _nazulService.GetPagedNazul(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Nazul nazul = new Nazul();
            nazul.IsActive = 1;
           

            nazul.VillageList = await _nazulService.GetAllVillageList();
            return View(nazul);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Nazul nazul)
        {
            try
            {
                bool IsValidpdf = CheckMimeType(nazul);
                bool IsValidpdf1 = CheckMimeType1(nazul);
                nazul.VillageList = await _nazulService.GetAllVillageList();

                if (ModelState.IsValid)
                {
                    if (IsValidpdf == true)
                    {
                        if (IsValidpdf1 == true)
                        {

                            FileHelper fileHelper = new FileHelper();
                        nazul.DocumentName = nazul.DocumentIFormFile == null ? nazul.DocumentName : fileHelper.SaveFile1(DocumentFilePath, nazul.DocumentIFormFile);
                        nazul.DocumentNameSizra = nazul.DocumentSizraIFormFile == null ? nazul.DocumentNameSizra : fileHelper.SaveFile1(DocumentSizraFilePath, nazul.DocumentSizraIFormFile);
                        var result = await _nazulService.Create(nazul);

                        if (result == true)
                        {
                            ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                            var list = await _nazulService.GetAllNazul();
                            return View("Index", list);
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            return View(nazul);
                         }
                      }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                            return View(nazul);
                        }
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                        return View(nazul);
                    }
                }
                else
                {
                    return View(nazul);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(nazul);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _nazulService.FetchSingleResult(id);
            Data.VillageList = await _nazulService.GetAllVillageList();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Nazul nazul)
        {
            bool IsValidpdf = CheckMimeType(nazul);
            bool IsValidpdf1 = CheckMimeType1(nazul);
            nazul.VillageList = await _nazulService.GetAllVillageList();
            if (ModelState.IsValid)
            {
                if (IsValidpdf == true)
                {
                    if (IsValidpdf1 == true)
                    {
                        FileHelper fileHelper = new FileHelper();
                        nazul.DocumentName = nazul.DocumentIFormFile == null ? nazul.DocumentName : fileHelper.SaveFile1(DocumentFilePath, nazul.DocumentIFormFile);
                        nazul.DocumentNameSizra = nazul.DocumentSizraIFormFile == null ? nazul.DocumentNameSizra : fileHelper.SaveFile1(DocumentSizraFilePath, nazul.DocumentSizraIFormFile);
                        try
                        {
                            var result = await _nazulService.Update(id, nazul);
                            if (result == true)
                            {
                                ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                                var result1 = await _nazulService.GetAllNazul();
                                return View("Index", result1);
                            }
                            else
                            {
                                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                                return View(nazul);

                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                        return View(nazul);
                    }
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                    return View(nazul);

                }
            }
            return View(nazul);
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)  
        {

            var result = await _nazulService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _nazulService.GetAllNazul();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _nazulService.GetAllNazul();
                return View("Index", result1);
            }

        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _nazulService.FetchSingleResult(id);
            Data.VillageList = await _nazulService.GetAllVillageList();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.Download)]

        public async Task<IActionResult> NazulList()
        {
            var result = await _nazulService.GetAllNazul();
            List<NazulListDto> data = new List<NazulListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new NazulListDto()
                    {
                        Id = result[i].Id,
                        Village = result[i].Village == null ? "" : result[i].Village.Name,
                        Bigha_biswa_biswanshi = result[i].Bigha.ToString() +" - "+result[i].Biswa.ToString() +" - "+result[i].Biswanshi.ToString(),
                        //JaraiSakni = result[i].JaraiSakani,
                        //Language = result[i].Language,
                        //DateOfConsolidation = Convert.ToDateTime(result[i].DateOfNotification).ToString("dd-MMM-yyyy"),
                        //DateOfJamabandi = Convert.ToDateTime(result[i].YearOfJamabandi).ToString("dd-MMM-yyyy"),
                        //LastMutationNo = result[i].LastMutationNo,

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
            Nazul Data = await _nazulService.FetchSingleResult(Id);
            string filename = DocumentFilePath + Data.DocumentName;
           
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, file.GetContentType(filename));
        }
        public async Task<IActionResult> ViewUploadedDocumentSizra(int Id)
        {
            FileHelper file = new FileHelper();
            Nazul Data = await _nazulService.FetchSingleResult(Id);
            string filename = DocumentSizraFilePath + Data.DocumentNameSizra;

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
            DocumentFilePath = _configuration.GetSection("FilePaths:Nazul:DocumentFIlePath").Value.ToString();
            IFormFile files = Request.Form.Files["file"];
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                DocumentFilePath = _configuration.GetSection("FilePaths:Nazul:DocumentFIlePath").Value.ToString();
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
                                fullpath = _configuration.GetSection("FilePaths:Nazul:DocumentFIlePath").Value.ToString();
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


        [HttpPost]
        public JsonResult CheckFile1()
        {
            bool IsImg = true;
            string fullpath = string.Empty;
            //   string fullpath = string.Empty;
            string extension = string.Empty;
            DocumentSizraFilePath = _configuration.GetSection("FilePaths:SizraNazul:DocumentFIlesPath").Value.ToString();
            IFormFile files = Request.Form.Files["file"];
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                DocumentSizraFilePath = _configuration.GetSection("FilePaths:SizraNazul:DocumentFIlesPath").Value.ToString();
                string FilePath = Path.Combine(DocumentSizraFilePath, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(DocumentSizraFilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(DocumentSizraFilePath);// Try to create the directory.
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
                                fullpath = _configuration.GetSection("FilePaths:SizraNazul:DocumentFIlesPath").Value.ToString();
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



        public bool CheckMimeType(Nazul nazul)
        {
            bool Flag = true;
            string fullpath = string.Empty;
          
            string extension = string.Empty;
            DocumentFilePath = _configuration.GetSection("FilePaths:Nazul:DocumentFIlePath").Value.ToString();
            IFormFile files = nazul.DocumentIFormFile;
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                DocumentFilePath = _configuration.GetSection("FilePaths:Nazul:DocumentFIlePath").Value.ToString();
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
                                fullpath = _configuration.GetSection("FilePaths:Nazul:DocumentFIlePath").Value.ToString();
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
                        else
                        {
                            Flag = false;
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

        public bool CheckMimeType1(Nazul nazul)
        {
            bool Flag = true;
            string fullpath = string.Empty;
            //   string fullpath = string.Empty;
            string extension = string.Empty;
            DocumentSizraFilePath = _configuration.GetSection("FilePaths:SizraNazul:DocumentFIlesPath").Value.ToString();
            IFormFile files = nazul.DocumentSizraIFormFile;
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                DocumentSizraFilePath = _configuration.GetSection("FilePaths:SizraNazul:DocumentFIlesPath").Value.ToString();
                string FilePath = Path.Combine(DocumentSizraFilePath, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(DocumentSizraFilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(DocumentSizraFilePath);// Try to create the directory.
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
                                fullpath = _configuration.GetSection("FilePaths:SizraNazul:DocumentFIlesPath").Value.ToString();
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
                        else
                        {
                            Flag = false;
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
