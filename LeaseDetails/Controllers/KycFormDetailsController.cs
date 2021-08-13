using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Service.IApplicationService;
using Libraries.Model.Entity;
using Utility.Helper;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.Extensions.Configuration;
using Dto.Search;
using System.IO;
using System.Linq;
using LeaseDetails.Filters;
using Core.Enum;
using Service.IApplicationService;
using Microsoft.AspNetCore.Hosting;
using Dto.Master;
using System.Text;
using LeaseDetails.Helper;
using LeaseDetails.Models;
using System.Diagnostics;



namespace LeaseDetails.Controllers
{
    public class KycFormDetailsController : BaseController
    {
        private readonly IKycformService _kycformService;
        private readonly ISiteContext _siteContext;
        public KycFormDetailsController(IKycformService kycform, ISiteContext siteContext)
        {
            _kycformService = kycform;
            _siteContext=siteContext;
        }

        public IActionResult Index()
        {
            return View();

        }

        public async Task<PartialViewResult> KycFromApproval (string ApprovalType)
        {
            
            var result = await _kycformService.GetKycFormApprovalDetails(_siteContext.UserId, ApprovalType);

            return PartialView("_List", result);
        }


    }
}
