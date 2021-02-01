using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.ApplicationService;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using LandInventory.Filters;
using Core.Enum;
namespace LandInventory.Controllers
{
    public class ReportofLandTransferPropertyNoWiseController : BaseController
    {
        private readonly ILandTransferService _landtransferService;
        private readonly IPropertyRegistrationService _propertyRegistrationService;
        public ReportofLandTransferPropertyNoWiseController(ILandTransferService landtransferService, IPropertyRegistrationService propertyRegistrationService)
        {
            _propertyRegistrationService = propertyRegistrationService;
            _landtransferService = landtransferService;
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            Landtransfer model = new Landtransfer();
            List<Propertyregistration> registration = await _propertyRegistrationService.GetKhasraReportList();
            model.PropertyRegistrationList= await _propertyRegistrationService.GetKhasraReportList(); 
            return View(model);
        }
        public async Task<PartialViewResult> GetDetails(int? id)
        {
            id=id ?? 0;
            var result = await _landtransferService.GetLandTransferReportDataKhasraNumberWise(Convert.ToInt32(id));
            return PartialView("_List", result);
        }
    }
}