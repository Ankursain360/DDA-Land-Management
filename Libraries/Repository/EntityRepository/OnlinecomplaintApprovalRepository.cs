using Dto.Search;
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
   public class OnlinecomplaintApprovalRepository : GenericRepository<Onlinecomplaint>, IOnlinecomplaintApprovalRepository
    {
        public OnlinecomplaintApprovalRepository(DataContext dbContext) : base(dbContext)
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


      

        public async Task<PagedResult<Onlinecomplaint>> GetPagedOnlinecomplaint(OnlinecomplaintApprovalSearchDto model, int userId)
        {
            return  await _dbContext.Onlinecomplaint.
                Include(x => x.Location).Include(x => x.ComplaintType).
               Where(x=>x.IsActive==1 && x.ApprovedStatus==model.StatusId && (model.StatusId==0 ? x.PendingAt== userId : x.PendingAt == 0))
                .OrderByDescending(x => x.Id).GetPaged<Onlinecomplaint>(model.PageNumber, model.PageSize);


        }


    }
}
