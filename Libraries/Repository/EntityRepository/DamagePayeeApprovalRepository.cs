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

    public class DamagePayeeApprovalRepository : GenericRepository<Damagepayeeregistertemp>, IDamagePayeeApprovalRepository
    {
        public DamagePayeeApprovalRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Damagepayeeregistertemp>> GetPagedDamagePayeeRegisterForApproval(DamagepayeeRegisterApprovalDto model, bool IsUser)
        {
            if (IsUser)
            {

                return await _dbContext.Damagepayeeregistertemp
                                        .Include(x => x.Locality)
                                        .Include(x => x.District)
                                        .Where(x => x.IsActive == 1 && x.ApprovedStatus == model.StatusId)
                                        .GetPaged<Damagepayeeregistertemp>(model.PageNumber, model.PageSize);
            }
            else
            {
                return await _dbContext.Damagepayeeregistertemp
                                        .Include(x => x.Locality)
                                        .Include(x => x.District)
                                        .Where(x => x.IsActive == 1 
                                        && ( model.StatusId == 0 ? x.ApprovedStatus == 2 : x.ApprovedStatus == model.StatusId))
                                        .GetPaged<Damagepayeeregistertemp>(model.PageNumber, model.PageSize);
            }
        }
    }
}
