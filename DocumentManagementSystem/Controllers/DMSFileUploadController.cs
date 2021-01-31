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
                var result = await _dmsfileuploadService.Update(id,dmsfileupload);

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
            string path = Data.FilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        [HttpPost]
        public async Task<PartialViewResult> BulkUploadDetails([FromBody] DMSBulkViewBindDTO dtodata)
        {
            BulkUploadInfoDto data = new BulkUploadInfoDto();
            ViewBag.DepartmentList = await _dmsfileuploadService.GetDepartmentList();
            return PartialView("_BulkUpload", data);

        }

    }

}
