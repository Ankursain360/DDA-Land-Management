using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using CourtCasesManagement.Models;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Dto.Search;
using CourtCasesManagement.Filters;
using Core.Enum;
namespace CourtCasesManagement.Controllers
{
    public class CaseYearController : BaseController
    {
            private readonly ICaseyearService _caseyearService;

        public CaseYearController(ICaseyearService caseyearService) {
            _caseyearService = caseyearService;
        }


        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
            {
                return View();
            }

        public async Task<PartialViewResult> List([FromBody] CaseyearSearchDto model)
        {
            var result = await _caseyearService.GetPagedcaseyear(model);
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
        public async Task<IActionResult> Create(Caseyear caseyear)
            {
                try
                {

                    if (ModelState.IsValid)
                    {


                        var result = await _caseyearService.Create(caseyear);

                        if (result == true)
                        {
                            ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                            
                            var list = await _caseyearService.GetAllcaseyear();
                            return View("Index", list);
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            return View(caseyear);

                        }
                    }
                    else
                    {
                        return View(caseyear);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(caseyear);
                }
            }


        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
            {
                var Data = await _caseyearService.FetchSingleResult(id);
                if (Data == null)
                {
                    return NotFound();
                }
                return View(Data);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
           [AuthorizeContext(ViewAction.Edit)]
           public async Task<IActionResult> Edit(int id, Caseyear caseyear)
        {
            if (ModelState.IsValid)
            {
                try
                {


                    var result = await _caseyearService.Update(id,caseyear);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                        var list = await _caseyearService.GetAllcaseyear();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(caseyear);

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(caseyear);

                }
            }
            return View(caseyear);
        }

        [AcceptVerbs("Get", "Post")]
            [AllowAnonymous]
            public async Task<IActionResult> Exist(int Id, string Name)
            {
            var result = await _caseyearService.CheckUniqueName(Id, Name);
                if (result == false)
                {
                    return Json(true);
    }
                else
                {
                    return Json($"Case Year: {Name} already exist");
}
            }


    public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Renu
            {
                

                var result = await _caseyearService.Delete(id);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                }
                return RedirectToAction("Index", "CaseYear");
               
            }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
            {
                var Data = await _caseyearService.FetchSingleResult(id);
                if (Data == null)
                {
                    return NotFound();
                }
                return View(Data);
            }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)  // Used to Perform Delete Functionality added by Praveen
            {
                try
                {

                    var result = await _caseyearService.Delete(id);
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
                var list = await _caseyearService.GetAllcaseyear();
                return View("Index", list);
            }

        }
    }


