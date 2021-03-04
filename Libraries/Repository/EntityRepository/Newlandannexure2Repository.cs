using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Libraries.Repository.EntityRepository
{
    public class Newlandannexure2Repository : GenericRepository<Newlandannexure2>, INewlandannexure2Repository
    {
        public Newlandannexure2Repository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Newlandannexure2>> GetPagedNewlandannexure2(Newlandannexure1SearchDto model)
        {
            var data = await _dbContext.Newlandannexure2
                             .GetPaged<Newlandannexure2>(model.PageNumber, model.PageSize);

            return data;
        }

    }
}
