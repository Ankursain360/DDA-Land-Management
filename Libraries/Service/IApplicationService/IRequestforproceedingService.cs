using Libraries.Model.Entity;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Repository.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IRequestforproceedingService : IEntityService<Requestforproceeding>
    {

        Task<bool> Create(Requestforproceeding requestforproceeding);
        Task<bool> Update(int id, Requestforproceeding scheme);
        Task<List<Requestforproceeding>> GetAllRequestForProceeding();
        Task<Requestforproceeding> FetchSingleResult(int id);
        //Task<List<Requestforproceeding>> GetRequestUsingRepo();
        //Task<bool> Delete(int id);
        //Task<PagedResult<Requestforproceeding>> GetPagedRequestForProceeding(RequestForProceedingSearchDto model);

        //Task<bool> UpdateBeforeApproval(int id, Requestforproceeding requestforproceeding);


    }
}
