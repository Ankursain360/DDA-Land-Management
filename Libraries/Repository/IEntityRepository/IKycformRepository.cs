
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{

    public interface IKycformRepository : IGenericRepository<Kycform>
    {
        Task<List<Leasetype>> GetAllLeasetypeList();
        Task<List<Branch>> GetAllBranchList();
        Task<List<PropertyType>> GetAllPropertyTypeList();
        Task<List<Zone>> GetAllZoneList();
        Task<List<Locality>> GetLocalityList();
        Task<List<Kycform>> GetAllKycform();
        Task<PagedResult<Kycform>> GetPagedKycform(KycformSearchDto model);
        Task<bool> Saveleasepayment(Kycleasepaymentrpt payment);
        Task<bool> Savelicensepayment(Kyclicensepaymentrpt payment);

    }
}
