using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
namespace Libraries.Repository.IEntityRepository
{

    public interface IPaymentverificationRepository : IGenericRepository<Paymentverification>
    {
        Task<List<Paymentverification>> GetAllPaymentList();
        Task<PagedResult<Paymentverification>> GetPagedPaymentList(PaymentverificationSearchDto model);
        Task<PagedResult<Paymentverification>> GetPagedPaymentList2(PaymentverificationSearchDto model);
    }
}
