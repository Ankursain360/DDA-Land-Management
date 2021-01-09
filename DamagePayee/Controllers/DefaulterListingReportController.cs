//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;

//namespace DamagePayee.Controllers
//{
//    public class DefaulterListingReportController : Controller
//    {
//        public IActionResult Index()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Index(int id)
//        {
//            ViewBag.IsShowData = "Yes";
//            return View();
//        }
//    }
//}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;

namespace DamagePayee.Controllers
{
    public class DefaulterListingReportController : BaseController
    {
        private readonly IDemandLetterService _demandLetterService;

        public DefaulterListingReportController(IDemandLetterService demandLetterService)
        {
            _demandLetterService = demandLetterService;
        }
        //public async Task<IActionResult> Index()
        //{
        //    Demandletter model = new Demandletter();

        //    model.Damagelist = await _demandLetterService.GetAllDemandletter();
        //    return View(model);
        //}

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] DefaulterListingReportSearchDto defaulterListingReportSearchDto)
        {
            var result = await _demandLetterService.GetDefaultListingReportData(defaulterListingReportSearchDto);
            if (result != null)
            {
                return PartialView("_List", result);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return PartialView();
            }
        }

    }
}
