using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LandInventory.Controllers
{
    public class ListOfLocalitiesImagesNotUploadedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
