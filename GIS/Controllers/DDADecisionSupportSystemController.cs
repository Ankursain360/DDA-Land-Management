using Microsoft.AspNetCore.Mvc;

namespace GIS.Controllers
{
    public class DDADecisionSupportSystemController : Controller
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
