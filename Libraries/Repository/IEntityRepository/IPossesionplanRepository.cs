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
    
    public interface IPossesionplanRepository : IGenericRepository<Possesionplan>
    {
        Task<List<Allotmententry>> GetAllAllotmententry();
        Task<List<Possesionplan>> GetAllPossesionplan();
        Task<List<Leaseapplication>> GetAllLeaseApplication();
        Task<List<Allotmententry>> BindAllotmentDetails(int? AllotmentId);
        Task<List<Leaseapplication>> BindLeaseApplicationDetails(int? AppId);
        Task<PagedResult<Possesionplan>> GetPagedPossesionPlan(PossesionplanSearchDto model);
        Task<Khasra> FetchSingleKhasraResult(int? khasraId);
        string GetDownload(int id);

    }
}
