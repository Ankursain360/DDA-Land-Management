using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Libraries.Repository.IEntityRepository;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Dto.Component;

namespace Libraries.Service.ApplicationService
{

    public class PermissionsService : EntityService<Menuactionrolemap>, IPermissionsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPermissionsRepository _permissionsRepository;
        private readonly IMapper _mapper;
        public PermissionsService(IUnitOfWork unitOfWork,
            IPermissionsRepository permissionsRepository,
            IMapper mapper)
        : base(unitOfWork, permissionsRepository)
        {
            _unitOfWork = unitOfWork;
            _permissionsRepository = permissionsRepository;
            _mapper = mapper;
        }
       
        public async Task<List<Module>> GetModuleList()
        {
            return await _permissionsRepository.GetModuleList();
        }

        public async Task<List<MenuDetailDto>> GetMappedMenu(int moduleId, int roleId) {

            var result1 = await _permissionsRepository.GetPermission(moduleId, roleId);
            var result = result1.GroupBy(a => a.MenuId)
                .Select(a=>a.FirstOrDefault())
                    .Select(b=> new MenuDetailDto() {
                        Id = b.MenuId,
                        ParentId = b.Menu.ParentMenuId ?? 0,
                        Name = b.Menu.Name,
                        Url = b.Menu.Url,
                        SortOrder = b.Menu.SortBy
                    }).ToList();

            return result;
        }
       
    }
}
