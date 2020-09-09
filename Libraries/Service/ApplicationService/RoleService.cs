using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Microsoft.EntityFrameworkCore;
using Libraries.Repository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{

    public class RoleService : EntityService<Role>, IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleRepository _roleRepository;
        public RoleService(IUnitOfWork unitOfWork, IRoleRepository roleRepository)
        : base(unitOfWork, roleRepository)
        {
            _unitOfWork = unitOfWork;
            _roleRepository = roleRepository;
        }
        public async Task<List<Role>> GetAllRole()
        {
          
            return await _roleRepository.GetRole();
        }

        public async Task<List<Role>> GetRoleUsingRepo()
        {
            return await _roleRepository.GetRole();
        }

        public async Task<bool> Update(int id, Role role)
        {
            var result = await _roleRepository.FindBy(a => a.Id == id);
            Role model = result.FirstOrDefault();
            model.Name = role.Name;
            model.ZoneId = role.ZoneId;
            model.IsActive = role.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _roleRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Role role)
        {

            role.CreatedBy = 1;
            role.CreatedDate = DateTime.Now;
            _roleRepository.Add(role);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<Role> FetchSingleResult(int id)
        {
            var result = await _roleRepository.FindBy(a => a.Id == id);
            Role model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _roleRepository.FindBy(a => a.Id == id);
            Role model = form.FirstOrDefault();
            model.IsActive = 0;
            _roleRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<List<Zone>> GetAllZone()
        {
            List<Zone> zoneList = await _roleRepository.GetAllZone();
            return zoneList;
        }
        public async Task<bool> CheckUniqueName(int id, string name)
        {
            bool result = await _roleRepository.Any(id, name);
            return result;
        }
    }
}
