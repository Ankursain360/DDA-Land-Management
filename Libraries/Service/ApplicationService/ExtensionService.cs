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

    public class ExtensionService : EntityService<Extension>, IExtensionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExtensionRepository _extensionRepository;
        private readonly IMapper _mapper;
        public ExtensionService(IUnitOfWork unitOfWork,
            IExtensionRepository extensionRepository,
            IMapper mapper)
        : base(unitOfWork, extensionRepository)
        {
            _unitOfWork = unitOfWork;
            _extensionRepository = extensionRepository;
            _mapper = mapper;
        }

        public async Task<List<Documentchecklist>> GetDocumentChecklistDetails(int servicetypeid)
        {
            return await _extensionRepository.GetDocumentChecklistDetails(servicetypeid);
        }

        public async Task<PagedResult<Extension>> GetPagedExtensionServiceDetails(ExtensionServiceSearchDto model)
        {
            return await _extensionRepository.GetPagedExtensionServiceDetails(model);
        }

        public async Task<bool> SaveAllotteeServiceDocuments(List<Allotteeservicesdocument> allotteeservicesdocuments)
        {
            allotteeservicesdocuments.ForEach(x => x.CreatedDate = DateTime.Now);
            return await _extensionRepository.SaveAllotteeServiceDocuments(allotteeservicesdocuments);
        }

        public async Task<bool> Create(Extension extensionservice)
        {
            extensionservice.CreatedDate = DateTime.Now;
            _extensionRepository.Add(extensionservice);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Possesionplan> GetAllotteeDetails(int userId)
        {
            return await _extensionRepository.GetAllotteeDetails(userId);
        }

        public async Task<Timeextension> GetTimeLineExtensionFees()
        {
            return await _extensionRepository.GetTimeLineExtensionFees();
        }

        public async Task<Extension> FetchSingleResult(int id)
        {
            return await _extensionRepository.FetchSingleResult(id);
        }

        public async Task<Allotteeservicesdocument> FetchSingleResultDocument(int id)
        {
            return await _extensionRepository.FetchSingleResultDocument(id);
        }

        public async Task<bool> UpdateBeforeApproval(int id, Extension extension)
        {
            var result = await _extensionRepository.FindBy(a => a.Id == id);
            Extension model = result.FirstOrDefault();
            model.ApprovedStatus = extension.ApprovedStatus;
            model.PendingAt = extension.PendingAt;
            _extensionRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Update(int id, Extension extension)
        {
            var result = await _extensionRepository.FindBy(a => a.Id == id);
            Extension model = result.FirstOrDefault();
            model.AllotmentId = extension.AllotmentId;
            model.ServiceTypeId = extension.ServiceTypeId;
            model.LeaseApplicationId = extension.LeaseApplicationId;
            model.ExtensionPeriod = extension.ExtensionPeriod;
            model.ExtentionFees = extension.ExtentionFees;
            model.TotalAmount = extension.TotalAmount;
            model.Remarks = extension.Remarks;
            model.UserId = extension.UserId;
            model.IsActive = extension.IsActive;
            model.ApprovedStatus = extension.ApprovedStatus;
            model.PendingAt = extension.PendingAt;
            model.ModifiedBy = extension.ModifiedBy;
            model.ModifiedDate = DateTime.Now;
            _extensionRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Delete(int id, int userId)
        {
            var result = await _extensionRepository.FindBy(a => a.Id == id);
            Extension model = result.FirstOrDefault();
            model.IsActive = 0;
            model.ModifiedBy = userId;
            model.ModifiedDate = DateTime.Now;
            _extensionRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<List<Allotteeservicesdocument>> AlloteeDocumentListDetails(int id, int servicetypeid)
        {
            return await _extensionRepository.AlloteeDocumentListDetails(id, servicetypeid);
        }

        public async Task<bool> UpdateAllotteeServiceDocuments(int id, Allotteeservicesdocument allotteeservicesdocuments)
        {
            return await _extensionRepository.UpdateAllotteeServiceDocuments( id, allotteeservicesdocuments);
        }

        public async Task<bool> SaveAllotteeServiceDocumentsSingle(Allotteeservicesdocument item)
        {
            return await _extensionRepository.SaveAllotteeServiceDocumentsSingle(item);
        }

        public async Task<Extension> IsNeedAddMore()
        {
            return await _extensionRepository.IsNeedAddMore();
        }

        public async Task<bool> RollBackEntry(int id)
        {
            var result = await _extensionRepository.FindBy(a => a.Id == id);
            Extension model = result.FirstOrDefault();
            _extensionRepository.Delete(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> RollBackEntryDocument(int id, int serviceid)
        {
            return await _extensionRepository.RollBackEntryDocument(id, serviceid);
        }
    }
}
