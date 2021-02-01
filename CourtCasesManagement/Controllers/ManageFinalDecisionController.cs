using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using DDAPropertyREG.Models;
using Microsoft.AspNetCore.Mvc;
using CourtCasesManagement.Filters;
using Core.Enum;
namespace CourtCasesManagement.Controllers
{
    public class ManageFinalDecisionController : Controller
    {
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }
        [AuthorizeContext(ViewAction.Add)]
        public IActionResult Create()
        {
            return View();
        }
    }
}