using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{
    public class PageRoleService : EntityService<PageRole>, IPageRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPageRoleRepository _pageRoleRepository;
        public PageRoleService(IUnitOfWork unitOfWork, IPageRoleRepository pageRoleRepository)
        : base(unitOfWork, pageRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _pageRoleRepository = pageRoleRepository;
        }

        public async Task<List<PageRole>> GetAllPageRole()
        {
            var data= await _pageRoleRepository.GetAllPageRole();
            return data;
        }
        public async Task<bool> Create(PageRole pageRole)
        {
            pageRole.CreatedBy = 1;
            pageRole.CreatedDate = DateTime.Now;
            _pageRoleRepository.Add(pageRole);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<List<Module>> GetAllModuleList()
        {
            return await _pageRoleRepository.GetAllModule();
        }
        public async Task<List<Role>> GetAllRoleList()
        {
            return await _pageRoleRepository.GetAllRole();
        }
        public async Task<List<User>> GetUserList(int Role)
        {
            return await _pageRoleRepository.GetAllUser(Role);
        }
    }
}
