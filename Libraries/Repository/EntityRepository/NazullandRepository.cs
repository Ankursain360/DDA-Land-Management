using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Microsoft.EntityFrameworkCore;
using Libraries.Repository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Libraries.Repository.EntityRepository;
using Dto.Search;
using System.Linq;

namespace Libraries.Repository.EntityRepository
{
    
    public class NazullandRepository : GenericRepository<Nazulland>, INazullandRepository
    {
        public NazullandRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Nazulland>> GetPagedNazulland(NazullandSearchDto model)
        {
            return await _dbContext.Nazulland
                .Include(x => x.Division)
                .Where(x => x.IsActive == 1)
                .GetPaged<Nazulland>(model.PageNumber, model.PageSize);
        }
        public async Task<List<Nazulland>> GetNazulland()
        {
            return await _dbContext.Nazulland.ToListAsync();
        }

        

        public async Task<List<Nazulland>> GetAllNazulland()
        {
            return await _dbContext.Nazulland.Include(x => x.Division)
                 .Where(x => x.IsActive == 1)
                .ToListAsync();
        }
        public async Task<List<Division>> GetAllDivision()
        {
            List<Division> divisionList = await _dbContext.Division.Where(x => x.IsActive == 1).ToListAsync();
            return divisionList;
        }



        //public async Task<bool> Any(int id, string name)
        //{
        //    return await _dbContext.Division.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        //}
    }
}
