using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Libraries.Repository.IEntityRepository
{
    public interface INewlandannexure2Repository : IGenericRepository<Newlandannexure2>
    {
        Task<PagedResult<Newlandannexure2>> GetPagedNewlandannexure2(Newlandannexure1SearchDto model);

    }
}
