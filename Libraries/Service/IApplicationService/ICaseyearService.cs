using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{
    public interface ICaseyearService : IEntityService<Caseyear>
    {

        Task<List<CaseyearSearchDto>> Getcaseyear();
        Task<PagedResult<Caseyear>> GetPagedcaseyear(CaseyearSearchDto model);
        Task<List<Caseyear>> GetAllcaseyear(); // To Get all data added by renu
        Task<List<Caseyear>> GetcaseyearUsingRepo();

        Task<bool> Update(int id, Caseyear caseyear); // To Upadte Particular data added by renu
        Task<bool> Create(Caseyear caseyear);

        Task<Caseyear> FetchSingleResult(int id);  // To fetch Particular data added by renu

        Task<bool> Delete(int id);    // To Delete Data  added by renu

        Task<bool> CheckUniqueName(int id, string caseyear);   // To check Unique Value  for designation

    }
}
