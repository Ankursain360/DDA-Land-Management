
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
        Task<List<Locality>> GetLocalityList(int? zoneid);
        Task<bool> Create(Kycform kyc);

        Task<List<Kycform>> GetAllKycform();
        Task<Kycform> FetchSingleResult(int id);
        Task<Kycform> FetchKYCSingleResult(int id);
        Task<bool> Update(int id, Kycform kyc);

        Task<PagedResult<Kycform>> GetPagedKycform(KycformSearchDto model);

        Task<bool> Delete(int id);
        Task<bool> Saveleasepayment(Kycleasepaymentrpt payment);
        Task<bool> Savelicensepayment(Kyclicensepaymentrpt payment);

        //KYC Approval process methods : Added by ishu 20/7/2021
        Task<Kycworkflowtemplate> FetchSingleResultOnProcessGuid(string processguid);
        Task<List<Kycworkflowtemplate>> GetWorkFlowDataOnGuid(string processguid);
        Task<bool> UpdateBeforeApproval(int id, Kycform kyc);
        Task<bool> UpdateBeforeApproval1(int id, Kycform kyc); //in case of approved
        Task<bool> CreatekycApproval(Kycapprovalproccess kycapproval, int userId);
        Task<List<Kycform>> GetAlldownloadKycform(string mobileno);
        Task<List<KycFormApprovalDetailsSearchDto>> GetKycFormApprovalDetails(int Id, string ApprovalType);

    }
}
