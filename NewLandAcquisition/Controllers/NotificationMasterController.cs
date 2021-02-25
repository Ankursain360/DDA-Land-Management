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
    public class NotificationMasterController : BaseController
    {
        private readonly INewlandnotificationService _newlandnotificationService;
       
         public NotificationMasterController(INewlandnotificationService newlandnotificationService)
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
            Newlandvillage newlandvillage = new Newlandvillage();
            newlandvillage.IsActive = 1;
             return View(newlandvillage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Newlandnotification newlandnotification)
        {
            try
            {
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

        //  [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _newlandnotificationService.FetchSingleResult(id);
           if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //  [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Newlandnotification newlandnotification)
        {

               if (ModelState.IsValid)
                  {
                try
                {
                    newlandnotification.ModifiedBy = SiteContext.UserId;
                    var result = await _newlandnotificationService.Update(id, newlandnotification);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _newlandnotificationService.GetNewlandnotification();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(newlandnotification);
                    }
                }
                catch (Exception ex)
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

        //   [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _newlandnotificationService.Delete(id);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            var list = await _newlandnotificationService.GetNewlandnotification();
            return View("Index", list);
        }

        //[AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _newlandnotificationService.FetchSingleResult(id);
           
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        public async Task<IActionResult> Download()
        {
            List<Newlandnotification> result = await _newlandnotificationService.GetNewlandnotification();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"village.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }

    }
}
