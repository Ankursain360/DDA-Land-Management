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
using Microsoft.AspNetCore.Http;
using System.IO;
using LeaseForPublic.Filters;
using Core.Enum;

namespace LeaseForPublic.Controllers
{
    public class ViewPaymentHistoryController : BaseController
    {
        private readonly IPaymentService _paymentService;
        public IConfiguration _configuration;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;

        public ViewPaymentHistoryController(IPaymentService paymentService,
            IConfiguration configuration, IWorkflowTemplateService workflowtemplateService,
            IApprovalProccessService approvalproccessService)
        {
            _configuration = configuration;
            _paymentService = paymentService;
            _workflowtemplateService = workflowtemplateService;
            _approvalproccessService = approvalproccessService;

        }

        public async Task<IActionResult> Index()
        {
            Payment data = new Payment();
            var result = await _paymentService.GetAllotteeDetails(SiteContext.UserId);
            ViewBag.PaymentTypeId = result.Allotment.LeasesTypeId;
            ViewBag.AllotmentId = result.AllotmentId;
            ViewBag.LeasePaymentList = await _paymentService.LeasePaymentTypeListBind(result.AllotmentId);
            return View(data);
        }

        public async Task<PartialViewResult> List(int AllotmentId, int LeasePaymentTyeId)
        {
            var result = await _paymentService.GetAlloteeLeasePaymentDetails(AllotmentId, LeasePaymentTyeId);
            if(LeasePaymentTyeId == 8)
            {
                return PartialView("_ListDocumentCharge", result);
            }
            else
            {
                return PartialView("_List", result);
            }
        }
    }
}
