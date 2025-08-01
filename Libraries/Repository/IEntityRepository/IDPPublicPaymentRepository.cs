﻿

using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{

    public interface IDPPublicPaymentRepository : IGenericRepository<Demandletters>
    {
        Task<Damagepayeeregister> FetchDamagePayeeRegisterDetails(int userId);
        Task<Demandletters> FatchDemandPaymentDetails(string FileNo);
        Task<List<Demandletters>> GetDemandDetails(string FileNo);
    }
}
