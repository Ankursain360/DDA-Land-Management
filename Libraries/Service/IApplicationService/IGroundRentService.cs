using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{

    public interface IGroundRentService : IEntityService<Groundrent>
    {
        Task<List<Groundrent>> GetAllGroundRent();
        Task<List<PropertyType>> GetAllPropertyTypeList();
        Task<bool> Update(int id, Groundrent rent);
        Task<bool> Create(Groundrent rate);
        Task<Groundrent> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Groundrent>> GetPagedGroundRent(GroundrentSearchDto model);

    }
}
