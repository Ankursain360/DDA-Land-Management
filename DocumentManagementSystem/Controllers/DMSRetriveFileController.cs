using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using Dto.Search;
using Dto.Master;
using DocumentManagementSystem.Filters;
using Core.Enum;
using Microsoft.Extensions.Configuration;
using Utility.Helper;
using System.IO;

namespace DocumentManagementSystem.Controllers
{
    public class DMSRetriveFileController : BaseController
    {
        private readonly IDmsFileUploadService _dmsfileuploadService;
        public IConfiguration _Configuration;

        public DMSRetriveFileController(IDmsFileUploadService dmsfileuploadService, IConfiguration configuration)
        {
            _dmsfileuploadService = dmsfileuploadService;
            _Configuration = configuration;
        }



        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            DMSRetriveFileReportDtoProfile data = new DMSRetriveFileReportDtoProfile();
            ViewBag.LocalityList = await _dmsfileuploadService.GetLocalityList();
            ViewBag.DepartmentList = await _dmsfileuploadService.GetDepartmentList();
            ViewBag.KhasraNoList = await _dmsfileuploadService.GetKhasraNoList();
            return View(data);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] DMSRetriveFileSearchDto model)
        {
            var result = await _dmsfileuploadService.GetPagedDMSRetriveFileReport(model);

            if (result != null)
            {
                var data = await _dmsfileuploadService.GetDMSUserRights(SiteContext.UserId);
                TempData["View"] = data != null ? data.Viewright : 0;
                TempData["Download"] = data != null ? data.Downloadright : 0;
                ViewBag.View = data != null ? data.Viewright : 0;
                ViewBag.Download = data != null ? data.Downloadright : 0;
                return PartialView("_List", result);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return PartialView();
            }
        }
        public async Task<IActionResult> View(int Id)
        {
            FileHelper file = new FileHelper();
            string right = TempData["View"] as string;
            var data = await _dmsfileuploadService.GetDMSUserRights(SiteContext.UserId);
            if ((data != null ? data.Viewright : 0) == 0)
            {
                return RedirectToAction("UnAuthorized", "Home");
            }
            else
            {

                Dmsfileupload Data = await _dmsfileuploadService.FetchSingleResult(Id);
                string path = Data.FilePath;
                byte[] FileBytes = System.IO.File.ReadAllBytes(path);
                return File(FileBytes, file.GetContentType(path));
            }
        }

        public async Task<IActionResult> Download(int Id)
        {
            FileHelper file = new FileHelper();
            var data = await _dmsfileuploadService.GetDMSUserRights(SiteContext.UserId);
            if ((data != null ? data.Downloadright : 0) == 0)
            {
                return RedirectToAction("UnAuthorized", "Home");
            }
            else
            {
                Dmsfileupload Data = await _dmsfileuploadService.FetchSingleResult(Id);
              
                string filename = Data.FilePath;
                return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
            }
        }
    }
}