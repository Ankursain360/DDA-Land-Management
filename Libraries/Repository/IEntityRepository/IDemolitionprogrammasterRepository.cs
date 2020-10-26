using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IDemolitionprogrammasterRepository : IGenericRepository<Demolitionprogram>
    {


        Task<PagedResult<Demolitionprogram>> GetPagedDemolitionprogrammaster(DemolitionprogrammasterSearchDto model);

        Task<List<Demolitionprogram>> GetDemolitionprogrammaster();

    }
}
