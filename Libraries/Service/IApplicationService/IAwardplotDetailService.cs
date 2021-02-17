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
    public interface IAwardplotDetailService
    {
        Task<List<Awardplotdetails>> GetAwardplotdetails();
        Task<List<Awardmasterdetail>> GetAllAWardmaster();
        Task<List<Khasra>> BindKhasra();
        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Awardplotdetails>> GetAwardplotdetailsUsingRepo();
        Task<bool> Update(int id, Awardplotdetails awardplotdetails);
        Task<bool> Create(Awardplotdetails awardplotdetails);
        Task<Awardplotdetails> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Awardplotdetails>> GetPagedAwardplotdetails(AwardPlotDetailSearchDto model);
        Task<List<AwardReportDtoProfile>> BindAwardNoDateList();
        Task<PagedResult<Awardplotdetails>> GetPagedPossessionReport(AwardReportSearchDto model);
    }
}
