using Dto.Search;
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

namespace LandInventory.Controllers
{
    public class PlanningController : Controller
    {

        public IConfiguration _configuration;
        public readonly IPlanningService _planningService;
        public PlanningController(IPlanningService planningService, IConfiguration configuration)
        {
            _planningService = planningService;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> VerificationPage()
        {
            return View();
        }
        public async Task<PartialViewResult> List([FromBody] PlanningSearchDto dto)
        {
            var list = await _planningService.GetPagedPlanning(dto);
            return PartialView("_List", list);
        }
        public async Task<PartialViewResult> VerificationPageList([FromBody] PlanningSearchDto dto)
        {
            var list = await _planningService.GetUnverifiedPagedPlanning(dto);
            return PartialView("_PlanningVerification", list);
        }
        public async Task<IActionResult> Create()
        {
            Planning planning = new Planning();
            planning.DepartmentList = await _planningService.GetAllDepartment();
            planning.ZoneList = await _planningService.GetAllZone(planning.DepartmentId);
            planning.DivisionList = await _planningService.GetAllDivision(planning.ZoneId);
            return View(planning);
        }
        [HttpPost]
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
                result = await _planningService.VerifyProperties(planning.Id);
                if (result)
                {
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
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
    }
}
