using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.ApplicationService;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using SiteMaster.Filters;
using Utility.Helper;

namespace SiteMaster.Controllers
{
    public class MenuController : BaseController
    {
        public readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] MenuSearchDto model)
        {
            var result = await _menuService.GetPagedMenu(model);
            return PartialView("_List", result);
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Menu model = new Menu();
            model.IsActive = 1;
            model.modulelist = await _menuService.GetAllModule();
            model.parentmenulist = await _menuService.GetAllParentmenu();
            


            return View(model);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]

        public async Task<IActionResult> Create(Menu menu)
        {
            try
            {
                menu.modulelist = await _menuService.GetAllModule();
                menu.parentmenulist = await _menuService.GetAllParentmenu();
                if (ModelState.IsValid)
                {
                    var result = await _menuService.Create(menu);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _menuService.GetAllMenu();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(menu);
                    }
                }
                else
                {
                    return View(menu);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(menu);
            }
        }

        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _menuService.FetchSingleResult(id);
            Data.modulelist = await _menuService.GetAllModule();
            Data.parentmenulist = await _menuService.GetAllParentmenu();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]

        public async Task<IActionResult> Edit(int id, Menu menu)
        {
            menu.modulelist = await _menuService.GetAllModule();
            menu.parentmenulist = await _menuService.GetAllParentmenu();

            if (ModelState.IsValid)
            {
                try
                {

                    var result = await _menuService.Update(id, menu);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(menu);

                    }
                }
                catch (DbUpdateConcurrencyException)
                {

                }
            }
            return View(menu);
        }


        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id,  string Name,int ModuleId)
        {
            var result = await _menuService.CheckUniqueName(Id, ModuleId, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Menu: {Name} already exist");
            }
        }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _menuService.Delete(id);
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
            var list = await _menuService.GetAllMenu();
            return View("Index", list);
        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _menuService.FetchSingleResult(id);
            Data.modulelist = await _menuService.GetAllModule();
            Data.parentmenulist = await _menuService.GetAllParentmenu();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Download()
        {
            List<Menu> result = await _menuService.GetAllMenu();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"Menu.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }

    }
}



 
