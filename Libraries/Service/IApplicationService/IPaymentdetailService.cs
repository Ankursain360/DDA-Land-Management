using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.Common;
using Dto.Search;
using Libraries.Repository.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IPaymentdetailService : IEntityService<Paymentdetail>
    {
        Task<List<Paymentdetail>> GetAllPaymentdetail();
        Task<List<Paymentdetail>> GetPaymentdetailUsingRepo();

        Task<bool> Update(int id, Paymentdetail paymentdetail);
        Task<bool> Create(Paymentdetail paymentdetail);
        Task<Paymentdetail> FetchSingleResult(int id);
        Task<bool> Delete(int id);


        Task<PagedResult<Paymentdetail>> GetPagedPaymentdetail(PaymentdetailSearchDto model);



    }
}

