using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Microsoft.EntityFrameworkCore;
using Libraries.Repository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Service.ApplicationService
{
  public  class RequestService : EntityService<Request>, IRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRequestRepository _requestRepository;



        public RequestService(IUnitOfWork unitOfWork, IRequestRepository requestRepository)
      : base(unitOfWork, requestRepository)
        {
            _unitOfWork = unitOfWork;
            _requestRepository = requestRepository;
        }
        public async Task<List<Request>> GetAllRequest()
        {

            return await _requestRepository.GetAllRequest();
        }
        public async Task<List<Request>> GetAllRequestList(RequestSearchDto model)
        {
            return await _requestRepository.GetAllRequestList(model);
        }
        public async Task<bool> Create(Request request)
        {

            request.CreatedBy = 1;
            request.CreatedDate = DateTime.Now;
            _requestRepository.Add(request);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<List<Request>> GetRequestUsingRepo()
        {
            return await _requestRepository.GetAllRequest();
        }

        public async Task<bool> Update(int id, Request scheme)
        {
            var result = await _requestRepository.FindBy(a => a.Id == id);
            Request model = result.FirstOrDefault();
            model.PproposalName = scheme.PproposalName;
            model.PfileNo = scheme.PfileNo;
            model.RequiringBody = scheme.RequiringBody;
            model.AreaLocality = scheme.AreaLocality;
            model.TaunderRequest = scheme.TaunderRequest;

            model.UnitArea = scheme.UnitArea;
            model.PurposeOfAcquistion = scheme.PurposeOfAcquistion;
            model.LayoutPlan = scheme.LayoutPlan;
            model.Remarks = scheme.Remarks;

            model.IsActive = scheme.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _requestRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Request> FetchSingleResult(int id)
        {
            var result = await _requestRepository.FindBy(a => a.Id == id);
            Request model = result.FirstOrDefault();
            return model;
        }


        public async Task<bool> Delete(int id)
        {
            var form = await _requestRepository.FindBy(a => a.Id == id);
            Request model = form.FirstOrDefault();
            model.IsActive = 0;
            _requestRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

      


        public async Task<PagedResult<Request>> GetPagedRequest(RequestSearchDto model)
        {
            return await _requestRepository.GetPagedRequest(model);
        }

        public async Task<bool> UpdateBeforeApproval(int id, Request request)
        {
            var result = await _requestRepository.FindBy(a => a.Id == id);
            Request model = result.FirstOrDefault();

            model.ApprovedStatus = request.ApprovedStatus;
            model.PendingAt = request.PendingAt;
            _requestRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<List<TrackingListDataDto>> GetPagedTrackingList(TrackingListSearchDto model)
        {
            return await _requestRepository.GetPagedTrackingList(model);
        }

        public async Task<bool> RollBackEntry(int id)
        {
            var result = await _requestRepository.FindBy(a => a.Id == id);
            Request model = result.FirstOrDefault();
            _requestRepository.Delete(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
