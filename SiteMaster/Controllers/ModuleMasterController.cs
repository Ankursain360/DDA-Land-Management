using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace SiteMaster.Controllers
{
    public class ModuleMasterController : Controller
    {
        private readonly IModuleService _moduleService;

        public ModuleMasterController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _moduleService.GetAllModule();
            return View(result);

        }

        public IActionResult Create()
        {
            return View();
        }

    }
}