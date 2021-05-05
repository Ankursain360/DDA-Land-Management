using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Libraries.Repository.IEntityRepository;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Dto.Search;
using Dto.Master;
using AutoMapper;

namespace Libraries.Service.ApplicationService
{

    public class DocumentCheckListService : EntityService<Documentchecklist>, IDocumentCheckListService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDocumentCheckListRepository _documentCheckListRepository;
        private readonly IMapper _mapper;
        public DocumentCheckListService(IUnitOfWork unitOfWork,
            IDocumentCheckListRepository documentCheckListRepository,
            IMapper mapper)
        : base(unitOfWork, documentCheckListRepository)
        {
            _unitOfWork = unitOfWork;
            _documentCheckListRepository = documentCheckListRepository;
            _mapper = mapper;
        }
        public async Task<Documentchecklist> FetchSingleResult(int id)
        {
            return await _documentCheckListRepository.FetchSingleResult( id);
        }
        public async Task<bool> Update(int id, Documentchecklist documentchecklist)
        {
            var result = await _documentCheckListRepository.FindBy(a => a.Id == id);
            Documentchecklist model = result.FirstOrDefault();
            model.ServiceTypeId = documentchecklist.ServiceTypeId;
            model.Name = documentchecklist.Name;
            model.Description = documentchecklist.Description;
            model.IsMandatory = documentchecklist.IsMandatory;
            model.IsActive = documentchecklist.IsActive;
            model.ModifiedDate = DateTime.Now;
            _documentCheckListRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Documentchecklist documentchecklist)
        {
            documentchecklist.CreatedDate = DateTime.Now;
            _documentCheckListRepository.Add(documentchecklist);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<bool> CheckUniqueName(int id, string Name,int  ServiceTypeId)
        {
            bool result = await _documentCheckListRepository.Any(id, Name, ServiceTypeId);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _documentCheckListRepository.FindBy(a => a.Id == id);
            Documentchecklist model = form.FirstOrDefault();
            model.IsActive = 0;
            _documentCheckListRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<List<Servicetype>> GetServiceTypeList()
        {
            
            return await _documentCheckListRepository.GetServiceTypeList();
        }

        public async Task<List<Documentchecklist>> GetAllDocumentchecklist()
        {
            return await _documentCheckListRepository.GetAllDocumentchecklist();
        }


        public async Task<PagedResult<Documentchecklist>> GetPagedDocumentChecklistData(DocumentChecklistSearchDto model)
        {
            return await _documentCheckListRepository.GetPagedDocumentChecklistData(model);
        }
    }
}
