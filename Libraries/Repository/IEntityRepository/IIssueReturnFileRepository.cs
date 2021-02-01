using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
   
   public interface IIssueReturnFileRepository : IGenericRepository<Issuereturnfile>
    {
        Task<List<Datastoragedetails>> GetFileNoList();
        Task<PagedResult<Datastoragedetails>> GetPagedIssueReturnFile(IssueReturnFileSearchDto model);

        //Task<List<Zone>> GetAllZone(int departmentId);
        Task<List<Department>> GetAllDepartment();
        Task<List<Branch>> GetAllBranch();
        Task<List<Designation>> GetAllDesignation();
        Task<Issuereturnfile> FetchSingleReceiptResult(int id);
    }
}
