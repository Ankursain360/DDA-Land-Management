using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourtCasesManagement.Filters;
using Core.Enum;
namespace CourtCasesManagement.Controllers
{
    public class ReportOfCasesBranchLawyerSpecifiedController : Controller
    {
        [AuthorizeContext(ViewAction.View)]
        public IActionResult RptCasesBranchLawyerSpecified()
        {
            return View();
        }
    }
}