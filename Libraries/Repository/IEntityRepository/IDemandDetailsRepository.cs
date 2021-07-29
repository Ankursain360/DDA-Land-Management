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
    public interface IDemandDetailsRepository : IGenericRepository<Kycform>
    {
        Task<PagedResult<Kycform>> GetPagedDemandDetails(DemandDetailsSearchDto model, string MobileNo);


        Task<List<DemandPaymentDetailsDto>> GetPaymentDetails(int FileNo);



    }
}
