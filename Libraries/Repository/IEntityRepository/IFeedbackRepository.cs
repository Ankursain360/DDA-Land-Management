using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IFeedbackRepository : IGenericRepository<tblfeedback>
    {
        Task<List<tblfeedback>> GetTblfeedbacks();
        Task<bool> Any(int id, string name);
        Task<PagedResult<tblfeedback>> GetPagedResult(FeedbackSearchDto model);
    } 
} 
