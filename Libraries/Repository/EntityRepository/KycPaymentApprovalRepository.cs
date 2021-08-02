

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

    public class KycPaymentApprovalRepository : GenericRepository<Kycdemandpaymentdetails>, IKycPaymentApprovalRepository
    {
        public KycPaymentApprovalRepository(DataContext dbContext) : base(dbContext)
        {

        }
       
        public async Task<List<Kycworkflowtemplate>> GetWorkFlowDataOnGuid(string processguid)
        {
            return await _dbContext.Kycworkflowtemplate
                                     .Where(x => x.ProcessGuid == processguid
                                     && x.IsActive == 1
                                     )
                                     .OrderByDescending(x => x.Id)
                                     .ToListAsync();
        }
        public async Task<PagedResult<Kycdemandpaymentdetails>> GetPagedKycPaymentDetails(KycPaymentApprovalSearchDto model, int userId)
        {
            var AllDataList = await _dbContext.Kycdemandpaymentdetails.ToListAsync();
            var UserWiseDataList = AllDataList.Where(x => x.PendingAt.Split(',').Contains(userId.ToString()));
            List<int> myIdList = new List<int>();
            foreach (Kycdemandpaymentdetails myLine in UserWiseDataList)
            {
                if (myLine != null)
                {
                    myIdList.Add(myLine.Id);
                }
            }
            int[] myIdArray = myIdList.ToArray();

            var data = await _dbContext.Kycdemandpaymentdetails
                                        .Include(x => x.Kyc)
                                        .Include(x => x.Kyc.PropertyType)
                                        .Include(x => x.Kyc.Branch)
                                        .Include(x => x.Kyc.Zone)
                                        .Include(x => x.Kyc.Locality)
                                        .Include(x => x.ApprovedStatusNavigation)
                                        .Where(x => x.IsActive == 1
                                        && (model.StatusId == 0 ? x.PendingAt != "0" : x.PendingAt == "0")
                                        && (model.StatusId == 0 ? (myIdArray).Contains(x.Id) : x.PendingAt == "0")
                                        && (model.approvalstatusId == 0 ? (x.ApprovedStatus == x.ApprovedStatus) : (x.ApprovedStatus == model.approvalstatusId))
                                        )
                                        .GetPaged<Kycdemandpaymentdetails>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Kycdemandpaymentdetails
                                        .Include(x => x.Kyc)
                                        .Include(x => x.Kyc.PropertyType)
                                        .Include(x => x.Kyc.Branch)
                                        .Include(x => x.Kyc.Zone)
                                        .Include(x => x.Kyc.Locality)
                                        .Include(x => x.ApprovedStatusNavigation)
                                        .Where(x => x.IsActive == 1
                                        && (model.StatusId == 0 ? x.PendingAt != "0" : x.PendingAt == "0")
                                        && (model.StatusId == 0 ? (myIdArray).Contains(x.Id) : x.PendingAt == "0")
                                        && (model.approvalstatusId == 0 ? (x.ApprovedStatus == x.ApprovedStatus) : (x.ApprovedStatus == model.approvalstatusId))
                                        )
                                        .OrderBy(a => a.TotalDues)
                                        .GetPaged<Kycdemandpaymentdetails>(model.PageNumber, model.PageSize);

                                              
                        break;



                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):

                        data = null;
                        data = await _dbContext.Kycdemandpaymentdetails
                                       .Include(x => x.Kyc)
                                       .Include(x => x.Kyc.PropertyType)
                                        .Include(x => x.Kyc.Branch)
                                        .Include(x => x.Kyc.Zone)
                                        .Include(x => x.Kyc.Locality)
                                       .Include(x => x.ApprovedStatusNavigation)
                                       .Where(x => x.IsActive == 1
                                       && (model.StatusId == 0 ? x.PendingAt != "0" : x.PendingAt == "0")
                                       && (model.StatusId == 0 ? (myIdArray).Contains(x.Id) : x.PendingAt == "0")
                                       && (model.approvalstatusId == 0 ? (x.ApprovedStatus == x.ApprovedStatus) : (x.ApprovedStatus == model.approvalstatusId))
                                       )
                                       .OrderByDescending(a => a.TotalDues)
                                       .GetPaged<Kycdemandpaymentdetails>(model.PageNumber, model.PageSize);

                        break;
                }
            }

            return data;
        }

       

        public async Task<Kycdemandpaymentdetails> FetchSingleResult(int id)
        {
            return await _dbContext.Kycdemandpaymentdetails
                                   .Include(x => x.ApprovedStatusNavigation)
                                   .Include(x => x.Kyc)
                                   .Where(x => x.Id == id)
                                   .FirstOrDefaultAsync();
        }

        public async Task<bool> IsApplicationPendingAtUserEnd(int id, int userId)
        {
            var result = false;
            var AllDataList = await _dbContext.Kycdemandpaymentdetails
                                                .Where(x => x.IsActive == 1 && x.Id == id)
                                                .ToListAsync();
            var UserWiseDataList = AllDataList.Where(x => x.PendingAt.Split(',').Contains(userId.ToString())).ToList();

            if (UserWiseDataList.Count == 0)
                result = false;
            else
                result = true;

            return result;
        }

        //********* rpt ! Allottee challan  Details  repeter**********

        public async Task<bool> SaveChallan(Kycdemandpaymentdetailstablec challan)
        {
            _dbContext.Kycdemandpaymentdetailstablec.Add(challan);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<Kycdemandpaymentdetailstablec>> GetAllChallan(int id)
        {
            return await _dbContext.Kycdemandpaymentdetailstablec.Where(x => x.DemandPaymentId == id && x.IsActive == 1).ToListAsync();
        }

        public async Task<bool> DeleteChallan(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Kycdemandpaymentdetailstablec.Where(x => x.DemandPaymentId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
    }
}
