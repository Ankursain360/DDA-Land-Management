using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface ISakanidetailRepository : IGenericRepository<Sakanidetail>
    {
        Task<PagedResult<Sakanidetail>> GetPagedSakanidetail(SakaniDetailsSearchDto model);
        Task<List<Sakanidetail>> GetSakanidetail();
        Task<List<Khewat>> GetAllKhewat();
        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Khasra>> BindKhasra();
    }
}
