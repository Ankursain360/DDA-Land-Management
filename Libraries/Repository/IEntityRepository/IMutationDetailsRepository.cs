using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IMutationDetailsRepository : IGenericRepository<Mutationdetails>
    {
        Task<List<Mutationdetails>> GetAllMutationDetails();
        Task<List<Locality>> GetLocalityList();
        Task<List<District>> GetDistrictList();
        Task<bool> Any(int id, string name);


        /* open Single file upload */
        Task<Mutationdetails> SaveMutationAtsFilePath(int id);
        Task<Mutationdetails> SaveMutationGPAFilePath(int id);
        Task<Mutationdetails> SaveMutationMoneyReceiptFilePath(int id);
        Task<Mutationdetails> SaveMutationSignSPCFilePath(int id);
        Task<Mutationdetails> SaveMutationAddressProofFilePath(int id);
        Task<Mutationdetails> SaveMutationAffitDevitFilePath(int id);
        Task<Mutationdetails> SaveMutationIndemnityFilePath(int id);
        Task<Mutationdetails> GetPhotoPropFile(int id);
        /* Close*/

        /*Repeater*/
        Task<bool> SaveMutationOldDamage(Mutationolddamageassesse oldDamage);
        Task<List<Damagepayeeregistertemp>> FetchSingleResult(int id);
        Task<Damagepayeeregistertemp> FetchMutationDetailsUserId(int userId);

    }
}
