﻿using Core.Enum;
using Dto.Master;
using Dto.Search;
using EncroachmentDemolition.Filters;
using Libraries.Model.Entity;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Utility.Helper;

namespace EncroachmentDemolition.Controllers
{
    public class MonthlyRosterController : BaseController
    {
        public readonly IMonthlyRosterService _monthlyRosterService;
        public MonthlyRosterController(IMonthlyRosterService monthlyRosterService)
        {
            _monthlyRosterService = monthlyRosterService;
        }

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            MonthlyRoaster model = new MonthlyRoaster();
            model.SecurityGuardList = await _monthlyRosterService.SecurityGuardList();
            model.DepartmentList = await _monthlyRosterService.GetAllDepartmentList();
            model.YearList = await GetYearList();
            model.MonthList = await GetMonthsList();
            return View(model);
        }


        [AuthorizeContext(ViewAction.Edit)]
        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] MonthlyRoasterDto monthlyRoasterDto)
        {
            MonthlyRoaster model = new MonthlyRoaster();
            model = await _monthlyRosterService.GetMonthlyRoasterById(monthlyRoasterDto.Id);
            model.SecurityGuardList = await _monthlyRosterService.SecurityGuardList();
            model.DepartmentList = await _monthlyRosterService.GetAllDepartmentList();
            model.ZoneList = await _monthlyRosterService.GetAllZone(model.DepartmentId);
            model.DivisionList = await _monthlyRosterService.GetAllDivisionList(model.ZoneId);
            model.LocalityList = await _monthlyRosterService.GetAllLocalityList(model.DivisionId);
            model.PrimaryList = await _monthlyRosterService.GetPrimaryListNoList(model.DivisionId, model.DepartmentId, model.ZoneId, model.LocalityId ?? 0);
            model.YearList = await GetYearList();
            model.MonthList = await GetMonthsList();
            if (ModelState.IsValid)
            {
                var monthlyRoaster = new MonthlyRoaster();
                monthlyRoaster.Id = monthlyRoasterDto.Id;
                monthlyRoaster.Month = monthlyRoasterDto.month;
                monthlyRoaster.Year = monthlyRoasterDto.year;
                monthlyRoaster.ZoneId = monthlyRoasterDto.Zone;
                monthlyRoaster.DivisionId = monthlyRoasterDto.Division;
                monthlyRoaster.LocalityId = monthlyRoasterDto.Locality;
                monthlyRoaster.Template = monthlyRoasterDto.Template;
                monthlyRoaster.ModifiedBy = SiteContext.UserId;
                monthlyRoaster.UserprofileId = monthlyRoasterDto.securityGuard;
                var result = await _monthlyRosterService.Update(monthlyRoasterDto.Id, monthlyRoaster);
                if (result)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    return Json(Url.Action("Index", "MonthlyRoster"));
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return Json(Url.Action("Create", "MonthlyRoster"));
                }
            }
            else
            {
                return Json(Url.Action("Create", "MonthlyRoster"));
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            MonthlyRoaster model = new MonthlyRoaster();
            model = await _monthlyRosterService.GetMonthlyRoasterById(id);
            model.SecurityGuardList = await _monthlyRosterService.SecurityGuardList();
            model.DepartmentList = await _monthlyRosterService.GetAllDepartmentList();
            model.ZoneList = await _monthlyRosterService.GetAllZone(model.DepartmentId);
            model.DivisionList = await _monthlyRosterService.GetAllDivisionList(model.ZoneId);
            model.LocalityList = await _monthlyRosterService.GetAllLocalityList(model.DivisionId);
            model.PrimaryList = await _monthlyRosterService.GetPrimaryListNoList(model.DivisionId, model.DepartmentId, model.ZoneId, model.LocalityId ?? 0);
            model.YearList = await GetYearList();
            model.MonthList = await GetMonthsList();
            var dates = new List<MonthlyRoasterPartial>();
            for (var date = new DateTime(model.Year, model.Month, 1); date.Month == model.Month; date = date.AddDays(1))
            {
                dates.Add(new MonthlyRoasterPartial
                {
                    Date = Convert.ToDateTime(date).ToString("dd-MMM-yyyy"),
                    Day = Convert.ToDateTime(date).DayOfWeek.ToString()
                });
            }
            model.MonthlyRoasterPartial = dates;
            return View(model);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            MonthlyRoaster model = new MonthlyRoaster();
            model = await _monthlyRosterService.GetMonthlyRoasterById(id);
            model.SecurityGuardList = await _monthlyRosterService.SecurityGuardList();
            model.DepartmentList = await _monthlyRosterService.GetAllDepartmentList();
            model.ZoneList = await _monthlyRosterService.GetAllZone(model.DepartmentId);
            model.DivisionList = await _monthlyRosterService.GetAllDivisionList(model.ZoneId);
            model.LocalityList = await _monthlyRosterService.GetAllLocalityList(model.DivisionId);
            model.PrimaryList = await _monthlyRosterService.GetPrimaryListNoList(model.DivisionId, model.DepartmentId, model.ZoneId, model.LocalityId ?? 0);
            model.YearList = await GetYearList();
            model.MonthList = await GetMonthsList();
            var dates = new List<MonthlyRoasterPartial>();
            for (var date = new DateTime(model.Year, model.Month, 1); date.Month == model.Month; date = date.AddDays(1))
            {
                dates.Add(new MonthlyRoasterPartial
                {
                    Date = Convert.ToDateTime(date).ToString("dd-MMM-yyyy"),
                    Day = Convert.ToDateTime(date).DayOfWeek.ToString()
                });
            }
            model.MonthlyRoasterPartial = dates;
            return View(model);
        }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _monthlyRosterService.DeleteRoaster(id);
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
        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create([FromBody] MonthlyRoasterDto monthlyRoasterDto)
        {
            MonthlyRoaster model = new MonthlyRoaster();
            model.SecurityGuardList = await _monthlyRosterService.SecurityGuardList();
            model.DepartmentList = await _monthlyRosterService.GetAllDepartmentList();
            model.YearList = await GetYearList();
            model.MonthList = await GetMonthsList();
            if (ModelState.IsValid)
            {
                var modelData = new MonthlyRoaster
                {
                    Month = monthlyRoasterDto.month,
                    Year = monthlyRoasterDto.year,
                    DepartmentId = monthlyRoasterDto.Department,
                    ZoneId = monthlyRoasterDto.Zone,
                    DivisionId = monthlyRoasterDto.Division,
                    LocalityId = monthlyRoasterDto.Locality,
                    UserprofileId = monthlyRoasterDto.securityGuard,
                    CreatedBy = SiteContext.UserId,
                    CreatedDate = DateTime.Now,
                    Template = monthlyRoasterDto.Template,
                    IsActive = 1
                };
                bool result = await _monthlyRosterService.Create(modelData);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    return Json(Url.Action("Index", "MonthlyRoster"));
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return Json(Url.Action("Create", "MonthlyRoster"));

                }
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return Json(Url.Action("Create", "MonthlyRoster"));
            }
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] MonthlyRoasterSearchDto monthlyRoasterSearchDto)
        {
            var model = await _monthlyRosterService.GetAllRoasterDetails(monthlyRoasterSearchDto);
            return PartialView("_List", model);
        }
        public async Task<List<DropdownDto>> GetMonthsList()
        {
            var months = new List<DropdownDto>();
            for (int i = 1; i <= 12; i++)
            {
                months.Add(new DropdownDto
                {
                    Value = i.ToString(),
                    Text = DateTimeFormatInfo.CurrentInfo.GetMonthName(i)
                });
            }
            return months;
        }
        public async Task<List<DropdownDto>> GetYearList()
        {
            var Years = new List<DropdownDto>();
            for (int i = 2000; i <= DateTime.Now.Year; i++)
            {
                Years.Add(new DropdownDto
                {
                    Value = i.ToString(),
                    Text = i.ToString()
                });
            }
            return Years;
        }
        public async Task<PartialViewResult> GetMonthlyDetails(int? month, int? year, int? department, int? division, int? zone, int? locality)
        {
            var dates = new List<MonthlyRoasterPartial>();
            ViewBag.PrimaryList = await _monthlyRosterService.GetPrimaryListNoList(division ?? 0, department ?? 0, zone ?? 0, locality ?? 0);
            for (var date = new DateTime(Convert.ToInt32(year ?? 0), month ?? 0, 1); date.Month == month; date = date.AddDays(1))
            {
                dates.Add(new MonthlyRoasterPartial
                {
                    Date = Convert.ToDateTime(date).ToString("dd-MMM-yyyy"),
                    Day = Convert.ToDateTime(date).DayOfWeek.ToString()
                });
            }
            return PartialView("_listMonthlyDetails", dates);
        }
        [HttpGet]
        public async Task<JsonResult> GetZoneList(int? DepartmentId)
        {
            DepartmentId = DepartmentId ?? 0;
            var zoneList = await _monthlyRosterService.GetAllZone(Convert.ToInt32(DepartmentId));
            return Json(zoneList);
        }

        [HttpGet]
        public async Task<JsonResult> GetDivisionList(int? ZoneId)
        {
            ZoneId = ZoneId ?? 0;
            return Json(await _monthlyRosterService.GetAllDivisionList(Convert.ToInt32(ZoneId)));
        }
        [HttpGet]
        public async Task<JsonResult> GetLocalityList(int? DivisionId)
        {
            DivisionId = DivisionId ?? 0;
            return Json(await _monthlyRosterService.GetAllLocalityList(Convert.ToInt32(DivisionId)));
        }

        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> MonthlyRoasterList()
        {
            var result = await _monthlyRosterService.GetAllmonthlyrosterlist();
            List<MonthlyRoasterListDto> data = new List<MonthlyRoasterListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new MonthlyRoasterListDto()
                    {
                        Id = result[i].Id,
                        Department = result[i].Department == null ? "" : result[i].Department.Name,
                        Zone = result[i].Zone == null ? "" : result[i].Zone.Name,
                        Division = result[i].Division == null ? "" : result[i].Division.Name,
                        LocalityVillage = result[i].Locality == null ? "" : result[i].Locality.Name,
                        SecurityGuard = result[i].Userprofile == null ? "" : result[i].Userprofile.User.NormalizedUserName,
                        Year = result[i].Year.ToString(),
                        Month = new DateTime(result[i].Year, result[i].Month, 1).ToString("MMMM"),
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }
    }
}
