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

namespace Libraries.Repository.EntityRepository
{
    public class DemolitionchecklistRepository : GenericRepository<Demolitionchecklist>, IDemolitionchecklistRepository
    {

        public DemolitionchecklistRepository(DataContext dbcontext) : base(dbcontext)
        { }

        public async Task<List<Demolitionchecklist>> GetDemolitionchecklist()
        {
            return await _dbContext.Demolitionchecklist.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<PagedResult<Demolitionchecklist>> GetPagedDemolitionchecklist(DemolitionchecklistSearchDto model)
        {
            // return await _dbContext.Demolitionchecklist.Where(x => x.IsActive == 1).GetPaged<Demolitionchecklist>(model.PageNumber, model.PageSize);
            //if ((string.IsNullOrEmpty(model.name)))
            //{
            //    return await _dbContext.Demolitionchecklist.Where(s => s.IsActive == 1).OrderBy(s => s.Id).GetPaged<Demolitionchecklist>(model.PageNumber, model.PageSize);

            //}
            //else
            //{
                return await _dbContext.Demolitionchecklist.Where(s => (string.IsNullOrEmpty(model.name) || s.ChecklistDescription.Contains(model.name)))
                   .OrderBy(x => x.Id)
                   .ThenByDescending(x => x.IsActive == 1)
                   .ThenBy(x => x.ChecklistDescription)
                   .GetPaged<Demolitionchecklist>(model.PageNumber, model.PageSize);
            }
        



    }
}
