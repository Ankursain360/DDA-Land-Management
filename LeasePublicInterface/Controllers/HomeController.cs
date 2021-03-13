

    using Dto.Master;
    using Microsoft.AspNetCore.Mvc;
    using Service.IApplicationService;
    using LeasePublicInterface.Helper;
    using LeasePublicInterface.Models;
    using System.Diagnostics;
    using System.Threading.Tasks;

    namespace LeasePublicInterface.Controllers
    {
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



            public IActionResult ExceptionLog()
            {
                return View();
            }
        }
    }
