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
using SiteMaster.Models;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Dto.Search;
using SiteMaster.Filters;
using Core.Enum;
using Dto.Master;

namespace SiteMaster.Controllers
{
    public class DepartmentTargetController : BaseController
    {
        private readonly IDepartmenttargetService _departmenttargetService;
        private readonly IDepartmentService _departmentService;
        public DepartmentTargetController(IDepartmenttargetService departmenttargetService)
        {
            _departmenttargetService = departmenttargetService;
        }
      //  [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<PartialViewResult> List([FromBody] DepartmentTargetSearchDto model)
        {
            var result = await _departmenttargetService.GetPagedDepartmenttarget(model);
            return PartialView("_List", result);
        }


    //    [AuthorizeContext(ViewAction.Add)]
       //  [HttpPost]
        public async Task<IActionResult> Create()
        {
            Departmenttarget model = new Departmenttarget();
            model.IsActive = 1;
            model.DepartmentList = await _departmenttargetService.GetAllDepartment();
            return View(model);
        }
        //  [AuthorizeContext(ViewAction.Add)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Departmenttarget departmenttarget)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _departmenttargetService.Create(departmenttarget);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _departmenttargetService.GetAllDepartmenttarget();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(departmenttarget);
                    }
                }
                else
                {
                    return View(departmenttarget);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(departmenttarget);
            }
        }
        //   [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _departmenttargetService.FetchSingleResult(id);
            Data.DepartmentList = await _departmenttargetService.GetAllDepartment();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
      //  [AuthorizeContext(ViewAction.Edit)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Departmenttarget departmenttarget)
        {
            departmenttarget.DepartmentList = await _departmenttargetService.GetAllDepartment();
            if (ModelState.IsValid)
            {
                try
                {

                   var result = await _departmenttargetService.Update(id, departmenttarget);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                        var list = await _departmenttargetService.GetAllDepartmenttarget();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(departmenttarget);

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(departmenttarget);

                }
            }
            return View(departmenttarget);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _departmenttargetService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Department: {Name} already exist");
            }
        }



        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Renu
        {
            
            var result = await _departmenttargetService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            return RedirectToAction("Index", "DepartmentTarget");
           
        }


       // [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _departmenttargetService.FetchSingleResult(id);
            Data.DepartmentList = await _departmenttargetService.GetAllDepartment();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
      //  [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)  // Used to Perform Delete Functionality 
        {
            try
            {

                var result = await _departmenttargetService.Delete(id);
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
            var list = await _departmenttargetService.GetAllDepartmenttarget();
                 return View("Index", list);
        }
    }

}
