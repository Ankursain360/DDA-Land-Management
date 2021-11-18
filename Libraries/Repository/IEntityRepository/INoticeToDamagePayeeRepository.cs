using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface INoticeToDamagePayeeRepository : IGenericRepository<Noticetodamagepayee>
    {
        // Task<List<Damagepayeeregistertemp>> GetAllDamagepayeeregister(string fileNo);
        // Task<List<Damagepayeeregistertemp>> Getpersonelinfotemp(int Id);
        Task<PagedResult<Noticetodamagepayee>> GetPagedNoticetodamagepayee(NoticetodamagepayeeSearchDto model);
        Task<PagedResult<Noticetodamagepayee>> GetPagedNoticeGenerationReport(NoticeGenerationReportSearchDto model);


        Task<List<Noticetodamagepayee>> GetFileNoList();
        Task<List<Noticetodamagepayee>> GetAllNoticetoDamagePayee();

  

        decimal GetRebateCharges();
    }
}
