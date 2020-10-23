using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
  

    public interface IDemolitionchecklistRepository : IGenericRepository<Demolitionchecklist>
    {



      
      
        Task<PagedResult<Demolitionchecklist>> GetPagedDemolitionchecklist(DemolitionchecklistSearchDto model);

        Task<List<Demolitionchecklist>> GetDemolitionchecklist();
    }



}
