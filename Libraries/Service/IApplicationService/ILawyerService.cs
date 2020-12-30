using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
namespace Libraries.Service.IApplicationService
{
    public interface ILawyerService : IEntityService<Lawyer>
    {

        Task<List<Lawyer>> GetAllLawyer();
        Task<List<Lawyer>> GetLawyerUsingReport();

        Task<bool> Update(int id, Lawyer lawyer);
        Task<bool> Create(Lawyer lawyer);
        Task<Lawyer> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<bool> CheckUniqueName(int id, string lawyer);

        Task<PagedResult<Lawyer>> GetPagedLawyer(LawyerSearchDto model);
    }
}
