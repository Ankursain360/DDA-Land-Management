
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
            var result = await _requestApprovalProcessRepository.FindBy(a => a.Id == id);
            Request model = result.FirstOrDefault();
            return model;
        }




        public async Task<PagedResult<Request>> GetPagedProcessRequest(RequestApprovalSearchDto model, int userId)
        {
            return await _requestApprovalProcessRepository.GetPagedProcessRequest(model, userId);
        }




    }
}
