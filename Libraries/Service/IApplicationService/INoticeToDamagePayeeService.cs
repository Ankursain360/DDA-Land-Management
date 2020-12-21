using Libraries.Model.Entity;
using Libraries.Service.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface INoticeToDamagePayeeService : IEntityService<Damagepayeeregister>
    {

        Task<List<Damagepayeeregister>> GetAllDamagepayeeregister(int fileNo);


    }
}
