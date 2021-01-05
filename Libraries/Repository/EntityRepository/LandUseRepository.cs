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
    public class LandUseRepository : GenericRepository<Landuse>, ILandUseRepository
    {

        public LandUseRepository(DataContext dbContext) : base(dbContext)
        {

        }


        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Landuse.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }

        public async Task<PagedResult<Landuse>> GetPagedLandUse(LandUseSearchDto model)
        {

            //string Data = model.name;
            //if ((string.IsNullOrEmpty(model.name)))
            //{
            //    return await _dbContext.Landuse.Where(s => s.IsActive == 1).OrderBy(s => s.Id).GetPaged<Landuse>(model.PageNumber, model.PageSize);

            //}
            //else
            //{
                return await _dbContext.Landuse.Where(s => (string.IsNullOrEmpty(model.name) || s.Name.Contains(model.name)))
                   .OrderBy(x => x.Id)
                   .ThenByDescending(x => x.IsActive == 1)
                   .ThenBy(x => x.Name)
                   .GetPaged<Landuse>(model.PageNumber, model.PageSize);
           // }
        }
        }
    }



