//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Service.IApplicationService
//{
//    class IBranchService
//    {
//    }
//}
using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libraries.Service.IApplicationService
{
    public interface IBranchService : IEntityService<Branch>
    {

        Task<List<BranchDto>> GetBranch();
        Task<bool> Update(int id, Branch branch); 

        Task<bool> Create(Branch branch);

        Task<Branch> FetchSingleResult(int id);  

        Task<bool> Delete(int id);   

        Task<bool> CheckUniqueName(int id, string branch);
        Task<bool> CheckUniqueCode(int id, string code);
        Task<List<Department>> GetDropDownList();
        Task<List<Branch>> GetAllDetails();
        Task<List<Branch>> GetBranchUsingRepo();
        Task<PagedResult<Branch>> GetPagedBranch(BranchSearchDto model);
        Task<List<BranchDto>> GetGetBranchList(int departmentid);
    }
}

