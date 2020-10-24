using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Repository.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IDemolitionprogrammasterService
    {




        Task<List<Demolitionprogrammaster>> GetDemolitionprogrammaster();

        Task<List<Demolitionprogrammaster>> GetDemolitionprogrammasterUsingRepo();

        Task<bool> Update(int id, Demolitionprogrammaster demolitionprogrammaster);
        Task<bool> Create(Demolitionprogrammaster demolitionprogrammaster);
        Task<Demolitionprogrammaster> FetchSingleResult(int id);
        Task<bool> Delete(int id);



        Task<PagedResult<Demolitionprogrammaster>> GetPagedDemolitionprogrammaster(DemolitionprogrammasterSearchDto model);




    }
}
