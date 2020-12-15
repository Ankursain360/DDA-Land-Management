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
    public interface IDamagePayeeRegistrationService : IEntityService<Payeeregistration>
    {
        Task<List<Payeeregistration>> GetAllPayeeregistration();
        
        Task<bool> Update(int id, Payeeregistration payeeregistration);
        Task<bool> Create(Payeeregistration payeeregistration);
        Task<Payeeregistration> FetchSingleResult(int id);
        Task<bool> Delete(int id);
      
        Task<PagedResult<Payeeregistration>> GetPagedDamagePayeeRegistration(DamagePayeeRegistrationSearchDto model);

    }
}
