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
using Microsoft.AspNetCore.Mvc.Rendering;
using Dto.Search;
using SiteMaster.Filters;
using Core.Enum;
using Utility.Helper;
using Dto.Master;

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
        public async Task<IActionResult> MenuList()
        {
            var result = await _menuService.GetAllMenu();
            List<MenuListDto> data = new List<MenuListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new MenuListDto()
                    {
                        Id = result[i].Id,
                        ModuleName = result[i].ModuleId==null ?"": result[i].Module.Name,
                        MenuName = result[i].Name,
                        SortBy = result[i].SortBy.ToString(),
                        ParentMenu = result[i].ParentMenuId == null ? "" : result[i].ParentMenu.Name,                     
                        IsActive = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        

    }
}



 
