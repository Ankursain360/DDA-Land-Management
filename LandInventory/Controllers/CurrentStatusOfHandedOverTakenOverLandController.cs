using System;
using System.Collections.Generic;
using System.IO;
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

namespace LandInventory.Controllers
{
    public class CurrentStatusOfHandedOverTakenOverLandController : BaseController
    {
        public IConfiguration _configuration;
        public readonly ILandTransferService _landTransferService;
        public readonly IPropertyRegistrationService _propertyregistrationService;
        public readonly ICurrentstatusoflandhistoryService _currentstatusoflandhistoryService;
        //string targetPathLayout = string.Empty;
        string surveyReportFilePath = string.Empty;
        string actionReportFilePath = string.Empty;
        public CurrentStatusOfHandedOverTakenOverLandController(ILandTransferService landTransferService, IConfiguration configuration, ICurrentstatusoflandhistoryService currentstatusoflandhistoryService, IPropertyRegistrationService propertyregistrationService)
        {
            _landTransferService = landTransferService;
            _configuration = configuration;
            _currentstatusoflandhistoryService = currentstatusoflandhistoryService;
            _propertyregistrationService = propertyregistrationService;
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] LandTransferSearchDto model)
        {
            var result = await _landTransferService.GetPagedLandTransfer(model);
            return PartialView("_List", result);
        }
        public IActionResult Index()
        {
            //List<Landtransfer> list = await _landTransferService.GetAllLandTransfer();
            //return View(list);
            return View();
        }
        public async Task<IActionResult> Create(int id)
        {
            Currentstatusoflandhistory Model = new Currentstatusoflandhistory();
            //var Data = await _landTransferService.FetchSingleResultWithPropertyRegistration(id);
            var Data = await _landTransferService.FetchSingleResult(id);
            Data.HandedOverZoneList = await _landTransferService.GetAllZone(Data.HandedOverDepartmentId ?? 0);
            Data.HandedOverDivisionList = await _landTransferService.GetAllDivisionList(Data.HandedOverZoneId == null ? 0 : Data.HandedOverZoneId);
            Data.TakenOverZoneList = await _landTransferService.GetAllZone(Data.TakenOverDepartmentId ?? 0);
            Data.TakenOverDivisionList = await _landTransferService.GetAllDivisionList(Data.TakenOverZoneId == null ? 0 : Data.HandedOverZoneId);

            Data.Propertyregistration = await _propertyregistrationService.FetchSingleResult(Data.PropertyRegistrationId);
            Data.Propertyregistration.ClassificationOfLandList = await _propertyregistrationService.GetClassificationOfLandDropDownList();
            Data.Propertyregistration.LandUseList = await _propertyregistrationService.GetLandUseDropDownList();
            Data.Propertyregistration.DisposalTypeList = await _propertyregistrationService.GetDisposalTypeDropDownList();
            Data.Propertyregistration.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
            Data.Propertyregistration.ZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.Propertyregistration.DepartmentId);
            Data.Propertyregistration.LocalityList = await _propertyregistrationService.GetLocalityDropDownList(Data.Propertyregistration.DivisionId);
            Data.Propertyregistration.DivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.Propertyregistration.ZoneId);

            Data.Propertyregistration.TakenOverDepartmentList = await _propertyregistrationService.GetTakenDepartmentDropDownList();
            Data.Propertyregistration.TakenOverZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.Propertyregistration.TakenOverDepartmentId ?? 0);
            Data.Propertyregistration.TakenOverDivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.Propertyregistration.TakenOverZoneId ?? 0);

            Data.Propertyregistration.HandOverDepartmentList = await _propertyregistrationService.GetHandedDepartmentDropDownList();
            Data.Propertyregistration.HandedOverZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.Propertyregistration.HandedOverDepartmentId ?? 0);
            Data.Propertyregistration.HandedOverDivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.Propertyregistration.HandedOverZoneId ?? 0);


            Data.LandTransferList = await _landTransferService.GetAllLandTransfer(Data.Propertyregistration.Id);
            Data.DepartmentList = await _landTransferService.GetAllDepartment();
            Model.LandTransfer = Data;
            return View(Model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(int id, Currentstatusoflandhistory currentstatusoflandhistory)
        {
            var Data = await _landTransferService.FetchSingleResult(id);

            Data.DepartmentList = await _landTransferService.GetAllDepartment();

            currentstatusoflandhistory.LandTransfer = Data;
            currentstatusoflandhistory.Id = 0;
            if (currentstatusoflandhistory.LandTransferId == 0)
            {
                return NotFound();
            }
            //var errors = ModelState.Values.SelectMany(x => x.Errors);
            //ModelState.Remove(null);
            //if (ModelState.IsValid)
            {
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
                    //var result1 = await _landTransferService.GetAllLandTransfer();
                    return View("Index");
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(currentstatusoflandhistory);
                }
            }
        }
        public async Task<IActionResult> ViewHistory(int id)
        {
            List<Currentstatusoflandhistory> list = await _currentstatusoflandhistoryService.GetCurrentstatusoflandhistory(id);
            return View(list);
            //return View();  
        }

        public IActionResult History(int id)
        {
            var Id = id;
            // var Id = Request.Path.ToString().Split('/').LastOrDefault();
            //var Id = Context.Request.Query["id"];
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> HistoryDetails([FromBody] CurrentstatusoflandhistorySearchDto model)
        {
            // var Id = Request.Path.ToString().Split('/').LastOrDefault();
            var result = await _currentstatusoflandhistoryService.GetPagedCurrentstatusoflandhistory(model);
            return PartialView("_HistoryDetails", result);
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

        // **************** download file **********************

        public async Task<IActionResult> DownloadSurveyReportFile(int Id)
        {
            FileHelper file = new FileHelper();
            var Data = await _currentstatusoflandhistoryService.FetchSingleResult(Id);

            string filename = Data.SurveyReportFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadActionReportFile(int Id)
        {
            FileHelper file = new FileHelper();
            var Data = await _currentstatusoflandhistoryService.FetchSingleResult(Id);
            string filename = Data.ActionReportFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> Download(int Id)
        {
            FileHelper file = new FileHelper();
            var Data = await _landTransferService.FetchSingleResult(Id);
            string filename = Data.CopyofOrderDocPath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public FileResult ViewDocument(string path)
        {
            FileHelper file = new FileHelper();
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
    }
}