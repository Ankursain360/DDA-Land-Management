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
   public class AnnexureARepository : GenericRepository<Demolitionchecklist>, IAnnexureARepository
    {
        public AnnexureARepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Demolitionchecklist>> GetDemolitionchecklist()
        {
            return await _dbContext.Demolitionchecklist.ToListAsync();
        }

        public async Task<List<Demolitiondocument>> GetDemolitiondocument()
        {
            return await _dbContext.Demolitiondocument.ToListAsync();
        }

        public async Task<List<Fixingdemolition>> GetFixingdemolition(int encroachmentId)
        {
            return await _dbContext.Fixingdemolition.Where(x => x.EncroachmentId == encroachmentId && x.IsActive == 1).ToListAsync();
        }


        public async Task<bool> SaveFixingdemolition(Fixingdemolition fixingdemolition)
        {
            _dbContext.Fixingdemolition.Add(fixingdemolition);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }


    }
}
