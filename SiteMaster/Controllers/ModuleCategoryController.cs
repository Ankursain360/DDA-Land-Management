using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;
using SiteMaster.Filters;
using Core.Enum;
using Dto.Master;
using Utility.Helper;
using System.Collections.Generic;

namespace SiteMaster.Controllers
{
    public class ModuleCategoryController : BaseController
    {
        private readonly IModuleCategoryService _modulecategoryService;

        public ModuleCategoryController(IModuleCategoryService modulecategoryService)
        {
            _modulecategoryService = modulecategoryService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _modulecategoryService.GetAllModuleCategory();
            return View(result);
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] ModuleCategorySearchDto model)
        {
            var result = await _modulecategoryService.GetPagedModuleCategory(model);
            return PartialView("_List", result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ModuleCategory moduleCategory)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _modulecategoryService.Create(moduleCategory);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _modulecategoryService.GetAllModuleCategory();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(moduleCategory);

                    }
                }
                else
                {
                    return View(moduleCategory);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(moduleCategory);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _modulecategoryService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ModuleCategory moduleCategory)
        {
            if (ModelState.IsValid)
            {
                var result = await _modulecategoryService.Update(id, moduleCategory);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                    var list = await _modulecategoryService.GetAllModuleCategory();
                    return View("Index", list);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(moduleCategory);
                }
            }
            return View(moduleCategory);
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _modulecategoryService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Pankaj
        {
            var result = await _modulecategoryService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            return RedirectToAction("Index", "Bundle");
        }



        public async Task<IActionResult> Delete(int id)  // Used to Perform Delete Functionality added by Pankaj
        {
            try
            {
                var result = await _modulecategoryService.Delete(id);
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
            var list = await _modulecategoryService.GetAllModuleCategory();
            return View("Index", list);
        }

        //Download

        public async Task<IActionResult> ModuleCategoryList()
        {
            var result = await _modulecategoryService.GetAllModuleCategory();
            List<ModuleCategoryListDto> data = new List<ModuleCategoryListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new ModuleCategoryListDto()
                    {
                        Id = result[i].Id,                       
                        CategoryName = result[i].CategoryName,
                        IsActive = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }

    }
}
