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

        public int GetPreviousApprovalId(int proccessid, int serviceid)
        {
            return _approvalproccessRepository.GetPreviousApprovalId(proccessid, serviceid);
        }

        public async Task<bool> UpdatePreviousApprovalProccess(int previousApprovalId, Approvalproccess approvalproccess, int userId)
        {
            var result = await _approvalproccessRepository.FindBy(a => a.Id == previousApprovalId);
            Approvalproccess model = result.FirstOrDefault();
            model.Remarks = approvalproccess.Remarks;
            model.PendingStatus = approvalproccess.PendingStatus;
            model.Status = approvalproccess.Status;
            model.ModifiedBy = userId;
            model.ModifiedDate = DateTime.Now;
            _approvalproccessRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<List<Approvalproccess>> GetHistoryDetails(int proccessid, int id)
        {
            return await _approvalproccessRepository.GetHistoryDetails(proccessid,  id);
        }
    }
}
