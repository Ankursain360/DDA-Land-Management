using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class ApplicationModificationDetailsRepository : GenericRepository<ApplicationModificationDetails>, IApplicationModificationDetailsRepository
    {
        
        public ApplicationModificationDetailsRepository(DataContext dbContext) : base(dbContext)
        {
           
        }

        public DateTime? GetApplicationModificationDetails()
        {
            try
            {
                var data = _dbContext.applicationmodificationdetails.Select(x => new { x.updated, x.Isactive }).OrderByDescending(e => e.updated).FirstOrDefault(x => x.Isactive == 1)?.updated;
                //var data = await _dbContext.applicationmodificationdetails.Where(e=>e.Isactive == 1).OrderByDescending(e=>e.updated).ToListAsync();
                // var data = result.Select(e => new { e.updated });
                //var data = await _dbContext.applicationmodificationdetails.Select(e => new { e.updated }).ToListAsync();
                return data;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
