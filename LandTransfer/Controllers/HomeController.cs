using Microsoft.AspNetCore.Mvc;
using Service.IApplicationService;
using LandTransfer.Filters;
using LandTransfer.Models;
using System.Diagnostics;

namespace LandTransfer.Controllers
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
