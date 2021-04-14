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
using AcquiredLandInformationManagement.Filters;
using Core.Enum;
using Dto.Search;
namespace AcquiredLandInformationManagement.Controllers
{
    public class AppealDetailController : Controller
    {
        private readonly IAppealdetailService _appealdetailService;


        public AppealDetailController(IAppealdetailService appealdetailService)
        {
            _appealdetailService = appealdetailService;
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            var list = await _appealdetailService.GetAllAppealdetail();
            return View(list);
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] AppealdetailSearchDto model)
        {
            var result = await _appealdetailService.GetPagedAppealdetail(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Appealdetail appealdetail)
        {
            try
            {
               

                if (ModelState.IsValid)
                {
                    var result = await _appealdetailService.Create(appealdetail);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _appealdetailService.GetAllAppealdetail();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(appealdetail);
                    }
                }
                else
                {
                    return View(appealdetail);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(appealdetail);
            }
        }



        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _appealdetailService.FetchSingleResult(id);
           
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Appealdetail appealdetail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _appealdetailService.Update(id, appealdetail);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _appealdetailService.GetAllAppealdetail();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(appealdetail);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(appealdetail);
                }
            }
            else
            {
                return View(appealdetail);
            }
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _appealdetailService.Delete(id);
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
            var list = await _appealdetailService.GetAllAppealdetail();
            return View("Index", list);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _appealdetailService.FetchSingleResult(id);

           


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }




    }
}

