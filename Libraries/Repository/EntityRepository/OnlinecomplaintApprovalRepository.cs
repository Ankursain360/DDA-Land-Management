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
            var data = await _dbContext.Onlinecomplaint.
                Include(x => x.Location).Include(x => x.ComplaintType).
               Where(x=>x.IsActive==1 && x.ApprovedStatus==model.StatusId && (model.StatusId==0 ? x.PendingAt== userId : x.PendingAt == 0))
                .OrderByDescending(x => x.Id).GetPaged<Onlinecomplaint>(model.PageNumber, model.PageSize);


            int SortOrder = (int)model.orderby;
            if (SortOrder == 1)
            {
                switch (model.colname.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderBy(x => x.Name).ToList();
                        break;
                    case ("CONTACT"):
                        data.Results = data.Results.OrderBy(x => x.Contact).ToList();
                        break;
                    case ("EMAIL"):
                        data.Results = data.Results.OrderBy(x => x.Email).ToList();
                        break;
                    case ("ADDRESS"):
                        data.Results = data.Results.OrderBy(x => x.AddressOfComplaint).ToList();
                        break;

                    case ("COMPLAINTTYPE"):
                        data.Results = data.Results.OrderBy(x => x.ComplaintType.Name).ToList();
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.colname.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderByDescending(x => x.Name).ToList();
                        break;
                    case ("CONTACT"):
                        data.Results = data.Results.OrderByDescending(x => x.Contact).ToList();
                        break;
                    case ("EMAIL"):
                        data.Results = data.Results.OrderByDescending(x => x.Email).ToList();
                        break;
                    case ("ADDRESS"):
                        data.Results = data.Results.OrderByDescending(x => x.AddressOfComplaint).ToList();
                        break;
                    case ("COMPLAINTTYPE"):
                        data.Results = data.Results.OrderByDescending(x => x.ComplaintType.Name).ToList();
                        break;

                }
            }
            return data;




        }


    }
}
