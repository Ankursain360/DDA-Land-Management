using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class CaseyearRepository : GenericRepository<Caseyear>, ICaseyearRepository
    {
        public CaseyearRepository(DataContext dbcontext) : base(dbcontext)
        { }
        public async Task<List<Caseyear>> GetAllCaseyear()
        {
            return await _dbContext.Caseyear.Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<PagedResult<Caseyear>> GetPagedCaseyear(CaseyearSearchDto model)
        {
            //return await _dbContext.Caseyear.OrderByDescending(x => x.Id).GetPaged<Caseyear>(model.PageNumber, model.PageSize);
            var data = await _dbContext.Caseyear
            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                            .OrderBy(s => s.Name)
                            .OrderByDescending(s => s.IsActive)
                             .GetPaged<Caseyear>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderBy(x => x.Name).ToList();
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderByDescending(x => x.Name).ToList();
                        break;

                }
            }
            return data;
        }

    }
}
