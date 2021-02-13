using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public interface IPaymentdetailRepository : IGenericRepository<Paymentdetail>
    {
        Task<List<Paymentdetail>> GetPaymentdetail();

        Task<PagedResult<Paymentdetail>> GetPagedPaymentdetail(PaymentdetailSearchDto model);
    }
}


