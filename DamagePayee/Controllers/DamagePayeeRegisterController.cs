using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;

namespace DamagePayee.Controllers
{
    public class DamagePayeeRegisterController : BaseController
    {
        private readonly IDamagepayeeregisterService _damagepayeeregisterService;
        public IConfiguration _configuration;
        public DamagePayeeRegisterController(IDamagepayeeregisterService damagepayeeregisterService, IConfiguration configuration)
        {
            _configuration = configuration;
            _damagepayeeregisterService = damagepayeeregisterService;
        }
        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult Create()
        //{
        //    return View();
        //}
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DamagepayeeregisterSearchDto model)
        {
            var result = await _damagepayeeregisterService.GetPagedDamagepayeeregister(model);
            return PartialView("_List", result);
        }
        async Task BindDropDown(Damagepayeeregister damagepayeeregister)
        {
            damagepayeeregister.LocalityList = await _damagepayeeregisterService.GetLocalityList();
            damagepayeeregister.DistrictList = await _damagepayeeregisterService.GetDistrictList();
        }
        public async Task<IActionResult> Create()
        {
            Damagepayeeregister damagepayeeregister = new Damagepayeeregister();

            await BindDropDown(damagepayeeregister);
            return View(damagepayeeregister);
        }
        //[HttpPost]

        //public async Task<IActionResult> Create(Damagepayeeregister damagepayeeregister)
        //{
        //    await BindDropDown(damagepayeeregister);
        //    string PhotoFilePathLayout = _configuration.GetSection("FilePaths:DamagePayeeFiles:ATSGPADocument").Value.ToString();
        //    string RecieptDocumentPathLayout = _configuration.GetSection("FilePaths:DamagePayeeFiles:RecieptDocument").Value.ToString();
        //    if (ModelState.IsValid)
        //    {

        //    }
        //    return View();
        //}
    }
}
