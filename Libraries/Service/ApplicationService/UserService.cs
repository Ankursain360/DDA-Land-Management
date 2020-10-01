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
    public class UserService : EntityService<User>, IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        : base(unitOfWork, userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }
        public async Task<bool> CheckUniqueLoginName(int id, string loginname)
        {
            bool result = await _userRepository.AnyLoginName(id, loginname);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _userRepository.FindBy(a => a.Id == id);
            User model = form.FirstOrDefault();
            model.IsActive = 0;
            _userRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<User> FetchSingleResult(int id)
        {
            var result = await _userRepository.FindBy(a => a.Id == id);
            User model = result.FirstOrDefault();
            return model;
        }

        public async Task<List<Department>> GetAllDepartment()
        {
            List<Department> departmentList = await _userRepository.GetAllDepartment();
            return departmentList;
        }

        public async Task<List<User>> GetAllUser()
        {

            return await _userRepository.GetUser();
        }

        public async Task<List<Role>> GetAllRole()
        {
            List<Role> roleList = await _userRepository.GetAllRole();
            return roleList;
        }

        public async Task<List<User>> GetUserUsingRepo()
        {
            return await _userRepository.GetUser();
        }

        public async Task<bool> Update(int id, User user)
        {
            var result = await _userRepository.FindBy(a => a.Id == id);
            User model = result.FirstOrDefault();
            model.DistrictId = user.DistrictId;
            model.LoginName = user.LoginName;
            model.DisplayName = user.DisplayName;
            model.Email = user.Email;
            model.RoleId = user.RoleId;
            model.ContactNo = user.ContactNo;
            model.AadharcardNo = user.AadharcardNo;
            model.IsActive = user.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _userRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(User user)
        {
            user.CreatedBy = 1;
            user.CreatedDate = DateTime.Now;
            user.PrevPassword1 = "T";
            user.PrevPassword2 = "T";
            user.PrevPassword3 = "T";
            user.PrevPassword4 = "T";
            user.PrevPassword5 = "T";
            _userRepository.Add(user);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            return await _userRepository.GetAllZone(departmentId);
        }
    }
}
