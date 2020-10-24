using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IDemolitiondocumentRepository : IGenericRepository<Demolitiondocument>
    {



        Task<PagedResult<Demolitiondocument>> GetPagedDemolitiondocument(DemolitiondocumentSearchDto model);

        Task<List<Demolitiondocument>> GetDemolitiondocument();

    }
}
