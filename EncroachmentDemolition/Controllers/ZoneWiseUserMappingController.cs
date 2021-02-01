using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EncroachmentDemolition.Filters;
using Core.Enum;
namespace EncroachmentDemolition.Controllers
{
    public class ZoneWiseUserMappingController : Controller
    {
        public IActionResult ZoneWiseUserMapping()
        {
            return View();
        }
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

        [AuthorizeContext(ViewAction.Edit)]
        public IActionResult Edit()
        {
            return View();
        }

        [AuthorizeContext(ViewAction.Delete)]
        public IActionResult Delete()
        {
            return View();
        }

        [AuthorizeContext(ViewAction.View)]
        public IActionResult ViewDetails()
        {
            return View();
        }
    }
}