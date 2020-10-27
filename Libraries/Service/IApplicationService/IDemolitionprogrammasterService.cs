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




        Task<List<Demolitionprogram>> GetDemolitionprogrammaster();

        Task<List<Demolitionprogram>> GetDemolitionprogrammasterUsingRepo();

        Task<bool> Update(int id, Demolitionprogram demolitionprogrammaster);
        Task<bool> Create(Demolitionprogram demolitionprogrammaster);
        Task<Demolitionprogram> FetchSingleResult(int id);
        Task<bool> Delete(int id);



        Task<PagedResult<Demolitionprogram>> GetPagedDemolitionprogrammaster(DemolitionprogrammasterSearchDto model);




    }
}
