using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using EncroachmentDemolition.Filters;
using System.Drawing.Imaging;
using Core.Enum;
namespace EncroachmentDemolition.Controllers
{
    public class DemolationController : Controller
    {
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {   return View();  }


        public IActionResult Report()
        {
            return View();
        }
    }
}