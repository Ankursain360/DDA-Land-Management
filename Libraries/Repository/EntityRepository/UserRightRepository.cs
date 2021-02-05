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


        //public async Task<PagedResult<Userprofile>> GetPagedUserprofile(UserRightsSearchDto model)
        //{

        //    var data = await _dbContext.Userprofile.Include(a => a.User)
        //        .Include(b => b.Department)
        //        .Include(c => c.Dmsfileright)
        //        .Where(x => (x.DepartmentId == (model.department == 0 ? x.DepartmentId : model.department)
        //        && (x.IsActive == 1)
        //      //  && x.Department.IsActive == 1
        //        )
        //        ).GetPaged<Userprofile>(model.PageNumber, model.PageSize);

        //    return data;
        //}




        public async Task<List<UserrightsListDataDto>> GetPagedUserprofile(UserRightsSearchDto model)

        {
            try {
           
               
                var data = await _dbContext.LoadStoredProcedure("bindUserrights")
                                            .WithSqlParams(("P_departmentId", model.department))
                                      


                                            .ExecuteStoredProcedureAsync<UserrightsListDataDto>();
            //  return data;

            return (List<UserrightsListDataDto>)data;
            }
            catch (Exception ex)
            {

                throw;
            }
            }



    }
}
