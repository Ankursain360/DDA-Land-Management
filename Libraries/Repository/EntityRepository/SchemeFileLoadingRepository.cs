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

namespace Libraries.Repository.EntityRepository
{
    public class SchemeFileLoadingRepository : GenericRepository<Schemefileloading>, ISchemeFileLoadingRepository
    {

        public SchemeFileLoadingRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Schemefileloading>> GetPagedSchemeFileLoading(SchemeFileLoadingSearchDto model)
        {
            var data = await _dbContext.Schemefileloading
                             .Where(x => (string.IsNullOrEmpty(model.schemename) || x.SchemeName.Contains(model.schemename))
                  && (string.IsNullOrEmpty(model.schemecode) || x.SchemeCode.Contains(model.schemecode)))
                 .GetPaged<Schemefileloading>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("SCHEMENAME"):
                        data = null;
                        data = await _dbContext.Schemefileloading
                           .Where(x => (string.IsNullOrEmpty(model.schemename) || x.SchemeName.Contains(model.schemename))
                            && (string.IsNullOrEmpty(model.schemecode) || x.SchemeCode.Contains(model.schemename)))
                           .OrderBy(s => s.SchemeName)
                           .GetPaged<Schemefileloading>(model.PageNumber, model.PageSize);

                        break;
                    case ("SCHEMECODE"):
                        data = null;
                        data = await _dbContext.Schemefileloading
                           .Where(x => (string.IsNullOrEmpty(model.schemename) || x.SchemeName.Contains(model.schemename))
                            && (string.IsNullOrEmpty(model.schemecode) || x.SchemeCode.Contains(model.schemecode)))
                           .OrderBy(s => s.SchemeCode)
                           .GetPaged<Schemefileloading>(model.PageNumber, model.PageSize);
                        break;
                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.Schemefileloading
                           .Where(x => (string.IsNullOrEmpty(model.schemename) || x.SchemeName.Contains(model.schemename))
                            && (string.IsNullOrEmpty(model.schemecode) || x.SchemeCode.Contains(model.schemecode)))
                           .OrderByDescending(x => x.IsActive)
                           .GetPaged<Schemefileloading>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("SCHEMENAME"):
                        data = null;
                        data = await _dbContext.Schemefileloading
                           .Where(x => (string.IsNullOrEmpty(model.schemename) || x.SchemeName.Contains(model.schemename))
                            && (string.IsNullOrEmpty(model.schemecode) || x.SchemeCode.Contains(model.schemecode)))
                           .OrderByDescending(s => s.SchemeName)
                           .GetPaged<Schemefileloading>(model.PageNumber, model.PageSize);
                        break;
                    case ("SCHEMECODE"):
                        data = null;
                        data = await _dbContext.Schemefileloading
                           .Where(x => (string.IsNullOrEmpty(model.schemename) || x.SchemeName.Contains(model.schemename))
                            && (string.IsNullOrEmpty(model.schemecode) || x.SchemeCode.Contains(model.schemecode)))
                           .OrderByDescending(x => x.SchemeCode)
                           .GetPaged<Schemefileloading>(model.PageNumber, model.PageSize);
                        break;

                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.Schemefileloading
                           .Where(x => (string.IsNullOrEmpty(model.schemename) || x.SchemeName.Contains(model.schemename))
                            && (string.IsNullOrEmpty(model.schemecode) || x.SchemeCode.Contains(model.schemecode)))
                           .OrderBy(x => x.IsActive)
                           .GetPaged<Schemefileloading>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;
        }
        public async Task<List<Schemefileloading>> GetSchemeFileloading()
        {
            return await _dbContext.Schemefileloading.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<bool> Any(int id, string schemename)
        {
            return await _dbContext.Schemefileloading.AnyAsync(t => t.Id != id && t.SchemeName.ToLower() == schemename.ToLower());
        }
    }

}
