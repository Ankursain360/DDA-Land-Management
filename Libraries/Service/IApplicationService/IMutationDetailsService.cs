using Libraries.Model.Entity;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Libraries.Repository.Common;
using Dto.Search;

namespace Libraries.Service.IApplicationService
{
    public interface IMutationDetailsService : IEntityService<Mutationdetailstemp>
    {
        Task<List<Mutationdetails>> GetAllMutationDetails();
        Task<List<Locality>> GetLocalityList();
        Task<List<District>> GetDistrictList();
        Task<bool> Create(Mutationdetailstemp details);
        Task<Mutationdetails> GetPhotoPropFile(int id);
        
        Task<bool> Update(int id, Mutationdetailstemp details);
        Task<bool> Delete(int id);
        Task<Mutationdetails> SaveMutationAtsFilePath(int id);
        Task<Mutationdetails> SaveMutationGPAFilePath(int id);
        Task<Mutationdetails> SaveMutationMoneyReceiptFilePath(int id);
        Task<Mutationdetails> SaveMutationSignSPCFilePath(int id);
        Task<Mutationdetails> SaveMutationAddressProofFilePath(int id);
        Task<Mutationdetails> SaveMutationAffitDevitFilePath(int id);
        Task<Mutationdetails> SaveMutationIndemnityFilePath(int id);

        Task<bool> SaveMutationOldDamage(Mutationolddamageassesse oldDamage);

        Task<Damagepayeeregister> FetchMutationDetailsUserId(int Id);
        Task<PagedResult<Damagepayeeregister>> GetPagedSubsitutionMutationDetails(SubstitutionMutationDetailsDto model);
        Task<List<Damagepayeepersonelinfo>> GetPersonalInfo(int id);
        Task<List<Allottetype>> GetAllottetype(int id);
    }
}
