using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface INoticeToDamagePayeeService : IEntityService<Noticetodamagepayee>
    {

        Task<List<Noticetodamagepayee>> GetAllNoticetoDamagePayee();

        Task<bool> Create(Noticetodamagepayee noticetodamagepayee);

        Task<List<Noticetodamagepayee>> GetsingleData(int id);
        Task<PagedResult<Noticetodamagepayee>> GetPagedNoticetodamagepayee(NoticetodamagepayeeSearchDto model);

        Task<List<Noticetodamagepayee>> GetFileNoList();
        Task<bool> Update(int id, Noticetodamagepayee noticetodamagepayee);
        Task<Noticetodamagepayee> FetchSingleResult(int id);

        Task<PagedResult<Noticetodamagepayee>> GetPagedNoticeGenerationReport(NoticeGenerationReportSearchDto model);



    }
}
