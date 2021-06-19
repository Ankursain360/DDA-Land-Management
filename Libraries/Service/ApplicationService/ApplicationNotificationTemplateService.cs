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
    public class ApplicationNotificationTemplateService : EntityService<ApplicationNotificationTemplate>, IApplicationNotificationTemplateService

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationNotificationTemplateRepository _applicationNotificationRepository;

        public ApplicationNotificationTemplateService(IUnitOfWork unitOfWork, IApplicationNotificationTemplateRepository applicationNotificationRepository)
        : base(unitOfWork, applicationNotificationRepository)
        {
            _unitOfWork = unitOfWork;
            _applicationNotificationRepository = applicationNotificationRepository;
        }

        public async Task<List<ApplicationNotificationTemplate>> GetAllTemplate()
        {
            return await _applicationNotificationRepository.GetAll();
        }

        public async Task<PagedResult<ApplicationNotificationTemplate>> GetPagedTemplate(ApplicationNotificationTemplateSearchDto model)
        {
            return await _applicationNotificationRepository.GetPagedTemplate(model);
        }

        public async Task<ApplicationNotificationTemplate> FetchSingleResult(int id)
        {
            var result = await _applicationNotificationRepository.FindBy(a => a.Id == id);
            ApplicationNotificationTemplate model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, ApplicationNotificationTemplate template)
        {
            var result = await _applicationNotificationRepository.FindBy(a => a.Id == id);
            ApplicationNotificationTemplate model = result.FirstOrDefault();
            model.Name = template.Name;
            model.Template = template.Template;
            model.URL = template.URL;
            model.ModifiedDate = DateTime.Now;
            model.IsActive = template.IsActive;
            model.ModifiedBy = template.ModifiedBy;
            _applicationNotificationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(ApplicationNotificationTemplate template)
        {  
            _applicationNotificationRepository.Add(template);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<bool> CheckUniqueName(int id, string templatename)
        {
            bool result = await _applicationNotificationRepository.Any(id, templatename);
            //  var result1 = _dbContext.Actions.Any(t => t.Id != id && t.Name == designation.Name);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _applicationNotificationRepository.FindBy(a => a.Id == id);
            ApplicationNotificationTemplate model = form.FirstOrDefault();
            model.IsActive = 0;
            model.ModifiedDate = DateTime.Now;
            _applicationNotificationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
