
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Repository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{

    public class KycformApprovalRepository : GenericRepository<Kycform>, IKycformApprovalRepository
    {
        public KycformApprovalRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Kycform>> GetPagedKycFormDetails(KycFormApprovalSearchDto model, int userId,int? BranchId)
        {
            var AllDataList = await _dbContext.Kycform.ToListAsync();
            var UserWiseDataList = AllDataList.Where(x => x.PendingAt.Split(',').Contains(userId.ToString()));
            List<int> myIdList = new List<int>();
            foreach (Kycform myLine in UserWiseDataList)
            {
                if (myLine != null)
                {
                    myIdList.Add(myLine.Id);
                }
            }
            int[] myIdArray = myIdList.ToArray();

            var data = await _dbContext.Kycform
                                        .Include(x => x.ApprovedStatusNavigation)
                                        .Include(x => x.Branch)
                                        .Include(x => x.LeaseType)
                                        .Include(x => x.Locality)
                                        .Include(x => x.PropertyType)
                                        .Include(x => x.Zone)
                                        .Where(x => x.IsActive == 1 && x.BranchId == BranchId
                                        && (model.StatusId == 0 ? x.PendingAt != "0" : x.PendingAt == "0")
                                        && (model.StatusId == 0 ? (myIdArray).Contains(x.Id) : x.PendingAt == "0")
                                        && (model.approvalstatusId == 0 ? (x.ApprovedStatus == x.ApprovedStatus) : (x.ApprovedStatus == model.approvalstatusId))
                                        )
                                        .GetPaged<Kycform>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Kycform
                                                .Include(x => x.ApprovedStatusNavigation)
                                                .Include(x => x.Branch)
                                                .Include(x => x.LeaseType)
                                                .Include(x => x.Locality)
                                                .Include(x => x.PropertyType)
                                                .Include(x => x.Zone)
                                                .Where(x => x.IsActive == 1 && x.BranchId == BranchId
                                                && (model.StatusId == 0 ? x.PendingAt != "0" : x.PendingAt == "0")
                                                && (model.StatusId == 0 ? (myIdArray).Contains(x.Id) : x.PendingAt == "0")
                                                && (model.approvalstatusId == 0 ? (x.ApprovedStatus == x.ApprovedStatus) : (x.ApprovedStatus == model.approvalstatusId))
                                                )
                                                .OrderBy(a => a.CreatedDate)

                                                .GetPaged<Kycform>(model.PageNumber, model.PageSize);
                        break;
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Kycform
                                                .Include(x => x.ApprovedStatusNavigation)
                                                .Include(x => x.Branch)
                                                .Include(x => x.LeaseType)
                                                .Include(x => x.Locality)
                                                .Include(x => x.PropertyType)
                                                .Include(x => x.Zone)
                                                .Where(x => x.IsActive == 1 && x.BranchId == BranchId
                                                && (model.StatusId == 0 ? x.PendingAt != "0" : x.PendingAt == "0")
                                                && (model.StatusId == 0 ? (myIdArray).Contains(x.Id) : x.PendingAt == "0")
                                                && (model.approvalstatusId == 0 ? (x.ApprovedStatus == x.ApprovedStatus) : (x.ApprovedStatus == model.approvalstatusId))
                                                )
                                                .OrderBy(a => a.Property)
                                              
                                                .GetPaged<Kycform>(model.PageNumber, model.PageSize);
                        break;



                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DATE"):

                        data = null;
                        data = await _dbContext.Kycform
                                                .Include(x => x.ApprovedStatusNavigation)
                                                .Include(x => x.Branch)
                                                .Include(x => x.LeaseType)
                                                .Include(x => x.Locality)
                                                .Include(x => x.PropertyType)
                                                .Include(x => x.Zone)
                                                .Where(x => x.IsActive == 1 && x.BranchId == BranchId
                                                && (model.StatusId == 0 ? x.PendingAt != "0" : x.PendingAt == "0")
                                                && (model.StatusId == 0 ? (myIdArray).Contains(x.Id) : x.PendingAt == "0")
                                                && (model.approvalstatusId == 0 ? (x.ApprovedStatus == x.ApprovedStatus) : (x.ApprovedStatus == model.approvalstatusId))
                                                )
                                                .OrderByDescending(x => x.CreatedDate)

                                               .GetPaged<Kycform>(model.PageNumber, model.PageSize);
                        break;
                    case ("NAME"):

                        data = null;
                        data = await _dbContext.Kycform
                                                .Include(x => x.ApprovedStatusNavigation)
                                                .Include(x => x.Branch)
                                                .Include(x => x.LeaseType)
                                                .Include(x => x.Locality)
                                                .Include(x => x.PropertyType)
                                                .Include(x => x.Zone)
                                                .Where(x => x.IsActive == 1 && x.BranchId == BranchId
                                                && (model.StatusId == 0 ? x.PendingAt != "0" : x.PendingAt == "0")
                                                && (model.StatusId == 0 ? (myIdArray).Contains(x.Id) : x.PendingAt == "0")
                                                && (model.approvalstatusId == 0 ? (x.ApprovedStatus == x.ApprovedStatus) : (x.ApprovedStatus == model.approvalstatusId))
                                                )
                                                .OrderByDescending(x => x.Property)
                                             
                                               .GetPaged<Kycform>(model.PageNumber, model.PageSize);
                        break;
                }
            }

                return data;
            }

        public async Task<Kycform> FetchSingleResult(int id)
        {
              return await _dbContext.Kycform
                                     .Include(x => x.ApprovedStatusNavigation)
                                     .Include(x => x.Branch)
                                     .Include(x => x.LeaseType)
                                     .Include(x => x.Locality)
                                     .Include(x => x.PropertyType)
                                     .Include(x => x.Zone)
                                     .Where(x => x.Id == id)
                                     .FirstOrDefaultAsync();
        }

        public async Task<Kycworkflowtemplate> FetchSingleResultOnProcessGuidWithVersion(string processguid, string version)
        {
            return await _dbContext.Kycworkflowtemplate
                                   .Where(x => x.ProcessGuid == processguid
                                   && x.Version == version
                                   && x.IsActive == 1
                                   )
                                   .OrderByDescending(x => x.Id)
                                   .Take(1)
                                   .FirstOrDefaultAsync();
        }

        public async Task<Kycworkflowtemplate> FetchSingleResultOnProcessGuid(string processguid)
        {
            return await _dbContext.Kycworkflowtemplate
                                    .Where(x => x.ProcessGuid == processguid && x.EffectiveDate <= DateTime.Now
                                    && x.IsActive == 1
                                    )
                                    .OrderByDescending(x => x.Id)
                                    .Take(1)
                                    .FirstOrDefaultAsync();
        }
        public async Task<bool> IsApplicationPendingAtUserEnd(int id, int userId)
        {
            var result = false;
            var AllDataList = await _dbContext.Kycform
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
