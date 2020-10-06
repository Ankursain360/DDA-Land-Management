using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Repository.IEntityRepository
{
    public interface IEnhancecompensationRepository : IGenericRepository<Enhancecompensation>
    {
        Task<List<Enhancecompensation>> GetAllEnhancecompensation();

        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Khasra>> BindKhasra();

        Task<PagedResult<Enhancecompensation>> GetPagedEnhancecompensation(EnhancecompensationSearchDto model);

    }
}
