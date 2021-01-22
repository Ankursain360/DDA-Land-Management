using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourtCasesManagement.Filters;
using Core.Enum;
namespace CourtCasesManagement.Controllers
{
    public class ReportOfCasesCourtWiseController : Controller
    {
        [AuthorizeContext(ViewAction.View)]
        public IActionResult RptCasesCourtWise()
        {
            return View();
        }
    }
}