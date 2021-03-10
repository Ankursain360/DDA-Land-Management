using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using SiteMaster.Filters;
using Utility.Helper;

namespace SiteMaster.Controllers
{
     public class StructureController : BaseController
    {
        private readonly IStructureService _structureService;

        public StructureController(IStructureService structureService)
        {
            _structureService = structureService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }
       

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] StructureSearchDto model)
        {
           
            var result = await _structureService.GetPagedStructure(model);
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
        public async Task<IActionResult> Create(Structure structure)
        {
            try
            {

                if (ModelState.IsValid)
                {


                    var result = await _structureService.Create(structure);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        
                        var list = await _structureService.GetAllStructure();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(structure);

                    }
                }
                else
                {
                    return View(structure);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(structure);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _structureService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Structure structure)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _structureService.Update(id, structure);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                        var list = await _structureService.GetAllStructure();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(structure);

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(structure);

                }
            }
            return View(structure);
        }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _structureService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Structure Name : {Name} already exist");
            }
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _structureService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _structureService.GetAllStructure();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _structureService.GetAllStructure();
                return View("Index", result1);
            }
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _structureService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Download()
        {
            List<Structure> result = await _structureService.GetAllStructure();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"Structure.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }
    }
}
