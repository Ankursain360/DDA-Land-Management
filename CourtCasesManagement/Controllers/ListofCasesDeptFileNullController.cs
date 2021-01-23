using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourtCasesManagement.Filters;
using Core.Enum;
namespace CourtCasesManagement.Controllers
{
    public class ListofCasesDeptFileNullController : Controller
    {
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }
     
        public IActionResult ListofCases_Dept_File_Null()
        {
            return View();
        }
    }
}
