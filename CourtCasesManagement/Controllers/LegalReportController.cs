using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CourtCasesManagement.Controllers
{
    public class LegalReportController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }


    }
}
