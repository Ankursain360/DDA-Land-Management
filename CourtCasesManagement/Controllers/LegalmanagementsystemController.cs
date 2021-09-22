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
using CourtCasesManagement.Filters;
using Core.Enum;
using Dto.Master;
using Utility.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace CourtCasesManagement.Controllers
{
    public class LegalmanagementsystemController : BaseController
    {
        public object JsonRequestBehavior { get; private set; }
        private readonly ILegalmanagementsystemservice _legalmanagementsystemService;
        public IConfiguration _configuration;
         public LegalmanagementsystemController(ILegalmanagementsystemservice legalmanagementsystemService, IConfiguration configuration)
        {
            _configuration = configuration;
            _legalmanagementsystemService = legalmanagementsystemService;
        }

        async Task BindDropDownView(Legalmanagementsystem legalmanagementsystem)
        {
            legalmanagementsystem.ZoneList = await _legalmanagementsystemService.GetZoneList();
            legalmanagementsystem.CasestatusList = await _legalmanagementsystemService.GetCasestatusList();
            legalmanagementsystem.CourttypeList = await _legalmanagementsystemService.GetCourttypeList();
            legalmanagementsystem.LocalityList = await _legalmanagementsystemService.GetLocalityList(Convert.ToInt32(legalmanagementsystem.ZoneId));
        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            Legalmanagementsystem model = new Legalmanagementsystem();

            await BindDropDownView(model);
            return View(model);
        }
        
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] LegalManagementSystemSearchDto model)
        {
            var result = await _legalmanagementsystemService.GetPagedLegalmanagementsystem(model);
            return PartialView("_List", result);
        }

        [HttpGet]
        public async Task<JsonResult> GetLocalityList(int? zoneId)
        {
            zoneId = zoneId ?? 0;
            return Json(await _legalmanagementsystemService.GetLocalityList(Convert.ToInt32(zoneId)));
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Legalmanagementsystem model = new Legalmanagementsystem();
            await BindDropDownView(model);
            return View(model);
        }
       
        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Legalmanagementsystem legalmanagementsystem)
        {
            bool IsValidpdf = CheckMimeType(legalmanagementsystem);
            await BindDropDownView(legalmanagementsystem);
            legalmanagementsystem.ZoneList = await _legalmanagementsystemService.GetZoneList();
            legalmanagementsystem.LocalityList = await _legalmanagementsystemService.GetLocalityList(Convert.ToInt32(legalmanagementsystem.ZoneId));
            legalmanagementsystem.CasestatusList = await _legalmanagementsystemService.GetCasestatusList();
            legalmanagementsystem.CourttypeList = await _legalmanagementsystemService.GetCourttypeList();
             string JudgementFilePathLayout = _configuration.GetSection("FilePaths:CourtCasesManagementFiles:JudgementFileDocument").Value.ToString();
            string StayFilePathLayout = _configuration.GetSection("FilePaths:CourtCasesManagementFiles:StayFileDocument").Value.ToString();
            string DocumentFilePathLayout = _configuration.GetSection("FilePaths:CourtCasesManagementFiles:DocumentFileDocument").Value.ToString();

            try
            {
                if (ModelState.IsValid)
                {
                    if (IsValidpdf == true)
                    {
                        FileHelper fileHelper = new FileHelper();
                        if (legalmanagementsystem.DocumentFile != null)
                        {
                            legalmanagementsystem.DocumentFilePath = fileHelper.SaveFile(DocumentFilePathLayout, legalmanagementsystem.DocumentFile);
                        }
                        if (legalmanagementsystem.StayFile != null)
                        {
                            legalmanagementsystem.StayInterimGrantedDocument = fileHelper.SaveFile(StayFilePathLayout, legalmanagementsystem.StayFile);
                        }
                        if (legalmanagementsystem.JudgementFile != null)
                        {
                            legalmanagementsystem.JudgementFilePath = fileHelper.SaveFile(JudgementFilePathLayout, legalmanagementsystem.JudgementFile);
                        }

                        var result = await _legalmanagementsystemService.Create(legalmanagementsystem);
                        if (result == true)
                        {
                            ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                            ViewBag.Items = await _legalmanagementsystemService.GetAllLegalmanagementsystem();

                            return View("Index");
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            await BindDropDownView(legalmanagementsystem);
                            return View(legalmanagementsystem);
                        }
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                        return View(legalmanagementsystem);
                    }
                }
                else { return View(legalmanagementsystem); }
            }
            catch(Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(legalmanagementsystem);
            }
        }


        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _legalmanagementsystemService.FetchSingleResult(id);
            Data.ZoneList = await _legalmanagementsystemService.GetZoneList();
            Data.LocalityList = await _legalmanagementsystemService.GetLocalityList(Convert.ToInt32(Data.ZoneId));
            Data.CourttypeList = await _legalmanagementsystemService.GetCourttypeList();
            Data.CasestatusList = await _legalmanagementsystemService.GetCasestatusList();
            ViewBag.ExistDocFile = Data.DocumentFilePath;
            ViewBag.ExistJFile = Data.JudgementFilePath;
            ViewBag.ExistStayFile = Data.StayInterimGrantedDocument;
            if ( Data==null)
            { return NotFound(); }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _legalmanagementsystemService.FetchSingleResult(id);
            Data.ZoneList = await _legalmanagementsystemService.GetZoneList();
            Data.LocalityList = await _legalmanagementsystemService.GetLocalityList(Convert.ToInt32(Data.ZoneId));
            Data.CourttypeList = await _legalmanagementsystemService.GetCourttypeList();
            Data.CasestatusList = await _legalmanagementsystemService.GetCasestatusList();
            ViewBag.ExistDocFile = Data.DocumentFilePath;
            ViewBag.ExistJFile = Data.JudgementFilePath;
            ViewBag.ExistStayFile = Data.StayInterimGrantedDocument;
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Legalmanagementsystem legalmanagementsystem,IFormFile Assignfile, IFormFile AssignJfile, IFormFile AssignSIfile)
        {
            bool IsValidpdf = CheckMimeType(legalmanagementsystem);
            await BindDropDownView(legalmanagementsystem);
            legalmanagementsystem.ZoneList = await _legalmanagementsystemService.GetZoneList();
            legalmanagementsystem.LocalityList = await _legalmanagementsystemService.GetLocalityList(Convert.ToInt32(legalmanagementsystem.ZoneId));
            legalmanagementsystem.CasestatusList = await _legalmanagementsystemService.GetCasestatusList();
            legalmanagementsystem.CourttypeList = await _legalmanagementsystemService.GetCourttypeList();
            ViewBag.ExistDocFile = legalmanagementsystem.DocumentFilePath;
            ViewBag.ExistJFile = legalmanagementsystem.JudgementFilePath;
            ViewBag.ExistStayFile = legalmanagementsystem.StayInterimGrantedDocument;
           
            if (ModelState.IsValid)
            {
                if (IsValidpdf == true)
                {

                    /* For Layout Plan File Upload*/
                    string FileName = "";
                    string filePath = "";
                    legalmanagementsystem.DocumentFile = Assignfile;
                    string DocumentFilePathLayout = _configuration.GetSection("FilePaths:CourtCasesManagementFiles:DocumentFileDocument").Value.ToString();
                    if (legalmanagementsystem.DocumentFile != null)
                    {
                        if (!Directory.Exists(DocumentFilePathLayout))
                        {
                            // Try to create the directory.
                            DirectoryInfo di = Directory.CreateDirectory(DocumentFilePathLayout);
                        }
                        FileName = Guid.NewGuid().ToString() + "_" + legalmanagementsystem.DocumentFile.FileName;
                        filePath = Path.Combine(DocumentFilePathLayout, FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            legalmanagementsystem.DocumentFile.CopyTo(stream);
                        }
                        legalmanagementsystem.DocumentFilePath = filePath;
                    }
                    /*.......For Judgement File .......*/
                    string FileNameJ = "";
                    string filePathJ = "";
                    legalmanagementsystem.JudgementFile = AssignJfile;
                    string JudgementFilePathLayout = _configuration.GetSection("FilePaths:CourtCasesManagementFiles:JudgementFileDocument").Value.ToString();
                    if (legalmanagementsystem.JudgementFile != null)
                    {
                        if (!Directory.Exists(JudgementFilePathLayout))
                        {
                            // Try to create the directory.
                            DirectoryInfo dij = Directory.CreateDirectory(JudgementFilePathLayout);
                        }
                        FileNameJ = Guid.NewGuid().ToString() + "_" + legalmanagementsystem.JudgementFile.FileName;
                        filePathJ = Path.Combine(JudgementFilePathLayout, FileNameJ);
                        using (var stream = new FileStream(filePathJ, FileMode.Create))
                        {
                            legalmanagementsystem.JudgementFile.CopyTo(stream);
                        }
                        legalmanagementsystem.JudgementFilePath = filePathJ;
                    }
                    /*.......For Stay Interim File .......*/
                    string FileNameS = "";
                    string filePathS = "";
                    legalmanagementsystem.StayFile = AssignJfile;
                    string StayFilePathLayout = _configuration.GetSection("FilePaths:CourtCasesManagementFiles:StayFileDocument").Value.ToString();
                    if (legalmanagementsystem.StayFile != null)
                    {
                        if (!Directory.Exists(StayFilePathLayout))
                        {
                            // Try to create the directory.
                            DirectoryInfo dij = Directory.CreateDirectory(StayFilePathLayout);
                        }
                        FileNameS = Guid.NewGuid().ToString() + "_" + legalmanagementsystem.StayFile.FileName;
                        filePathS = Path.Combine(StayFilePathLayout, FileNameS);
                        using (var stream = new FileStream(filePathS, FileMode.Create))
                        {
                            legalmanagementsystem.StayFile.CopyTo(stream);
                        }
                        legalmanagementsystem.StayInterimGrantedDocument = filePathS;
                    }



                    var result = await _legalmanagementsystemService.Update(id, legalmanagementsystem);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var result1 = await _legalmanagementsystemService.GetAllLegalmanagementsystem();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(legalmanagementsystem);
                    }
                }
                else
                {


                    ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                    return View(legalmanagementsystem);
                }
                }
                else
                {
                    return View(legalmanagementsystem);
                }
            }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _legalmanagementsystemService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"File No. : {Name} already exist");
            }
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
            string filename = _legalmanagementsystemService.GetDownload(Id);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DocDownload(int Id)
        {
            string filename = _legalmanagementsystemService.GetDocDownload(Id);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> JDocDownload(int Id)
        {
            string filename = _legalmanagementsystemService.GetJDocDownload(Id);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filename), Path.GetFileName(filename));
        }


        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _legalmanagementsystemService.Delete(id);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                }
                
            }
            catch { }
            return RedirectToAction("Index", "Legalmanagementsystem");
        }
        public async Task<IActionResult> DeleteConfirmed(int id)  // NOT IN WORKING Used to Perform Delete Functionality added by Renu
        {
            var result = await _legalmanagementsystemService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            return RedirectToAction("Index", "Legalmanagementsystem");
        }


        public async Task<IActionResult> LegalManagementSystemList()
        {
            var result = await _legalmanagementsystemService.GetAllLegalmanagementsystem();
            List<LegalManagementSystemListDto> data = new List<LegalManagementSystemListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new LegalManagementSystemListDto()
                    {
                        Id = result[i].Id,
                        FileNo = result[i].FileNo,
                        CourtCaseNo = result[i].CourtCaseNo,
                        Zone = result[i].Zone == null ? "" : result[i].Zone.Name,
                        Village = result[i].Locality == null ? "" : result[i].Locality.Name,
                        COC = result[i].IsActive.ToString() == "1" ? "Yes" : "No",
                        Jugement= result[i].IsActive.ToString() == "1" ? "Yes" : "No",
                        StayinterimGranted= result[i].IsActive.ToString() == "1" ? "Yes" : "No",
                        IsActive = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }


        [HttpPost]
        public JsonResult CheckFile()
        {
            bool IsImg = true;
            string fullpath = string.Empty;
            //   string fullpath = string.Empty;
            string extension = string.Empty;
            string DocumentFilePathLayout = _configuration.GetSection("FilePaths:CourtCasesManagementFiles:DocumentFileDocument").Value.ToString();           
            IFormFile files = Request.Form.Files["file"];
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                DocumentFilePathLayout = _configuration.GetSection("FilePaths:CourtCasesManagementFiles:DocumentFileDocument").Value.ToString();
                string FilePath = Path.Combine(DocumentFilePathLayout, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(DocumentFilePathLayout))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(DocumentFilePathLayout);// Try to create the directory.
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
                                fullpath = _configuration.GetSection("FilePaths:CourtCasesManagementFiles:DocumentFileDocument").Value.ToString();
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


        public bool CheckMimeType(Legalmanagementsystem legalmanagementsystem)
        {
            bool Flag = true;
            string fullpath = string.Empty;          
            string extension = string.Empty;
            string DocumentFilePathLayout = _configuration.GetSection("FilePaths:CourtCasesManagementFiles:DocumentFileDocument").Value.ToString();
            IFormFile files = legalmanagementsystem.DocumentFile;
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                DocumentFilePathLayout = _configuration.GetSection("FilePaths:CourtCasesManagementFiles:DocumentFileDocument").Value.ToString();
                string FilePath = Path.Combine(DocumentFilePathLayout, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(DocumentFilePathLayout))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(DocumentFilePathLayout);// Try to create the directory.
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
                                fullpath = _configuration.GetSection("FilePaths:CourtCasesManagementFiles:DocumentFileDocument").Value.ToString();
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
