using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public interface IZoneRepository : IGenericRepository<Zone>
    {
        Task<List<Zone>> GetZone();
    }
}