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
        //Task<Mutationdetails> GetPhotoPropFile(int id);
        
        Task<bool> Update(int id, Mutationdetailstemp details);
        Task<bool> Delete(int id);
       
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
