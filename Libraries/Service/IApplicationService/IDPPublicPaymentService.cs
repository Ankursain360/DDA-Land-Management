


using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{

    public interface IDPPublicPaymentService : IEntityService<Demandletters>
    {
        Task<Damagepayeeregister> FetchDamagePayeeRegisterDetails(int userId);
        // Task<PagedResult<Damagepayeeregister>> GetPagedDamagepayeeregister(DamagepayeeregistertempSearchDto model);
        Task<List<Demandletters>> GetDemandDetails(string FileNo);

    }
}
