using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Dto.Master;

namespace Libraries.Repository.IEntityRepository
{
    public interface IRequestforproceedingRepository : IGenericRepository<Requestforproceeding>
    {
        Task<List<Allotmententry>> GetAllAllotment();
        Task<List<Honble>> GetAllHonble();
        Task<List<Requestforproceeding>> GetAllRequestForProceeding();
        Task<PagedResult<Requestforproceeding>> GetPagedRequestForProceeding(RequestForProceedingSearchDto model);
        Task<List<UserBindDropdownDto>> BindUsernameNameList();
    }
}
