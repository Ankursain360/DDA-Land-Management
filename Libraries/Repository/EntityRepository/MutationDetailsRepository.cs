using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class MutationDetailsRepository : GenericRepository<Mutationdetails>, IMutationDetailsRepository
    {
        public MutationDetailsRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public Task<bool> Any(int id, string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Locality>> GetAllLocality(int zoneId)
        {
            List<Locality> localityList = await _dbContext.Locality.Where(x => x.ZoneId == zoneId && x.IsActive == 1).ToListAsync();
            return localityList;
        }

        public async Task<List<Mutationdetails>> GetAllMutationDetails()
        {
            var data = await _dbContext.Mutationdetails
                .Include(x => x.Locality)
                .Include(x => x.Zone)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            return data;
        }

        public async Task<List<Zone>> GetAllZone()
        {
            List<Zone> zoneList = await _dbContext.Zone.Where(x => x.IsActive == 1).ToListAsync();
            return zoneList;
        }
    }
}
