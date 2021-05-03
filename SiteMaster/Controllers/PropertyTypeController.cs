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
using SiteMaster.Filters;
using Core.Enum;
using System.Collections.Generic;
using Utility.Helper;

namespace SiteMaster.Controllers
{
    public class PropertyTypeController : BaseController
    {
        private readonly IPropertyTypeService _PropertyTypeService;
        public PropertyTypeController(IPropertyTypeService PropertyTypeService)
        {
            _PropertyTypeService = PropertyTypeService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            var list = _PropertyTypeService.GetAllPropertyType();
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] PropertyTypeSearchDto model)
        {
            var result = await _PropertyTypeService.GetPagedPropertyType(model);
            return PartialView("_List", result);
        }


        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            PropertyType PropertyType = new PropertyType();
            PropertyType.IsActive = 1;
            PropertyType.CreatedBy = SiteContext.UserId;
           
            return View(PropertyType);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(PropertyType PropertyType)
        {
            try
            {
              
                if (ModelState.IsValid)
                {
                    var result = await _PropertyTypeService.Create(PropertyType);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        //var list = await _PropertyTypeService.GetAllPropertyType();
                        //return View("Index", list);
                        return RedirectToAction("Index", "PropertyType");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(PropertyType);
                    }
                }
                else
                {
                    return View(PropertyType);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(PropertyType);
            }
        }


        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            PropertyType PropertyType = new PropertyType();
            

            var Data = await _PropertyTypeService.FetchSingleResult(id);
          

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, PropertyType PropertyType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    PropertyType.ModifiedBy = SiteContext.UserId;
                    
                    var result = await _PropertyTypeService.Update(id, PropertyType);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        //var list = await _PropertyTypeService.GetAllPropertyType();
                        //return View("Index", list);
                        return RedirectToAction("Index", "PropertyType");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(PropertyType);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(PropertyType);
                }
            }
            else
            {
                return View(PropertyType);
            }
        }



        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {

                var result = await _PropertyTypeService.Delete(id);
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
            //var list = await _PropertyTypeService.GetAllPropertyType();
            //return View("Index", list);
            return RedirectToAction("Index", "PropertyType");
        }



        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _PropertyTypeService.FetchSingleResult(id);
         

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Download()
        {
            List<PropertyType> result = await _PropertyTypeService.GetAll();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"PropertyType.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }





    }
}
