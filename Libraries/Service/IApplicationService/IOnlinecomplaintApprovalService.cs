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
    public interface IOnlinecomplaintApprovalService : IEntityService<Onlinecomplaint>
    {

        Task<List<ComplaintType>> GetAllComplaintType();

       Task<List<Location>> GetAllLocation();
       Task<Onlinecomplaint> FetchSingleResult(int id);
        Task<PagedResult<Onlinecomplaint>> GetPagedOnlinecomplaint(OnlinecomplaintApprovalSearchDto model, int userId);

    




    }
}
