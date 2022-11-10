
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;


namespace Libraries.Repository.IEntityRepository
{
    public interface IUndersection17plotdetailRepository : IGenericRepository<Undersection17plotdetail>
    {
        Task<PagedResult<Undersection17plotdetail>> GetPagedUndersection17plotdetail(Undersection17plotdetailSearchDto model);
        Task<List<Undersection17plotdetail>> GetAllUndersection17plotdetail();
        Task<List<Undersection17plotdetail>> GetAllUndersection17plotdetailList(Undersection17plotdetailSearchDto model);

        Task<List<Acquiredlandvillage>> GetAllVillageList();
        Task<List<Khasra>> BindKhasra(int? villageId);
        Task<Khasra> FetchSingleKhasraResult(int? khasraId);
        Task<List<Undersection17>> GetAllUndersection17List();

       
        Task<List<Unotification17detailsListDto>> GetPagednotification17detailsList(Unotification17detailsSearchDto model);
        Task<PagedResult<Undersection17plotdetail>> GetAllNotificationList(NotificationList17SearchDto model);

    }
}
