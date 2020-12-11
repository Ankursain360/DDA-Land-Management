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
   public class AnnexureARepository : GenericRepository<Fixingdemolition>, IAnnexureARepository
    {
        public AnnexureARepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Demolitionchecklist>> GetDemolitionchecklist()
        {
            return await _dbContext.Demolitionchecklist.ToListAsync();
        }
        public async Task<List<Demolitionprogram>> GetDemolitionprogram()
        {
            return await _dbContext.Demolitionprogram.ToListAsync();
        }
        public async Task<List<Demolitiondocument>> GetDemolitiondocument()
        {
            return await _dbContext.Demolitiondocument.ToListAsync();
        }
        public async Task<List<Fixingdemolition>> GetFixingdemolition(int id)
        {
            return await _dbContext.Fixingdemolition.Where(x => x.EncroachmentId == id).Include(x => x.Encroachment).ToListAsync();
        }
        public async Task<bool> SaveFixingdocument(Fixingdocument fixingdocument)
        {
            _dbContext.Fixingdocument.Add(fixingdocument);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<bool> Savefixingchecklist(Fixingchecklist fixingchecklist)
        {
            _dbContext.Fixingchecklist.Add(fixingchecklist);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<bool> SaveFixingprogram(Fixingprogram fixingprogram)
        {
            _dbContext.Fixingprogram.Add(fixingprogram);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<Fixingchecklist>> Getfixingchecklist(int fixingdemolitionId)
        {
            return await _dbContext.Fixingchecklist.Where(x => x.FixingdemolitionId == fixingdemolitionId && x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Fixingprogram>> Getfixingprogram(int fixingdemolitionId)
        {
            return await _dbContext.Fixingprogram
                                    .Include(x => x.DemolitionProgram)
                                    .Where(x => x.FixingdemolitionId == fixingdemolitionId && x.IsActive == 1)
                                    .ToListAsync();
        }
        public async Task<List<Fixingdocument>> Getfixingdocument(int fixingdemolitionId)
        {
            return await _dbContext.Fixingdocument.Where(x => x.FixingdemolitionId == fixingdemolitionId && x.IsActive == 1).ToListAsync();
        }
        public async Task<PagedResult<EncroachmentRegisteration>> GetPagedDetails(AnnexureASearchDto model)
        {
            var InInspectionId = (from x in _dbContext.EncroachmentRegisteration
                                  where x.WatchWard != null && x.IsActive == 1
                                  select x.WatchWardId).ToArray();

            return await _dbContext.EncroachmentRegisteration
                                     .Where(x => x.IsActive == 1 && x.ApprovedStatus == 1
                                      && !(InInspectionId).Contains(x.Id))
                                     .GetPaged<EncroachmentRegisteration>(model.PageNumber, model.PageSize);
        }

        public async Task<Fixingdemolition> FetchSingleResult(int id)
        {
            return await _dbContext.Fixingdemolition
                               .Include(x => x.Fixingchecklist)
                               .Include(x => x.Fixingdocument)
                               .Include(x => x.Fixingprogram)
                               .Where(x => x.Id == id)
                               .FirstOrDefaultAsync();
        }

        public async Task<Fixingdocument> GetAnnexureAfiledetails(int id)
        {
            return await _dbContext.Fixingdocument.Where(x => x.FixingdemolitionId == id && x.IsActive == 1).FirstOrDefaultAsync();
        }
    }
}
