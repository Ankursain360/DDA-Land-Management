using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CourtCasesManagement.Controllers
{
    public class ReportOfCasesCourtWiseController : Controller
    {
        public IActionResult RptCasesCourtWise()
        {
            return View();
        }
    }
}