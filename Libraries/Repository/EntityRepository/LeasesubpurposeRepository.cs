using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
    public class LeasesubpurposeRepository : GenericRepository<Leasesubpurpose>, ILeasesubpurposeRepository
    {

        public LeasesubpurposeRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<bool> Any(int id, string SubPurposeUse, int PurposeUseId)
        {
            return await _dbContext.Leasesubpurpose.AnyAsync(t => t.Id != id && t.SubPurposeUse.ToLower() == SubPurposeUse.ToLower() && t.PurposeUseId == PurposeUseId);
        }
        public async Task<Leasesubpurpose> FetchSingleResult(int id)
        {
            return await _dbContext.Leasesubpurpose
                                        .Include(x => x.PurposeUse)
                                        .Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();
        }

        public async Task<PagedResult<Leasesubpurpose>> GetPagedLeasesubpurpose(LeasesubpurposeSearchDto model)
        {
            var data = await _dbContext.Leasesubpurpose
                                        .Include(x => x.PurposeUse)
                                        .Where(x => x.PurposeUseId == (model.purposeId == 0 ? x.PurposeUseId : model.purposeId)
                                        && (x.SubPurposeUse != null ? x.SubPurposeUse.Contains(model.subpurposeuse == "" ? x.SubPurposeUse : model.subpurposeuse) : true)
                                        )
                                        .GetPaged<Leasesubpurpose>(model.PageNumber, model.PageSize);
            //int SortOrder = (model.SortBy.ToUpper() == "ISACTIVE") && ((int)model.SortOrder == 1) ? 2 : (model.SortBy.ToUpper() == "ISACTIVE") && ((int)model.SortOrder == 1) ? 1 : (int)model.SortOrder;
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                if (model.SortBy.ToUpper() == "ISACTIVE")
                {
                    data = await _dbContext.Leasesubpurpose
                                   .Include(x => x.PurposeUse)
                                   .Where(x => x.PurposeUseId == (model.purposeId == 0 ? x.PurposeUseId : model.purposeId)
                                   && (x.SubPurposeUse != null ? x.SubPurposeUse.Contains(model.subpurposeuse == "" ? x.SubPurposeUse : model.subpurposeuse) : true)
                                   )
                                   .OrderByDescending(s => s.IsActive)
                                   .GetPaged<Leasesubpurpose>(model.PageNumber, model.PageSize);
                }
                else
                {
                    data = await _dbContext.Leasesubpurpose
                                   .Include(x => x.PurposeUse)
                                   .Where(x => x.PurposeUseId == (model.purposeId == 0 ? x.PurposeUseId : model.purposeId)
                                   && (x.SubPurposeUse != null ? x.SubPurposeUse.Contains(model.subpurposeuse == "" ? x.SubPurposeUse : model.subpurposeuse) : true)
                                   )
                                   .OrderBy(s =>
                                   (model.SortBy.ToUpper() == "SUBPURPOSEUSE" ? s.SubPurposeUse
                                   : model.SortBy.ToUpper() == "PURPOSEUSE" ? (s.PurposeUse == null ? null : s.PurposeUse.PurposeUse)
                                   // : model.SortBy.ToUpper() == "ISACTIVE" ? s.IsActive
                                   : s.SubPurposeUse)
                                   )
                                   .GetPaged<Leasesubpurpose>(model.PageNumber, model.PageSize);
                }

            }
            else if (SortOrder == 2)
            {
                data = null;
                if (model.SortBy.ToUpper() == "ISACTIVE")
                {
                    data = await _dbContext.Leasesubpurpose
                                   .Include(x => x.PurposeUse)
                                   .Where(x => x.PurposeUseId == (model.purposeId == 0 ? x.PurposeUseId : model.purposeId)
                                   && (x.SubPurposeUse != null ? x.SubPurposeUse.Contains(model.subpurposeuse == "" ? x.SubPurposeUse : model.subpurposeuse) : true)
                                   )
                                   .OrderBy(s => s.IsActive)
                                   .GetPaged<Leasesubpurpose>(model.PageNumber, model.PageSize);
                }
                else
                {
                    data = await _dbContext.Leasesubpurpose
                                   .Include(x => x.PurposeUse)
                                   .Where(x => x.PurposeUseId == (model.purposeId == 0 ? x.PurposeUseId : model.purposeId)
                                   && (x.SubPurposeUse != null ? x.SubPurposeUse.Contains(model.subpurposeuse == "" ? x.SubPurposeUse : model.subpurposeuse) : true)
                                   )
                                   .OrderByDescending(s =>
                                   (model.SortBy.ToUpper() == "SUVPERPOUSUSE" ? s.SubPurposeUse
                                   : model.SortBy.ToUpper() == "PURPOSEUSE" ? (s.PurposeUse == null ? null : s.PurposeUse.PurposeUse)
                                   // : model.SortBy.ToUpper() == "ISACTIVE" ? s.IsActive
                                   : s.SubPurposeUse)
                                   )
                                   .GetPaged<Leasesubpurpose>(model.PageNumber, model.PageSize);
                }
            }
            return data;
        }
        public async Task<List<Leasepurpose>> GetPurposeUseList()
        {
            return await _dbContext.Leasepurpose.Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Leasesubpurpose>> GetAllLeaseSubpurpose()
        {

            List<Leasesubpurpose> leaseSubPurposeList = await _dbContext.Leasesubpurpose.Include(x => x.PurposeUse)
                .Where(x => x.IsActive == 1).ToListAsync();
            return leaseSubPurposeList;
        }

        public async Task<List<Leasepurpose>> GetAllLeasepurpose()
        {
            List<Leasepurpose> leasePurposeList = await _dbContext.Leasepurpose.Where(x => x.IsActive == 1).ToListAsync();
            return leasePurposeList;
        }
    }


}
