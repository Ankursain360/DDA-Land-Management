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
        public async Task<PagedResult<Damagepayeeregistertemp>> GetPagedDamagePayeeRegisterForApproval(DamagepayeeRegisterApprovalDto model)
        {
            return await _dbContext.Damagepayeeregistertemp
                                    .Include(x => x.Locality)
                                    .Include(x => x.District)
                                    .Where(x => x.IsActive == 1 && x.ApprovedStatus == model.StatusId)
                                    .GetPaged<Damagepayeeregistertemp>(model.PageNumber, model.PageSize);
        }
    }
}
