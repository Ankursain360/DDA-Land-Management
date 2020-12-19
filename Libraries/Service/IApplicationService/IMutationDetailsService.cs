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
        Task<bool> SaveMutationPhotoPropFile(Mutationdetailsphotoproperty details);
        
        Task<bool> Update(int id, Mutationdetails details);
        Task<bool> Delete(int id);
        string SaveMutationAtsFilePath(int id);
        string SaveMutationGPAFilePath(int id);
        string SaveMutationMoneyReceiptFilePath(int id);
        string SaveMutationSignSPCFilePath(int id);
        string SaveMutationAddressProofFilePath(int id);
        string SaveMutationAffitDevitFilePath(int id);
        string SaveMutationIndemnityFilePath(int id);

        Task<bool> SaveMutationOldDamage(Mutationolddamageassesse oldDamage);
        //Task<PagedResult<>> GetPagedEncroachmentRegisteration(EncroachmentRegisterationDto model);



    }
}
