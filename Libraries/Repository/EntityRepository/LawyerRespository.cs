using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
    public class LawyerRespository : GenericRepository<Lawyer>, ILawyerRepository
    {
        public LawyerRespository(DataContext dbcontext) : base(dbcontext)
        { }

        public async Task<List<Lawyer>> GetLawyer()
        {
            return await _dbContext.Lawyer.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<PagedResult<Lawyer>> GetPagedLawyer(LawyerSearchDto model)
        {
            return await _dbContext.Lawyer.Where(x => x.IsActive == 1).GetPaged<Lawyer>(model.PageNumber, model.PageSize);
        }
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Lawyer.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }


    }
}
