using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{

    public interface IMutationRepository : IGenericRepository<Mutation>
    {
        int GetLocalityByName(string name);
        int GetKhasraByName(string name);
        Task<bool> Any(int id, string fileNo);
        Task<PagedResult<Mutation>> GetPagedDMSFileUploadList(DemandListDetailsSearchDto model);
        Task<Mutation> FetchSingleResult(int id);
        Task<List<Acquiredlandvillage>> GetVillageList();
        Task<List<Khasra>> GetKhasraList(int id);
    }
}
