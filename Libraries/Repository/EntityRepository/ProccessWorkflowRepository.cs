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

        public async Task<List<Approvalproccess>> GetHistoryDetails(int proccessid, int id)
        {
            var result = await _dbContext.Approvalproccess
                                    // .Include(x => x.SendFromUser)
                                    .Where(x => x.ProccessID == proccessid && x.ServiceId == id)
                                    .ToListAsync();

            return result;

        }

        public int GetPreviousApprovalId(int proccessid, int serviceid)
        {
            var File = (from f in _dbContext.Approvalproccess
                        where f.ProccessID == proccessid && f.ServiceId == serviceid
                        orderby f.Id descending
                        select f.Id).First();

            return File;
        }
    }
}
