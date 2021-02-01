using System;
using System.Collections.Generic;
using System.Text;
using Dto.Search;
using System.Threading.Tasks;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Dto.Master;

namespace Libraries.Repository.EntityRepository
{
    public class CaseyearRepository : GenericRepository<Caseyear>, ICaseyearRepository
    {
        public CaseyearRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<PagedResult<Caseyear>> GetPagedCaseyear(CaseyearSearchDto model)
        {
            var data= await _dbContext.Caseyear
                                .Where(x => string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                .GetPaged<Caseyear>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Caseyear
                                .Where(x => string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                .OrderBy(s => s.Name)
                                .GetPaged<Caseyear>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Caseyear
                                .Where(x => string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                .OrderByDescending(s => s.IsActive)
                                .GetPaged<Caseyear>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Caseyear
                                .Where(x => string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                .OrderByDescending(s => s.Name)
                                .GetPaged<Caseyear>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Caseyear
                                .Where(x => string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                .OrderBy(s => s.IsActive)
                                .GetPaged<Caseyear>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;
        }

        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Caseyear.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }

       
        public async Task<List<Caseyear>> GetAllCaseyear()
        {
            return await _dbContext.Caseyear.Where(x => x.IsActive == 1).ToListAsync();
        }
    }
}
