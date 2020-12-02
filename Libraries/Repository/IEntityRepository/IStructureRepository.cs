using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
     public interface IStructureRepository : IGenericRepository<Structure>
    {

        Task<bool> Any(int id, string name);
        
        Task<List<Structure>> GetAllStructure();
        Task<PagedResult<Structure>> GetPagedStructure(StructureSearchDto model);
    }
}
