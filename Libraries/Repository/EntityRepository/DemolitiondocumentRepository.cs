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
    public class DemolitiondocumentRepository : GenericRepository<Demolitiondocument>, IDemolitiondocumentRepository
    {
        public DemolitiondocumentRepository(DataContext dbcontext) : base(dbcontext)
        { }



        public async Task<List<Demolitiondocument>> GetDemolitiondocument()
        {
            return await _dbContext.Demolitiondocument.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<PagedResult<Demolitiondocument>> GetPagedDemolitiondocument(DemolitiondocumentSearchDto model)
        {
           var data= await _dbContext.Demolitiondocument
                    .Where(s => (string.IsNullOrEmpty(model.name) || s.DocumentName.Contains(model.name)))
                    .GetPaged<Demolitiondocument>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Demolitiondocument
                        .Where(s => (string.IsNullOrEmpty(model.name) || s.DocumentName.Contains(model.name)))
                        .OrderBy(x => x.DocumentName)
                        .GetPaged<Demolitiondocument>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Demolitiondocument
                        .Where(s => (string.IsNullOrEmpty(model.name) || s.DocumentName.Contains(model.name)))
                        .OrderByDescending(x => x.IsActive)
                        .GetPaged<Demolitiondocument>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Demolitiondocument
                        .Where(s => (string.IsNullOrEmpty(model.name) || s.DocumentName.Contains(model.name)))
                        .OrderByDescending(x => x.DocumentName)
                        .GetPaged<Demolitiondocument>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Demolitiondocument
                        .Where(s => (string.IsNullOrEmpty(model.name) || s.DocumentName.Contains(model.name)))
                        .OrderBy(x => x.IsActive)
                        .GetPaged<Demolitiondocument>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;
        }


    }
}

