using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IMutationDetailsRepository : IGenericRepository<Mutationdetails>
    {
        Task<List<Mutationdetails>> GetAllMutationDetails();
        Task<List<Locality>> GetAllLocality(int zoneId);
        Task<List<Zone>> GetAllZone();
        Task<bool> Any(int id, string name);
        
    }
}
