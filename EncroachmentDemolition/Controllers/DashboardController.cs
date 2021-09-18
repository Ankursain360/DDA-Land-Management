using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EncroachmentDemolition.Filters;
using System.Drawing;
using System.Drawing.Imaging;
using Core.Enum;

namespace EncroachmentDemolition.Controllers
{
    public class DashboardController : Controller
    {
        [ServiceFilter(typeof(AuditFilterAttribute))]
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
