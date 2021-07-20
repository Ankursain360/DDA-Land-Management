

using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{

    public interface IKycformApprovalRepository : IGenericRepository<Kycform>
    {
        
      //  Task<List<Branch>> GetAllBranchList();
       
        //Task<List<Kycform>> GetAllKycform();
        //Task<PagedResult<Kycform>> GetPagedKycform(KycformSearchDto model);
      

    }
}
