
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
        Task<List<Locality>> GetLocalityList(int? zoneid);
        Task<List<Kycform>> GetAllKycform();
        Task<Kycform> FetchKYCSingleResult(int id);
        Task<PagedResult<Kycform>> GetPagedKycform(KycformSearchDto model);
        Task<bool> Saveleasepayment(Kycleasepaymentrpt payment);
        Task<bool> Savelicensepayment(Kyclicensepaymentrpt payment);

        //KYC Approval process methods : Added by ishu 20/7/2021
        Task<Kycworkflowtemplate> FetchSingleResultOnProcessGuid(string processguid);
        Task<bool> CreatekycApproval(Kycapprovalproccess kycapproval);
        Task<List<Kycworkflowtemplate>> GetWorkFlowDataOnGuid(string processguid);

        Task<List<KycFormApprovalDetailsSearchDto>> GetKycFormApprovalDetails(int Id, string ApprovalType);
    }
}
