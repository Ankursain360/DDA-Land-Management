
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

    public class ActiontakenbyddaService : EntityService<Actiontakenbydda>, IActiontakenbyddaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActionTakenByDdaRepository _actionTakenByDdaRepository;

        public ActiontakenbyddaService(IUnitOfWork unitOfWork, IActionTakenByDdaRepository actionTakenByDdaRepository)
        : base(unitOfWork, actionTakenByDdaRepository)
        {
            _unitOfWork = unitOfWork;
            _actionTakenByDdaRepository = actionTakenByDdaRepository;
        }

        public async Task<List<Requestforproceeding>> GetAllActionIndex()
        {

            return await _actionTakenByDdaRepository.GetAllActionIndex();
        }
        public async Task<PagedResult<Requestforproceeding>> GetPagedRequestForProceeding(ActionTakenByDDASearchDto model)
        {
            var data = await _actionTakenByDdaRepository.GetPagedRequestForProceeding(model);
            return data;
        }
        public async Task<List<UserBindDropdownDto>> BindUsernameNameList()
        {
            return await _actionTakenByDdaRepository.BindUsernameNameList();
        }

        public async Task<List<Allotmententry>> GetAllAllotment()
        {
            List<Allotmententry> List = await _actionTakenByDdaRepository.GetAllAllotment();
            return List;
        }

        public async Task<List<Honble>> GetAllHonble()
        {
            List<Honble> List = await _actionTakenByDdaRepository.GetAllHonble();
            return List;
        }
        public async Task<Requestforproceeding> FetchSingleReqDetails(int? RequestId)
        {
            return await _actionTakenByDdaRepository.FetchSingleReqDetails(RequestId);
        }
        public async Task<List<Leasenoticegeneration>> FetchNoticeGenerationDetails(int? RequestId)
        {
            return await _actionTakenByDdaRepository.FetchNoticeGenerationDetails(RequestId);
        }

        public async Task<Leasenoticegeneration> FetchSingleNotice(int? id)
        {
            return await _actionTakenByDdaRepository.FetchSingleNotice(id);
        }

        public async Task<List<Allotteeevidenceupload>> FetchAllotteeEvidenceDetails(int? RequestId)
        {
            return await _actionTakenByDdaRepository.FetchAllotteeEvidenceDetails(RequestId);
        }

        public async Task<Allotteeevidenceupload> FetchSingleEvidence(int? id)
        {
            return await _actionTakenByDdaRepository.FetchSingleEvidence(id);
        }
        public async Task<List<Hearingdetails>> FetchHearingDetails(int? RequestId)
        {
            return await _actionTakenByDdaRepository.FetchHearingDetails(RequestId);
        }
        public async Task<bool> Update(int id, Actiontakenbydda actionbydda)
        {

            Actiontakenbydda model = await _actionTakenByDdaRepository.FetchSingleResult(id);
            model.HandedTakenByDda = actionbydda.HandedTakenByDda;
            model.HandedTakenByDdadate = actionbydda.HandedTakenByDdadate;
            model.PlotRestored = actionbydda.PlotRestored;
            model.CurrentStatus = actionbydda.CurrentStatus;
            model.Document = actionbydda.Document;
            model.IsActive = 1;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = actionbydda.ModifiedBy;
            _actionTakenByDdaRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> Create(Actiontakenbydda actionbydda)
        {

            actionbydda.CreatedDate = DateTime.Now;
            _actionTakenByDdaRepository.Add(actionbydda);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<Actiontakenbydda> FetchSingleResult(int id)
        {

            return await _actionTakenByDdaRepository.FetchSingleResult(id);

        }
        public async Task<bool> Delete(int id)
        {
            var form = await _actionTakenByDdaRepository.FindBy(a => a.Id == id);
            Actiontakenbydda model = form.FirstOrDefault();
            model.IsActive = 0;
            _actionTakenByDdaRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
