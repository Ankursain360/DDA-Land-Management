using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IStructureService : IEntityService<Structure>
    {
        Task<List<Structure>> GetAllStructure();
        Task<List<Structure>> GetStructureUsingRepo();
        Task<bool> Update(int id, Structure structure);
        Task<bool> Create(Structure structure);
        Task<Structure> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<bool> CheckUniqueName(int id, string structure);   // To check Unique Value  
        Task<PagedResult<Structure>> GetPagedStructure(StructureSearchDto model);
       
    }
}
