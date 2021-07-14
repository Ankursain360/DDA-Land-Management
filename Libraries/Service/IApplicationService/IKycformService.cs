
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

    public interface IKycformService : IEntityService<Kycform>
    {

        Task<List<Leasetype>> GetAllLeasetypeList();
        Task<List<Branch>> GetAllBranchList();
        Task<List<PropertyType>> GetAllPropertyTypeList();
        Task<List<Zone>> GetAllZoneList();
        Task<List<Locality>> GetLocalityList();
        Task<bool> Create(Kycform kyc);

        Task<bool> Saveleasepayment(Kycleasepaymentrpt payment);
        Task<bool> Savelicensepayment(Kyclicensepaymentrpt payment);
    }
}
