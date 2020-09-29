using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;


namespace Libraries.Repository.IEntityRepository
{
    public interface IClassificationOfLandRepository : IGenericRepository<Classificationofland>
    {

        Task<bool> Any(int id, string name);
        Task<PagedResult<Classificationofland>> GetPagedClassificationOfLand(ClassificationOfLandSearchDto model);
    }
}