using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IIssueReturnFileService : IEntityService<Issuereturnfile>

    {
        Task<List<Datastoragedetails>> GetFileNoList();
        Task<List<Department>> GetAllDepartment();
        Task<List<Branch>> GetAllBranch();
        Task<List<Designation>> GetAllDesignation();
        Task<bool> Create(Issuereturnfile model);
        Task<bool> UpdateIssueFileStatus(int id);
        Task<bool> UpdateReturnFileStatus(int id);
        Task<Issuereturnfile> FetchSingleResult(int id);
        Task<Issuereturnfile> FetchSingleReceiptResult(int id);
        Task<Issuereturnfile> FetchfiletResult(int id);
        Task<Issuereturnfile> FetchReturnReceiptResult(int id);
        Task<bool> Update(int id, Issuereturnfile issuereturnfile);
        Task<List<Datastoragedetails>> GetIssuereturnfile();
        Task<PagedResult<Datastoragedetails>> GetPagedIssueReturnFile(IssueReturnFileSearchDto model);
    }
}
