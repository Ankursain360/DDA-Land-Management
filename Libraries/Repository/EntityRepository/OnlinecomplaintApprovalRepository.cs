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
            var AllDataList = await _dbContext.Onlinecomplaint.ToListAsync();
            var UserWiseDataList = AllDataList.Where(x => x.PendingAt.Split(',').Contains(userId.ToString()));
            List<int> myIdList = new List<int>();
            foreach (Onlinecomplaint myLine in UserWiseDataList)
                myIdList.Add(myLine.Id);
            int[] myIdArray = myIdList.ToArray();

            var data = await _dbContext.Onlinecomplaint
                                        .Include(x => x.Location)
                                        .Include(x => x.ComplaintType)
                                        .Include(x => x.ApprovedStatusNavigation)
                                        .Where(x=>x.IsActive==1 
                                        && (model.StatusId == 0 ? x.PendingAt != "0" : x.PendingAt == "0")
                                        && (model.StatusId == 0 ? (myIdArray).Contains(x.Id) : x.PendingAt == "0")
                                        )
                                        .OrderByDescending(x => x.Id).GetPaged<Onlinecomplaint>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                data = await _dbContext.Onlinecomplaint
                                        .Include(x => x.Location)
                                        .Include(x => x.ComplaintType)
                                        .Include(x => x.ApprovedStatusNavigation)
                                        .Where(x => x.IsActive == 1 
                                        && (model.StatusId == 0 ? x.PendingAt != "0" : x.PendingAt == "0")
                                        && (model.StatusId == 0 ? (myIdArray).Contains(x.Id) : x.PendingAt == "0")
                                        )
                                       .OrderBy(s =>
                                       (model.SortBy.ToUpper() == "NAME" ? s.Name
                                       : model.SortBy.ToUpper() == "CONTACT" ? s.Contact
                                       : model.SortBy.ToUpper() == "EMAIL" ? s.Email
                                       : model.SortBy.ToUpper() == "ADDRESS" ? s.AddressOfComplaint
                                       : model.SortBy.ToUpper() == "COMPLAINTTYPE" ? (s.ComplaintType == null ? null :s.ComplaintType.Name)
                                       : s.Name)
                                       )
                                       .OrderByDescending(x => x.Id).GetPaged<Onlinecomplaint>(model.PageNumber, model.PageSize);


            }
            else if (SortOrder == 2)
            {
                data = null;
                data = await _dbContext.Onlinecomplaint
                                        .Include(x => x.Location)
                                        .Include(x => x.ComplaintType)
                                        .Include(x => x.ApprovedStatusNavigation)
                                        .Where(x => x.IsActive == 1 
                                        && (model.StatusId == 0 ? x.PendingAt != "0" : x.PendingAt == "0")
                                        && (model.StatusId == 0 ? (myIdArray).Contains(x.Id) : x.PendingAt == "0")
                                        )
                                       .OrderByDescending(s =>
                                       (model.SortBy.ToUpper() == "NAME" ? s.Name
                                       : model.SortBy.ToUpper() == "CONTACT" ? s.Contact
                                       : model.SortBy.ToUpper() == "EMAIL" ? s.Email
                                       : model.SortBy.ToUpper() == "ADDRESS" ? s.AddressOfComplaint
                                       : model.SortBy.ToUpper() == "COMPLAINTTYPE" ? (s.ComplaintType == null ? null : s.ComplaintType.Name)
                                       : s.Name)
                                       )
                                       .OrderByDescending(x => x.Id).GetPaged<Onlinecomplaint>(model.PageNumber, model.PageSize);

            }

            return data;
           
        }
        public async Task<bool> IsApplicationPendingAtUserEnd(int id, int userId)
        {
            var result = false;
            var AllDataList = await _dbContext.Onlinecomplaint
                                                .Where(x => x.IsActive == 1 && x.Id == id)
                                                .ToListAsync();
            var UserWiseDataList = AllDataList.Where(x => x.PendingAt.Split(',').Contains(userId.ToString())).ToList();

            if (UserWiseDataList.Count == 0)
                result = false;
            else
                result = true;

            return result;
        }

    }
}
