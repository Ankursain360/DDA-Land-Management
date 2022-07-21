using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using Dto.Search;
using Core.Enum;
using Utility.Helper;
using Dto.Master;
using LandInventory.Filters;

using System.Collections.Generic;
using System.Linq;



namespace LandInventory.Controllers
{
    public class PropertyInventoryReport : BaseController
    {
        private readonly IPropertyRegistrationService _propertyregistrationService;

        public PropertyInventoryReport(IPropertyRegistrationService propertyregistrationService)
        {
            _propertyregistrationService = propertyregistrationService;
        }

        async Task BindDropDown(Propertyregistration propertyregistration)
        {
            propertyregistration.ClassificationOfLandList = await _propertyregistrationService.GetClassificationOfLandDropDownListReport();
       
            propertyregistration.LandUseList = await _propertyregistrationService.GetLandUseDropDownList();
            propertyregistration.DisposalTypeList = await _propertyregistrationService.GetDisposalTypeDropDownList();
            propertyregistration.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
            propertyregistration.KhasraNoList = await _propertyregistrationService.GetKhasraReportList();
         
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Propertyregistration propertyregistration = new Propertyregistration();
            propertyregistration.LitigationStatus = 2;
            propertyregistration.Encroached = 2;
            await BindDropDown(propertyregistration);
            return View(propertyregistration);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] PropertyRegisterationReportSearchDto model)
        {
            var result = await _propertyregistrationService.GetPropertyRegisterationReportData(model);

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

        #region Dropdown Dependency calls added  by renu 
        [HttpGet]
        public async Task<JsonResult> GetZoneList(int? departmentId)
        {
            departmentId = departmentId ?? 0;
            return Json(await _propertyregistrationService.GetZoneDropDownList(Convert.ToInt32(departmentId)));
        }

        [HttpGet]
        public async Task<JsonResult> GetLocalityList(int? zoneId)
        {
            zoneId = zoneId ?? 0;
            return Json(await _propertyregistrationService.GetLocalityDropDownList(Convert.ToInt32(zoneId)));
        }

        [HttpGet]
        public async Task<JsonResult> GetDivisionList(int? zoneId)
        {
            zoneId = zoneId ?? 0;
            return Json(await _propertyregistrationService.GetDivisionDropDownList(Convert.ToInt32(zoneId)));
        }
        #endregion


        public async Task<IActionResult> PropertyInventoryReportList()
        {
            var result = await _propertyregistrationService.GetAllPropertyRegistrationReportList();
            List<PropertyInventoryReportListDto> data = new List<PropertyInventoryReportListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    
                    data.Add(new PropertyInventoryReportListDto()
                    {

                        Id = result[i].Id,
                        InventoriedIn = result[i].InventoriedInId.ToString() == "1" ? "VLMS" : "Used",
                        PlannedUnplannedLand = result[i].PlannedUnplannedLand,

                        ClassificationofLand = result[i].ClassificationOfLand == null ? " " : result[i].ClassificationOfLand.Name,
                        Department = result[i].Department == null ? " " : result[i].Department.Name,
                        Zone = result[i].Zone == null ? " " : result[i].Zone.Name,
                        Division = result[i].Division == null ? " " : result[i].Division.Name,
                        Locality = result[i].Locality == null ? " " : result[i].Locality.Name,
                        KhasraNo = result[i].KhasraNo,
                        Colony = result[i].Colony,
                        Sector = result[i].Sector,
                        Block = result[i].Block,
                        Pocket = result[i].Pocket,
                        PlotNo = result[i].PlotNo,
                        PrimaryListNo = result[i].PrimaryListNo,
                        AddressWithLandmark = result[i].Palandmark,
                        AreaUnit = result[i].AreaUnit == 0 ? "bigha-biswa-bishwani" : result[i].AreaUnit == 1 ? "Sq Yd." : result[i].AreaUnit == 2 ? "Acre" : "Hectare",
                        TotalArea = result[i].TotalAreaInSqAcreHt.ToString(),
                        //    if (result[i].AreaUnit == 0)
                        //{
                        //   resu
                        //}
                        //  TotalArea = result[i].AreaUnit==0? result[i].TotalAreaInBigha.ToString() ?
                        TotalAreaSqmt = result[i].TotalArea.ToString(),
                        Encroachment = result[i].EncroachmentStatusId == 0 ? "No" : "Yes".ToString(),
                        EncroachmentStatus = result[i].EncroachedPartiallyFully == "0" ? "Partially Encroached" : "Fully Encroached",
                        EncroachmentArea = result[i].EncrochedArea.ToString(),
                        BuiltUpInEncroachmentArea = result[i].BuiltUpEncraochmentArea.ToString(),
                        Vacant = result[i].Vacant.ToString(),
                        ActionOnEncroachment = result[i].ActionOnEncroachment,
                        EncroachemntDetails = result[i].EncraochmentDetails,
                        ProtectionOfLand = result[i].Boundary == 0 ? "Boundary Wall" : result[i].AreaUnit == 1 ? "Fencing" : "None".ToString(),
                        AreaCovered = result[i].BoundaryAreaCovered.ToString(),
                        Dimension = result[i].BoundaryDimension,
                        BoundaryRemarks = result[i].BoundaryRemarks,
                        BuiltType = result[i].BuiltUp == 0 ? "No" : "Yes",
                        LitigationStatus = result[i].LitigationStatus == 0 ? "No" : "Yes",
                        CourtName = result[i].CourtName,
                        CaseNo = result[i].CaseNo,
                        OppositeParty = result[i].OppositeParty,
                        LitigationStatusRemarks = result[i].LitigationStatusRemarks,
                        GeoReferencing = result[i].GeoReferencing == 0 ? "No" : "Yes",
                        Remarks = result[i].Remarks,

                    }) ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }



    }
}
