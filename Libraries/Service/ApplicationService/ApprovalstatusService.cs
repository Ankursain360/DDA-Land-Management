using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ApplicationService
{
    public class ApprovalstatusService : EntityService<Approvalstatus>, IApprovalstatusService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApprovalstatusRepository _approvalstatusRepository;
        public ApprovalstatusService(IUnitOfWork unitOfWork, IApprovalstatusRepository approvalstatusRepository)
        : base(unitOfWork, approvalstatusRepository)
        {
            _unitOfWork = unitOfWork;
            _approvalstatusRepository = approvalstatusRepository;
        }
        public async Task<PagedResult<Approvalstatus>> GetPagedApprovalStatus(ApprovalstatusSearchDto model)
        {
            return await _approvalstatusRepository.GetPagedApprovalStatus(model);
        }

        public async Task<List<Approvalstatus>> GetAllApprovalstatus()
        {
            return await _approvalstatusRepository.GetAllApprovedstatus();
        } 
        public async Task<bool> Create(Approvalstatus approvalstatus)
        {
            approvalstatus.CreatedBy = 1;
            approvalstatus.CreatedDate = DateTime.Now;            
            _approvalstatusRepository.Add(approvalstatus);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> Update(int id, Approvalstatus approvalstatus)
        {
            var result = await _approvalstatusRepository.FindBy(a => a.Id == id);
            Approvalstatus model = result.FirstOrDefault();
            
            model.StatusCode = approvalstatus.StatusCode;         
            model.SentStatusName = approvalstatus.SentStatusName;         
            model.Name = approvalstatus.Name;         
            model.IsActive = approvalstatus.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _approvalstatusRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<Approvalstatus> FetchSingleResult(int id)
        {
            var result = await _approvalstatusRepository.FindBy(a => a.Id == id);
            Approvalstatus model = result.FirstOrDefault();
            return model;
        }
        public async Task<bool> Delete(int id)
        {
            var form = await _approvalstatusRepository.FindBy(a => a.Id == id);
            Approvalstatus model = form.FirstOrDefault();
            model.IsActive = 0;
            _approvalstatusRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
