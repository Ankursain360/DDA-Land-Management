using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.Common;
using Dto.Search;
using Libraries.Repository.Common;

namespace Libraries.Service.IApplicationService
{
    public interface INewLandPaymentdetailService : IEntityService<Newlandpaymentdetail>
    {
        Task<List<Newlandpaymentdetail>> GetAllPaymentdetail();
        Task<List<Newlandpaymentdetail>> GetPaymentdetailUsingRepo();

        Task<bool> Update(int id, Newlandpaymentdetail newlandpaymentdetail);
        Task<bool> Create(Newlandpaymentdetail newlandpaymentdetail);
        Task<Newlandpaymentdetail> FetchSingleResult(int id);
        Task<bool> Delete(int id);


        Task<PagedResult<Newlandpaymentdetail>> GetPagedPaymentdetail(NewLandPaymentDetailSearchDto model);



    }
}

