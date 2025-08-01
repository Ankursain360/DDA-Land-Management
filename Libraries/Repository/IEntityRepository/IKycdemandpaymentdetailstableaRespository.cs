﻿using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
namespace Libraries.Repository.IEntityRepository
{
    public interface  IKycdemandpaymentdetailstableaRespository : IGenericRepository<Kycdemandpaymentdetailstablea>
    {
        Task<List<Kycdemandpaymentdetailstablea>> FetchResult(int id);
        Task<bool> RollBackEntry(int Id);//added by ishu
    }
}
