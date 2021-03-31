
using Dto.Master;
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

namespace Libraries.Service.ApplicationService
{

    public class JudgementService : EntityService<Judgement>, IJudgementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJudgementRepository _judgementRepository;

        public JudgementService(IUnitOfWork unitOfWork, IJudgementRepository judgementRepository)
        : base(unitOfWork, judgementRepository)
        {
            _unitOfWork = unitOfWork;
            _judgementRepository = judgementRepository;
        }
        public async Task<PagedResult<Requestforproceeding>> GetPagedRequestForProceeding(RequestForProceedingSearchDto model)
        {
            var data =  await _judgementRepository.GetPagedRequestForProceeding(model);
            return data;
        }
        public async Task<List<UserBindDropdownDto>> BindUsernameNameList()
        {
            return await _judgementRepository.BindUsernameNameList();
        }

        public async Task<List<Allotmententry>> GetAllAllotment()
        {
            List<Allotmententry> List = await _judgementRepository.GetAllAllotment();
            return List;
        }

        public async Task<List<Honble>> GetAllHonble()
        {
            List<Honble> List = await _judgementRepository.GetAllHonble();
            return List;
        }
        public async Task<Requestforproceeding> FetchSingleReqDetails(int? RequestId)
        {
            return await _judgementRepository.FetchSingleReqDetails(RequestId);
        }
        public async Task<List<Leasenoticegeneration>> FetchNoticeGenerationDetails(int? RequestId)
        {
            return await _judgementRepository.FetchNoticeGenerationDetails(RequestId);
        }

        public async Task<Leasenoticegeneration> FetchSingleNotice(int? id)
        {
            return await _judgementRepository.FetchSingleNotice(id);
        }

        public async Task<List<Allotteeevidenceupload>> FetchAllotteeEvidenceDetails(int? RequestId)
        {
            return await _judgementRepository.FetchAllotteeEvidenceDetails(RequestId);
        }

        public async Task<Allotteeevidenceupload> FetchSingleEvidence(int? id)
        {
            return await _judgementRepository.FetchSingleEvidence(id);
        }
        public async Task<List<Hearingdetails>> FetchHearingDetails(int? RequestId)
        {
            return await _judgementRepository.FetchHearingDetails(RequestId);
        }
        public async Task<bool> Update(int id, Judgement judge)
        {
           
            Judgement model = await _judgementRepository.FetchSingleResult(id);
            model.ForwardToUserId = judge.ForwardToUserId;
            model.FilePath = judge.FilePath;
            model.JudgementStatusId = judge.JudgementStatusId;
            model.Remarks = judge.Remarks;
            model.IsActive = 1;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = judge.ModifiedBy;
            _judgementRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> Create(Judgement judge)
        {
           
            judge.CreatedDate = DateTime.Now;
            _judgementRepository.Add(judge);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<Judgement> FetchSingleResult(int id)
        {

            return await _judgementRepository.FetchSingleResult(id);
          
        }
        public async Task<bool> Delete(int id)
        {
            var form = await _judgementRepository.FindBy(a => a.Id == id);
            Judgement model = form.FirstOrDefault();
            model.IsActive = 0;
            _judgementRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<List<Judgementstatus>> GetJudgementStatusList()
        {
            List<Judgementstatus> List = await _judgementRepository.GetJudgementStatusList();
            return List;
        }
    }
}
