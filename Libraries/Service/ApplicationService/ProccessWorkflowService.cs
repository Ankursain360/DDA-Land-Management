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


        //public async Task<bool> Update(int id, Approvalproccess approvalproccess, int userId)
        //{
        //    var result = await _proccessWorkflowRepository.FindBy(a => a.Id == id);
        //    Approvalproccess model = result.FirstOrDefault();
        //    model.ModifiedBy = userId;
        //    model.ModifiedDate = DateTime.Now;
        //    _proccessWorkflowRepository.Edit(model);
        //    return await _unitOfWork.CommitAsync() > 0;
        //}

        public async Task<bool> Create(Processworkflow proccess)
        {
            proccess.CreatedDate = DateTime.Now;
            _proccessWorkflowRepository.Add(proccess);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public int GetPreviousApprovalId(int proccessid, int serviceid)
        {
            return _proccessWorkflowRepository.GetPreviousApprovalId(proccessid, serviceid);
        }

        //public async Task<bool> UpdatePreviousApprovalProccess(int previousApprovalId, Processworkflow processworkflow, int userId)
        //{
        //    var result = await _proccessWorkflowRepository.FindBy(a => a.Id == previousApprovalId);
        //    Approvalproccess model = result.FirstOrDefault();
        //    model.Remarks = approvalproccess.Remarks;
        //    model.PendingStatus = approvalproccess.PendingStatus;
        //    model.Status = approvalproccess.Status;
        //    model.ModifiedBy = userId;
        //    model.ModifiedDate = DateTime.Now;
        //    _proccessWorkflowRepository.Edit(model);
        //    return await _unitOfWork.CommitAsync() > 0;
        //}

        public async Task<List<Approvalproccess>> GetHistoryDetails(int proccessid, int id)
        {
            return await _proccessWorkflowRepository.GetHistoryDetails(proccessid, id);
        }

        public int FetchCountResultForProccessWorkflow(int workflowTemplateId)
        {
            return _proccessWorkflowRepository.FetchCountResultForProccessWorkflow(workflowTemplateId);
        }
    }
}
