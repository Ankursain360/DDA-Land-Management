using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
namespace Libraries.Repository.IEntityRepository
{
    public interface IUndersection4Repository : IGenericRepository<Undersection4>
    {
        Task<List<Undersection4>> GetAllUndersection4();
        Task<List<Purpose>> GetAllPurpose();
        Task<bool> Any(int id, string number);
        Task<PagedResult<Undersection4>> GetPagedUndersection4details(Undersection4SearchDto model);

    }
}
