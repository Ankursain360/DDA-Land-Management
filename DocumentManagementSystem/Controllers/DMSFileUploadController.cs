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

        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Dmsfileupload dmsfileupload = new Dmsfileupload();
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


    }
}
