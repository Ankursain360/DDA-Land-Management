using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EntityRepository
{
    public class DemolitionprogrammasterRepository : GenericRepository<Demolitionprogram>, IDemolitionprogrammasterRepository
    {
        public DemolitionprogrammasterRepository(DataContext dbcontext) : base(dbcontext)
        { }



        public async Task<List<Demolitionprogram>> GetDemolitionprogrammaster()
        {
            return await _dbContext.Demolitionprogram.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<PagedResult<Demolitionprogram>> GetPagedDemolitionprogrammaster(DemolitionprogrammasterSearchDto model)
        {
           
                var data= await _dbContext.Demolitionprogram
                        .Where(s => (string.IsNullOrEmpty(model.name) || s.Items.Contains(model.name)))
                        .GetPaged<Demolitionprogram>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("ITEM"):
                        data = null;
                        data = await _dbContext.Demolitionprogram
                        .Where(s => (string.IsNullOrEmpty(model.name) || s.Items.Contains(model.name)))
                        .OrderBy(x => x.Items)
                        .GetPaged<Demolitionprogram>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Demolitionprogram
                        .Where(s => (string.IsNullOrEmpty(model.name) || s.Items.Contains(model.name)))
                        .OrderByDescending(x => x.IsActive)
                        .GetPaged<Demolitionprogram>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("ITEM"):
                        data = null;
                        data = await _dbContext.Demolitionprogram
                        .Where(s => (string.IsNullOrEmpty(model.name) || s.Items.Contains(model.name)))
                        .OrderByDescending(x => x.Items)
                        .GetPaged<Demolitionprogram>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Demolitionprogram
                        .Where(s => (string.IsNullOrEmpty(model.name) || s.Items.Contains(model.name)))
                        .OrderBy(x => x.IsActive)
                        .GetPaged<Demolitionprogram>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;
        }
    }

    }

