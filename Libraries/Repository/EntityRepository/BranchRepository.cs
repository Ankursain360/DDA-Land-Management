using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
    public class BranchRepository : GenericRepository<Branch>, IBranchRepository
    {

        public BranchRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Branch>> GetAllDetails()
        {
            var data = await _dbContext.Branch.Include(s => s.Department).Where(s => s.IsActive == 1).OrderBy(s => s.Id).ToListAsync();
            return data;
        }
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Branch.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> anyCode(int id, string code)
        {
            return await _dbContext.Branch.AnyAsync(t => t.Id != id && t.Code.ToLower() == code.ToLower());
        }

        public async Task<List<Department>> GetDepartmentList()
        {
            var departmentList = await _dbContext.Department.Where(x => x.IsActive == 1).ToListAsync();
            return departmentList;
        }

        public async Task<PagedResult<Branch>> GetPagedBranch(BranchSearchDto model)
        {
            var data = await _dbContext.Branch.Include(s => s.Department)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
                        .GetPaged<Branch>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DEPARTMENT"):
                        data = null;
                        data = await _dbContext.Branch
                            .Include(s => s.Department)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
                            .OrderBy(x => x.Department.Name)
                            .GetPaged<Branch>(model.PageNumber, model.PageSize);
                        break;


                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Branch
                            .Include(s => s.Department)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
                            .OrderBy(x => x.Name)
                            .GetPaged<Branch>(model.PageNumber, model.PageSize);
                        break;
                    case ("CODE"):
                        data = null;
                        data = await _dbContext.Branch
                            .Include(s => s.Department)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
                            .OrderBy(x => x.Code)
                            .GetPaged<Branch>(model.PageNumber, model.PageSize);
                        break;
                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.Branch
                            .Include(s => s.Department)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
                            .OrderByDescending(x => x.IsActive)
                            .GetPaged<Branch>(model.PageNumber, model.PageSize);
                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DEPARTMENT"):
                        data = null;
                        data = await _dbContext.Branch
                            .Include(s => s.Department)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
                            .OrderByDescending(x => x.Department.Name)
                            .GetPaged<Branch>(model.PageNumber, model.PageSize);
                        break;


                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Branch
                            .Include(s => s.Department)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
                            .OrderByDescending(x => x.Name)
                            .GetPaged<Branch>(model.PageNumber, model.PageSize);
                        break;
                    case ("CODE"):
                        data = null;
                        data = await _dbContext.Branch
                            .Include(s => s.Department)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
                            .OrderByDescending(x => x.Code)
                            .GetPaged<Branch>(model.PageNumber, model.PageSize);
                        break;
                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.Branch
                            .Include(s => s.Department)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
                            .OrderBy(x => x.IsActive)
                            .GetPaged<Branch>(model.PageNumber, model.PageSize);
                        break;


                }
            }
            return data;
        }

        public async Task<List<Branch>> GetGetBranchList(int departmentId)
        {
            return await _dbContext.Branch
                                    .Where(x => x.DepartmentId == departmentId && x.IsActive == 1)
                                    .ToListAsync();
        }
    }


}

