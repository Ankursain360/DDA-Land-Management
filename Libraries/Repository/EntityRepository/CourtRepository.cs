using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Libraries.Repository.EntityRepository
{
    public class CourtRepository : GenericRepository<Court>, ICourtRepository
    {

        public CourtRepository(DataContext dbcontext) : base(dbcontext)
        { }

        public async Task<List<Court>> GetAllCourt()
        {
            return await _dbContext.Court.Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<PagedResult<Court>> GetPagedCourt(CourtSearchDto model)
        {
            return await _dbContext.Court.OrderByDescending(x => x.Id).GetPaged<Court>(model.PageNumber, model.PageSize);
            //return await _dbContext.Court
            //                .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
            //                  && (string.IsNullOrEmpty(model.address) || x.Address.Contains(model.address))
            //                  && (string.IsNullOrEmpty(model.phoneno) || x.PhoneNo.Contains(model.phoneno)))
                           
            //                .OrderByDescending(s => s.IsActive)

            //            .GetPaged<Court>(model.PageNumber, model.PageSize);
        }
    }
}
