using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;
using NewLandAcquisition.Filters;
using Core.Enum;
using System.Data;
using Newtonsoft.Json;
using Utility.Helper;
using System.Collections.Generic;


namespace NewLandAcquisition.Controllers
{
    public class NewlandNotificationController : BaseController
    {
        private readonly INewlandnotificationService _newlandnotificationService;
        public NewlandNotificationController(INewlandnotificationService newlandnotificationService)
        {
            _newlandnotificationService = newlandnotificationService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NewlandnotificationSearchDto model)
        {
            var result = await _newlandnotificationService.GetPagedNewlandnotificationdetails(model);

            return PartialView("_List", result);
        }
        public async Task<IActionResult> Create()
        {
            Newlandnotification newlandnotification = new Newlandnotification();
            newlandnotification.IsActive = 1;
         //   newlandnotification.notificationtypeList = await _newlandnotificationService.GetNotificationType();
           
            return View(newlandnotification);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Newlandnotification newlandnotification)
        {
            try
            {
                newlandnotification.notificationtypeList = await _newlandnotificationService.GetNotificationType();
               
                if (ModelState.IsValid)
                {
                    newlandnotification.CreatedBy = SiteContext.UserId;
                    var result = await _newlandnotificationService.Create(newlandnotification);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _newlandnotificationService.GetNewlandnotification();
                          
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(newlandnotification);
                    }
                }
                else
                {
                    return View(newlandnotification);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(newlandnotification);
            }
        }

    }
}
