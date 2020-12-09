using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.OptionEnums;

namespace EncroachmentDemolition.Controllers
{
    public class DemolitionReportController : BaseController
    {
       
        private readonly IEncroachmentRegisterationService _encroachmentRegisterationService;

        public DemolitionReportController(IEncroachmentRegisterationService encroachmentRegisterationService)
        {
            _encroachmentRegisterationService = encroachmentRegisterationService;
        }

        async Task BindDropDown(EncroachmentRegisteration encroachmentRegisteration)
        {
            encroachmentRegisteration.LocalityList = await _encroachmentRegisterationService.GetAllLocalityList();

        }
       
        public async Task<IActionResult> Index()
        {
            EncroachmentRegisteration model = new EncroachmentRegisteration();

            await BindDropDown(model);
            return View(model);
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DemolitionReportSearchDto model)
        {

            if (model != null)
            {
                var result = await _encroachmentRegisterationService.GetPagedDemolitionReport(model);
               
                return PartialView("_List", result);
            }
            else
            {
                ViewBag.Message = Alert.Show("No Data Found", "", AlertType.Warning);
                return PartialView("_List", null);
            }
        }

    }
}
