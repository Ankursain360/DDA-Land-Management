using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
namespace Libraries.Repository.EntityRepository
{
    public class DivisionRepository : GenericRepository<Division>, IDivisionRepository
    {
        public DivisionRepository(DataContext dbcontext) : base(dbcontext)
        { }

        public async Task<List<Division>> GetDivisions()
        {
            return await _dbContext.Division.ToListAsync();
        }


     

        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Division.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }







    }
}
