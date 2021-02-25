using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public interface INewLandPaymentDetailRepository : IGenericRepository<Newlandpaymentdetail>
    {
        Task<List<Newlandpaymentdetail>> GetPaymentdetail();

        Task<PagedResult<Newlandpaymentdetail>> GetPagedPaymentdetail(NewLandPaymentDetailSearchDto model);
    }
}
