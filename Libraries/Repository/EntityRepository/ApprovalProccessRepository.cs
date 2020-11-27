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
