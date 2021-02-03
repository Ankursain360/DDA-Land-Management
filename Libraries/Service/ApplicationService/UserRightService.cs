using AutoMapper;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Microsoft.AspNetCore.Identity;
using Model.Entity;
using Repository.IEntityRepository;
using Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


using System.Linq;


namespace Service.ApplicationService
{
   public class UserRightService : EntityService<Dmsfileright>, IUserRightService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRightRepository _userrightRepository;

        public UserRightService(IUnitOfWork unitOfWork, IUserRightRepository userrightRepository)
    : base(unitOfWork, userrightRepository)
        {
            _unitOfWork = unitOfWork;
            _userrightRepository = userrightRepository;
        }



        public async Task<List<Department>> GetDepartmentList()
        {
            List<Department> departmentList = await _userrightRepository.GetDepartmentList();
            return departmentList;
        }


        public async Task<PagedResult<Userprofile>> GetPagedUserprofile(UserRightsSearchDto model)
        {
            return await _userrightRepository.GetPagedUserprofile(model);
        }





        public async Task<List<Dmsfileright>> GetDMSFileRight()
        {
            return await _userrightRepository.GetDMSFileRight();
        }








        public async Task<bool> AddUpdateDmsRight(List<UserRightsMapDto> model)
        {
            try
            {
                for (var i = 0; i < model.Count; i++)
                {
                    int userid = model[i].UserId;

                    var result = await _userrightRepository.FindBy(a => a.UserId == userid);
                    _userrightRepository.RemoveRange(result);
                }
                List<Dmsfileright> permission = model.Select(a => new Dmsfileright()
                {
                    UserId = a.UserId,
                    Downloadright = a.Downloadright,
                    Viewright = a.Viewright,

                    CreatedBy = 1,
                    CreatedDate = DateTime.Now
                }).ToList();

                await _userrightRepository.AddRange(permission);
                int saved = await _unitOfWork.CommitAsync();

                return saved > 0;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }



    }
}
