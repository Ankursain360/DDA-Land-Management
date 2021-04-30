using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
  public  interface IOnlinecomplaintApprovalRepository : IGenericRepository<Onlinecomplaint>
    {


        Task<List<ComplaintType>> GetAllComplaintType();

        Task<List<Location>> GetAllLocation();
        Task<PagedResult<Onlinecomplaint>> GetPagedOnlinecomplaint(OnlinecomplaintApprovalSearchDto model, int userId);
        Task<bool> IsApplicationPendingAtUserEnd(int id, int userId);

    }
}
