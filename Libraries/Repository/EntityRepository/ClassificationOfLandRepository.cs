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
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
    public class ClassificationOfLandRepository : GenericRepository<Classificationofland>, IClassificationOfLandRepository
    {

        public ClassificationOfLandRepository(DataContext dbContext) : base(dbContext)
        {

        }


        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Classificationofland.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }

        public async Task<PagedResult<Classificationofland>> GetPagedClassificationOfLand(ClassificationOfLandSearchDto model)
        {
            return await _dbContext.Classificationofland.OrderBy(s => s.Id).GetPaged<Classificationofland>(model.PageNumber, model.PageSize);
        }
    }


}
