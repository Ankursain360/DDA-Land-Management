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

    public class ExtensionService : EntityService<Mortgage>, IExtensionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMortgageRepository _mortgageRepository;
        private readonly IMapper _mapper;
        public ExtensionService(IUnitOfWork unitOfWork,
            IMortgageRepository mortgageRepository,
            IMapper mapper)
        : base(unitOfWork, mortgageRepository)
        {
            _unitOfWork = unitOfWork;
            _mortgageRepository = mortgageRepository;
            _mapper = mapper;
        }

        public async Task<List<Documentchecklist>> GetDocumentChecklistDetails(int servicetypeid)
        {
            return await _mortgageRepository.GetDocumentChecklistDetails(servicetypeid);
        }

        public async Task<PagedResult<Mortgage>> GetPagedMortgageDetails(MortgageSearchDto model)
        {
            return await _mortgageRepository.GetPagedMortgageDetails(model);
        }

        public async Task<bool> SaveAllotteeServiceDocuments(List<Allotteeservicesdocument> allotteeservicesdocuments)
        {
            allotteeservicesdocuments.ForEach(x => x.CreatedDate = DateTime.Now);
            return await _mortgageRepository.SaveAllotteeServiceDocuments(allotteeservicesdocuments);
        }

        public async Task<bool> Create(Mortgage mortgage)
        {
            mortgage.CreatedDate = DateTime.Now;
            _mortgageRepository.Add(mortgage);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Possesionplan> GetAllotteeDetails(int userId)
        {
            return await _mortgageRepository.GetAllotteeDetails(userId);
        }
    }
}
