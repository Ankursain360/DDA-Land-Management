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
    }
}
