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

namespace Libraries.Repository.EntityRepository
{
    public class DesignationRepository : GenericRepository<Designation>, IDesignationRepository
    {
       
        public DesignationRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<PagedResult<Designation>> GetPagedDesignation(DesignationSearchDto model) {
            return await _dbContext.Designation.GetPaged<Designation>(model.PageNumber, model.PageSize);
        }
       
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Designation.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }
    }
}
