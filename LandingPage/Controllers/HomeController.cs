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

namespace LandingPage.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ISiteContext _siteContext;
        private readonly IUserProfileService _userProfileService;

       
        public HomeController(ISiteContext siteContext,
          IUserProfileService userProfileService, IModuleService moduleService)
        {
            _siteContext = siteContext;
            _userProfileService = userProfileService;
            _moduleService = moduleService;
        }
       

        private readonly IModuleService _moduleService;


        public async Task<IActionResult> Index()
        {
            //  var list = await _moduleService.GetAllModule();
            var result = await _moduleService.ModuleFromMenuRoleActionMap(SiteContext.RoleId ?? 0);
            List<RoleWiseModuleMappingDto> data = new List<RoleWiseModuleMappingDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new RoleWiseModuleMappingDto()
                    {
                        Id = result[i].ModuleId ?? 0,
                        Url = result[i].Module == null ? "" : result[i].Module.Url,
                        Target = result[i].Module == null ? "" : result[i].Module.Target,
                        Icon = result[i].Module == null ? "" : result[i].Module.Icon,
                        Name = result[i].Module == null ? "" : result[i].Module.Name,
                        Description = result[i].Module == null ? "" : result[i].Module.Description
                    });
                }
            }
            return View(data);           
        }

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
