using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using System.Threading.Tasks;
using LeaseDetails.Filters;
using Core.Enum;
using System.Collections.Generic;
using Utility.Helper;

namespace LeaseDetails.Controllers
{
    public class GroundRentController : BaseController
    {
        private readonly IGroundRentService _groundRentService;
        public GroundRentController(IGroundRentService groundRentService)
        {
            _groundRentService = groundRentService;
        }
       // [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
             var list =  _groundRentService.GetAllGroundRent();
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] GroundrentSearchDto model)
        {
            var result = await _groundRentService.GetPagedGroundRent(model);
            return PartialView("_List", result);
        }


     //   [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Groundrent groundrent = new Groundrent();
            groundrent.IsActive = 1;
            groundrent.CreatedBy = SiteContext.UserId;
            groundrent.PropertyTypeList = await _groundRentService.GetAllPropertyTypeList();
            return View(groundrent);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
       // [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Groundrent groundrent)
        {
            try
            {
                groundrent.PropertyTypeList = await _groundRentService.GetAllPropertyTypeList();
                if (ModelState.IsValid)
                {
                    var result = await _groundRentService.Create(groundrent);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        //var list = await _groundRentService.GetAllGroundRent();
                        //return View("Index", list);
                        return RedirectToAction("Index", "GroundRent");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(groundrent);
                    }
                }
                else
                {
                    return View(groundrent);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(groundrent);
            }
        }
        //[AcceptVerbs("Get", "Post")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Exist(int Id, string Name)
        //{
        //    var result = await _groundRentService.CheckUniqueName(Id, Name);
        //    if (result == false)
        //    {
        //        return Json(true);
        //    }
        //    else
        //    {
        //        return Json($"Village: {Name} already exist");
        //    }
        //}

      //  [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _groundRentService.FetchSingleResult(id);
            Data.PropertyTypeList = await _groundRentService.GetAllPropertyTypeList();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
       // [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Groundrent groundrent)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    groundrent.ModifiedBy = SiteContext.UserId;
                    var result = await _groundRentService.Update(id, groundrent);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        //var list = await _groundRentService.GetAllGroundRent();
                        //return View("Index", list);
                        return RedirectToAction("Index", "GroundRent");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(groundrent);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(groundrent);
                }
            }
            else
            {
                return View(groundrent);
            }
        }



      //  [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)  
        {
            try
            {

                var result = await _groundRentService.Delete(id);
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
            //var list = await _groundRentService.GetAllGroundRent();
            // return View("Index", list);
            return RedirectToAction("Index", "GroundRent");
        }



       // [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _groundRentService.FetchSingleResult(id);
            Data.PropertyTypeList = await _groundRentService.GetAllPropertyTypeList();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
      


        public async Task<IActionResult> Download()
        {
            List<Groundrent> result = await _groundRentService.GetAll();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"GroundRent.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }





    }
}
