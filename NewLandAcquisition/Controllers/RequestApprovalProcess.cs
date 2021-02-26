using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewLandAcquisition.Controllers
{
    public class RequestApprovalProcess : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
