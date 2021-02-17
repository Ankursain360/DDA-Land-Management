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
    public interface IAwardplotDetailsRepository : IGenericRepository<Awardplotdetails>
    {
        Task<List<Awardplotdetails>> GetAwardplotdetails();
        Task<List<Awardmasterdetail>> GetAllAWardmaster();
        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Khasra>> BindKhasra();
        Task<PagedResult<Awardplotdetails>> GetPagedAwardplotdetails(AwardPlotDetailSearchDto model);
        Task<List<AwardReportDtoProfile>> BindAwardNoDateList();
        Task<PagedResult<Awardplotdetails>> GetPagedAwardReport(AwardReportSearchDto model);
    }
}
