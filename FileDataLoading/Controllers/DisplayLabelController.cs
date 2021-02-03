using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
//using FileDataLoading.Models;

namespace FileDataLoading.Controllers
{
    public class DisplayLabelController : BaseController
    {
        private readonly IDataStorageService _datastorageService;

        public DisplayLabelController(IDataStorageService datastorageService)
        {
            _datastorageService = datastorageService;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DisplayLabelSearchDto model)
        {
            var result = await _datastorageService.GetPagedDisplayLabel(model);
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


        public IActionResult PrintLabel()
        {
            return PartialView("PrintLabel");
        }
    }
}
