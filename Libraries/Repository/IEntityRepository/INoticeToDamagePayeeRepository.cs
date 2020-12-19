using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface INoticeToDamagePayeeRepository : IGenericRepository<Damagepayeeregister>
    {
        Task<List<Damagepayeeregister>> GetAllDamagepayeeregister(int fileNo);


    }
}
