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
using Microsoft.Extensions.Configuration;
using Utility.Helper;
using Dto.Common;
using SiteMaster.Filters;
using Service.IApplicationService;


using Core.Enum;


namespace SiteMaster.Controllers
{

    public class ApprovalProcessController : BaseController
    {
        public IConfiguration _configuration;
        private readonly IApprovalCompleteService _approvalCompleteService;


        public ApprovalProcessController(IApprovalCompleteService approvalcompleteService, IConfiguration configuration)
        {
            _approvalCompleteService = approvalcompleteService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] ApprovalCompleteSearchDto report)
        {
            int UserId = SiteContext.UserId;
            report.userid = UserId;
            var result = await _approvalCompleteService.GetApprovalCompleteModule(report);
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

        public IActionResult Index()
        {
            // int a = SiteContext.UserId;
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetApprovalProcessDetails(int? worktemplateId)
        {
            int worktemplateProcessId = Convert.ToInt32(worktemplateId);

            if (worktemplateProcessId == 20)
                return Json(_configuration.GetSection("ApprovalProccessPath:LeaseApplicationFormApproval").Value.ToString());
            else if (worktemplateProcessId == 2)
                return Json(_configuration.GetSection("ApprovalProccessPath:WatchWardApproval").Value.ToString());
            else
                return Json(_configuration.GetSection("ApprovalProccessPath:SiteMaster").Value.ToString());
        }

    }
}
