using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Utility.Helper;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Master;

namespace DamagePayee.Controllers
{
    public class DamageCalculatorController : BaseController
    {
        private readonly IDamageCalculationService _damagecalculationService;
        public IConfiguration _configuration;
        public DamageCalculatorController(IDamageCalculationService damagecalculationService, IConfiguration configuration)
        {
            _configuration = configuration;
            _damagecalculationService = damagecalculationService;

        }
        public async Task<IActionResult> Index()
        {
            Damagecalculation damagecalculation = new Damagecalculation();
            damagecalculation.PropertyType1 = await _damagecalculationService.GetPropertyTypes();
            damagecalculation.LocalityList = await _damagecalculationService.GetLocalities();
            return View(damagecalculation);
        }


        [HttpPost]
        public async Task<PartialViewResult> DamageCalculate([FromBody] DamageCalculationDto dto)
        {
            Damagecalculation damagecalculation = new Damagecalculation();
            damagecalculation.DamageRateCalculationList.Add(new Damagecalculation
            {
                StartDate = DateTime.Now
            });
            return PartialView("_DamageCalculate", damagecalculation);
        }

        //[HttpPost]
        //public async Task<IActionResult> Index(Damagecalculation damagecalculation)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        return RedirectToAction("DamageCalculate", "DamageCalculator", new { id = damagecalculation });
        //    }
        //    else
        //    {
        //        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //        return View(damagecalculation);
        //    }
        //}


        //public async Task<PartialViewResult> DamageCalculate(Damagecalculation damagecalculation)
        //{
        //    return PartialView("_DamageCalculate", damagecalculation);
        //}

    }
}
