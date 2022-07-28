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
    public interface INewdamagepayeeregistrationService : IEntityService<Newdamagepayeeregistration>
    {
        Task<List<Newdamagepayeeregistration>> GetAllDamagePayee();
        Task<List<District>> GetAllDistrict();
        Task<List<Approvalstatus>> GetAllApprovalStatus();
        Task<List<Acquiredlandvillage>> GetAllVillage(int districtId);
        Task<List<New_Damage_Colony>> GetAllColony(int villageId);
        Task<PagedResult<Newdamagepayeeregistration>> GetPagedDamagePayee(DamagePayeeSearchDto model);
      //  Task<bool> Any(int id, string name);
    }
}
