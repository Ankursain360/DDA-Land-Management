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

        //  Task<List<Damagepayeeregistertemp>> GetAllDamagepayeeregister(string fileNo);
        //  Task<List<Damagepayeeregistertemp>> Getpersonelinfotemp(int Id);
        Task<List<Noticetodamagepayee>> GetAllNoticetoDamagePayee();

        Task<bool> Create(Noticetodamagepayee noticetodamagepayee);

        Task<List<Noticetodamagepayee>> GetsingleData(int id);
        Task<PagedResult<Noticetodamagepayee>> GetPagedNoticetodamagepayee(NoticetodamagepayeeSearchDto model);

        Task<List<Noticetodamagepayee>> GetFileNoList();



    }
}
