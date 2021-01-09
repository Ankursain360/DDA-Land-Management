using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IMutationDetailsRepository : IGenericRepository<Mutationdetailstemp>
    {
        Task<List<Mutationdetails>> GetAllMutationDetails();
        Task<List<Locality>> GetLocalityList();
        Task<List<District>> GetDistrictList();
        Task<bool> Any(int id, string name);
        //Task AddMutationDetails(Mutationdetailstemp details);


        /* open Single file upload */
        //Task<Mutationdetails> SaveMutationAtsFilePath(int id);
        //Task<Mutationdetails> SaveMutationGPAFilePath(int id);
        //Task<Mutationdetails> SaveMutationMoneyReceiptFilePath(int id);
        //Task<Mutationdetails> SaveMutationSignSPCFilePath(int id);
        //Task<Mutationdetails> SaveMutationAddressProofFilePath(int id);
        //Task<Mutationdetails> SaveMutationAffitDevitFilePath(int id);
        //Task<Mutationdetails> SaveMutationIndemnityFilePath(int id);
        //Task<Mutationdetails> GetPhotoPropFile(int id);
        /* Close*/

        /*Repeater*/
        //Task<bool> SaveMutationOldDamage(Mutationolddamageassesse oldDamage);
        Task<List<Damagepayeeregistertemp>> FetchSingleResult(int id);
        Task<Damagepayeeregister> FetchDamageResult(int Id);
        Task<PagedResult<Damagepayeeregister>> GetPagedSubsitutionMutationDetails(SubstitutionMutationDetailsDto model);
        Task<List<Damagepayeepersonelinfo>> GetPersonalInfo(int id);
        Task<List<Allottetype>> GetAllottetype(int id);
        Task<Mutationdetailstemp> FetchMutationSingleResult(int id);
        Task<Mutationdetailstemp> FetchSingleResultMutationId(int id);
        Task<Damagepayeepersonelinfo> GetPersonelInfoFile(int id);
        Task<Allottetype> GetAlloteeTypeFile(int id);
    }
}
