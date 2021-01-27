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

    public class DamagePayeeApprovalRepository : GenericRepository<Damagepayeeregister>, IDamagePayeeApprovalRepository
    {
        public DamagePayeeApprovalRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<PagedResult<Damagepayeeregister>> GetPagedDamageForApproval(DamagepayeeRegisterApprovalDto model, int userId)
        {
            var data = await _dbContext.Damagepayeeregister
                                        .Include(x => x.Locality)
                                        .Include(x => x.District)
                                        .Where(x => x.IsActive == 1 && x.ApprovedStatus == model.StatusId
                                        && (model.StatusId == 0 ? x.PendingAt == userId : x.PendingAt == 0)
                                        )
                                        .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                data = await _dbContext.Damagepayeeregister
                                .Include(x => x.Locality)
                                .Include(x => x.District)
                                .Where(x => x.IsActive == 1 && x.ApprovedStatus == model.StatusId
                                && (model.StatusId == 0 ? x.PendingAt == userId : x.PendingAt == 0)
                                )
                                .OrderBy(s =>
                                (model.SortBy.ToUpper() == "FILENO" ? s.FileNo : model.SortBy.ToUpper() == "DISTRICT" ? s.District.Name : model.SortBy.ToUpper() == "LOCALITY" ? s.Locality.Name : s.FileNo)
                                )
                                .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
            }
            else if (SortOrder == 2)
            {
                data = null;
                data = await _dbContext.Damagepayeeregister
                                .Include(x => x.Locality)
                                .Include(x => x.District)
                                .Where(x => x.IsActive == 1 && x.ApprovedStatus == model.StatusId
                                && (model.StatusId == 0 ? x.PendingAt == userId : x.PendingAt == 0)
                                )
                                .OrderByDescending(s =>
                                (model.SortBy.ToUpper() == "FILENO" ? s.FileNo : model.SortBy.ToUpper() == "DISTRICT" ? (s.District == null ? null : s.District.Name) : model.SortBy.ToUpper() == "LOCALITY" ? (s.Locality != null ? s.Locality.Name : null) : s.FileNo)
                                )
                                .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
            }
            return data;
        }

        public async Task<PagedResult<Damagepayeeregister>> GetPagedDamagePayeeRegisterForApproval(DamagepayeeRegisterApprovalDto model, bool IsUser)
        {
            if (IsUser)
            {

                return await _dbContext.Damagepayeeregister
                                        .Include(x => x.Locality)
                                        .Include(x => x.District)
                                        .Where(x => x.IsActive == 1 && x.ApprovedStatus == model.StatusId)
                                        .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
            }
            else
            {
                return await _dbContext.Damagepayeeregister
                                        .Include(x => x.Locality)
                                        .Include(x => x.District)
                                        .Where(x => x.IsActive == 1
                                        && (model.StatusId == 0 ? x.ApprovedStatus == 2 : x.ApprovedStatus == model.StatusId))
                                        .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
            }
        }
    }
}
