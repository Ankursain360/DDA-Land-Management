using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CourtCasesManagement.Controllers
{
    public class ListofCasesDeptFileNullController : Controller
    {
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
