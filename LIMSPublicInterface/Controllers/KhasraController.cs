using Microsoft.AspNetCore.Mvc;

namespace LIMSPublicInterface.Controllers
{
    public class KhasraController : Controller
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
