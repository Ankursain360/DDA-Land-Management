using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Libraries.Repository.IEntityRepository;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Libraries.Model;
using Dto.Search;

namespace Libraries.Service.ApplicationService
{

    public class ApprovalProccessService : EntityService<Approvalproccess>, IApprovalProccessService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApprovalProccessRepository _approvalproccessRepository;

        public ApprovalProccessService(IUnitOfWork unitOfWork, IApprovalProccessRepository approvalproccessRepository)
        : base(unitOfWork, approvalproccessRepository)
        {
            _unitOfWork = unitOfWork;
            _approvalproccessRepository = approvalproccessRepository;
        }


        public async Task<bool> Update(int id, Approvalproccess approvalproccess, int userId)
        {
            var result = await _approvalproccessRepository.FindBy(a => a.Id == id);
            Approvalproccess model = result.FirstOrDefault();
            model.ModifiedBy = userId;
            model.ModifiedDate = DateTime.Now;
            _approvalproccessRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Approvalproccess approvalproccess, int userId)
        {

            approvalproccess.CreatedBy = userId;
            approvalproccess.CreatedDate = DateTime.Now;
            _approvalproccessRepository.Add(approvalproccess);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public int GetPreviousApprovalId(string proccessguid, int serviceid)
        {
            return _approvalproccessRepository.GetPreviousApprovalId(proccessguid, serviceid);
        }
        public int GetPreviouskycApprovalId(string proccessguid, int serviceid)//added by ishu 22/7/2021
        {
            return _approvalproccessRepository.GetPreviouskycApprovalId(proccessguid, serviceid);
        }

        public async Task<bool> UpdatePreviousApprovalProccess(int previousApprovalId, Approvalproccess approvalproccess, int userId)
        {
            var result = await _approvalproccessRepository.FindBy(a => a.Id == previousApprovalId);
            Approvalproccess model = result.FirstOrDefault();
            model.PendingStatus = approvalproccess.PendingStatus;
            model.ModifiedBy = userId;
            model.ModifiedDate = DateTime.Now;
            _approvalproccessRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> UpdatePreviouskycApprovalProccess(int previousApprovalId, Kycapprovalproccess approvalproccess, int userId) //23 july 2021  ishu
        {
           return await _approvalproccessRepository.UpdatePreviouskycApprovalProccess(previousApprovalId, approvalproccess, userId);
        }


        public async Task<List<ApprovalHistoryListDataDto>> GetHistoryDetails(string proccessguid, int id)
        {
            return await _approvalproccessRepository.GetHistoryDetails(proccessguid, id);
        }
        public async Task<List<ApprovalHistoryListDataDto>> GetKYCHistoryDetails(string proccessguid, int id)//for kycapproval history
        {
            return await _approvalproccessRepository.GetKYCHistoryDetails(proccessguid, id);
        }
        public async Task<List<ApprovalHistoryListDataDto>> GetKYCPaymentHistoryDetails(string proccessguid, int id)//for kycapproval history
        {
            return await _approvalproccessRepository.GetKYCPaymentHistoryDetails(proccessguid, id);
        }

        public int CheckIsApprovalStart(string proccessguid, int serviceid)
        {
            return _approvalproccessRepository.CheckIsApprovalStart(proccessguid, serviceid);
        }

        public async Task<Approvalproccess> FetchApprovalProcessDocumentDetails(int id)
        {
            return await _approvalproccessRepository.FetchApprovalProcessDocumentDetails(id);
        }

        public async Task<Kycapprovalproccess> FetchKYCApprovalProcessDocumentDetails(int id)//added by ishu 22/7/2021
        {
            return await _approvalproccessRepository.FetchKYCApprovalProcessDocumentDetails(id);
        }

        public async Task<List<Approvalstatus>> BindDropdownApprovalStatus(int[] actions)
        {
            return await _approvalproccessRepository.BindDropdownApprovalStatus(actions);
        }

        public async Task<Approvalstatus> FetchSingleApprovalStatus(int id)
        {
            return await _approvalproccessRepository.FetchSingleApprovalStatus(id);
        }
       

        public async Task<Approvalstatus> GetStatusIdFromStatusCode(int statuscode)
        {
            return await _approvalproccessRepository.GetStatusIdFromStatusCode(statuscode);
        }

        public async Task<bool> RollBackEntry(string processguid, int serviceid)
        {
            var result = await _approvalproccessRepository.FindBy(a => a.ProcessGuid == processguid && a.ServiceId == serviceid);
            Approvalproccess model = result.FirstOrDefault();
            if (model != null)
            {
                _approvalproccessRepository.Delete(model);
                return await _unitOfWork.CommitAsync() > 0;
            }
            else
            {
                return true;
            }
        }
        public async Task<bool> KycRollBackEntry(string processguid, int serviceid)//added by ishu
        {
            return await _approvalproccessRepository.KycRollBackEntry(processguid, serviceid);
            
        }
        public async Task<Approvalproccess> FirstApprovalProcessData(string processguid, int serviceid)
        {
            return await _approvalproccessRepository.FirstApprovalProcessData(processguid, serviceid);
        }
        public async Task<Kycapprovalproccess> FirstkycApprovalProcessData(string processguid, int serviceid)//added by ishu 22/7/2021
        {
            return await _approvalproccessRepository.FirstkycApprovalProcessData(processguid, serviceid);
        }

        public async Task<Approvalproccess> CheckLastUserForRevert(string processguid, int serviceid, int level)
        {
            return await _approvalproccessRepository.CheckLastUserForRevert( processguid,  serviceid,  level);
        }
        public async Task<Kycapprovalproccess> CheckLastKycUserForRevert(string processguid, int serviceid, int level)//added by ishu 23/7/2021
        {
            return await _approvalproccessRepository.CheckLastKycUserForRevert(processguid, serviceid, level);
        }

        public async Task<Kycapprovalproccess> KycUserDeficiencyForRevert(string processguid, int serviceid, int level)//added by ishu 23/7/2021
        {
            return await _approvalproccessRepository.KycUserDeficiencyForRevert(processguid, serviceid, level);
        }

        public async Task<Kycapprovalproccess> KycUserResubmitForApproval(string processguid, int serviceid, int level)//added by ishu 27/7/2021
        {
            return await _approvalproccessRepository.KycUserResubmitForApproval(processguid, serviceid, level);
        }
        public async Task<ApplicationNotificationTemplate> FetchSingleNotificationTemplate(string guid)
        {
            return await _approvalproccessRepository.FetchSingleNotificationTemplate(guid);
        }
    }
}
