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
using Utility.Helper;

namespace LandTransfer.Controllers
{
    public class CurrentStatusOfHandedOverTakenOverLandController : BaseController
    {
        public IConfiguration _configuration;
        public readonly ILandTransferService _landTransferService;
        //string targetPathLayout = string.Empty;
        string surveyReportFilePath = string.Empty;
        string actionReportFilePath = string.Empty;
        public CurrentStatusOfHandedOverTakenOverLandController(ILandTransferService landTransferService, IConfiguration configuration)
        {
            _landTransferService = landTransferService;
            _configuration = configuration;
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
        public async Task<IActionResult> Create(int id, Landtransfer landTransfer)
        {
            var Data = await _landTransferService.FetchSingleResult(id);

            Data.DepartmentList = await _landTransferService.GetAllDepartment();
            Data.ZoneList = await _landTransferService.GetAllZone(Data.DepartmentId);
            Data.DivisionList = await _landTransferService.GetAllDivisionList(Data.ZoneId);
            Data.LocalityList = await _landTransferService.GetAllLocalityList(Data.DivisionId);
            //if (Data == null)
            //{
            //    return NotFound();
            //}
            //return View(Data);

          

            if (ModelState.IsValid)

            {
                //var result = await _landTransferService.Create(landTransfer);
                //if (result)
                //{

                surveyReportFilePath = _configuration.GetSection("FilePaths:CurrentStatusOfLand:CurrentLandSurveyReport").Value.ToString();
                actionReportFilePath = _configuration.GetSection("FilePaths:CurrentStatusOfLand:CurrentLandActionReport").Value.ToString();
                FileHelper file = new FileHelper();

                if (landTransfer.SurveyReportFile != null)
                {
                    landTransfer.SurveyReportFilePath = file.SaveFile(surveyReportFilePath, landTransfer.SurveyReportFile);
                }
                if (landTransfer.ActionReportFile != null)
                {
                    landTransfer.ActionReportFilePath = file.SaveFile(actionReportFilePath, landTransfer.ActionReportFile);
                }

                Currentstatusoflandhistory model = new Currentstatusoflandhistory();

                model.LandTransferId = landTransfer.Id;
                model.Tsssurvey = landTransfer.TSSSurvey;
                model.SurveyReportFilePath = landTransfer.SurveyReportFilePath;
                model.Encroachment = landTransfer.Encroachment;
                model.EncroachedArea = landTransfer.EncroachedArea;
                model.ActionOnEncroachment = landTransfer.ActionOnEncroachment;
                model.ActionReportFilePath = landTransfer.ActionReportFilePath;
                model.FencingBoundaryWall = landTransfer.FencingBoundaryWall;
                model.AreaCovered = landTransfer.AreaCovered;
                model.Dimension = landTransfer.Dimension;
                model.PlotUtilization = landTransfer.PlotUtilization;
                model.AreaUtilised = landTransfer.AreaUtilised;
                model.BalanceArea = landTransfer.BalanceArea;

                model.Status = landTransfer.Status;
                model.PlannedUnplannedLand = landTransfer.PlannedUnplannedLand;
                model.MainLandUse = landTransfer.MainLandUse;
                model.SubUse = landTransfer.SubUse;
                model.Remarks = landTransfer.currentLandRemarks;


                var result2 = await _landTransferService.SaveCurrentstatusoflandhistory(model);


                
                if (result2 == true)
                {
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    var result1 = await _landTransferService.GetAllLandTransfer();
                    return View("Index", result1);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(Data);
                }
            }
            //    else
            //    {
            //        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            //        return View(Data);
            //    }
            //}
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(Data);
            }
            //return View(Data);

        }
        public IActionResult ViewHistory()
        {
            return View();
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
