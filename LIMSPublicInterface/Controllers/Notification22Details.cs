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
    public class Notification22Details : Controller
    {
        private readonly IUndersection22plotdetailsService _undersection22plotdetailsService;

        public Notification22Details(IUndersection22plotdetailsService undersection22plotdetailsService)
        {
            _undersection22plotdetailsService = undersection22plotdetailsService;
        }

        public async Task<IActionResult> Index()
        {
            Undersection22plotdetails model = new Undersection22plotdetails();

            model.Undersection22List = await _undersection22plotdetailsService.GetAllUndersection22();
         
            model.IsActive = 1;
            return View(model);
        }



        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] Unotification22detailsSearchDto model)
        {
            var result = await _undersection22plotdetailsService.GetPagednotification22detailsList(model);
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
