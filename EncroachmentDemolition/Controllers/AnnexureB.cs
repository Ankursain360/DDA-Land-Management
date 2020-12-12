using Microsoft.AspNetCore.Mvc;

namespace EncroachmentDemolition.Controllers
{
    public class AnnexureB : Controller
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
