using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{

    public interface IGroundRentService : IGenericRepository<Groundrent>
    {
        Task<List<Groundrent>> GetAllGroundRent();
        Task<PagedResult<Groundrent>> GetPagedGroundRent(GroundrentSearchDto model);
    }
}
