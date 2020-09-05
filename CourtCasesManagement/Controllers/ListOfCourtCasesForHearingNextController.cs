using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CourtCasesManagement.Controllers
{
    public class ListOfCourtCasesForHearingNextController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListOfCourtCasesForHearing_NextHearingDate()
        {
            return View();
        }

    }
}
