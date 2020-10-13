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

        //async Task handedoverdepartment(Landtransfer landtransfer)
        //{

        //    landtransfer.handeoverdepartmentlist = await _landTransferService.GetAllHandoverDepartment();
        //}

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public async Task<IActionResult> Index()
        //{
        //    Landtransfer model = new Landtransfer();
        //    model.DepartmentList = await _landTransferService.GetAllDepartment();
        //     return View(model);
        //}


        public async Task<IActionResult> Create()
        {
            Landtransfer model = new Landtransfer();
            model.LandTransferList = await _landTransferService.GetAllLandTransferList();
            //  model.handeoverdepartmentlist = await _landTransferService.GetAllHandoverDepartment();

            return View(model);
        }




        //public async Task<PartialViewResult> GetDetails(int handedover)
        //{
        //    var result = await _landTransferService.GetLandTransferReportDepartmentwise(handedover);

        //    if (result != null)
        //    {
        //        return PartialView("Index", result);
        //    }
        //    else
        //    {
        //        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //        return PartialView();
        //    }



        //}




         public async Task<PartialViewResult> GetDetails(int? id)
        {
            id=id ?? 0;
            var result = await _landTransferService.GetLandTransferReportdataHandover(Convert.ToInt32(id));
            return PartialView("_List", result);
        }





        }
  
    
    
    }