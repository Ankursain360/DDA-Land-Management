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
    public interface IPossessiondetailsService
    {
        Task<List<Khasra>> BindKhasra(int? villageId);
        Task<List<Acquiredlandvillage>> GetAllVillage();

        Task<List<Possessiondetails>> GetAllPossessiondetails();
        Task<List<Possessiondetails>> GetPossessiondetailsUsingRepo();
        Task<bool> Update(int id, Possessiondetails possessiondetails);
        Task<bool> Create(Possessiondetails possessiondetails);
        Task<Possessiondetails> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Possessiondetails>> GetPagedNoPossessiondetails(PossessiondetailsSearchDto model);


        Task<Khasra> FetchSingleKhasraResult(int? khasraId);
        Task<PagedResult<Possessiondetails>> GetPagedPossessionReport(PossessionReportSearchDto model);
        Task<List<PossessionReportDtoProfile>> BindPossessionDateList();
        Task<List<VillageAndKhasraDetailListDto>> GetPagedvillageAndKhasradetailsList(VillageAndKhasraDetailsSearchDto model);
        Task<List<AcquiredLandVillageListSearchDto>> GetPagedKhasraDetails(VillageAndKhasraDetailsSearchDto model);
        Task<List<Possessiondetails>> GetAllPossessionReport(PossessionReportSearchDto model);

    }
}
