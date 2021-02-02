using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Model.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.IApplicationService
{
    public interface IUserRightService : IEntityService<Dmsfileright>
    {
        Task<List<Department>> GetDepartmentList();
        Task<PagedResult<Userprofile>> GetPagedUserprofile(UserRightsSearchDto model);
        Task<bool> AddUpdateDmsRight(List<UserRightsMapDto> model);
        Task<List<Dmsfileright>> GetDMSFileRight();

    }
}
