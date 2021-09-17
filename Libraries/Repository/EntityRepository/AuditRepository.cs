using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
   public class AuditRepository : GenericRepository<AuditModel>, IAuditRepository
    {
        public AuditRepository(DataContext dbContext) : base(dbContext)
        {

        }
   
        public async  Task<bool> InsertAuditLogs(AuditModel objauditmodel)
        {
            try
            {

                objauditmodel.CreatedDate = DateTime.Now;
               
                 _dbContext.AuditModel.Add(objauditmodel);
                var Result = await _dbContext.SaveChangesAsync();
                return Result > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
