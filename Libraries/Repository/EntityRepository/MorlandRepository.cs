using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
namespace Libraries.Repository.EntityRepository
{
    public class MorlandRepository : GenericRepository<Morland>, IMorlandRepository
    {
        public MorlandRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<LandNotification>> GetAllLandNotification()
        {
            List<LandNotification> notificationList = await _dbContext.LandNotification.ToListAsync();
            return notificationList;
        }

        public async Task<List<Serialnumber>> GetAllSerialnumber()
        {
            List<Serialnumber> serialnumberlist = await _dbContext.Serialnumber.ToListAsync();
            return serialnumberlist;
        }




        public async Task<List<Morland>> GetAllMorland()
        {
            return await _dbContext.Morland.Include(x => x.LandNotification).Include(x => x.Serialnumber).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<PagedResult<Morland>> GetPagedMorland(MorLandsSearchDto model)
        {
            return await _dbContext.Morland.GetPaged<Morland>(model.PageNumber, model.PageSize);
        }




    }
}
