using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Repository.IEntityRepository
{
    public interface IUndersection6plotRepository: IGenericRepository<Undersection6plot>
    {

        Task<List<Undersection6plot>> GetAllUndersection6Plot();
        Task<List<Undersection6>> GetAllNotificationNo();
        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Khasra>> BindKhasra(int? villageId);
        Task<Khasra> FetchSingleKhasraResult(int? khasraId);
        Task<PagedResult<Undersection6plot>> GetPagedNoUndersection6plot(NotificationUndersection6plotDto model);
        Task<List<Unotification6detailsListDto>> GetPagednotification6detailsList(Unotification6detailsSearchDto model);

    }
}
