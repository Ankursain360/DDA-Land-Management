using Microsoft.AspNetCore.Mvc;

namespace LIMSPublicInterface.Controllers
{
    public class KhasraController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult Index(int id)
        //{
        //    ViewBag.IsShowData = "Yes";
        //    return View();
        //}
        public IActionResult Create()
        {
            return View();
        }
    }
}
