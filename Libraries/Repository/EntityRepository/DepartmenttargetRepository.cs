using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ubiety.Dns.Core.Records.NotUsed;

namespace Libraries.Repository.EntityRepository
{
    public class DepartmenttargetRepository:GenericRepository<Departmenttarget>,IDepartmenttargetRepository
        {
        public DepartmenttargetRepository(DataContext dbContext) : base(dbContext)
        {  }
       
        public async Task<PagedResult<Departmenttarget>> GetPagedDepartmenttarget(DepartmentTargetSearchDto model)
        {
            var data = await _dbContext.Departmenttarget
                         .Include(x => x.Department)
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.Department.Name.Contains(model.name)))
                            .GetPaged<Departmenttarget>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DEPARTMENT"):
                        data = null;
                        data = await _dbContext.Departmenttarget
                        .Include(x => x.Department)
                      //  .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Department.Name.Contains(model.name)))
                           //  && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                           .OrderBy(x => x.Department.Name)
                            .GetPaged<Departmenttarget>(model.PageNumber, model.PageSize);
                        break;
                   
                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.Departmenttarget
                     //   .Include(x => x.Zone)
                        .Include(x => x.Department)
                       // .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Department.Name.Contains(model.name)))
                          //   && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                            .OrderByDescending(x => x.IsActive)
                            .GetPaged<Departmenttarget>(model.PageNumber, model.PageSize); ;

                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("DEPARTMENT"):
                        data = null;
                        data = await _dbContext.Departmenttarget
                        //  .Include(x => x.Zone)
                        .Include(x => x.Department)
                            //  .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Department.Name.Contains(model.name)))
                           //  && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                           .OrderByDescending(x => x.Department.Name)
                            .GetPaged<Departmenttarget>(model.PageNumber, model.PageSize);
                        break;

                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.Departmenttarget
                        //   .Include(x => x.Zone)
                        .Include(x => x.Department)
                            // .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Department.Name.Contains(model.name)))
                            //   && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                            .OrderBy(x => x.IsActive)
                            .GetPaged<Departmenttarget>(model.PageNumber, model.PageSize); ;

                        break;

                }
            }
            return data;
        }

        public async Task<List<Departmenttarget>> GetDepartmenttarget()
        {
            return await _dbContext.Departmenttarget.Where(x => x.IsActive == 1).ToListAsync();
            //return await _dbContext.casenature.OrderByDescending(s => s.IsActive).ToListAsync();
        }
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Departmenttarget.AnyAsync(t => t.Id != id && t.Department.Name.ToLower() == name.ToLower());
        }
        public async Task<List<Departmenttarget>> GetAllDepartmenttarget()
        {
            var data = await _dbContext.Departmenttarget.Include(x => x.Department).OrderByDescending(x => x.Id).ToListAsync();
            return data;
        }
        public async Task<bool> AnyName(int Id, string Name, int DepartmentId)
        {
            return await _dbContext.Departmenttarget.AnyAsync(t => t.Id != Id && t.DepartmentId == DepartmentId && t.IsActive == 1 );
        }
        public async Task<bool> AnyCode(int id, int code)
        {
            return await _dbContext.Departmenttarget.AnyAsync(t => t.Id != id && t.DepartmentId == code);
        }
        public async Task<List<Department>> GetAllDepartment()
        {
            List<Department> departmentList = await _dbContext.Department.Where(x => x.IsActive == 1).ToListAsync();
            return departmentList;
        }
        public async Task<List<WeeklyFileReportListDataDto>> GetPagedWeeklyFileReport(WeeklyFileReportSearchDto model, int UserId)
        {

            int SortOrder = (int)model.SortOrder;
            var data = await _dbContext.LoadStoredProcedure("BindWeeklyReport")
                                             .WithSqlParams(
                                             ("UserId", UserId),
                                             ("Dept_id", model.DeptId),
                                             ("FromDate", model.FromDate), 
                                             ("ToDate", model.ToDate),
                                             ("P_SortOrder", SortOrder),
                                             ("P_SortBy", model.SortBy))
                                             .ExecuteStoredProcedureAsync<WeeklyFileReportListDataDto>();
            return (List<WeeklyFileReportListDataDto>)data;


        }

    }

}

