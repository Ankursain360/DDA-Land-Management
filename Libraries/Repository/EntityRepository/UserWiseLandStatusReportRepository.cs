using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Repository.Common;

namespace Libraries.Repository.EntityRepository
{
    public class UserWiseLandStatusReportRepository : GenericRepository<Vacantlandimage>, IUserWiseLandStatusReportRepository
    {
        public UserWiseLandStatusReportRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Department>> GetAllDepartment()
        {
            return await _dbContext.Department.Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Division>> GetAllDivision(int zoneId)
        {
            return await _dbContext.Division.Where(x => x.ZoneId == zoneId && x.IsActive == 1).ToListAsync();
        }


        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            return await _dbContext.Zone.Where(x => x.DepartmentId == departmentId && x.IsActive == 1).ToListAsync();
        }
        public async Task<PagedResult<Vacantlandimage>> GetPagedUserWiseLandStatusReport(UserWiseLandStatusReportSearchDto model)
        {
            
            var data = await _dbContext.Vacantlandimage

                .Include(x => x.DepartmentNavigation)
                .Include(x => x.ZoneNavigation)
                .Include(x => x.DivisionNavigation)
                .Where(x => (x.DepartmentId == ((model.departmentId ?? 0) == 0 ? x.DepartmentId : model.departmentId))
                && (x.ZoneId == (model.zoneId == 0 ? x.ZoneId : model.zoneId))
                && (x.DivisionId == (model.divisionId == 0 ? x.DivisionId : model.divisionId))

                )
                .OrderBy(x => x.DepartmentNavigation.Name)

                                                .ThenBy(x => x.ZoneNavigation.Name)
                                                .ThenBy(x => x.DivisionNavigation.Name)
                                            .GetPaged<Vacantlandimage>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("DEPARTMENT"):
                        data.Results = data.Results.OrderBy(x => x.DepartmentNavigation).ToList();
                        break;
                    case ("ZONE"):
                        data.Results = data.Results.OrderBy(x => x.ZoneNavigation).ToList();
                        break;

                    case ("DIVISION"):
                        data.Results = data.Results.OrderBy(x => x.DivisionNavigation).ToList();
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {


                    case ("DEPARTMENT"):
                        data.Results = data.Results.OrderByDescending(x => x.DepartmentNavigation).ToList();
                        break;
                    case ("ZONE"):
                        data.Results = data.Results.OrderByDescending(x => x.ZoneNavigation).ToList();
                        break;

                    case ("DIVISION"):
                        data.Results = data.Results.OrderByDescending(x => x.DivisionNavigation).ToList();
                        break;

                }
            }
            return data;
        }




    }
}
