using Dto.Component;
using EncroachmentDemolition.Helper;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EncroachmentDemolition.Components
{
    public class SideNavViewComponent : ViewComponent
    {
        private readonly IPermissionsService _permissionsService;
        private readonly IConfiguration _configuration;
        private readonly ISiteContext _siteContext;

        public SideNavViewComponent(IPermissionsService permissionsService,
            IConfiguration configuration,
            ISiteContext siteContext)
        {
            _permissionsService = permissionsService;
            _configuration = configuration;
            _siteContext = siteContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _permissionsService.GetMappedMenu(_configuration.GetValue<string>("ModuleId"), _siteContext.RoleId.Value);
            var menu = GetMenu(result, 0);
            return View("SideNav", menu);
        }

        private IList<SideNavDto> GetMenu(IList<MenuDetailDto> menuList, int parentId)
        {
            var children = GetChildrenMenu(menuList, parentId);

            if (!children.Any())
            {
                return new List<SideNavDto>();
            }

            List<SideNavDto> vmList = new List<SideNavDto>();
            foreach (var item in children)
            {
                var menu = menuList.FirstOrDefault(x => x.Id == item.Id);
                var vm = new SideNavDto
                {
                    Id = menu.Id,
                    MenuId = menu.Id,
                    Url = menu.Url,
                    Name = menu.Name,
                    ParentId = Convert.ToInt32(menu.ParentId),
                    Children = GetMenu(menuList, menu.Id)
                };
                vmList.Add(vm);
            }
            return vmList;
        }

        private IList<MenuDetailDto> GetChildrenMenu(IList<MenuDetailDto> menuList, int parentId = 0)
        {
            return menuList.Where(x => x.ParentId == parentId).OrderBy(x => x.SortOrder).ToList();
        }
    }
}
