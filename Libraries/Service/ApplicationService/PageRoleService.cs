using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using MySqlX.XDevAPI.Common;
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
        public async Task<bool> Create(AssignPageRoleWise assignPageRoleWise)
        {
            assignPageRoleWise.CreatedBy = 1;
            assignPageRoleWise.CreatedDate = DateTime.Now;
            var result = await _pageRoleRepository.Add(assignPageRoleWise);
            return result;
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

        public async Task<List<PageRole>> GetPageRoleDetailsRoleWise(int moduleId, int roleId)
        {
            return await _pageRoleRepository.GetPageRoleDetailsRoleWise(moduleId, roleId);
        }

        public async Task<List<PageRole>> GetPageRoleDetailsUserWise(int moduleId, int roleId, int? userId)
        {
            return await _pageRoleRepository.GetPageRoleDetailsRoleWise(moduleId, roleId,userId);
        }

        public async Task<bool> DeletePageRole(PageRole pageRole)
        {
            return await _pageRoleRepository.DeletePageRole(pageRole);
        }

        public async Task<bool> DeleteAssignPageRoleWise(AssignPageRoleWise assignPageRoleWise)
        {
            return await _pageRoleRepository.DeleteAssignPageRoleWise(assignPageRoleWise);
        }

        public async Task<bool> CreatePageRole(PageRole pageRole)
        {
            pageRole.CreatedBy = 1;
            pageRole.CreatedDate = DateTime.Now;
            var result = await _pageRoleRepository.AddPageRole(pageRole);
            return result;
        }
    }
}
