using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{
   public interface IOtherlandnotificationService : IEntityService<Otherlandnotification>
    {

        Task<PagedResult<Otherlandnotification>> GetPagedOtherlandnotification(OtherlandnotificationSearchDto model);

        Task<List<Otherlandnotification>> GetOtherlandnotification();
        Task<List<Otherlandnotification>> GetOtherlandnotificationUsingRepo();

        Task<bool> Update(int id, Otherlandnotification otherlandnotification);
        Task<bool> Create(Otherlandnotification otherlandnotification);
        Task<Otherlandnotification> FetchSingleResult(int id);
        Task<bool> Delete(int id);



    }
}
