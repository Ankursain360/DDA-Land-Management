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
using LandInventory.Filters;
using Core.Enum;
using Utility.Helper;
using Dto.Master;
using Microsoft.AspNetCore.Http;

namespace LandInventory.Controllers
{
    public class RestorePropertyReportController : Controller
    {
            private readonly IPropertyRegistrationService _propertyregistrationService;

            public RestorePropertyReportController(IPropertyRegistrationService propertyregistrationService)
            {
                _propertyregistrationService = propertyregistrationService;
            }
        async Task BindDropDown(Propertyregistration propertyregistration)
        {
        
            propertyregistration.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
          
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Create()
        {
            Propertyregistration propertyregistration = new Propertyregistration();

            await BindDropDownView(propertyregistration);
            return View(propertyregistration);
        }
        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody]PropertyRegisterationSearchDto model)
        {
            dynamic result = null;
            if(model!= null)
            result = await _propertyregistrationService.GetRestorePropertyReportData(model);

            if (result != null)
            {
                return PartialView("_Index", result);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return PartialView("_Index", result);
            }

        }
        // Dropdown Dependency  calls below
        [HttpGet]
        public async Task<JsonResult> GetZoneList(int? departmentId)
        {
            departmentId = departmentId ?? 0;
            return Json(await _propertyregistrationService.GetZoneDropDownList(Convert.ToInt32(departmentId)));
        }
        [HttpGet]
        public async Task<JsonResult> GetDivisionList(int? zoneId)
        {
            zoneId = zoneId ?? 0;
            return Json(await _propertyregistrationService.GetDivisionDropDownList(Convert.ToInt32(zoneId)));
        }
        [HttpGet]
        public async Task<JsonResult> GetLocalityList(int? divisionId)
        {
            try
            {
                divisionId = divisionId ?? 0;
                return Json(await _propertyregistrationService.GetLocalityDropDownList2(Convert.ToInt32(divisionId)));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task BindDropDownView(Propertyregistration propertyregistration)
        {
            propertyregistration.ClassificationOfLandList = await _propertyregistrationService.GetClassificationOfLandDropDownListReport();
            propertyregistration.LandUseList = await _propertyregistrationService.GetLandUseDropDownList();
            propertyregistration.DisposalTypeList = await _propertyregistrationService.GetDisposalTypeDropDownList();
            propertyregistration.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList(); propertyregistration.TakenOverDepartmentList = await _propertyregistrationService.GetTakenDepartmentDropDownList();
            propertyregistration.HandOverDepartmentList = await _propertyregistrationService.GetHandedDepartmentDropDownList();
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _propertyregistrationService.FetchSingleResult(id);
            ViewBag.LayoutDocView = Data.LayoutFilePath;
            ViewBag.GeoDocView = Data.GeoFilePath;
            ViewBag.TakenOverDocView = Data.TakenOverFilePath;
            ViewBag.HandedOverDocView = Data.HandedOverFilePath;
            ViewBag.DisposalTypeDocView = Data.DisposalTypeFilePath;
            ViewBag.Isdispodedtrue = Data.IsDisposed;      
            await BindDropDownView(Data);

            Data.ZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.DepartmentId);
            Data.LocalityList = await _propertyregistrationService.GetLocalityDropDownList(Data.ZoneId);
            Data.DivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.ZoneId);
            Data.HandedOverZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.DepartmentId);
            Data.HandedOverDivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.ZoneId);
            Data.TakenOverZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.DepartmentId);
            Data.TakenOverDivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.ZoneId);


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> RestorePropertyReportList([FromBody] PropertyRegisterationSearchDto model)
        {
            var result = await _propertyregistrationService.GetAllRestorePropertyReportDataList(model);
            List<RestorePropertyReportListDto> data = new List<RestorePropertyReportListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new RestorePropertyReportListDto()
                    {
                        Id = result[i].Id,
                        InventoriedIn = result[i].InventoriedInId.ToString() == "1" ? "VLMS" : "Used",
                        PlannedUnplannedLand = result[i].PlannedUnplannedLand,

                        ClassificationofLand = result[i].ClassificationOfLand == null ? " " : result[i].ClassificationOfLand.Name,
                        Department = result[i].Department == null ? " " : result[i].Department.Name,
                        Zone = result[i].Zone == null ? " " : result[i].Zone.Name,
                        Division = result[i].Division == null ? " " : result[i].Division.Name,
                        Locality = result[i].LocalityId == null ? " " : result[i].Locality.Name,
                        KhasraNo = result[i].KhasraNo,
                        AddressWithLandmark = result[i].Palandmark,
                        PrimaryListNo = result[i].PrimaryListNo,
                        AreaInSqm = result[i].TotalArea.ToString(),
                        RestoreReason = result[i].Restoreproperty == null ? " " : result[i].Restoreproperty.RestoreReason,
                        RestoreDate = result[i].Restoreproperty == null ? " " : result[i].Restoreproperty.RestoreDate.ToString("dd-MMM-yyyy"),

                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            HttpContext.Session.Set("file", memory);
            //TempData["file"] = memory;
            return Ok();
        }
        [HttpGet]
        [AuthorizeContext(ViewAction.Download)]
        public virtual IActionResult download()
        {
            byte[] data = HttpContext.Session.Get("file") as byte[];
            HttpContext.Session.Remove("file");
            //byte[] data = TempData["file"] as byte[];
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "RestorePropertyReport.xlsx");

        }

    }
}
