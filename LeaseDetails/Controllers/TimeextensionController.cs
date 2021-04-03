
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using LeaseDetails.Filters;
using Utility.Helper;

namespace LeaseDetails.Controllers
{

    public class TimeextensionController : BaseController
    {
        private readonly ITimeextensionService _timeextensionService;

        public TimeextensionController(ITimeextensionService timeextensionService)
        {
            _timeextensionService = timeextensionService;
        }

        public IActionResult Index()
        {
           
            return View();
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] TimeextensionSearchDto model)
        {

            var result = await _timeextensionService.GetPagedTimeextension(model);
            return PartialView("_List", result);
        }
        //   [AuthorizeContext(ViewAction.Add)]
        public  IActionResult Create()
        {
            
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //  [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Timeextension time)
        {
          
            try
            {

                if (ModelState.IsValid)
                {

                    time.CreatedBy = SiteContext.UserId;
                    var result = await _timeextensionService.Create(time);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        
                        var list = await _timeextensionService.GetAllTimeextension();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(time);

                    }
                }
                else
                {
                    return View(time);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(time);
            }
        }
        // [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _timeextensionService.FetchSingleResult(id);
          
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        //  [ValidateAntiForgeryToken]
        //  [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Timeextension time)
        {
            
            if (ModelState.IsValid)
            {
                try
                {

                    time.ModifiedBy = SiteContext.UserId;
                    var result = await _timeextensionService.Update(id, time);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                        var list = await _timeextensionService.GetAllTimeextension();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(time);

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(time);

                }
            }
            return View(time);
        }

        //  [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _timeextensionService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _timeextensionService.GetAllTimeextension();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _timeextensionService.GetAllTimeextension();
                return View("Index", result1);
            }
        }

        //  [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _timeextensionService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        //  [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Download()
        {
            List<Timeextension> result = await _timeextensionService.GetAllTimeextension();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"Timeextension.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }
      
    }
}
