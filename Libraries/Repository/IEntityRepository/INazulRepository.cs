using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{

    public interface INazulRepository : IGenericRepository<Nazul>
    {
       
        Task<List<Village>> GetAllVillage();
        //Task<bool> Any(int id, string name);
        Task<List<Nazul>> GetAllNazul();
        Task<PagedResult<Nazul>> GetPagedNazul(NazulSearchDto model);
    }
}
