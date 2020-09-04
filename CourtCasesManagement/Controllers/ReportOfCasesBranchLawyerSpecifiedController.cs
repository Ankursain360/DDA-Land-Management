using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CourtCasesManagement.Controllers
{
    public class ReportOfCasesBranchLawyerSpecifiedController : Controller
    {
        public IActionResult RptCasesBranchLawyerSpecified()
        {
            return View();
        }
    }
}