﻿using Libraries.Model.Entity;
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

    public class WorkflowTemplateService : EntityService<WorkflowTemplate>, IWorkflowTemplateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWorkflowTemplateRepository _workflowtemplateRepository;

        public WorkflowTemplateService(IUnitOfWork unitOfWork, IWorkflowTemplateRepository workflowtemplateRepository)
        : base(unitOfWork, workflowtemplateRepository)
        {
            _unitOfWork = unitOfWork;
            _workflowtemplateRepository = workflowtemplateRepository;
        }

        public async Task<List<WorkflowTemplate>> GetAllWorkflowTemplate()
        {
            return await _workflowtemplateRepository.GetAll();
        }

        public async Task<PagedResult<WorkflowTemplate>> GetPagedWorkflowTemplate(WorkflowTemplateSearchDto model)
        {
            return await _workflowtemplateRepository.GetPagedWorkflowTemplate(model);
        }

        public async Task<WorkflowTemplate> FetchSingleResult(int id)
        {
            var result = await _workflowtemplateRepository.FindBy(a => a.Id == id);
            WorkflowTemplate model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, WorkflowTemplate workflowtemplate)
        {
            var result = await _workflowtemplateRepository.FindBy(a => a.Id == id);
            WorkflowTemplate model = result.FirstOrDefault();
            model.Name = workflowtemplate.Name;
            model.Description = workflowtemplate.Description;
            model.ModuleId = workflowtemplate.ModuleId;
            model.Slatime = workflowtemplate.Slatime;
            model.EffectiveDate = workflowtemplate.EffectiveDate;
            model.Template = workflowtemplate.Template;
            model.ModifiedDate = DateTime.Now;
            model.IsActive = workflowtemplate.IsActive;
            _workflowtemplateRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(WorkflowTemplate workflowtemplate)
        {
            workflowtemplate.CreatedDate = DateTime.Now;
            _workflowtemplateRepository.Add(workflowtemplate);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<bool> CheckUniqueName(int id, string workflowtemplate)
        {
            bool result = await _workflowtemplateRepository.Any(id, workflowtemplate);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _workflowtemplateRepository.FindBy(a => a.Id == id);
            WorkflowTemplate model = form.FirstOrDefault();
            model.IsActive = 0;
            _workflowtemplateRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<List<Module>> GetAllModuleList()
        {
            return await _workflowtemplateRepository.GetAllModuleList();
        }

        public async Task<List<Approvalstatus>> GetApprovalStatusListData()
        {
            return await _workflowtemplateRepository.GetApprovalStatusListData();
        }

        public int ProcessGuidBasisCount(string ProcessGuid)
        {
            return _workflowtemplateRepository.ProcessGuidBasisCount(ProcessGuid);
        }

        public async Task<WorkflowTemplate> FetchSingleResultOnProcessGuid(string processguid)
        {
            return await _workflowtemplateRepository.FetchSingleResultOnProcessGuid(processguid);
        }

        public async Task<WorkflowTemplate> FetchSingleResultOnProcessGuidWithVersion(string processguid, string version)
        {
            return await _workflowtemplateRepository.FetchSingleResultOnProcessGuidWithVersion(processguid , version);
        }

        public async Task<List<WorkflowTemplate>> GetWorkFlowDataOnGuid(string processguid)
        {
            return await _workflowtemplateRepository.GetWorkFlowDataOnGuid(processguid);
        }

        public async Task<int> GetStatusCodeFromId(int id)
        {
            return await _workflowtemplateRepository.GetStatusCodeFromId(id);
        }
    }
}
