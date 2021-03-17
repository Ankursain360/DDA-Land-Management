using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Repository.Common;
using Dto.Master;

namespace Libraries.Service.IApplicationService
{
    public interface IPossesionplanService
    {
       
        Task<bool> Delete(int id);
       Task<Possesionplan> FetchSingleResult(int id);
        Task<List<Leaseapplication>> BindLeaseApplicationDetails(int? appId)
;
        Task<List<Allotmententry>> BindAllotmentDetails(int? AllotmentId)
;
        Task<List<Allotmententry>> GetAllAllotmententry();
        Task<List<Leaseapplication>> GetAllLeaseApplication()
;
        Task<List<Possesionplan>> GetAllPossesionplan();
        Task<bool> Update(int id, Possesionplan possesionplan);
        Task<bool> Create(Possesionplan possesionplan);
        Task<PagedResult<Possesionplan>> GetPagedPossesionPlan(PossesionplanSearchDto model);
        string GetDownload(int id);



    }

}
