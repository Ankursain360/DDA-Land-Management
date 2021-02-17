using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IMutationService : IEntityService<Mutation>
    {
        Task<List<Acquiredlandvillage>> GetVillageList();
        Task<List<Khasra>> GetKhasraList(int id);
        Task<PagedResult<Mutation>> GetPagedDMSFileUploadList(DemandListDetailsSearchDto model);
        Task<bool> Create(Mutation mutation);
        Task<Mutation> FetchSingleResult(int id);
        Task<bool> Update(int id, Mutation mutation);
        Task<bool> Delete(int id, int userId);
        Task<bool> CheckUniqueName(int id, string fileNo);
        Task<List<Mutationparticulars>> GetMutationParticulars(int id);
        Task<bool> SaveMutationParticulars(List<Mutationparticulars> mutationparticulars);
        Task<bool> DeleteMutationParticulars(int id);
    }
}
