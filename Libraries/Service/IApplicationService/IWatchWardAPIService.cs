
using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
    public interface IWatchWardAPIService : IEntityService<Watchandward>
    {
        Task<List<APIGetPrimaryListNoListDto>> GetPrimaryListNoList();
        Task<bool> Create(ApiSaveWatchandwardDto dto); // To Create Particular data 
        Task<bool> SaveWatchandwardphotofiledetails(Watchandwardphotofiledetails item);
        Task<bool> SaveWatchandwardreportfiledetails(Watchandwardreportfiledetails item);
        Task<List<ApiSaveWatchandwardDto>> GetAllWatchandward(ApiWatchWardParmsDto dto);
    }
}
