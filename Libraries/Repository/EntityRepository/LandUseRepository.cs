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
            // string OrderAsc = conv(model.SortBy; //"NameAsc";
            //string OrderDesc = model.SortOrder; //"NameDesc";


            //if (Data == OrderAsc) //((model.SortBy =="Name") && (model.sortOrder =="Asc"))
            //{
            //    return await _dbContext.Landuse//.Where(s => (string.IsNullOrEmpty(model.name) || s.Name.Contains(model.name)))
            //        .OrderBy(s => s.Name).GetPaged<Landuse>(model.PageNumber, model.PageSize);

            //}
            //else if (Data==OrderDesc) //((model.SortBy == "Name") && (model.sortOrder == "Desc"))
            //{ 
            //    return await _dbContext.Landuse//.Where(s => (string.IsNullOrEmpty(model.name) || s.Name.Contains(model.name)))
            //        .OrderByDescending(s => s.Name).GetPaged<Landuse>(model.PageNumber, model.PageSize); 
            //}
            //else
            //{
            try
            {
                var modelorder = model.SortOrder.ToString();
                var modelsort = model.SortBy.ToString();

                return await _dbContext.Landuse.Where(s => (string.IsNullOrEmpty(model.name) || s.Name.Contains(model.name)))
                   //.OrderBy(x => x.Id)
                   .OrderBy(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                   .ThenByDescending(x => x.IsActive == 1)
                   .ThenBy(x => x.Name)
                   .GetPaged<Landuse>(model.PageNumber, model.PageSize);
            }
            catch(Exception ex)
            {
                return await _dbContext.Landuse//.Where(s => (string.IsNullOrEmpty(model.name) || s.Name.Contains(model.name)))
                    .OrderByDescending(s => s.Name).GetPaged<Landuse>(model.PageNumber, model.PageSize);
            }
            // }
        }
        }
    }



