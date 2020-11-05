using Microsoft.AspNetCore.Mvc;
using Service.IApplicationService;
using LandInventory.Filters;
using LandInventory.Models;
using System.Diagnostics;

namespace LandInventory.Controllers
{
    [TypeFilter(typeof(CustomExceptionHandlerFilter))]
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
