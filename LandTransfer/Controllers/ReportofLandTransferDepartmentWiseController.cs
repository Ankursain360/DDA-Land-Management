using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using LandTransfer.Filters;
using Core.Enum;
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

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Landtransfer landtransfer = new Landtransfer();

            await BindDropDown(landtransfer);
            return View(landtransfer);
        }
       

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] LandTransferSearchDto model)
        {

            if (model != null)
            {
                var result = await _landTransferService.GetPagedLandtransferReportDeptWise(model);
                ViewBag.ReportType = model.reportType;
                return PartialView("_List", result);
            }
            else
            {
                ViewBag.Message = Alert.Show("No Data Found", "", AlertType.Warning);
                return PartialView("_List", null);
            }
        }


    }



}