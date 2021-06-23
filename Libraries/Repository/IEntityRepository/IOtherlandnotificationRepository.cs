using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Repository.IEntityRepository
{
    public interface IOtherlandnotificationRepository : IGenericRepository<Otherlandnotification>
    {
        Task<PagedResult<Otherlandnotification>> GetPagedOtherlandnotification(OtherlandnotificationSearchDto model);

        Task<List<Otherlandnotification>> GetOtherlandnotification();
    }
}
