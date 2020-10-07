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
using SiteMaster.Controllers;

namespace LandTransfer.Controllers
{
    public class LandTransferController : BaseController
    {
        public readonly ILandTransferService _landTransferService;
        public LandTransferController(ILandTransferService landTransferService)
        {
            _landTransferService = landTransferService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            Landtransfer model = new Landtransfer();
            model.DepartmentList = await _landTransferService.GetAllDepartment();
            model.ZoneList = await _landTransferService.GetAllZone(model.DepartmentId);
            model.DivisionList = await _landTransferService.GetAllDivisionList(model.ZoneId);
            model.LocalityList = await _landTransferService.GetAllLocalityList(model.DivisionId);
            return View(model);
        }
        [HttpGet]
        public async Task<JsonResult> GetZoneList(int? DepartmentId)
        {
            DepartmentId = DepartmentId ?? 0;
            return Json(await _landTransferService.GetAllZone(Convert.ToInt32(DepartmentId)));
        }

        [HttpGet]
        public async Task<JsonResult> GetDivisionList(int? ZoneId)
        {
            ZoneId = ZoneId ?? 0;
            return Json(await _landTransferService.GetAllDivisionList(Convert.ToInt32(ZoneId)));
        }
        [HttpGet]
        public async Task<JsonResult> GetLocalityList(int? DivisionId)
        {
            DivisionId = DivisionId ?? 0;
            return Json(await _landTransferService.GetAllLocalityList(Convert.ToInt32(DivisionId)));
        }
        public async Task<PartialViewResult> GetHistoryDetails(string KhasraNo)
        {
            try
            {
                var result = await _landTransferService.GetHistoryDetails(KhasraNo);
                if (result != null)
                {
                    return PartialView("_HistoryDetails", result);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return PartialView();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
