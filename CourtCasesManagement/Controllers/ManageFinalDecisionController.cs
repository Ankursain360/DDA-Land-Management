using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using DDAPropertyREG.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourtCasesManagement.Controllers
{
    public class ManageFinalDecisionController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}