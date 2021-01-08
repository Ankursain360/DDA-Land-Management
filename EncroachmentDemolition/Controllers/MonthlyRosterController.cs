using Dto.Master;
using Libraries.Model.Entity;
using Microsoft.AspNetCore.Mvc;
using Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace EncroachmentDemolition.Controllers
{
    public class MonthlyRosterController : Controller
    {
        public readonly IMonthlyRosterService _monthlyRosterService;
        public MonthlyRosterController(IMonthlyRosterService monthlyRosterService)
        {
            _monthlyRosterService = monthlyRosterService;
        }
        public async Task<IActionResult> Index()
        {
            MonthlyRoaster model = new MonthlyRoaster();
            model.DepartmentList = await _monthlyRosterService.GetAllDepartmentList();
            return View();
        }

        public async Task<IActionResult> Create()
        {
            MonthlyRoaster model = new MonthlyRoaster();
            model.SecurityGuardList = await _monthlyRosterService.SecurityGuardList();
            model.DepartmentList = await _monthlyRosterService.GetAllDepartmentList();
            model.YearList = await GetYearList();
            model.MonthList = await GetMonthsList();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(MonthlyRoaster monthlyRoaster)
        {
            MonthlyRoaster model = new MonthlyRoaster();
            model.SecurityGuardList = await _monthlyRosterService.SecurityGuardList();
            model.DepartmentList = await _monthlyRosterService.GetAllDepartmentList();
            model.YearList = await GetYearList();
            model.MonthList = await GetMonthsList();
            return View(model);
        }
        public async Task<List<DropdownDto>> GetMonthsList()
        {
            var months = new List<DropdownDto>();
            for (int i = 1; i < 12; i++)
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
        public async Task<PartialViewResult> GetMonthlyDetails(int? month)
        {
            //var dates = new List<MonthlyRoasterPartial>();
            //for (var date = new DateTime(2020, month ?? 0, 1); date.Month == month; date = date.AddDays(1))
            //{
            //    dates.Add(new MonthlyRoasterPartial
            //    {
            //        Date = Convert.ToDateTime(date).ToString("dd-MMM-yyyy"),
            //        Day = Convert.ToDateTime(date).DayOfWeek.ToString()
            //    });
            //}
            MonthlyRoaster model = new MonthlyRoaster();
            for (var date = new DateTime(2020, month ?? 0, 1); date.Month == month; date = date.AddDays(1))
            {
                model.Dailyroaster.Add(new DailyRoaster
                {
                    Date = Convert.ToDateTime(date),
                    Day = Convert.ToDateTime(date).DayOfWeek.ToString()
                });
            }
            return PartialView("_listMonthlyDetails", model);
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
    }
}
