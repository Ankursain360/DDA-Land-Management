using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Repository.Common;
namespace Libraries.Service.IApplicationService
{
    public interface IDemolitionchecklistService
    {



        Task<List<Demolitionchecklist>> GetDemolitionchecklist();
      
        Task<List<Demolitionchecklist>> GetDemolitionchecklistUsingRepo();

        Task<bool> Update(int id, Demolitionchecklist demolitionchecklist);
        Task<bool> Create(Demolitionchecklist demolitionchecklist);
        Task<Demolitionchecklist> FetchSingleResult(int id);
        Task<bool> Delete(int id);

     

        Task<PagedResult<Demolitionchecklist>> GetPagedDemolitionchecklist(DemolitionchecklistSearchDto model);

    }
}
