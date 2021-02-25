using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Dto.Master;

namespace Libraries.Repository.IEntityRepository
{
    public interface InewlandawardplotdetailsRepository : IGenericRepository<Newlandawardplotdetails>
    {
        Task<List<Newlandawardplotdetails>> GetAwardplotdetails();
        Task<List<Newlandawardmasterdetail>> GetAllAWardmaster();
        Task<List<Newlandvillage>> GetAllVillage();
        Task<List<Newlandkhasra>> BindKhasra();
        Task<PagedResult<Newlandawardplotdetails>> GetPagedAwardplotdetails(NewlandawardplotdetailsSearchDto model);
        
    }
}
