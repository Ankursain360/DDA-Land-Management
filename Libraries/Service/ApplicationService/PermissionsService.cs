using AutoMapper;
using Core.Enum;
using Dto.Component;
using Dto.Master;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{
    public class PermissionsService : EntityService<Menuactionrolemap>, IPermissionsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPermissionsRepository _permissionsRepository;
        private readonly IActionsRepository _actionsRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IMapper _mapper;
        public PermissionsService(IUnitOfWork unitOfWork,
            IPermissionsRepository permissionsRepository,
            IActionsRepository actionsRepository,
            IMenuRepository menuRepository,
            IModuleRepository moduleRepository,
            IMapper mapper)
        : base(unitOfWork, permissionsRepository)
        {
            _unitOfWork = unitOfWork;
            _permissionsRepository = permissionsRepository;
            _actionsRepository = actionsRepository;
            _menuRepository = menuRepository;
            _moduleRepository = moduleRepository;
            _mapper = mapper;
        }

        public async Task<List<Module>> GetModuleList()
        {
            return await _permissionsRepository.GetModuleList();
        }

        public async Task<List<MenuDetailDto>> GetMappedMenu(string moduleId, int roleId)
        {

            var allocatedMenu = await _permissionsRepository.GetPermission(moduleId, roleId);
            var result = allocatedMenu.GroupBy(a => a.MenuId)
                .Select(a => a.FirstOrDefault())
                    .Select(b => new MenuDetailDto()
                    {
                        Id = b.MenuId,
                        ParentId = b.Menu.ParentMenuId ?? 0,
                        Name = b.Menu.Name,
                        Url = b.Menu.Url,
                        SortOrder = b.Menu.SortBy
                    }).ToList();

            var menuId = allocatedMenu.Select(a => a.Menu.ParentMenuId).Distinct();
            var menuDetail = await _menuRepository.FindBy(a => menuId.Contains(a.Id));
            var parentMenu = menuDetail.Select(b => new MenuDetailDto()
            {
                Id = b.Id,
                ParentId = b.ParentMenuId ?? 0,
                Name = b.Name,
                Url = null,
                SortOrder = b.SortBy
            });

            result.AddRange(parentMenu);

            return result;
        }

        public async Task<List<PermissionDto>> GetMappedMenuWithAction(int moduleId, int roleId)
        {
            var actions = await _actionsRepository.FindBy(a => a.IsActive == 1);
            var permissions = await _permissionsRepository.GetMappedMenuWithAction(moduleId);
            var menuAction = permissions.SelectMany(a => a.Menuactionrolemap).Where(b=>b.RoleId==roleId).ToList();
            var result = permissions.GroupBy(a => a.Id)
                .Select(b => b.FirstOrDefault())
                    .Select(c => new PermissionDto()
                    {
                        Id = c.Id,
                        ParentId = c.ParentMenuId ?? 0,
                        Name = c.Name
                    }).ToList();

            foreach (var item in result)
            {
                var menuPermission = actions.Select(a => new MappedMenuActionDto()
                {
                    ActionId = a.Id,
                    ActionName = a.Name,
                    IsActive = a.IsActive,
                    IsAvailable = menuAction.Any(c=>c.ActionId==a.Id && c.MenuId==item.Id)
                }).ToList();
                item.Actions = menuPermission;
            }

            return result;
        }

        public async Task<bool> AddUpdatePermission(List<MenuActionRoleMapDto> model)
        {
            int roleId = model.FirstOrDefault().RoleId;
            int moduleId = model.FirstOrDefault().ModuleId;
            var result = await _permissionsRepository.FindBy(a => a.RoleId == roleId && a.ModuleId==moduleId);
            _permissionsRepository.RemoveRange(result);

            List<Menuactionrolemap> permission = model.Select(a => new Menuactionrolemap()
            {
                MenuId = a.MenuId,
                ActionId = a.ActionId,
                RoleId = a.RoleId,
                ModuleId= a.ModuleId,
                CreatedBy = 1,
                CreatedDate = DateTime.Now
            }).ToList();

            await _permissionsRepository.AddRange(permission);
            int saved = await _unitOfWork.CommitAsync();

            return saved > 0;
        }

        public async Task<bool> ValidatePermission(ViewAction action, int roleId, string moduleGuid)
        {
            Module module = await _moduleRepository.GetModuleByGuid(moduleGuid);
            string actionName= action.ToString();
            return await _permissionsRepository.AuthorizeUser(actionName, roleId, module.Id);
        }
    }
}
