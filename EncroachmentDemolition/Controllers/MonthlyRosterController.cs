using Libraries.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace EncroachmentDemolition.Controllers
{
    public class MonthlyRosterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        public PartialViewResult GetMonthlyDetails(int? month)
        {
            var dates = new List<MonthlyRoster>();
            for (var date = new DateTime(2020, month ?? 0, 1); date.Month == month; date = date.AddDays(1))
            {
                dates.Add(new MonthlyRoster
                {
                    Date = Convert.ToDateTime(date).ToString("dd-MMM-yyyy"),
                    Day = Convert.ToDateTime(date).DayOfWeek.ToString()
                });
            }
            return PartialView("_listMonthlyDetails", dates);
        }
    }
}
