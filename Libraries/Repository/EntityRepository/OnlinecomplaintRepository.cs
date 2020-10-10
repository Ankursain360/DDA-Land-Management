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
    public class OnlinecomplaintRepository : GenericRepository<Onlinecomplaint>, IOnlinecomplaintRepository
    {
        public OnlinecomplaintRepository(DataContext dbContext) : base(dbContext)
        {

        }


        public async Task<List<ComplaintType>> GetAllComplaintType()
        {
            List<ComplaintType> ComplaintList = await _dbContext.ComplaintType.Where(x => x.IsActive == 1).ToListAsync();
            return ComplaintList;
        }
        public async Task<List<Location>> GetAllLocation()
        {
            List<Location> LocationList = await _dbContext.Location.Where(x => x.IsActive == 1).ToListAsync();
            return LocationList;
        }


        public async Task<List<Onlinecomplaint>> GetAllOnlinecomplaint()
        {
            return await _dbContext.Onlinecomplaint.Include(x => x.Location).Include(x => x.ComplaintType).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<PagedResult<Onlinecomplaint>> GetPagedOnlinecomplaint(OnlinecomplaintSearchDto model)
        {
            return await _dbContext.Onlinecomplaint.Include(x => x.Location).Include(x => x.ComplaintType).OrderByDescending(x => x.Id).GetPaged<Onlinecomplaint>(model.PageNumber, model.PageSize);
        }



    }
}
