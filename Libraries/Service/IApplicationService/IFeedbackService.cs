using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IFeedbackService : IEntityService<tblfeedback>
    {
        Task<List<tblfeedback>> GetTblfeedbacks();
        Task<tblfeedback> GetSingleResult(int id);
        Task<bool> Any(int id, string name);
        Task<PagedResult<tblfeedback>> GetPagedResult(FeedbackSearchDto model);
        Task<bool> Create(tblfeedback tblfeedback);
    }
}
