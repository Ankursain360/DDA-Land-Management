using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiteMaster.Models;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using SiteMaster.Filters;
namespace SiteMaster.Controllers
{
    [TypeFilter(typeof(CustomExceptionHandlerFilter))]
    public class HomeController : Controller
    {
        private readonly ICountryService _countryService;

        public HomeController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public IActionResult Index()
        {
            //throw new Exception();
            return View();
        }
        //public async Task<IActionResult> Index()
        //{
        //    List<Country> result = await _countryService.GetAllCountry();
        //    return View(result);
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
