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
    public class ReportofLandTransferDepartmentWiseController : Controller
    {
        private readonly ILandTransferService _landTransferService;

        public ReportofLandTransferDepartmentWiseController(ILandTransferService landTransferService)
        {
            _landTransferService = landTransferService;
        }

        async Task BindDropDown(Landtransfer landtransfer)
        {
            landtransfer.DepartmentList = await _landTransferService.GetAllDepartment();
          
        }
        public async Task<IActionResult> Create()
        {
            Landtransfer landtransfer = new Landtransfer();

            await BindDropDown(landtransfer);
            return View(landtransfer);
        }
        public async Task<PartialViewResult> GetDetails(int reportType, int departmentId)
        {
            ViewBag.ReportType = reportType;

            var result = await _landTransferService.GetLandTransferReportDataDepartmentWise(reportType, departmentId);

            if (result != null)
            {
                return PartialView("_Index", result);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return PartialView();
            }

        }
        


        }
  
    
    
    }