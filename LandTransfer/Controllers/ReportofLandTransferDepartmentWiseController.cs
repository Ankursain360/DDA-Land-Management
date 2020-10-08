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

        async Task GetAllDepartment(Landtransfer landtransfer)
        {
           
            landtransfer.DepartmentList = await _landTransferService.GetAllDepartment();
                   }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Index()
        {
            Landtransfer model = new Landtransfer();
            model.DepartmentList = await _landTransferService.GetAllDepartment();
             return View(model);
        }


        public async Task<PartialViewResult> GetDetails(int department, int zone, int division, int locality)
        {
            var result = await _landTransferService.GetLandTransferReportData(department, zone, division, locality);

            if (result != null)
            {
                return PartialView("Index", result);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return PartialView();
            }



        }

        }
  
    
    
    }