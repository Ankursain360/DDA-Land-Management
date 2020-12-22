using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IDamagePayeeRegistrationRepository : IGenericRepository<Payeeregistration>
    {

        //Task<bool> Any(int id, string name);

        Task<List<Payeeregistration>> GetAllPayeeregistration();
        Task<PagedResult<Payeeregistration>> GetPagedDamagePayeeRegistration(DamagePayeeRegistrationSearchDto model);
        Task<bool> Anyemail(int Id, string emailid);
        Task<bool> Any(int id, string name);
    }
}
