using System;
using System.Collections.Generic;
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
using Microsoft.AspNetCore.Hosting;
using Dto.Search;
using DocumentManagementSystem.Filters;
using Core.Enum;
using Utility.Helper;
using Dto.Master;
using Newtonsoft.Json;
using CsvHelper;
using System.Text;

namespace DocumentManagementSystem.Controllers
{
    public class DMSFileUploadController : BaseController
    {
        private readonly IDmsFileUploadService _dmsfileuploadService;
        public IConfiguration _Configuration;
        string UploadFilePath = "";
        string targetPathGeo = "";
        public DMSFileUploadController(IDmsFileUploadService dmsfileuploadService, IConfiguration configuration)
        {
            _dmsfileuploadService = dmsfileuploadService;
            _Configuration = configuration;
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            ViewBag.LocalityList = await _dmsfileuploadService.GetLocalityList();
            ViewBag.DepartmentList = await _dmsfileuploadService.GetDepartmentList();
            ViewBag.KhasraNoList = await _dmsfileuploadService.GetKhasraNoList();
            return View();
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
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Dmsfileupload dmsfileupload = new Dmsfileupload();
            dmsfileupload.IsActive = 1;
            await BindDropDown(dmsfileupload);
            return View(dmsfileupload);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Create(Dmsfileupload dmsfileupload)
        {
            await BindDropDown(dmsfileupload);
            dmsfileupload.DepartmentList = await _dmsfileuploadService.GetDepartmentList();
            dmsfileupload.LocalityList = await _dmsfileuploadService.GetLocalityList();
            dmsfileupload.KhasraNoList = await _dmsfileuploadService.GetKhasraNoList();

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

                dmsfileupload.CreatedBy = SiteContext.UserId;
                var result = await _dmsfileuploadService.Create(dmsfileupload);

                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    ViewBag.LocalityList = await _dmsfileuploadService.GetLocalityList();
                    ViewBag.DepartmentList = await _dmsfileuploadService.GetDepartmentList();
                    ViewBag.KhasraNoList = await _dmsfileuploadService.GetKhasraNoList();
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


        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _dmsfileuploadService.FetchSingleResult(id);
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
            ViewBag.LocalityList = await _dmsfileuploadService.GetDepartmentList();
            ViewBag.DepartmentList = await _dmsfileuploadService.GetLocalityList();
            ViewBag.KhasraNoList = await _dmsfileuploadService.GetKhasraNoList();
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
            //string path = Data.FilePath;
            //byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            //return File(FileBytes, file.GetContentType(path));
            string filename = Data.FilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }

        public async Task<IActionResult> DownloadCSVFormat()
        {
            FileHelper file = new FileHelper();
            //string path = Data.FilePath;
            //byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            //return File(FileBytes, file.GetContentType(path));
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
            var result = false;
            int row = 1;
            if (ModelState.IsValid)
            {

                //#region File Upload  Added by Renu 29 Jan 2021
                //UploadFilePath = _Configuration.GetSection("FilePaths:FileUpload:FilePath").Value.ToString();
                //if (dmsfileupload.FileUpload != null)
                //{
                //    if (!Directory.Exists(UploadFilePath))
                //    {
                //        DirectoryInfo di = Directory.CreateDirectory(UploadFilePath);// Try to create the directory.
                //    }
                //    dmsfileupload.FileName = Guid.NewGuid().ToString() + "_" + dmsfileupload.FileUpload.FileName;
                //    dmsfileupload.FilePath = Path.Combine(UploadFilePath, dmsfileupload.FileName);
                //    using (var stream = new FileStream(dmsfileupload.FilePath, FileMode.Create))
                //    {
                //        dmsfileupload.FileUpload.CopyTo(stream);
                //    }
                //}
                //#endregion
                List<DMSCSVTableDTO> data;
                string jsonString;
                StringBuilder HtmlSummary = new StringBuilder();
                string text = "Not Saved File No.<ul>";
                HtmlSummary.Append(text);
                StringBuilder HtmlSummaryUniq = new StringBuilder();
                string textuniq = "File No. Already Exists<ul>";
                HtmlSummaryUniq.Append(textuniq);
                using (var sreader = new StreamReader(dmsfileupload.FileUpload.OpenReadStream()))
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
                            dmsfileupload.FilePath = dmsfileupload.PdfLocationPath;
                        else
                            dmsfileupload.FilePath = dmsfileupload.PdfLocationPath + @"\" + (rows[7].ToString());

                        dmsfileupload.IsFileBulkUpload = "File Upload";
                        dmsfileupload.IsActive = 1;
                        dmsfileupload.CreatedBy = SiteContext.UserId;
                        if(await _dmsfileuploadService.CheckUniqueName(dmsfileupload.FileNo))
                        {
                            result = await _dmsfileuploadService.Create(dmsfileupload);
                            if (!result)
                            {
                                text = "<li>" + dmsfileupload.FileNo + "</li>";
                                HtmlSummary.Append(text);
                            }
                        }
                        else
                        {
                            textuniq = "<li>" + dmsfileupload.FileNo + "</li>";
                            HtmlSummaryUniq.Append(textuniq);
                        }
                    }
                }
                //using (var fs = new StreamReader(dmsfileupload.FileName))
                //{
                //    data = new CsvReader((IParser)fs).GetRecords<DMSCSVTableDTO>().ToList();
                //}

                ////Csv data as Json string if needed
                //jsonString = JsonConvert.SerializeObject(data);
                //foreach (var details in data)
                //{
                //    dmsfileupload.FileNo = details.FileNo;
                //    dmsfileupload.AlloteeName = details.AlloteeName;
                //    dmsfileupload.LocalityId = details.LocalityId;
                //    dmsfileupload.KhasraNoId = details.KhasraNoId;
                //    dmsfileupload.PropertyNoAddress = details.PropertyNoAddress;
                //    dmsfileupload.Title = details.Title;
                //    dmsfileupload.AlmirahNo = details.AlmirahNo;
                //    dmsfileupload.FileName = details.FileName;
                //    dmsfileupload.FilePath = dmsfileupload.PdfLocationPath;
                //    dmsfileupload.CreatedBy = SiteContext.UserId;
                //    result = await _dmsfileuploadService.Create(dmsfileupload);
                //}



                if (result == true)
                {
                    HtmlSummary.Append("</ul>");
                    ViewBag.Summary = HtmlSummary.ToString();
                    HtmlSummaryUniq.Append("</ul>");
                    ViewBag.SummaryUniq = HtmlSummaryUniq.ToString();
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    ViewBag.LocalityList = await _dmsfileuploadService.GetLocalityList();
                    ViewBag.DepartmentList = await _dmsfileuploadService.GetDepartmentList();
                    ViewBag.KhasraNoList = await _dmsfileuploadService.GetKhasraNoList();
                    return View("Index");
                }
                else
                {
                    HtmlSummary.Append("</ul>");
                    ViewBag.Summary = HtmlSummary.ToString();
                    HtmlSummaryUniq.Append("</ul>");
                    ViewBag.SummaryUniq = HtmlSummaryUniq.ToString();
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    ViewBag.LocalityList = await _dmsfileuploadService.GetLocalityList();
                    ViewBag.DepartmentList = await _dmsfileuploadService.GetDepartmentList();
                    ViewBag.KhasraNoList = await _dmsfileuploadService.GetKhasraNoList();
                    return View("Index");

                }
            }
            else
            {

                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                ViewBag.LocalityList = await _dmsfileuploadService.GetLocalityList();
                ViewBag.DepartmentList = await _dmsfileuploadService.GetDepartmentList();
                ViewBag.KhasraNoList = await _dmsfileuploadService.GetKhasraNoList();
                return View("Index");
            }

        }
    }

}
