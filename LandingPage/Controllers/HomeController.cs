using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LandingPage.Models;
using Libraries.Service.IApplicationService;
using LandingPage.Helper;
using Service.IApplicationService;
using Dto.Master;
using Libraries.Model.Entity;

namespace LandingPage.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ISiteContext _siteContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IModuleService _moduleService;
        private readonly IModuleCategoryService _modulecategoryService;



        public HomeController(ISiteContext siteContext,
          IUserProfileService userProfileService, IModuleService moduleService, IModuleCategoryService modulecategoryService)
        {
            _siteContext = siteContext;
            _userProfileService = userProfileService;
            _moduleService = moduleService;
            _modulecategoryService = modulecategoryService;
        }

        #region changes regarding dynamic categorization of landing page based on module category   Renu added by 7 May 2021
        public async Task<IActionResult> Index()
        {
            var moduleCategoryData = await _moduleService.GetModuleCategory();
            var result = await _moduleService.ModuleFromMenuRoleActionMap(SiteContext.RoleId ?? 0);
            List<RoleWiseModuleMappingDto> data = new List<RoleWiseModuleMappingDto>();
            if (moduleCategoryData != null)
            {
                for (int i = 0; i < moduleCategoryData.Count; i++)
                {
                    data.Add(new RoleWiseModuleMappingDto()
                    {
                        ModuleCategoryId = moduleCategoryData[i].Id,
                        ModuleCategoryName = moduleCategoryData[i].CategoryName,
                        ParentId = 0
                    });
                }
            }
            var mdoulecatlisting = GetModuleListing(data, result);
            return View(mdoulecatlisting);
        }

        private IList<RoleWiseModuleMappingDto> GetModuleListing(IList<RoleWiseModuleMappingDto> modulecatList, IList<Menuactionrolemap> result)
        {
            List<RoleWiseModuleMappingDto> vmList = modulecatList.ToList();
            foreach (var item in modulecatList)
            {
                var filteredmodule = result.Where(x => x.Module.ModuleCategoryId == item.ModuleCategoryId)
                                            .OrderBy(x => x.Module.SortBy)
                                            .ToList();
                foreach (var mdoulelist in filteredmodule)
                {
                    var vm = new RoleWiseModuleMappingDto
                    {
                        Id = mdoulelist.ModuleId ?? 0,
                        Url = mdoulelist.Module == null ? "" : mdoulelist.Module.Url,
                        Target = mdoulelist.Module == null ? "" : mdoulelist.Module.Target,
                        Icon = mdoulelist.Module == null ? "" : mdoulelist.Module.Icon,
                        Name = mdoulelist.Module == null ? "" : mdoulelist.Module.Name,
                        Description = mdoulelist.Module == null ? "" : mdoulelist.Module.Description,
                        ParentId = mdoulelist.Module.ModuleCategoryId ?? 0
                    };
                    vmList.Add(vm);
                }
            }
            return vmList;
        }
        #endregion
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult UnAuthorized()
        {
            return View();
        }

        public IActionResult ExceptionLog()
        {
            return View();
        }
    }
}
