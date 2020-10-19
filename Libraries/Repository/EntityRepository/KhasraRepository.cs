using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
namespace Libraries.Repository.EntityRepository
{
    public class KhasraRepository : GenericRepository<Khasra>, IKhasraRepository
    {
        public KhasraRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Khasra>> GetPagedKhasra(KhasraMasterSearchDto model)
        {
            return await _dbContext.Khasra.Where(x => x.IsActive == 1).Include(x => x.Locality).Include(x => x.LandCategory).GetPaged<Khasra>(model.PageNumber, model.PageSize);
        }



        public async Task<List<LandCategory>> GetAllLandCategory()
        {
            List<LandCategory> landcategoryList = await _dbContext.LandCategory.ToListAsync();
            return landcategoryList;
        }

        public async Task<List<Locality>> GetAllLocalityList()
        {
            List<Locality> localityList = await _dbContext.Locality.ToListAsync();
            return localityList;
        }




        public async Task<List<Khasra>> GetAllKhasra()
        {
            return await _dbContext.Khasra.Include(x => x.LandCategory).Include(x => x.Locality).OrderByDescending(x => x.Id).ToListAsync();
        }






    }
}
