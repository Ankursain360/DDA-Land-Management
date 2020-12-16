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
    public class DoortodoorsurveyRepository : GenericRepository<Doortodoorsurvey>, IDoortodoorsurveyRepository
    {
        public DoortodoorsurveyRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Presentuse>> GetAllPresentuse()
        {
            List<Presentuse> presentuseList = await _dbContext.Presentuse.Where(x => x.IsActive == 1).ToListAsync();
            return presentuseList;
        }


        public async Task<List<Doortodoorsurvey>> GetDoortodoorsurvey()
        {
            return await _dbContext.Doortodoorsurvey.Include(x => x.PresentUseNavigation).OrderByDescending(x => x.Id).ToListAsync();
        }


        public async Task<PagedResult<Doortodoorsurvey>> GetPagedDoortodoorsurvey(DoortodoorsurveySearchDto model)
        {
            return await _dbContext.Doortodoorsurvey.Include(x => x.PresentUseNavigation).OrderByDescending(x => x.Id).GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);
        }




    }
}
