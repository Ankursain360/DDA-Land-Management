﻿using System.Collections.Generic;
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
    public class CasenatureRepository : GenericRepository<Casenature>, ICasenatureRepository
    {
        public CasenatureRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<PagedResult<Casenature>> GetPagedcasenature(CasenatureSearchDto model)
        {
          
           var  data= await _dbContext.Casenature
                                .Where(x => string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                           
                            .GetPaged<Casenature>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Casenature
                               .Where(x => string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                            .OrderBy(s => s.Name)
                        .GetPaged<Casenature>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Casenature
                             .Where(x => string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                            .OrderBy(x => x.IsActive == 0)
                        .GetPaged<Casenature>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Casenature
                             .Where(x => string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                            .OrderByDescending(x => x.Name)
                        .GetPaged<Casenature>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Casenature
                             .Where(x => string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             .OrderByDescending(x => x.IsActive == 0)
                              .GetPaged<Casenature>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;
        }
       
        public async Task<List<Casenature>> GetCasenature()
        {
             return await _dbContext.Casenature.Where(x => x.IsActive == 0).ToListAsync();
            //return await _dbContext.casenature.OrderByDescending(s => s.IsActive).ToListAsync();
        }



        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Casenature.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }

        public async Task<List<Casenature>> Getcasenature()
        {
            return await _dbContext.Casenature.Where(x => x.IsActive == 1).ToListAsync();

        }
    }
}
