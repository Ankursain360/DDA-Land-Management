using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{
    public interface ICasenatureService : IEntityService<Casenature>
    {
        Task<List<CasenatureSearchDto>> Getcasenature();
        Task<PagedResult<Casenature>> GetPagedcasenature(CasenatureSearchDto model);
        Task<List<Casenature>> GetAllcasenature(); // To Get all data added by renu
        Task<List<Casenature>> GetcasenatureUsingRepo();

        Task<bool> Update(int id, Casenature casenature); // To Upadte Particular data added by renu
        Task<bool> Create(Casenature casenature);

        Task<Casenature> FetchSingleResult(int id);  // To fetch Particular data added by renu

        Task<bool> Delete(int id);    // To Delete Data  added by renu
        
            Task<bool> CheckUniqueName(int id, string casenature);   // To check Unique Value  for designation
       
    }
}
