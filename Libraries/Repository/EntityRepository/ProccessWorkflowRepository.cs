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
    public class ProccessWorkflowRepository : GenericRepository<Processworkflow>, IProccessWorkflowRepository
    {

        public ProccessWorkflowRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public int FetchCountResultForProccessWorkflow(int workflowTemplateId)
        {
            var count = (from f in _dbContext.Processworkflow
                         where f.WorkflowTemplateId == workflowTemplateId
                         orderby f.Id
                         select f.Id).Count();

            return count;
        }

        public async Task<List<Approvalproccess>> GetHistoryDetails(string proccessguid, int id)
        {
            var result = await _dbContext.Approvalproccess
                                    .Where(x => x.ProcessGuid == proccessguid && x.ServiceId == id)
                                    .ToListAsync();

            return result;

        }

        public int GetPreviousApprovalId(string proccessguid, int serviceid)
        {
            var File = (from f in _dbContext.Approvalproccess
                        where f.ProcessGuid == proccessguid && f.ServiceId == serviceid
                        orderby f.Id descending
                        select f.Id).First();

            return File;
        }
    }
}
