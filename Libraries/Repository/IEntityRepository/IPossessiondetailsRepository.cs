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
    public interface IPossessiondetailsRepository : IGenericRepository<Possessiondetails>
    {

        Task<List<Possessiondetails>> GetAllPossessiondetails();
        Task<List<Possessiondetails>> GetAllNoPossessiondetailsList(PossessiondetailsSearchDto model);
        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Khasra>> BindKhasra(int? villageId);
        Task<Khasra> FetchSingleKhasraResult(int? khasraId);
        Task<PagedResult<Possessiondetails>> GetPagedNoPossessiondetails(PossessiondetailsSearchDto model);
        Task<PagedResult<Possessiondetails>> GetPagedPossessionReport(PossessionReportSearchDto model);
        Task<List<PossessionReportDtoProfile>> BindPossessionDateList();
        Task<List<VillageAndKhasraDetailListDto>> GetPagedvillageAndKhasradetailsList(VillageAndKhasraDetailsSearchDto model);

        Task<List<AcquiredLandVillageListSearchDto>> GetPagedKhasraDetails(VillageAndKhasraDetailsSearchDto model);
        Task<List<Possessiondetails>> GetAllPossessionReport(PossessionReportSearchDto model); 
    }
}
