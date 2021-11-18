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
using Dto.Search;
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
        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] LandtrasferreportkhasranowiseDto model)
        {
          // id=id ?? 0;
            var result = await _landtransferService.GetLandTransferReportDataKhasraNumberWise(model);


            if (result != null)
            {
                return PartialView("_List", result);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return PartialView();
            }
            //return PartialView("_List", result);
        }


        //public async Task<PartialViewResult> GetDetails([FromBody] LandtrasferreportkhasranowiseDto model)
        //{
        //    var result = await _propertyregistrationService.GetPropertyRegisterationReportData(model);

        //    if (result != null)
        //    {
        //        return PartialView("_Index", result);
        //    }
        //    else
        //    {
        //        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //        return PartialView();
        //    }
        //}









    }
}