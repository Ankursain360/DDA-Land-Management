using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DamagePayee.Controllers
{
    public class DamagePayeeRegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PayeeRegistration()
        {
            return View();
        }

        public IActionResult DamagePayeeRegister()
        {
            return View();
        }

        public IActionResult NoticeToDamagePayee()
        {
            return View();
        }

        public IActionResult DemandLetter()
        {
            return View();
        }

        public IActionResult DamageCalculator()
        {
            return View();
        }

        public IActionResult DamageCalculationReport()
        {
            return View();
        }
    }
}
