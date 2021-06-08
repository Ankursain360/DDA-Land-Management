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

    public class UserNotificationService : EntityService<Usernotification>, IUserNotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserNotificationRepository _userNotificationRepository;

        public UserNotificationService(IUnitOfWork unitOfWork, IUserNotificationRepository userNotificationRepository)
        : base(unitOfWork, userNotificationRepository)
        {
            _unitOfWork = unitOfWork;
            _userNotificationRepository = userNotificationRepository;
        }


        public async Task<bool> Update(int id, Usernotification usernotification, int userId)
        {
            var result = await _userNotificationRepository.FindBy(a => a.Id == id);
            Usernotification model = result.FirstOrDefault();
            model.ModifiedBy = userId;
            model.ModifiedDate = DateTime.Now;
            _userNotificationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Usernotification usernotification, int userId)
        {

            usernotification.CreatedBy = userId;
            usernotification.CreatedDate = DateTime.Now;
            _userNotificationRepository.Add(usernotification);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public int GetPreviousApprovalId(string proccessguid, int serviceid)
        {
            return _userNotificationRepository.GetPreviousApprovalId(proccessguid, serviceid);
        }

        public async Task<List<ApprovalHistoryListDataDto>> GetHistoryDetails(string proccessguid, int id)
        {
            return await _userNotificationRepository.GetHistoryDetails(proccessguid, id);
        }
        public int CheckIsApprovalStart(string proccessguid, int serviceid)
        {
            return _userNotificationRepository.CheckIsApprovalStart(proccessguid, serviceid);
        }

        public async Task<Approvalproccess> FetchApprovalProcessDocumentDetails(int id)
        {
            return await _userNotificationRepository.FetchApprovalProcessDocumentDetails(id);
        }

        public async Task<List<Approvalstatus>> BindDropdownApprovalStatus(int[] actions)
        {
            return await _userNotificationRepository.BindDropdownApprovalStatus(actions);
        }

        public async Task<Approvalstatus> FetchSingleApprovalStatus(int id)
        {
            return await _userNotificationRepository.FetchSingleApprovalStatus(id);
        }

        public async Task<Approvalstatus> GetStatusIdFromStatusCode(int statuscode)
        {
            return await _userNotificationRepository.GetStatusIdFromStatusCode(statuscode);
        }

        public async Task<bool> RollBackEntry(string processguid, int serviceid)
        {
            var result = await _userNotificationRepository.FindBy(a => a.ProcessGuid == processguid && a.ServiceId == serviceid);
            Usernotification model = result.FirstOrDefault();
            _userNotificationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<Approvalproccess> FirstApprovalProcessData(string processguid, int serviceid)
        {
            return await _userNotificationRepository.FirstApprovalProcessData(processguid, serviceid);
        }

        public async Task<Approvalproccess> CheckLastUserForRevert(string processguid, int serviceid, int level)
        {
            return await _userNotificationRepository.CheckLastUserForRevert(processguid, serviceid, level);
        }
    }
}
