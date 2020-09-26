using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;


namespace Libraries.Repository.IEntityRepository
{
    public interface IDesignationRepository : IGenericRepository<Designation>
    {
        Task<PagedResult<Designation>> GetPagedDesignation(DesignationSearchDto model);

        Task<bool> Any(int id, string name);
    }
}