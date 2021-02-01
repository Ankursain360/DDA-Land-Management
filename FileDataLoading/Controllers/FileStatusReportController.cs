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
using FileDataLoading.Filters;
using Core.Enum;
using FileDataLoading.Helper;
using System.Collections.Generic;
using System.Linq;


namespace FileDataLoading.Controllers
{
    public class FileStatusReportController : BaseController
    {
               private readonly IDataStorageService _datastorageService;

                public FileStatusReportController(IDataStorageService datastorageService)
                {
                  _datastorageService = datastorageService;
              }

        public async Task<IActionResult> Create()
        {
            FileStatusReportDtoProfile datastoragedetails = new FileStatusReportDtoProfile();
           
            //ViewBag.BranchList = await _datastorageService.GetBranch();
            ViewBag.DepartmentList = await _datastorageService.GetDepartment();

            return View(datastoragedetails);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] FileStatusReportSearchDto model)
        {
            int UserId = SiteContext.UserId;
            var result = await _datastorageService.GetPagedFileStatusReportData(model, UserId);

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

