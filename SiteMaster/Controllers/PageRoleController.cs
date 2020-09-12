using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace SiteMaster.Controllers
{
    public class PageRoleController : Controller
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
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Index(PageRole pageRole)
        {
            return View();
        }
    }
}