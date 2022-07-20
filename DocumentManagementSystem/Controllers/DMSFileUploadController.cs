using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using Dto.Search;
using DocumentManagementSystem.Filters;
using Core.Enum;
using System.Configuration;
using System.Text.RegularExpressions;
using Utility.Helper;
using Dto.Master;
using Newtonsoft.Json;
using CsvHelper;
using System.Text;
using System.Collections.Generic;

namespace DocumentManagementSystem.Controllers
{
    public class DMSFileUploadController : BaseController
    {
        private readonly IDmsFileUploadService _dmsfileuploadService;
        public IConfiguration _Configuration;
        string UploadFilePath = "";
        string targetPathGeo = "";

        public object JsonRequestBehavior { get; private set; }

        public DMSFileUploadController(IDmsFileUploadService dmsfileuploadService, IConfiguration configuration)
        {
            _dmsfileuploadService = dmsfileuploadService;
            _Configuration = configuration;
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            Dmsfileupload dmsfileupload = new Dmsfileupload();
            ViewBag.LocalityList = await _dmsfileuploadService.GetLocalityList();
            ViewBag.DepartmentList = await _dmsfileuploadService.GetDepartmentList();
            ViewBag.KhasraNoList = await _dmsfileuploadService.GetKhasraNoList();
            ViewBag.categoryList = await _dmsfileuploadService.allcategoryList();

            dmsfileupload.VillageList = await _dmsfileuploadService.allVillageList(dmsfileupload.ZoneId);
            await BindDropDown(dmsfileupload);
            //ViewBag.ZoneList = await _dmsfileuploadService.allZoneList();
            //ViewBag.VillageList = await _dmsfileuploadService.allVillageList(ViewBag.ZoneId);
            return View(dmsfileupload);
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DMSFileUploadSearchDto model)
        {
            var result = await _dmsfileuploadService.GetPagedDMSFileUploadList(model);
            return PartialView("_List", result);
        }
        async Task BindDropDown(Dmsfileupload dmsfileupload)
        {
            dmsfileupload.DepartmentList = await _dmsfileuploadService.GetDepartmentList();
            dmsfileupload.LocalityList = await _dmsfileuploadService.GetLocalityList();
            dmsfileupload.KhasraNoList = await _dmsfileuploadService.GetKhasraNoList();
            dmsfileupload.ZoneList = await _dmsfileuploadService.allZoneList();
            dmsfileupload.CategoriesList = await _dmsfileuploadService.allcategoryList();

        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Dmsfileupload dmsfileupload = new Dmsfileupload();
            dmsfileupload.CategoriesList = await _dmsfileuploadService.allcategoryList();
            dmsfileupload.VillageList = await _dmsfileuploadService.allVillageList(dmsfileupload.ZoneId);
            dmsfileupload.IsActive = 1;
            await BindDropDown(dmsfileupload);
            ViewBag.PdfGenerate = "No";
            return View(dmsfileupload);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Create(Dmsfileupload dmsfileupload)
        {

            bool IsValidpdf = CheckMimeType(dmsfileupload);
            await BindDropDown(dmsfileupload);
            dmsfileupload.DepartmentList = await _dmsfileuploadService.GetDepartmentList();
            dmsfileupload.LocalityList = await _dmsfileuploadService.GetLocalityList();
            dmsfileupload.KhasraNoList = await _dmsfileuploadService.GetKhasraNoList();
            dmsfileupload.CategoriesList = await _dmsfileuploadService.allcategoryList();
            dmsfileupload.VillageList = await _dmsfileuploadService.allVillageList(dmsfileupload.ZoneId);

            if (ModelState.IsValid)
            {
                if (IsValidpdf == true)
                {
                    #region File Upload  Added by Renu 29 Jan 2021
                    /* For File Upload*/
                    UploadFilePath = _Configuration.GetSection("FilePaths:FileUpload:FilePath").Value.ToString();
                    if (dmsfileupload.FileUpload != null)
                    {
                        if (!Directory.Exists(UploadFilePath))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(UploadFilePath);// Try to create the directory.
                        }
                        dmsfileupload.FileName = Guid.NewGuid().ToString() + "_" + dmsfileupload.FileUpload.FileName;
                        dmsfileupload.FilePath = Path.Combine(UploadFilePath, dmsfileupload.FileName);
                        using (var stream = new FileStream(dmsfileupload.FilePath, FileMode.Create))
                        {
                            dmsfileupload.FileUpload.CopyTo(stream);
                        }
                    }
                    else
                    {

                    }

                    #endregion

                    dmsfileupload.CreatedBy = SiteContext.UserId;
                    var result = await _dmsfileuploadService.Create(dmsfileupload);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        ViewBag.LocalityList = await _dmsfileuploadService.GetLocalityList();
                        ViewBag.DepartmentList = await _dmsfileuploadService.GetDepartmentList();
                        ViewBag.KhasraNoList = await _dmsfileuploadService.GetKhasraNoList();
                        ViewBag.ZoneList = await _dmsfileuploadService.allZoneList();
                        ViewBag.categoryList = await _dmsfileuploadService.allcategoryList();
                        dmsfileupload.VillageList = await _dmsfileuploadService.allVillageList(dmsfileupload.ZoneId);

                        return View("Index");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        await BindDropDown(dmsfileupload);
                        return View(dmsfileupload);

                    }
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                    return View(dmsfileupload);
                }
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(dmsfileupload);
            }

        }


        [HttpPost]
        public JsonResult CheckFile()
        {
            bool IsImg = true;
            string fullpath = string.Empty;
         //   string fullpath = string.Empty;
            string extension = string.Empty;
            UploadFilePath = _Configuration.GetSection("FilePaths:FileUpload:FilePath").Value.ToString();
            IFormFile files = Request.Form.Files["file"];
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                UploadFilePath = _Configuration.GetSection("FilePaths:FileUpload:FilePath").Value.ToString();
                string FilePath = Path.Combine(UploadFilePath, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(UploadFilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(UploadFilePath);// Try to create the directory.
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
                                fullpath = _Configuration.GetSection("FilePaths:FileUpload:FilePath").Value.ToString();
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


        public bool CheckMimeType(Dmsfileupload dmsfileupload)
        {
            bool flag = true;
            decimal maxFileSize = 0;
            var supportedTypes = new[] { "pdf" };
            maxFileSize = Convert.ToDecimal(ConfigurationManager.AppSettings["FileSize"]);

            var fileExt = System.IO.Path.GetExtension(dmsfileupload.FileUpload.FileName).Substring(1);

            if (!supportedTypes.Contains(fileExt))
            {
                return false;
            }

            if (dmsfileupload.FileUpload != null)
            {
                try
                {
                    #region File Upload  Added by Renu 29 Jan 2021
                    /* For File Upload*/
                    UploadFilePath = _Configuration.GetSection("FilePaths:FileUpload:FilePath").Value.ToString();
                    if (dmsfileupload.FileUpload != null)
                    {
                        if (!Directory.Exists(UploadFilePath))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(UploadFilePath);// Try to create the directory.
                        }
                        dmsfileupload.FileName = Guid.NewGuid().ToString() + "_" + dmsfileupload.FileUpload.FileName;
                        dmsfileupload.FilePath = Path.Combine(UploadFilePath, dmsfileupload.FileName);
                        using (var stream = new FileStream(dmsfileupload.FilePath, FileMode.Create))
                        {
                            dmsfileupload.FileUpload.CopyTo(stream);

                        }
                        iTextSharp.text.pdf.PdfReader oPdfReader = new iTextSharp.text.pdf.PdfReader(dmsfileupload.FilePath);
                        oPdfReader.Close();
                    }
                    #endregion
                    FileInfo doc = new FileInfo(dmsfileupload.FilePath);
                    if (doc.Exists)
                    {
                        doc.Delete();
                    }
                }

                catch (iTextSharp.text.exceptions.InvalidPdfException)
                {
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return flag;
        }




        [HttpGet]
        public async Task<JsonResult> AllVillagedataList(int? zoneid)
        {
            zoneid = zoneid ?? 0;
            return Json(await _dmsfileuploadService.allVillageList(Convert.ToInt32(zoneid)));
        }


        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _dmsfileuploadService.FetchSingleResult(id);
            Data.VillageList = await _dmsfileuploadService.allVillageList(Data.ZoneId);
            Data.CategoriesList = await _dmsfileuploadService.allcategoryList();
            await BindDropDown(Data);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Edit(int id, Dmsfileupload dmsfileupload)
        {
            await BindDropDown(dmsfileupload);
            dmsfileupload.DepartmentList = await _dmsfileuploadService.GetDepartmentList();
            dmsfileupload.LocalityList = await _dmsfileuploadService.GetLocalityList();
            dmsfileupload.KhasraNoList = await _dmsfileuploadService.GetKhasraNoList();
            dmsfileupload.CategoriesList = await _dmsfileuploadService.allcategoryList();


            if (ModelState.IsValid)
            {
                #region File Upload  Added by Renu 29 Jan 2021
                /* For File Upload*/
                UploadFilePath = _Configuration.GetSection("FilePaths:FileUpload:FilePath").Value.ToString();
                if (dmsfileupload.FileUpload != null)
                {
                    if (!Directory.Exists(UploadFilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(UploadFilePath);// Try to create the directory.
                    }
                    dmsfileupload.FileName = Guid.NewGuid().ToString() + "_" + dmsfileupload.FileUpload.FileName;
                    dmsfileupload.FilePath = Path.Combine(UploadFilePath, dmsfileupload.FileName);
                    using (var stream = new FileStream(dmsfileupload.FilePath, FileMode.Create))
                    {
                        dmsfileupload.FileUpload.CopyTo(stream);
                    }
                }
                #endregion

                dmsfileupload.ModifiedBy = SiteContext.UserId;
                var result = await _dmsfileuploadService.Update(id, dmsfileupload);

                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    ViewBag.LocalityList = await _dmsfileuploadService.GetLocalityList();
                    ViewBag.DepartmentList = await _dmsfileuploadService.GetDepartmentList();
                    ViewBag.KhasraNoList = await _dmsfileuploadService.GetKhasraNoList();
                    ViewBag.categoryList = await _dmsfileuploadService.allcategoryList();
                    return View("Index");
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    await BindDropDown(dmsfileupload);
                    return View(dmsfileupload);

                }
            }
            else
            {
                return View(dmsfileupload);
            }
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _dmsfileuploadService.FetchSingleResult(id);
            Data.VillageList = await _dmsfileuploadService.allVillageList(Data.ZoneId);
            await BindDropDown(Data);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _dmsfileuploadService.Delete(id, SiteContext.UserId);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            ViewBag.DepartmentList = await _dmsfileuploadService.GetDepartmentList();
            ViewBag.LocalityList = await _dmsfileuploadService.GetLocalityList();
            ViewBag.KhasraNoList = await _dmsfileuploadService.GetKhasraNoList();
            ViewBag.categoryList = await _dmsfileuploadService.allcategoryList();
            return View("Index");
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
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

        public async Task<IActionResult> ViewFile(int Id)
        {
            FileHelper file = new FileHelper();
            Dmsfileupload Data = await _dmsfileuploadService.FetchSingleResult(Id);

            string filename = Data.FilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }

        public async Task<IActionResult> DownloadCSVFormat()
        {
            FileHelper file = new FileHelper();

            string filename = _Configuration.GetSection("FilePaths:DownloadCSVFormat:DownloadCSVFormat").Value.ToString();
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }

        [HttpPost]
        public async Task<PartialViewResult> BulkUploadDetails([FromBody] DMSBulkViewBindDTO dtodata)
        {
            BulkUploadInfoDto data = new BulkUploadInfoDto();
            ViewBag.DepartmentList = await _dmsfileuploadService.GetDepartmentList();
            return PartialView("_BulkUpload", data);

        }

        [HttpPost]
        public async Task<IActionResult> SaveBulkUploadDetails([FromBody] BulkUploadInfoDto model)
        {
            if (ModelState.IsValid)
            {

                return Json(Url.Action("Index", "UserManagement"));
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> CreateBulkUpload(Dmsfileupload dmsfileupload)
        {
            await BindDropDown(dmsfileupload);
            dmsfileupload.DepartmentList = await _dmsfileuploadService.GetDepartmentList();
            dmsfileupload.LocalityList = await _dmsfileuploadService.GetLocalityList();
            dmsfileupload.KhasraNoList = await _dmsfileuploadService.GetKhasraNoList();
            dmsfileupload.ZoneList = await _dmsfileuploadService.allZoneList();
            dmsfileupload.VillageList = await _dmsfileuploadService.allVillageList(dmsfileupload.ZoneId);

            var result = false;
            bool row = true;
            if (ModelState.IsValid)
            {
                List<DMSCSVTableDTO> data;
                string jsonString;
                StringBuilder HtmlSummary = new StringBuilder();
                string text = "Not Saved File No.<ul>";
                HtmlSummary.Append(text);
                StringBuilder HtmlSummaryUniq = new StringBuilder();
                string textuniq = "File No. Already Exists<ul>";
                HtmlSummaryUniq.Append(textuniq);
                using (var sreader = new StreamReader(dmsfileupload.BulkUpload.OpenReadStream()))
                {
                    string[] headers = sreader.ReadLine().Split(',');     //Title
                    while (!sreader.EndOfStream)                          //get all the content in rows 
                    {
                        string[] rows = sreader.ReadLine().Split(',');
                        dmsfileupload.FileNo = (rows[0].ToString());
                        dmsfileupload.AlloteeName = (rows[1].ToString());
                        var LocalityId = _dmsfileuploadService.GetLocalityByName((rows[2].ToString())); //To convert locality name to id
                        var KhasraId = _dmsfileuploadService.GetKhasraByName((rows[3].ToString()));//To convert khasra name to id

                        dmsfileupload.LocalityId = LocalityId;
                        dmsfileupload.KhasraNoId = KhasraId;



                        dmsfileupload.PropertyNoAddress = (rows[4].ToString());
                        dmsfileupload.Title = (rows[5].ToString());
                        dmsfileupload.AlmirahNo = (rows[6].ToString());
                        dmsfileupload.FileName = (rows[7].ToString());


                        if ((dmsfileupload.PdfLocationPath[dmsfileupload.PdfLocationPath.Length - 1]).ToString() == @"\" ? true : false)
                            dmsfileupload.FilePath = dmsfileupload.PdfLocationPath + (rows[7].ToString());
                        else
                            dmsfileupload.FilePath = dmsfileupload.PdfLocationPath + @"\" + (rows[7].ToString());
                        var ZoneId = _dmsfileuploadService.GetZoneByName((rows[8].ToString())); //To convert Zone name to id
                        var VillageId = _dmsfileuploadService.GetVillageByName((rows[9].ToString()));//To convert Village name to id

                        dmsfileupload.ZoneId = ZoneId;
                        dmsfileupload.VillageId = VillageId;
                        dmsfileupload.IsFileBulkUpload = "File Upload";
                        dmsfileupload.IsActive = 1;
                        dmsfileupload.CreatedBy = SiteContext.UserId;
                        if (!await _dmsfileuploadService.CheckUniqueName(0, dmsfileupload.FileNo))
                        {
                            result = await _dmsfileuploadService.Create(dmsfileupload);
                            if (!result)
                            {
                                text = "<li>" + dmsfileupload.FileNo + "</li>";
                                HtmlSummary.Append(text);
                                row = false;
                            }
                        }
                        else
                        {
                            textuniq = "<li>" + dmsfileupload.FileNo + "</li>";
                            HtmlSummaryUniq.Append(textuniq);
                            row = false;
                        }
                    }
                }


                if (row)
                {
                    HtmlSummary.Append("</ul>");
                    ViewBag.Summary = HtmlSummary.ToString();
                    HtmlSummaryUniq.Append("</ul>");
                    ViewBag.SummaryUniq = HtmlSummaryUniq.ToString();
                    ViewBag.PdfGenerate = "No";
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    ViewBag.LocalityList = await _dmsfileuploadService.GetLocalityList();
                    ViewBag.DepartmentList = await _dmsfileuploadService.GetDepartmentList();
                    ViewBag.KhasraNoList = await _dmsfileuploadService.GetKhasraNoList();
                    dmsfileupload.ZoneList = await _dmsfileuploadService.allZoneList();
                    dmsfileupload.VillageList = await _dmsfileuploadService.allVillageList(dmsfileupload.ZoneId);


                    return View("Index");
                }
                else
                {
                    HtmlSummary.Append("</ul>");
                    ViewBag.Summary = HtmlSummary.ToString();
                    HtmlSummaryUniq.Append("</ul>");
                    ViewBag.SummaryUniq = HtmlSummaryUniq.ToString();
                    ViewBag.Message = Alert.Show("Either all or some rows in file not Saved check Msg", "", AlertType.Warning);
                    ViewBag.PdfGenerate = "Yes";
                    ViewBag.LocalityList = await _dmsfileuploadService.GetLocalityList();
                    ViewBag.DepartmentList = await _dmsfileuploadService.GetDepartmentList();
                    ViewBag.KhasraNoList = await _dmsfileuploadService.GetKhasraNoList();
                    dmsfileupload.ZoneList = await _dmsfileuploadService.allZoneList();
                    dmsfileupload.VillageList = await _dmsfileuploadService.allVillageList(dmsfileupload.ZoneId);


                    dmsfileupload.IsActive = 1;
                    await BindDropDown(dmsfileupload);
                    return View("Create", dmsfileupload);

                }
            }
            else
            {

                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                ViewBag.LocalityList = await _dmsfileuploadService.GetLocalityList();
                ViewBag.DepartmentList = await _dmsfileuploadService.GetDepartmentList();
                ViewBag.KhasraNoList = await _dmsfileuploadService.GetKhasraNoList();
                dmsfileupload.ZoneList = await _dmsfileuploadService.allZoneList();
                dmsfileupload.VillageList = await _dmsfileuploadService.allVillageList(dmsfileupload.ZoneId);


                return View("Create");
            }

        }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _dmsfileuploadService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Department: {Name} already exist");
            }
        }



        public async Task<IActionResult> DmsFileUploadList()
        {
            var result = await _dmsfileuploadService.GetAllDMSFileUploadList();
            List<DmsFileUploadListDto> data = new List<DmsFileUploadListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new DmsFileUploadListDto()
                    {
                        Id = result[i].Id,
                        FileNo = result[i].FileNo,
                        AlloteeName = result[i].AlloteeName,
                        Department = result[i].Department == null ? "" : result[i].Department.Name.ToString(),
                        Locality = result[i].Locality == null ? "" : result[i].Locality.Name.ToString(),
                        KhasraNo = result[i].KhasraNo == null ? "" : result[i].KhasraNo.KhasraNo.ToString(),
                        Zone = result[i].Zone == null ? "" : result[i].Zone.Name.ToString(),
                        Village = result[i].Village == null ? "" : result[i].Village.Name.ToString(),


                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }



    }
}
