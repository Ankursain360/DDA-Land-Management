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
            return await _dbContext.Demolitiondocument.Where(x => x.IsActive == 1).GetPaged<Demolitiondocument>(model.PageNumber, model.PageSize);
        }




    }
}
