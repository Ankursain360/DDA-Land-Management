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
using Repository.Common;


namespace Libraries.Repository.EntityRepository
{
    public class ApprovalProccessRepository : GenericRepository<Approvalproccess>, IApprovalProccessRepository
    {

        public ApprovalProccessRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<ApprovalHistoryListDataDto>> GetHistoryDetails(string proccessguid, int id)
        {
            try
            {
                var data = await _dbContext.LoadStoredProcedure("ApprovalHistoryDetails")
                                            .WithSqlParams(("P_ProccessGuid", proccessguid), ("P_ServiceId", id)
                                            )
                                            .ExecuteStoredProcedureAsync<ApprovalHistoryListDataDto>();

                return (List<ApprovalHistoryListDataDto>)data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int GetPreviousApprovalId(string proccessguid, int serviceid)
        {
            var Id = (from f in _dbContext.Approvalproccess
                      where f.ProcessGuid == proccessguid && f.ServiceId == serviceid
                      orderby f.Id descending
                      select f.Id).First();

            return Id;
        }
        public int GetPreviouskycApprovalId(string proccessguid, int serviceid) //added by ishu 22/7/2021
        {
            var Id = (from f in _dbContext.Kycapprovalproccess
                      where f.ProcessGuid == proccessguid && f.ServiceId == serviceid
                      orderby f.Id descending
                      select f.Id).First();

            return Id;
        }

        
        public async Task<Kycapprovalproccess> kycFindBy(int previousApprovalId) //added by ishu 22 jul 2021
        {
            return await _dbContext.Kycapprovalproccess
                                    .Where(a => a.Id == previousApprovalId)
                                    .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdatePreviouskycApprovalProccess(int previousApprovalId, Kycapprovalproccess approvalproccess, int userId)//added by ishu 22 jul 2021
        {
            var result = await kycFindBy(previousApprovalId);
            Kycapprovalproccess model = result;
            model.PendingStatus = approvalproccess.PendingStatus;
            model.ModifiedBy = userId;
            model.ModifiedDate = DateTime.Now;

            _dbContext.Kycapprovalproccess.Update(model);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public int CheckIsApprovalStart(string proccessguid, int serviceid)
        {
            var File = (from f in _dbContext.Approvalproccess
                        where f.ProcessGuid == proccessguid && f.ServiceId == serviceid
                        orderby f.Id descending
                        select f.Id).Count();

            return File;
        }

        public async Task<Approvalproccess> FetchApprovalProcessDocumentDetails(int id)
        {
            return await _dbContext.Approvalproccess
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
        }
        public async Task<Kycapprovalproccess> FetchKYCApprovalProcessDocumentDetails(int id)//added by ishu 22/7//2021
        {
            return await _dbContext.Kycapprovalproccess
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
        }
        
        public async Task<List<Approvalstatus>> BindDropdownApprovalStatus(int[] actions)
        {
            return await _dbContext.Approvalstatus
                                     .Where(x => x.IsActive == 1 && actions.Contains(x.Id))
                                     .ToListAsync();
        }

        public async Task<Approvalstatus> FetchSingleApprovalStatus(int id)
        {
            return await _dbContext.Approvalstatus
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
        }

        public async Task<Approvalstatus> GetStatusIdFromStatusCode(int statuscode)
        {
            return await _dbContext.Approvalstatus
                                   .Where(x => x.StatusCode == statuscode)
                                   .FirstOrDefaultAsync();
        }

        public async Task<Approvalproccess> FirstApprovalProcessData(string processguid, int serviceid)
        {
            return await _dbContext.Approvalproccess
                                     .Where(x => x.ProcessGuid == processguid && x.ServiceId == serviceid)
                                     .OrderBy(x => x.Id)
                                     .FirstOrDefaultAsync();
        }
        public async Task<Kycapprovalproccess> FirstkycApprovalProcessData(string processguid, int serviceid) //added by ishu 22/7/2021
        {
            return await _dbContext.Kycapprovalproccess
                                    .Where(x => x.ProcessGuid == processguid && x.ServiceId == serviceid)
                                    .OrderBy(x => x.Id)
                                    .FirstOrDefaultAsync();
        }

        public async Task<Approvalproccess> CheckLastUserForRevert(string processguid, int serviceid, int level)
        {
            return await _dbContext.Approvalproccess
                                     .Where(x => x.ProcessGuid == processguid && x.ServiceId == serviceid
                                     && x.Level == level
                                     )
                                     .OrderBy(x => x.Id)
                                     .Take(1)
                                     .FirstOrDefaultAsync();
        }
        public async Task<Kycapprovalproccess> CheckLastKycUserForRevert(string processguid, int serviceid, int level)//added by ishu 23/7/2021
        {
            return await _dbContext.Kycapprovalproccess
                                      .Where(x => x.ProcessGuid == processguid && x.ServiceId == serviceid
                                      && x.Level == level
                                      )
                                      .OrderBy(x => x.Id)
                                      .Take(1)
                                      .FirstOrDefaultAsync();
        }
        public async Task<ApplicationNotificationTemplate> FetchSingleNotificationTemplate(string guid)
        {
            return await _dbContext.ApplicationNotificationTemplate
                                     .Where(x => x.UserNotificationGuid == guid && x.IsActive == 1)
                                     .FirstOrDefaultAsync();
        }
    }
}
