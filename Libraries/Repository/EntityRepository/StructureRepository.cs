using Dto.Search;
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
    public class StructureRepository : GenericRepository<Structure>, IStructureRepository
    {
        public StructureRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Structure>> GetPagedStructure(StructureSearchDto model)
        {
            var data = await _dbContext.Structure

                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                            .OrderByDescending(s => s.IsActive)
                            .GetPaged<Structure>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderBy(x => x.Name).ToList();
                        break;
                    case ("STATUS"):
                        data.Results = data.Results.OrderBy(x => x.IsActive).ToList();
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
                    case ("STATUS"):
                        data.Results = data.Results.OrderByDescending(x => x.IsActive).ToList();
                        break;
                }
            }
            return data;
            // return await _dbContext.Structure.GetPaged<Structure>(model.PageNumber, model.PageSize);
        }

        public async Task<List<Structure>> GetStructure()
        {
            return await _dbContext.Structure.Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Structure.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }

        public async Task<List<Structure>> GetAllStructure()
        {
            return await _dbContext.Structure.Where(x => x.IsActive == 1).ToListAsync();
        }

       
    }
}
