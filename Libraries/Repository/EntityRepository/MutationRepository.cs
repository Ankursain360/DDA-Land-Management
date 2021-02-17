using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class MutationRepository : GenericRepository<Mutation>, IMutationRepository
    {
        public MutationRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Mutation>> GetPagedDMSFileUploadList(DemandListDetailsSearchDto model)
        {
            var data = await _dbContext.Mutation
                                        .Include(x => x.AcquiredVillage)
                                        .Include(x => x.Khasra)
                                        .Where(x => x.AcquiredVillageId == (model.villageId == 0 ? x.AcquiredVillageId : model.villageId)
                                      //  && (x.DemandListNo != null ? x.DemandListNo.Contains(model.demandlistno == "" ? x.DemandListNo : model.demandlistno) : true)
                                        && (x.KhasraId == (model.KhasraId == 0 ? x.KhasraId : model.KhasraId))
                                        )
                                        .GetPaged<Mutation>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                data = await _dbContext.Mutation
                                        .Include(x => x.AcquiredVillage)
                                        .Include(x => x.Khasra)
                                        .Where(x => x.AcquiredVillageId == (model.villageId == 0 ? x.AcquiredVillageId : model.villageId)
                                        //  && (x.DemandListNo != null ? x.DemandListNo.Contains(model.demandlistno == "" ? x.DemandListNo : model.demandlistno) : true)
                                        && (x.KhasraId == (model.KhasraId == 0 ? x.KhasraId : model.KhasraId))
                                        )
                                .OrderBy(s =>
                                (model.SortBy.ToUpper() == "OWNER" ? s.MutationOwnerLessee
                                : model.SortBy.ToUpper() == "VILLAGE" ? (s.AcquiredVillage == null ? null : s.AcquiredVillage.Name)
                                : model.SortBy.ToUpper() == "KHASRANO" ? (s.Khasra != null ? s.Khasra.Name : null) : s.MutationOwnerLessee)
                                )
                                .GetPaged<Mutation>(model.PageNumber, model.PageSize);
            }
            else if (SortOrder == 2)
            {
                data = null;
                data = await _dbContext.Mutation
                                        .Include(x => x.AcquiredVillage)
                                        .Include(x => x.Khasra)
                                        .Where(x => x.AcquiredVillageId == (model.villageId == 0 ? x.AcquiredVillageId : model.villageId)
                                        //  && (x.DemandListNo != null ? x.DemandListNo.Contains(model.demandlistno == "" ? x.DemandListNo : model.demandlistno) : true)
                                        && (x.KhasraId == (model.KhasraId == 0 ? x.KhasraId : model.KhasraId))
                                        )
                                .OrderByDescending(s =>
                                 (model.SortBy.ToUpper() == "OWNER" ? s.MutationOwnerLessee
                                : model.SortBy.ToUpper() == "VILLAGE" ? (s.AcquiredVillage == null ? null : s.AcquiredVillage.Name)
                                : model.SortBy.ToUpper() == "KHASRANO" ? (s.Khasra != null ? s.Khasra.Name : null) : s.MutationOwnerLessee)
                                )
                                .GetPaged<Mutation>(model.PageNumber, model.PageSize);
            }
            return data;
        }

        public async Task<Mutation> FetchSingleResult(int id)
        {
            return await _dbContext.Mutation
                                        .Include(x => x.AcquiredVillage)
                                        .Include(x => x.Khasra)
                                        .Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();


        }
        public async Task<bool> Any(int id, string fileNo)
        {
            return await _dbContext.Dmsfileupload.AnyAsync(t => t.Id != id && t.FileNo.ToLower() == fileNo.ToLower());
        }
        public async Task<List<Acquiredlandvillage>> GetVillageList()
        {
            return await _dbContext.Acquiredlandvillage
                                     .Where(x => x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<List<Khasra>> GetKhasraList(int id)
        {
            return await _dbContext.Khasra
                                     .Where(x => x.IsActive == 1 && x.AcquiredlandvillageId == id)
                                     .ToListAsync();
        }

        public async Task<List<Mutationparticulars>> GetMutationParticulars(int id)
        {
            return await _dbContext.Mutationparticulars
                                       .Where(x => x.MutationId == id)
                                       .ToListAsync();
        }

        public async Task<bool> SaveMutationParticulars(List<Mutationparticulars> mutationparticulars)
        {
            await _dbContext.Mutationparticulars.AddRangeAsync(mutationparticulars);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<bool> DeleteMutationParticulars(int id)
        {
            _dbContext.RemoveRange(_dbContext.Mutationparticulars.Where(x => x.MutationId == id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
    }
}
