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
     public class IssueReturnFileRepository : GenericRepository<Issuereturnfile>, IIssueReturnFileRepository
    {
        public IssueReturnFileRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Datastoragedetails>> GetFileNoList()
        {
            var fileNoList = await _dbContext.Datastoragedetails.Where(x => x.IsActive == 1).ToListAsync();
            return fileNoList;
        }
    }
    
}
