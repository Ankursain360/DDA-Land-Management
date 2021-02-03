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
                return PartialView("_List", result);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return PartialView();
            }
        }
    }
}