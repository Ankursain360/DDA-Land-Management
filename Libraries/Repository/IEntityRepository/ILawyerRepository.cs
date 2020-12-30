using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public interface ILawyerRepository : IGenericRepository<Lawyer>
    {
        Task<List<Lawyer>> GetLawyer();
        Task<bool> Any(int id, string name);
        Task<PagedResult<Lawyer>> GetPagedLawyer(LawyerSearchDto model);


    }
}
