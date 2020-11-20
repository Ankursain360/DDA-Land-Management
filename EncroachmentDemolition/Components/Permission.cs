using Dto.Master;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EncroachmentDemolition.Components
{
    public class PermissionViewComponent : ViewComponent
    {
        private readonly IPermissionsService _permissionsService;

        public PermissionViewComponent(IPermissionsService permissionsService)
        {
            _permissionsService = permissionsService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int moduleId, int roleId)
        {
            var result = await _permissionsService.GetMappedMenuWithAction(moduleId, roleId);
            var menu = GetMenu(result, 0);
            return View("Permission", menu);
        }

        private IList<PermissionDto> GetMenu(IList<PermissionDto> menuList, int parentId)
        {
            var children = GetChildrenMenu(menuList, parentId);

            if (!children.Any())
            {
                return new List<PermissionDto>();
            }

            List<PermissionDto> vmList = new List<PermissionDto>();
            foreach (var item in children)
            {
                var menu = menuList.FirstOrDefault(x => x.Id == item.Id);
                var vm = new PermissionDto
                {
                    Id = menu.Id,
                    Name = menu.Name,
                    ParentId = Convert.ToInt32(menu.ParentId),
                    Children = GetMenu(menuList, menu.Id),
                    Actions = item.Actions
                };
                vmList.Add(vm);
            }
            return vmList;
        }

        private IList<PermissionDto> GetChildrenMenu(IList<PermissionDto> menuList, int parentId = 0)
        {
            return menuList.Where(x => x.ParentId == parentId).ToList();
        }
    }
}
