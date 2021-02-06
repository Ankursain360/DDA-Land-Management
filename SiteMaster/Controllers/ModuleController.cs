using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Dto.Search;
using SiteMaster.Filters;
using Core.Enum;
using Utility.Helper;

namespace SiteMaster.Controllers
{
    public class ModuleController : BaseController
    {
        private readonly IModuleService _moduleService;

        public ModuleController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            var result = await _moduleService.GetAllModule();
            return View(result);

        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] ModuleSearchDto model)
        {
            var result = await _moduleService.GetPagedModule(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
    
        public async Task<IActionResult> Create(Module module)
        {
                if (ModelState.IsValid)
                {
                    var result = await _moduleService.Create(module);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _moduleService.GetAllModule();
                        return View("Index", list);
                     }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(module);
                    }
                }
                else
                {
                    return View(module);
                }
        }


        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _moduleService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Edit(int id, Module module)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _moduleService.Update(id, module);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _moduleService.GetAllModule();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(module);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(module);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _moduleService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Module: {Name} already exist");
            }
        }


        [AuthorizeContext(ViewAction.Delete)]

        public async Task<IActionResult> Delete(int id)  //Not in use
        {
            
            var result = await _moduleService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _moduleService.GetAllModule();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _moduleService.GetAllModule();
                return View("Index", result1);
            }
        }

        public async Task<IActionResult> DeleteConfirmed(int id)  
        {
            var result = await _moduleService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _moduleService.GetAllModule();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _moduleService.GetAllModule();
                return View("Index", result1);
            }
        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _moduleService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Download()
        {
            List<Module> result = await _moduleService.GetAllModule();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"Module.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }
    }
}