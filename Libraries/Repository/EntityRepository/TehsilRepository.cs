using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Repository.EntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using System.Linq;

namespace Libraries.Repository.EntityRepository
{
   
      public class TehsilRepository : GenericRepository<Tehsil>, ITehsilRepository
    {
        public TehsilRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Tehsil>> GetPagedTehsil(TehsilSearchDto model)
        {
            var data = await _dbContext.Tehsil
                 .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                            /* &&( model.proposalDate == ' '  ? x.ProposalDate : (model.proposalDate == x.ProposalDate))*/)
                .GetPaged<Tehsil>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Tehsil
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             /*&& model.proposalDate == x.ProposalDate*/)
                             .OrderBy(a => a.Name)
                             .GetPaged<Tehsil>(model.PageNumber, model.PageSize);
                         break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Tehsil
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             /*&& model.proposalDate == x.ProposalDate*/)
                            .OrderByDescending(a => a.IsActive)
                            .GetPaged<Tehsil>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Tehsil
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                            /* && model.proposalDate == x.ProposalDate*/)
                             .OrderByDescending(a => a.Name)
                             .GetPaged<Tehsil>(model.PageNumber, model.PageSize);

                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Tehsil
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             /*&& model.proposalDate == x.ProposalDate*/)
                            .OrderBy(a => a.IsActive)
                            .GetPaged<Tehsil>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;
        }
        public async Task<List<Tehsil>> GetTehsil()
        {
            return await _dbContext.Tehsil.ToListAsync();
        }
        public async Task<List<Tehsil>> GetAllTehsil()
        {
            // return await _dbContext.Tehsil.Include(x => x.Name).ToListAsync();

            return await _dbContext.Tehsil.OrderByDescending(x => x.Id).ToListAsync();
        }
        //public async Task<List<Scheme>> GetAllScheme()
        //{
        //    List<Scheme> schemeList = await _dbContext.Scheme.ToListAsync();
        //    return schemeList;
        //}
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Tehsil.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }


    }
}
