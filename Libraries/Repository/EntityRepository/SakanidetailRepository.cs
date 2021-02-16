using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class SakanidetailRepository : GenericRepository<Saknidetails>,ISakanidetailRepository
        { 

        public SakanidetailRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<PagedResult<Saknidetails>> GetPagedSakanidetail(SakaniDetailsSearchDto model)
        {
            return await _dbContext.Saknidetails.GetPaged<Saknidetails>(model.PageNumber, model.PageSize);
        }
        public async Task<List<Khewat>> GetAllKhewat()
        {
            List<Khewat> awardList = await _dbContext.Khewat.Where(x => x.IsActive == 1).ToListAsync();
            return awardList;
        }
        public async Task<List<Saknidetails>> GetSakanidetail()
        {
            return await _dbContext.Saknidetails.Include(x => x.Village).Include(x => x.Khasra).OrderByDescending(x => x.Id).ToListAsync();
        }


        public async Task<List<Acquiredlandvillage>> GetAllVillage()
        {
            List<Acquiredlandvillage> villageList = await _dbContext.Acquiredlandvillage.Where(x => x.IsActive == 1).ToListAsync();
            return villageList;
        }



        public async Task<List<Khasra>> BindKhasra()
        {
            List<Khasra> KhasraList = await _dbContext.Khasra.Where(x => x.IsActive == 1).ToListAsync();
            return KhasraList;
        }






    }
}
