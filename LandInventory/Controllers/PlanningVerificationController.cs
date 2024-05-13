using Core.Enum;
using Dto.Search;
using LandInventory.Filters;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Utility.Helper;
using Dto.Master;


namespace LandInventory.Controllers
{
    public class PlanningVerificationController : BaseController
    {

        public IConfiguration _configuration;
        public readonly IPlanningService _planningService;
        public PlanningVerificationController(IPlanningService planningService, IConfiguration configuration)
        {
            _planningService = planningService;
            _configuration = configuration;
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> VerificationPage()
        {
            return View();
        }
        public async Task<PartialViewResult> VerificationPageList([FromBody] PlanningSearchDto dto)
        {
            var list = await _planningService.GetUnverifiedPagedPlanning(dto);
            return PartialView("_PlanningVerification", list);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Planning planning = new Planning();
            planning.DepartmentList = await _planningService.GetAllDepartment();
            planning.ZoneList = await _planningService.GetAllZone(planning.DepartmentId);
            planning.DivisionList = await _planningService.GetAllDivision(planning.ZoneId);
            return View(planning);
        }


        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Planning planning)
        {
            planning.DepartmentList = await _planningService.GetAllDepartment();
            planning.ZoneList = await _planningService.GetAllZone(planning.DepartmentId);
            planning.DivisionList = await _planningService.GetAllDivision(planning.ZoneId);
            if (ModelState.IsValid)
            {
                var result = await _planningService.Create(planning);
                if (result)
                {
                    List<PlanningProperties> planningProperties = new List<PlanningProperties>();
                    if (planning.PlannedProperties != null)
                    {
                        for (int i = 0; i < planning.PlannedProperties.Count; i++)
                        {
                            planningProperties.Add(new PlanningProperties
                            {
                                PropertyRegistrationId = planning.PlannedProperties[i],
                                PlanningId = planning.Id,
                                PropertyType = 1,
                                IsActive = 1,
                                CreatedBy = 1,
                                CreatedDate = DateTime.Now
                            });
                        }
                    }
                    if (planning.UnplannedProperties != null)
                    {
                        for (int i = 0; i < planning.UnplannedProperties.Count; i++)
                        {
                            planningProperties.Add(new PlanningProperties
                            {
                                PropertyRegistrationId = planning.UnplannedProperties[i],
                                PlanningId = planning.Id,
                                PropertyType = 0,
                                IsActive = 1,
                                CreatedBy = 1,
                                CreatedDate = DateTime.Now
                            });
                        }
                    }
                    result = await _planningService.CreateProperties(planningProperties);
                    if (result)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        return View("index");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(planning);
                    }
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                }
            }
            return View(planning);
        }


        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            Planning planning = await _planningService.FetchSingleResult(id);
            planning.DepartmentList = await _planningService.GetAllDepartment();
            planning.ZoneList = await _planningService.GetAllZone(planning.DepartmentId);
            planning.DivisionList = await _planningService.GetAllDivision(planning.ZoneId);
            planning.PlannedList = await _planningService.GetPlannedProperties(planning.DepartmentId, planning.ZoneId, planning.DivisionId);
            planning.UnplannedList = await _planningService.GetUnplannedProperties(planning.DepartmentId, planning.ZoneId, planning.DivisionId);
            planning.PlannedProperties = await _planningService.FetchPlannedProperties(id);
            planning.UnplannedProperties = await _planningService.FetchUnplannedProperties(id);
            return View(planning);
        }


        [AuthorizeContext(ViewAction.Verify)]
        public async Task<IActionResult> VerifyPropertyView(int id)
        {
            Planning planning = await _planningService.FetchSingleResult(id);
            planning.DepartmentList = await _planningService.GetAllDepartment();
            planning.ZoneList = await _planningService.GetAllZone(planning.DepartmentId);
            planning.DivisionList = await _planningService.GetAllDivision(planning.ZoneId);
            planning.PlannedList = await _planningService.GetPlannedProperties(planning.DepartmentId, planning.ZoneId, planning.DivisionId);
            planning.UnplannedList = await _planningService.GetUnplannedProperties(planning.DepartmentId, planning.ZoneId, planning.DivisionId);
            planning.PlannedProperties = await _planningService.FetchPlannedProperties(id);
            planning.UnplannedProperties = await _planningService.FetchUnplannedProperties(id);
            return View(planning);
        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            Planning planning = await _planningService.FetchSingleResult(id);
            planning.DepartmentList = await _planningService.GetAllDepartment();
            planning.ZoneList = await _planningService.GetAllZone(planning.DepartmentId);
            planning.DivisionList = await _planningService.GetAllDivision(planning.ZoneId);
            planning.PlannedList = await _planningService.GetPlannedProperties(planning.DepartmentId, planning.ZoneId, planning.DivisionId);
            planning.UnplannedList = await _planningService.GetUnplannedProperties(planning.DepartmentId, planning.ZoneId, planning.DivisionId);
            planning.PlannedProperties = await _planningService.FetchPlannedProperties(id);
            planning.UnplannedProperties = await _planningService.FetchUnplannedProperties(id);
            return View(planning);
        }


        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(Planning planning)
        {
            planning.DepartmentList = await _planningService.GetAllDepartment();
            planning.ZoneList = await _planningService.GetAllZone(planning.DepartmentId);
            planning.DivisionList = await _planningService.GetAllDivision(planning.ZoneId);
            planning.PlannedList = await _planningService.GetPlannedProperties(planning.DepartmentId, planning.ZoneId, planning.DivisionId);
            planning.UnplannedList = await _planningService.GetUnplannedProperties(planning.DepartmentId, planning.ZoneId, planning.DivisionId);
            if (ModelState.IsValid)
            {
                var result = await _planningService.Update(planning.Id, planning);
                if (result)
                {
                    List<PlanningProperties> planningProperties = new List<PlanningProperties>();
                    if (planning.PlannedProperties != null)
                    {
                        for (int i = 0; i < planning.PlannedProperties.Count; i++)
                        {
                            planningProperties.Add(new PlanningProperties
                            {
                                PropertyRegistrationId = planning.PlannedProperties[i],
                                PlanningId = planning.Id,
                                PropertyType = 1,
                                IsActive = 1,
                                CreatedBy = 1,
                                CreatedDate = DateTime.Now
                            });
                        }
                    }
                    if (planning.UnplannedProperties != null)
                    {
                        for (int i = 0; i < planning.UnplannedProperties.Count; i++)
                        {
                            planningProperties.Add(new PlanningProperties
                            {
                                PropertyRegistrationId = planning.UnplannedProperties[i],
                                PlanningId = planning.Id,
                                PropertyType = 0,
                                IsActive = 1,
                                CreatedBy = 1,
                                CreatedDate = DateTime.Now
                            });
                        }
                    }
                    result = await _planningService.CreateProperties(planningProperties);
                    if (result)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        return View("index");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(planning);
                    }
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(planning);
                }
            }
            else
            {
                return View(planning);
            }
        }
        [HttpPost]
        [AuthorizeContext(ViewAction.Verify)]
        public async Task<IActionResult> VerifyPropertyView(Planning planning)
        {
            planning.DepartmentList = await _planningService.GetAllDepartment();
            planning.ZoneList = await _planningService.GetAllZone(planning.DepartmentId);
            planning.DivisionList = await _planningService.GetAllDivision(planning.ZoneId);
            planning.PlannedList = await _planningService.GetPlannedProperties(planning.DepartmentId, planning.ZoneId, planning.DivisionId);
            planning.UnplannedList = await _planningService.GetUnplannedProperties(planning.DepartmentId, planning.ZoneId, planning.DivisionId);
            var result = await _planningService.VerifyProperty(planning.Id, planning);
            if (result)
            {
              var result1 = await _planningService.VerifyProperties(planning.Id);
                if (result1)
                {
                    ViewBag.Message = Alert.Show(Messages.Verifiedsuccesfuly, "", AlertType.Success);
                    return View("VerificationPage");
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(planning);
                }
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(planning);
            }
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _planningService.Delete(id);
            if (result)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                return View("Index");
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View("Index");
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetZoneList(int? DepartmentId)
        {
            DepartmentId = DepartmentId ?? 0;
            var zoneList = await _planningService.GetAllZone(Convert.ToInt32(DepartmentId));
            return Json(zoneList);
        }
        [HttpGet]
        public async Task<JsonResult> GetDivisionList(int? ZoneId)
        {
            ZoneId = ZoneId ?? 0;
            return Json(await _planningService.GetAllDivision(Convert.ToInt32(ZoneId)));
        }
        [HttpGet]
        public async Task<JsonResult> GetPlannedProperties(int? DepartmentId, int? ZoneId, int? DivisionId)
        {
            DepartmentId = DepartmentId ?? 0;
            ZoneId = ZoneId ?? 0;
            DivisionId = DivisionId ?? 0;
            return Json(await _planningService.GetPlannedProperties(Convert.ToInt32(DepartmentId), Convert.ToInt32(ZoneId), Convert.ToInt32(DivisionId)));
        }
        [HttpGet]
        public async Task<JsonResult> GetUnplannedProperties(int? DepartmentId, int? ZoneId, int? DivisionId)
        {
            DepartmentId = DepartmentId ?? 0;
            ZoneId = ZoneId ?? 0;
            DivisionId = DivisionId ?? 0;
            return Json(await _planningService.GetUnplannedProperties(Convert.ToInt32(DepartmentId), Convert.ToInt32(ZoneId), Convert.ToInt32(DivisionId)));
        }


        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> UpdatePlanningDetailsList()
        {

            string UnplannedProperty_text = string.Empty;
            string PlannedProperty_text = string.Empty;
            var result = await _planningService.GetAllPlanninglist();
            List<UpdatePlanningDetailsListDto> data = new List<UpdatePlanningDetailsListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    var pp = result[i].PlanningProperties;
                    foreach (var d in pp)
                    {
                        if (d.PropertyType == 0)
                        {
                            UnplannedProperty_text = "#aa";
                        }
                        if (d.PropertyType == 1)
                        {
                            UnplannedProperty_text = "#bb";
                        }

                    }
                    data.Add(new UpdatePlanningDetailsListDto()
                    {
                        Id = result[i].Id,
                        Department = result[i].Department == null ? " " : result[i].Department.Name,
                        Division = result[i].Division == null ? " " : result[i].Division.Name,
                        Zone = result[i].Zone == null ? " " : result[i].Zone.Name,
                        UnplannedProperty = UnplannedProperty_text,
                        plannedProperty = PlannedProperty_text,







                        //IsActive = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }






    }
}
