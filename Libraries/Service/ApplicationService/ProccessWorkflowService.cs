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

    public class ProccessWorkflowService : EntityService<Processworkflow>, IProccessWorkflowService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProccessWorkflowRepository _proccessWorkflowRepository;

        public ProccessWorkflowService(IUnitOfWork unitOfWork, IProccessWorkflowRepository proccessWorkflowRepository)
        : base(unitOfWork, proccessWorkflowRepository)
        {
            _unitOfWork = unitOfWork;
            _proccessWorkflowRepository = proccessWorkflowRepository;
        }
        public async Task<bool> Create(Processworkflow proccess)
        {
            proccess.CreatedDate = DateTime.Now;
            _proccessWorkflowRepository.Add(proccess);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public int GetPreviousApprovalId(string proccessguid, int serviceid)
        {
            return _proccessWorkflowRepository.GetPreviousApprovalId(proccessguid, serviceid);
        }
        public async Task<List<Approvalproccess>> GetHistoryDetails(string proccessguid, int id)
        {
            return await _proccessWorkflowRepository.GetHistoryDetails(proccessguid, id);
        }

        public int FetchCountResultForProccessWorkflow(int workflowTemplateId)
        {
            return _proccessWorkflowRepository.FetchCountResultForProccessWorkflow(workflowTemplateId);
        }
    }
}
