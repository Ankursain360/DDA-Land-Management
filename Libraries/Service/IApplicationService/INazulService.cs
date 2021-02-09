using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface INazulService
    {

        
        Task<List<Village>> GetAllVillage();
        Task<List<Nazul>> GetNazulUsingRepo();
        Task<List<Nazul>> GetAllNazul();

        Task<bool> Update(int id, Nazul nazul);
        Task<bool> Create(Nazul nazul);
        Task<Nazul> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Nazul>> GetPagedNazul(NazulSearchDto model);





    }
}