
using System;
using System.Collections.Generic;

using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Model.Entity;

namespace Libraries.Repository.IEntityRepository
{
    public interface IEncroachmentRegisterAPIRepository : IGenericRepository<EncroachmentRegisteration>
    {
        
        Task<List<APIGetDepartmentListDto>> GetDepartmentDropDownList();
        Task<List<ApiGetZoneListDto>> GetZoneDropDownList(int departmentId);
        Task<List<ApiGetDivisionListDto>> GetDivisionDropDownList(int zoneId);
        Task<List<ApiGetLocalityListDto>> GetLocalityDropDownList(int divisionId);
        Task<List<APIGetKhasraListDto>> GetKhasraDropDownList();
    }
}