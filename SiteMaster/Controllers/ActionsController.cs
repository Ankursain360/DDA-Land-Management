using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Dto.Search;
using SiteMaster.Filters;
using Core.Enum;
using Utility.Helper;
using System.Collections.Generic;
using Dto.Master;

namespace SiteMaster.Controllers
{
    public class ActionsController : BaseController
    {

        private readonly IActionsService _actionsService;

        public ActionsController(IActionsService actionsService)
        {
            _actionsService = actionsService;
        }

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] ActionsSearchDto model)
        {
            var result = await _actionsService.GetPagedActions(model);
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
        public async Task<IActionResult> Create(Actions actions)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _actionsService.Create(actions);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);

                        var list = await _actionsService.GetAllActions();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(actions);

                    }
                }
                else
                {
                    return View(actions);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(actions);
            }
        }


        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _actionsService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Actions actions)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _actionsService.Update(id, actions);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var result1 = await _actionsService.GetAllActions();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(actions);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(actions);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _actionsService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Actions: {Name} already exist");
            }
        }


        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)  //Not in use
        {

            var result = await _actionsService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _actionsService.GetAllActions();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _actionsService.GetAllActions();
                return View("Index", result1);
            }
        }

        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Renu
        {

            var result = await _actionsService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _actionsService.GetAllActions();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _actionsService.GetAllActions();
                return View("Index", result1);
            }

        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _actionsService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> ActionsList()
        {
            var result = await _actionsService.GetAllActions();
            List<ActionsListDto> data = new List<ActionsListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new ActionsListDto()
                    {
                        Id = result[i].Id,
                        ActionName = result[i].Name,
                        Icon = result[i].Icon,
                        Color = result[i].Color,
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

    }
}
