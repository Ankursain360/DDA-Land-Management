using System;
using System.Collections.Generic;
using System.Text;



using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Model.Entity;

namespace Libraries.Service.IApplicationService
{
    public interface IEncroachmentRegisterAPIService : IEntityService<EncroachmentRegisteration>
    {
        // Task<List<APIGetPrimaryListNoListDto>> GetPrimaryListNoList();
        // Task<bool> Create(ApiSaveWatchandwardDto dto); // To Create Particular data 
        //Task<bool> SaveWatchandwardphotofiledetails(Watchandwardphotofiledetails item);
        //Task<bool> SaveWatchandwardreportfiledetails(Watchandwardreportfiledetails item);
        //Task<List<ApiSaveWatchandwardDto>> GetAllWatchandward(ApiWatchWardParmsDto dto);
        //Task<Userprofile> GetUserOngivenUserId(int userId);
        //Task<bool> UpdateBeforeApproval(ApiSaveWatchandwardDto dto);
        Task<Zone> GetZonecode(int? zoneId);
        Task<List<APIGetDepartmentListDto>> GetDepartmentDropDownList();
        Task<List<ApiGetZoneListDto>> GetZoneDropDownList(int departmentId);
        Task<List<ApiGetDivisionListDto>> GetDivisionDropDownList(int zoneId);
        Task<List<ApiGetLocalityListDto>> GetLocalityDropDownList(int divisionId);
        Task<List<APIGetKhasraListDto>> GetKhasraDropDownList();
        Task<bool> Create(ApiSaveEncroachmentRegisterDto dto);
    }
}

