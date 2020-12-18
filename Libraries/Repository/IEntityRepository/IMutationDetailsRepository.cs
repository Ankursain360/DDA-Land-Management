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
        Task<List<Locality>> GetAllLocality(int zoneId);
        Task<List<Zone>> GetAllZone();
        Task<bool> Any(int id, string name);
        Task<bool> SaveMutationPhotoPropFile(Mutationdetailsphotoproperty details);

        /* open Single file upload */
        string SaveMutationAtsFilePath(int id);
        string SaveMutationGPAFilePath(int id);
        string SaveMutationMoneyReceiptFilePath(int id);
        string SaveMutationSignSPCFilePath(int id);
        string SaveMutationAddressProofFilePath(int id);
        string SaveMutationAffitDevitFilePath(int id);
        string SaveMutationIndemnityFilePath(int id);
        /* Close*/

        /*Repeater*/
        Task<bool> SaveMutationOldDamage(Mutationolddamageassesse oldDamage);

    }
}
