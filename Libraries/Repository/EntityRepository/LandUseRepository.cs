using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
    public class LandUseRepository : GenericRepository<Landuse>, ILandUseRepository
    {

        public LandUseRepository(DataContext dbContext) : base(dbContext)
        {

        }


        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Landuse.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }
    }


}
