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
using NewLandAcquisition.Filters;
using Core.Enum;

namespace NewLandAcquisition.Controllers
{
    public class NewlandAppealdetailController : Controller
    {
        private readonly INewlandAppealdetailservice _NewlandAppealdetailService;


        public NewlandAppealdetailController(INewlandAppealdetailservice NewlandAppealdetailService)
        {
            _NewlandAppealdetailService = NewlandAppealdetailService;
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            var list = await _NewlandAppealdetailService.GetNewlandappealdetails();
            return View(list);
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NewlandAppealdetailSearchDto model)
        {
            var result = await _NewlandAppealdetailService.GetPagedNewlandAppealdetail(model);
            return PartialView("_List", result);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Newlandappealdetail Newlandappealdetail)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    var result = await _NewlandAppealdetailService.Create(Newlandappealdetail);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _NewlandAppealdetailService.GetNewlandappealdetails();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(Newlandappealdetail);
                    }
                }
                else
                {
                    return View(Newlandappealdetail);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(Newlandappealdetail);
            }
        }



        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _NewlandAppealdetailService.FetchSingleResult(id);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Newlandappealdetail Newlandappealdetail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _NewlandAppealdetailService.Update(id, Newlandappealdetail);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _NewlandAppealdetailService.GetNewlandappealdetails();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(Newlandappealdetail);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(Newlandappealdetail);
                }
            }
            else
            {
                return View(Newlandappealdetail);
            }
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _NewlandAppealdetailService.Delete(id);
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
            var list = await _NewlandAppealdetailService.GetNewlandappealdetails();
            return View("Index", list);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _NewlandAppealdetailService.FetchSingleResult(id);




            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }




    }
}

