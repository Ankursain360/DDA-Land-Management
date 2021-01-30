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

namespace Libraries.Repository.EntityRepository
{
     public class IssueReturnFileRepository : GenericRepository<Issuereturnfile>, IIssueReturnFileRepository
    {
        public IssueReturnFileRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Department>> GetAllDepartment()
        {
            List<Department> departmentList = await _dbContext.Department.Where(x => x.IsActive == 1).ToListAsync();
            return departmentList;
        }
        public async Task<List<Branch>> GetAllBranch()
        {
            List<Branch> branchList = await _dbContext.Branch.Where(x => x.IsActive == 1).ToListAsync();
            return branchList;
        }
        public async Task<List<Designation>> GetAllDesignation()
        {
            List<Designation> designationList = await _dbContext.Designation.Where(x => x.IsActive == 1).ToListAsync();
            return designationList;
        }
        public async Task<List<Datastoragedetails>> GetFileNoList()
        {
            var fileNoList = await _dbContext.Datastoragedetails.Where(x => x.IsActive == 1).ToListAsync();
            return fileNoList;
        }
        public async Task<PagedResult<Datastoragedetails>> GetPagedIssueReturnFile(IssueReturnFileSearchDto model)
        {

            var data = await _dbContext.Datastoragedetails
                             .Include(x => x.Almirah)
                             .Include(x => x.Row)
                             .Include(x => x.Column)
                             .Include(x => x.Bundle)
                             .Where(x => (x.Id == (model.FileNo == 0 ? x.Id : model.FileNo)))
                             .GetPaged(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                   

                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Datastoragedetails
                                     .Include(x => x.Almirah)
                                     .Include(x => x.Row)
                                     .Include(x => x.Column)
                                     .Include(x => x.Bundle)
                                     .Where(x => (x.Id == (model.FileNo == 0 ? x.Id : model.FileNo)))
                                     .OrderByDescending(x => x.FileStatus)
                                     .GetPaged(model.PageNumber, model.PageSize);
                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                   
                   
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Datastoragedetails
                                     .Include(x => x.Almirah)
                                     .Include(x => x.Row)
                                     .Include(x => x.Column)
                                     .Include(x => x.Bundle)
                                    .Where(x => (x.Id == (model.FileNo == 0 ? x.Id : model.FileNo)))
                                    .OrderBy(x => x.FileStatus)
                                    .GetPaged(model.PageNumber, model.PageSize);
                        break;


                }
            }
            return data;


        }

        public async Task<Issuereturnfile> FetchSingleReceiptResult(int id)
        {
            return await _dbContext.Issuereturnfile
                                    .Include(x => x.Department)
                                    .Include(x => x.Branch)
                                    .Include(x => x.Designation)
                                     
                                    .Include(x => x.DataStorageDetails)
                                    .Include(x => x.DataStorageDetails.Almirah)
                                    .Include(x => x.DataStorageDetails.Row)
                                    .Include(x => x.DataStorageDetails.Column)
                                    .Include(x => x.DataStorageDetails.Bundle)
                                   .Where(x => x.Id == id)
                                   .SingleOrDefaultAsync();
        }
    }
    
}
