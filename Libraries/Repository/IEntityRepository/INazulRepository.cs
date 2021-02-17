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
        Task<PagedResult<Nazul>> GetPagedNazul(NazulSearchDto model);
        Task<List<Nazul>> GetAllNazul();

       
        Task<List<Acquiredlandvillage>> GetAllVillageList();
        Task<PagedResult<Nazul>> GetNazulReportData(NazulVillageReportSearchDto nazulVillageReportSearchDto);
    }
}
