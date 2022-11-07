
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
    public class RequestApprovalProcessService : EntityService<Request>, IRequestApprovalProcessService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRequestApprovalProcessRepository _requestApprovalProcessRepository;


        public RequestApprovalProcessService(IUnitOfWork unitOfWork, IRequestApprovalProcessRepository requestApprovalProcessRepository)
    : base(unitOfWork, requestApprovalProcessRepository)
        {
            _unitOfWork = unitOfWork;
            _requestApprovalProcessRepository = requestApprovalProcessRepository;
        }

        public async Task<Request> FetchSingleResult(int id)
        {
            return await _requestApprovalProcessRepository.FetchSingleResult(id);
        }
        public async Task<PagedResult<Request>> GetPagedProcessRequest(RequestApprovalSearchDto model, int userId)
        {
            return await _requestApprovalProcessRepository.GetPagedProcessRequest(model, userId);
        }
        public async Task<bool> IsApplicationPendingAtUserEnd(int id, int userId)
        {
            return await _requestApprovalProcessRepository.IsApplicationPendingAtUserEnd(id, userId);
        }
        public async Task<List<Request>> GetAllRequest()
        {
            return await _requestApprovalProcessRepository.GetAllRequest();
        }
        public async Task<List<Request>> GetAllProcessRequestList(RequestApprovalSearchDto model, int userId)
        {
            return await _requestApprovalProcessRepository.GetAllProcessRequestList(model, userId);
        }

    }
}
