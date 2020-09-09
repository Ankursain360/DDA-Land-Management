using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SiteMaster.Controllers
{
    public class VillageController : Controller
    {
        private readonly IVillageService _villageService;
        public VillageController(IVillageService villageService)
        {
            _villageService = villageService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _villageService.GetAllVillage();
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            Village village = new Village();
            village.ZoneList = await _villageService.GetAllZone();
            return View(village);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Village village)
        {
            try
            {
                village.ZoneList = await _villageService.GetAllZone();
                if (ModelState.IsValid)
                {
                    var result = await _villageService.Create(village);

                    if (result == true)
                    {
                    }
                    else
                    {
                    }
                }
                else
                {
                    return View(village);
                }
            }
            catch (Exception ex)
            {
            }
            return View(village);
        }
    }
}