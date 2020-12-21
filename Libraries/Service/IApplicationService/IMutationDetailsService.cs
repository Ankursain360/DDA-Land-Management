using Libraries.Model.Entity;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Libraries.Repository.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IMutationDetailsService : IEntityService<Mutationdetails>
    {
        Task<List<Mutationdetails>> GetAllMutationDetails();
        Task<List<Locality>> GetAllLocality(int zoneId);
        Task<List<Zone>> GetAllZone();
        Task<bool> Create(Mutationdetails details);
        Task<Mutationdetails> GetPhotoPropFile(int id);
        
        Task<bool> Update(int id, Mutationdetails details);
        Task<bool> Delete(int id);
        Task<Mutationdetails> SaveMutationAtsFilePath(int id);
        Task<Mutationdetails> SaveMutationGPAFilePath(int id);
        Task<Mutationdetails> SaveMutationMoneyReceiptFilePath(int id);
        Task<Mutationdetails> SaveMutationSignSPCFilePath(int id);
        Task<Mutationdetails> SaveMutationAddressProofFilePath(int id);
        Task<Mutationdetails> SaveMutationAffitDevitFilePath(int id);
        Task<Mutationdetails> SaveMutationIndemnityFilePath(int id);

        Task<bool> SaveMutationOldDamage(Mutationolddamageassesse oldDamage);
        //Task<PagedResult<>> GetPagedEncroachmentRegisteration(EncroachmentRegisterationDto model);



    }
}
