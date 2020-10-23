using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Repository.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IDemolitiondocumentService
    {



        Task<List<Demolitiondocument>> GetDemolitiondocument();

        Task<List<Demolitiondocument>> GetDemolitiondocumentUsingRepo();

        Task<bool> Update(int id, Demolitiondocument demolitiondocument);
        Task<bool> Create(Demolitiondocument demolitiondocument);
        Task<Demolitiondocument> FetchSingleResult(int id);
        Task<bool> Delete(int id);



        Task<PagedResult<Demolitiondocument>> GetPagedDemolitiondocument(DemolitiondocumentSearchDto model);



    }
}
