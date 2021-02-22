using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;


namespace LIMSPublicInterface.Controllers
{
    public class Notification17Details : Controller
    {
        private readonly IUndersection17plotdetailService _undersection17plotdetailService;

        public Notification17Details(IUndersection17plotdetailService undersection17plotdetailService)
        {
            _undersection17plotdetailService = undersection17plotdetailService;
        }
        public async Task<IActionResult> Index()

        {
            Undersection17plotdetail undersection17plotdetail = new Undersection17plotdetail();
            undersection17plotdetail.IsActive = 1;

            undersection17plotdetail.Undersection17List = await _undersection17plotdetailService.GetAllUndersection17List();
            return View(undersection17plotdetail);
        }


        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] Unotification17detailsSearchDto model)
        {
            var result = await _undersection17plotdetailService.GetPagednotification17detailsList(model);
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
