using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
namespace Libraries.Repository.EntityRepository
{
    public class LeasepurposeRepository : GenericRepository<Leasepurpose>, ILeasepurposeRepository
    {
        public LeasepurposeRepository(DataContext dbcontext) : base(dbcontext)
        { }

        public async Task<List<Leasepurpose>> GetLeasepurposes()
        {
            return await _dbContext.Leasepurpose.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<PagedResult<Leasepurpose>> GetpagedLeasepurpose(LeasepurposeSearchDto model)
        {
            var data = await _dbContext.Leasepurpose
                  .Where(x => (string.IsNullOrEmpty(model.purposeUse) || x.PurposeUse.Contains(model.purposeUse)))
               
                 .GetPaged<Leasepurpose>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("purposeUse"):
                        data = null;
                        data = await _dbContext.Leasepurpose
                            .Where(x => (string.IsNullOrEmpty(model.purposeUse) || x.PurposeUse.Contains(model.purposeUse)))
                           .OrderBy(s => s.PurposeUse)
                           .GetPaged<Leasepurpose>(model.PageNumber, model.PageSize);

                        break;
                   
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Leasepurpose
                         .Where(x => (string.IsNullOrEmpty(model.purposeUse) || x.PurposeUse.Contains(model.purposeUse)))
                           .OrderByDescending(x => x.IsActive)
                           .GetPaged<Leasepurpose>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("purposeUse"):
                        data = null;
                        data = await _dbContext.Leasepurpose
                            .Where(x => (string.IsNullOrEmpty(model.purposeUse) || x.PurposeUse.Contains(model.purposeUse)))
                
                           .OrderByDescending(s => s.PurposeUse)
                           .GetPaged<Leasepurpose>(model.PageNumber, model.PageSize);

                        break;

                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Leasepurpose
                         .Where(x => (string.IsNullOrEmpty(model.purposeUse) || x.PurposeUse.Contains(model.purposeUse)))
                 
                           .OrderBy(x => x.IsActive)
                           .GetPaged<Leasepurpose>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;
        }


    }
}

