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
    public class EncroachmentRegisterationApprovalRepository : GenericRepository<EncroachmentRegisteration>, IEncroachmentRegisterationApprovalRepository
    {
        public EncroachmentRegisterationApprovalRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<EncroachmentRegisteration>> GetPagedEncroachmentRegisteration(EncroachmentRegisterApprovalSearchDto model, int userId)
        {

            return await _dbContext.EncroachmentRegisteration
                                    .Where(x => x.IsActive == 1 && x.ApprovedStatus == model.StatusId 
                                    && (model.StatusId == 0 ? x.PendingAt == userId : x.PendingAt == 0))
                                    .GetPaged<EncroachmentRegisteration>(model.PageNumber, model.PageSize);
        }


        public async Task<List<EncroachmentRegisteration>> GetEncroachmentRegisteration()
        {
            return await _dbContext.EncroachmentRegisteration.ToListAsync();
        }
        //public async Task<List<EncroachmentRegisteration>> GetAllEncroachmentRegisteration()
        //{
        //    return await _dbContext.EncroachmentRegisteration.Include(x => x.Village)
        //        .Include(x => x.Khasra)
        //        .ToListAsync();
        //}
        public async Task<List<EncroachmentRegisteration>> GetAllEncroachmentRegisteration()
        {
            return await _dbContext.EncroachmentRegisteration.Include(x => x.Locality)
                .ToListAsync();
        }
        public async Task<List<Khasra>> GetAllKhasra()
        {
            List<Khasra> khasraList = await _dbContext.Khasra.Where(x => x.IsActive == 1).ToListAsync();
            return khasraList;
        }
        public async Task<List<Village>> GetAllVillage()
        {
            List<Village> villagelist = await _dbContext.Village.Where(x => x.IsActive == 1).ToListAsync();
            return villagelist;
        }
        public async Task<List<Locality>> GetAllLocality()
        {
            List<Locality> localitylist = await _dbContext.Locality.Where(x => x.IsActive == 1).ToListAsync();
            return localitylist;
        }

    }
}
