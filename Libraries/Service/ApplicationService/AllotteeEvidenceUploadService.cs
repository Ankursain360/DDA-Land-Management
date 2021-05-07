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

    public class AllotteeEvidenceUploadService : EntityService<Allotteeevidenceupload>, IAllotteeEvidenceUploadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAllotteeEvidenceUploadRepository _allotteeEvidenceUploadRepository;
        private readonly IMapper _mapper;
        public AllotteeEvidenceUploadService(IUnitOfWork unitOfWork,
            IAllotteeEvidenceUploadRepository allotteeEvidenceUploadRepository,
            IMapper mapper)
        : base(unitOfWork, allotteeEvidenceUploadRepository)
        {
            _unitOfWork = unitOfWork;
            _allotteeEvidenceUploadRepository = allotteeEvidenceUploadRepository;
            _mapper = mapper;
        }
        public async Task<List<Requestforproceeding>> GetAllotteeEvidenceDetails()
        {
            return await _allotteeEvidenceUploadRepository.GetAllotteeEvidenceDetails();
        }
        public async Task<Allotteeevidenceupload> FetchAllotteeEvidenceUploadDetails(int id)
        {
            return await _allotteeEvidenceUploadRepository.FetchAllotteeEvidenceUploadDetails(id);
        }

        public async Task<List<Allotteeevidenceupload>> GetAllotteeEvidenceHistoryDetails(int id)
        {
            return await _allotteeEvidenceUploadRepository.GetAllotteeEvidenceHistoryDetails(id);
        }

        public async Task<PagedResult<Requestforproceeding>> GetPagedRequestLetterDetails(AllotteeEvidenceSearchDto model)
        {
            return await _allotteeEvidenceUploadRepository.GetPagedRequestLetterDetails(model);
        }

        public async Task<bool> Update(int id, Allotteeevidenceupload allotteeevidenceupload)
        {
            var result = await _allotteeEvidenceUploadRepository.FindBy(a => a.Id == id);
            Allotteeevidenceupload model = result.FirstOrDefault();
            model.RequestProceedingId = allotteeevidenceupload.RequestProceedingId;
            model.DocumentName = allotteeevidenceupload.DocumentName;
            model.DocumentPatth = allotteeevidenceupload.DocumentPatth;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = allotteeevidenceupload.ModifiedBy;
            _allotteeEvidenceUploadRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Allotteeevidenceupload allotteeevidenceupload)
        {
            allotteeevidenceupload.CreatedDate = DateTime.Now;
            _allotteeEvidenceUploadRepository.Add(allotteeevidenceupload);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
