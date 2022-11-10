using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
namespace Libraries.Repository.IEntityRepository
{
    public interface IUnderSection6Repository : IGenericRepository<Undersection6>
    {

        Task<List<Undersection6>> GetAllUndersection6();
        Task<List<Undersection6>> GetAllUndersection6detailsList(Undersection6SearchDto model);
        Task<List<Undersection4>> GetAllundersection4();
     //   Task<bool> Any(int id, string number);
        Task<PagedResult<Undersection6>> GetPagedUndersection6details(Undersection6SearchDto model);


    }
}
