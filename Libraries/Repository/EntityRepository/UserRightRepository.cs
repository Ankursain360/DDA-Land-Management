using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Microsoft.EntityFrameworkCore;
using Model.Entity;
using Repository.IEntityRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using System;
using Repository.Common;


namespace Repository.EntityRepository
{
    public class UserRightRepository : GenericRepository<Dmsfileright>, IUserRightRepository
    {

        public UserRightRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Department>> GetDepartmentList()
        {
            var departmentList = await _dbContext.Department.Where(x => x.IsActive == 1).ToListAsync();
            return departmentList;
        }




        public async Task<List<Dmsfileright>> GetDMSFileRight()
        {
            return await _dbContext.Dmsfileright.ToListAsync();
        }





        public async Task<List<UserrightsListDataDto>> GetPagedUserprofile(UserRightsSearchDto model)

        {
            try
            {


                var data = await _dbContext.LoadStoredProcedure("bindUserrights")
                                            .WithSqlParams(("P_departmentId", model.department))



                                            .ExecuteStoredProcedureAsync<UserrightsListDataDto>();

                return (List<UserrightsListDataDto>)data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }



    }
}
