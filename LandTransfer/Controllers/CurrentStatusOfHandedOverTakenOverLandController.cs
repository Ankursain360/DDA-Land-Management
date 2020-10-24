using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Service.IApplicationService;
using Utility.Helper;

namespace LandTransfer.Controllers
{
    public class CurrentStatusOfHandedOverTakenOverLandController : BaseController
    {
        public IConfiguration _configuration;
        public readonly ILandTransferService _landTransferService;
        public readonly ICurrentstatusoflandhistoryService _currentstatusoflandhistoryService;
        //string targetPathLayout = string.Empty;
        string surveyReportFilePath = string.Empty;
        string actionReportFilePath = string.Empty;
        public CurrentStatusOfHandedOverTakenOverLandController(ILandTransferService landTransferService, IConfiguration configuration, ICurrentstatusoflandhistoryService currentstatusoflandhistoryService)
        {
            _landTransferService = landTransferService;
            _configuration = configuration;
            _currentstatusoflandhistoryService = currentstatusoflandhistoryService;
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] LandTransferSearchDto model)
        {
            var result = await _landTransferService.GetPagedLandTransfer(model);
            return PartialView("_List", result);
        }
        public async Task<IActionResult> Index()
        {
            List<Landtransfer> list = await _landTransferService.GetAllLandTransfer();
            return View(list);
        }
        public async Task<IActionResult> Create(int id)
        {
            Currentstatusoflandhistory Model = new Currentstatusoflandhistory();
            var Data = await _landTransferService.FetchSingleResult(id);

            Data.DepartmentList = await _landTransferService.GetAllDepartment();
            Data.ZoneList = await _landTransferService.GetAllZone(Data.DepartmentId);
            Data.DivisionList = await _landTransferService.GetAllDivisionList(Data.ZoneId);
            Data.LocalityList = await _landTransferService.GetAllLocalityList(Data.DivisionId);
            Model.LandTransfer = Data;
            if (Data == null)
            {
                return NotFound();
            }
            return View(Model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(int id, Currentstatusoflandhistory currentstatusoflandhistory)
        {
            var Data = await _landTransferService.FetchSingleResult(id);

            Data.DepartmentList = await _landTransferService.GetAllDepartment();
            Data.ZoneList = await _landTransferService.GetAllZone(Data.DepartmentId);
            Data.DivisionList = await _landTransferService.GetAllDivisionList(Data.ZoneId);
            Data.LocalityList = await _landTransferService.GetAllLocalityList(Data.DivisionId);
            currentstatusoflandhistory.LandTransfer = Data;

            if (id == 0)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                surveyReportFilePath = _configuration.GetSection("FilePaths:CurrentStatusOfLand:CurrentLandSurveyReport").Value.ToString();
                actionReportFilePath = _configuration.GetSection("FilePaths:CurrentStatusOfLand:CurrentLandActionReport").Value.ToString();
                FileHelper file = new FileHelper();

                if (currentstatusoflandhistory.SurveyReportFile != null)
                {
                    currentstatusoflandhistory.SurveyReportFilePath = file.SaveFile(surveyReportFilePath, currentstatusoflandhistory.SurveyReportFile);
                }
                if (currentstatusoflandhistory.ActionReportFile != null)
                {
                    currentstatusoflandhistory.ActionReportFilePath = file.SaveFile(actionReportFilePath, currentstatusoflandhistory.ActionReportFile);
                }
                currentstatusoflandhistory.LandTransferId = currentstatusoflandhistory.LandTransfer.Id;
                var result = await _currentstatusoflandhistoryService.Create(currentstatusoflandhistory);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    var result1 = await _landTransferService.GetAllLandTransfer();
                    return View("Index", result1);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(currentstatusoflandhistory);
                }
            //}
            //else
            //{
            //    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            //    return View(currentstatusoflandhistory);
            //}
        } 
        public async Task<IActionResult> ViewHistory(int landtransferId)
        {
            List<Currentstatusoflandhistory> list = await _landTransferService.GetCurrentstatusoflandhistory( landtransferId);
            return View(list);
            //return View();
        }

        //*************dropdown methods *****
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
    }
}
