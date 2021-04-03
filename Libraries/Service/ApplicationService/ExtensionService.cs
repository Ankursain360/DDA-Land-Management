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
    }
}
