using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;

namespace SiteMaster.Controllers
{
    public class PageRoleController : BaseController
    {
        public readonly IPageRoleService _pageRoleService;
        public PageRoleController(IPageRoleService pageRoleService)
        {
            _pageRoleService = pageRoleService;
        }
        public async Task<IActionResult> Index()
        {
            PageRole model = new PageRole();
            model.ModuleList = await _pageRoleService.GetAllModuleList();
            model.RoleList = await _pageRoleService.GetAllRoleList();
            model.UserList = await _pageRoleService.GetUserList(model.RoleId);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(PageRole pageRole)
        {
            pageRole.ModuleList = await _pageRoleService.GetAllModuleList();
            pageRole.RoleList = await _pageRoleService.GetAllRoleList();
            pageRole.UserList = await _pageRoleService.GetUserList(pageRole.RoleId);
            PageRole data = new PageRole();
            data.ModuleId = pageRole.ModuleId;
            data.UserId = pageRole.UserId;
            data.RoleId = pageRole.RoleId;
            await _pageRoleService.DeletePageRole(data);
            if (pageRole.OperationType == "User")
            {
                if (pageRole.PageIdList.Count > 0)
                {
                    var result = false;
                    List<PageRole> model = new List<PageRole>();
                    for (int i = 0; i < pageRole.PageIdList.Count; i++)
                    {
                        model.Add(new PageRole
                        {
                            CreatedBy = 1,
                            CreatedDate = DateTime.Now,
                            ModuleId = pageRole.ModuleId,
                            PageId = pageRole.PageIdList[i],
                            RAdd = Convert.ToByte(pageRole.RAddList[i]),
                            RDelete = Convert.ToByte(pageRole.RDeleteList[i]),
                            RDisplay = Convert.ToByte(pageRole.RDisplayList[i]),
                            RView = Convert.ToByte(pageRole.RViewList[i]),
                            REdit = Convert.ToByte(pageRole.REditList[i]),
                            UserId = pageRole.UserId,
                            RoleId = Convert.ToByte(pageRole.RoleId)
                        });
                    }
                    foreach (var item in model)
                    {
                        result = await _pageRoleService.CreatePageRole(item);
                    }
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(pageRole);
                    }
                }
                else
                {
                    return View(pageRole);
                }
            }
            else
            {
                if (pageRole.PageIdList.Count > 0)
                {
                    var result = false;
                    AssignPageRoleWise assignPageRoleWise = new AssignPageRoleWise();
                    assignPageRoleWise.RoleId = pageRole.RoleId;
                    assignPageRoleWise.ModuleId = pageRole.ModuleId;
                    await _pageRoleService.DeleteAssignPageRoleWise(assignPageRoleWise);
                    List<AssignPageRoleWise> model = new List<AssignPageRoleWise>();
                    for (int i = 0; i < pageRole.PageIdList.Count; i++)
                    {
                        model.Add(new AssignPageRoleWise
                        {
                            CreatedBy = 1,
                            CreatedDate = DateTime.Now,
                            ModuleId = pageRole.ModuleId,
                            PageId = Convert.ToInt32(pageRole.PageIdList[i]),
                            RAdd = Convert.ToByte(pageRole.RAddList[i]),
                            RDisplay = Convert.ToByte(pageRole.RDisplayList[i]),
                            RView = Convert.ToByte(pageRole.RViewList[i]),
                            RDelete = Convert.ToByte(pageRole.RDeleteList[i]),
                            REdit = Convert.ToByte(pageRole.REditList[i]),
                            RoleId = Convert.ToByte(pageRole.RoleId)
                        });
                    }
                    foreach (var item in model)
                    {
                        result = await _pageRoleService.Create(item);
                    }
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(pageRole);
                    }
                }
                else
                {
                    return View(pageRole);
                }
            }
        }        
        [HttpGet]
        public async Task<JsonResult> GetUserList(int? roleId)
        {
            roleId = roleId ?? 0;
            var data = await _pageRoleService.GetUserList(Convert.ToInt32(roleId));
            return Json(data);
        }
        [HttpGet]
        public async Task<JsonResult> GetPageRoleDetails(int moduleId, int roleId, int? userId, string operationType)
        {
            if (operationType == "Role")
            {
                var data = await _pageRoleService.GetPageRoleDetailsRoleWise(moduleId, roleId);
                return Json(data);
            }
            else
            {
                var data = await _pageRoleService.GetPageRoleDetailsUserWise(moduleId, roleId, userId);
                return Json(data);
            }
        }
    }
}