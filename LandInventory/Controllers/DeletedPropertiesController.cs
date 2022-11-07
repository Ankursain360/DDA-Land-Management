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

namespace LandInventory.Controllers
{
    public class DeletedPropertiesController : Controller
    {
        private readonly IPropertyRegistrationService _propertyregistrationService;

        public DeletedPropertiesController(IPropertyRegistrationService propertyregistrationService)
        {
            _propertyregistrationService = propertyregistrationService;
        }

        async Task BindDropDown(Propertyregistration propertyregistration)
        {
            propertyregistration.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
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
        public async Task<JsonResult> GetPrimaryNoList(int? divisionId)
        {
            divisionId = divisionId ?? 0;
            return Json(await _propertyregistrationService.GetPrimaryListNoList(Convert.ToInt32(divisionId)));
        }


        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Propertyregistration propertyregistration = new Propertyregistration();

            await BindDropDownView(propertyregistration);
            return View(propertyregistration);
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody]PropertyRegisterationSearchDto model)
        {
            var result = await _propertyregistrationService.GetDeletedLandReportData(model);
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

        public async Task<FileResult> DisposePdfFile(int Id)
        {
            FileHelper file = new FileHelper();
            Disposedproperty data = await _propertyregistrationService.FetchSingleRecord(Id);
            string path = data.DisposalTypeFilePath;
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, file.GetContentType(path));
        }




        public async Task<IActionResult> DeletedPropertyList([FromBody] PropertyRegisterationSearchDto model)
        {
            var result = await _propertyregistrationService.GetAllDeletedLandReportDataList(model);
            List<DeletedPropertyListDto> data = new List<DeletedPropertyListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new DeletedPropertyListDto()
                    {
                       /// Id = result[i].Id,
                        InventoriedIn = result[i].InventoriedInId.ToString() == "1" ? "VLMS" : "Used",
                        PlannedUnplannedLand = result[i].PlannedUnplannedLand,
                        ClassificationofLand = result[i].ClassificationOfLand == null ? " " : result[i].ClassificationOfLand.Name,
                        Department = result[i].Department == null ? " " : result[i].Department.Name,
                        Zone = result[i].Zone == null ? " " : result[i].Zone.Name,
                        Division = result[i].Division == null ? " " : result[i].Division.Name,
                        Locality = result[i].LocalityId == null ? " " : result[i].Locality.Name,                     
                        AddressWithLandmark=result[i].Palandmark,
                        PrimaryListNo = result[i].PrimaryListNo,
                        KhasraNo = result[i].KhasraNo,
                        Reason = result[i].Reason == null ? " " : result[i].Deletedproperty.Reason,
                        DeletedOn = result[i].Deletedproperty == null ? " " : result[i].Deletedproperty.DeletedDate.ToString("dd-MMM-yyyy"),

                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            TempData["file"] = memory;
            return Ok();
        }
        [HttpGet]
        public virtual IActionResult download()
        {
            byte[] data = TempData["file"] as byte[];
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MORLandInventory.xlsx");

        }


    }
}

