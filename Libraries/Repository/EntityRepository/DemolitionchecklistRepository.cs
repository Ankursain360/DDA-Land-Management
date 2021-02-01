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
           
                var data= await _dbContext.Demolitionchecklist
                         .Where(s => (string.IsNullOrEmpty(model.name) || s.ChecklistDescription.Contains(model.name)))
                         .GetPaged<Demolitionchecklist>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder ==1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DESCRIPTION"):
                        data = null;
                        data = await _dbContext.Demolitionchecklist
                         .Where(s => (string.IsNullOrEmpty(model.name) || s.ChecklistDescription.Contains(model.name)))
                         .OrderBy(x => x.ChecklistDescription)
                         .GetPaged<Demolitionchecklist>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Demolitionchecklist
                         .Where(s => (string.IsNullOrEmpty(model.name) || s.ChecklistDescription.Contains(model.name)))
                         .OrderByDescending(x => x.IsActive)
                         .GetPaged<Demolitionchecklist>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            else if (SortOrder ==2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DESCRIPTION"):
                        data = null;
                        data = await _dbContext.Demolitionchecklist
                         .Where(s => (string.IsNullOrEmpty(model.name) || s.ChecklistDescription.Contains(model.name)))
                         .OrderByDescending(x => x.ChecklistDescription)
                         .GetPaged<Demolitionchecklist>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Demolitionchecklist
                         .Where(s => (string.IsNullOrEmpty(model.name) || s.ChecklistDescription.Contains(model.name)))
                         .OrderBy(x => x.IsActive)
                         .GetPaged<Demolitionchecklist>(model.PageNumber, model.PageSize);
                        break;
                }
            } return data;
            }
        



    }
}
