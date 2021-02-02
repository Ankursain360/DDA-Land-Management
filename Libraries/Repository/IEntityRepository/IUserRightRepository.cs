using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Model.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.IEntityRepository
{
    public interface IUserRightRepository : IGenericRepository<Dmsfileright>
    {
        Task<List<Department>> GetDepartmentList();

        Task<PagedResult<Userprofile>> GetPagedUserprofile(UserRightsSearchDto model);

        Task<List<Dmsfileright>> GetDMSFileRight();

    }
}
