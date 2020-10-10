using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;

namespace LandTransfer.Controllers
{
    public class ReportofLandTransferPropertyNoWiseController : Controller
    {
        private readonly ILandTransferService _landtransferService;
        public ReportofLandTransferPropertyNoWiseController(ILandTransferService landtransferService)
        {
            _landtransferService = landtransferService;
        }
        public async Task<IActionResult> Index()
        {
            Landtransfer model = new Landtransfer();
            model.LandTransferList =await _landtransferService.GetAllLandTransferList();
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