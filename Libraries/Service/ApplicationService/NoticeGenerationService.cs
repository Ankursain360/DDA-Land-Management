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

    public class NoticeGenerationService : EntityService<Leasenoticegeneration>, INoticeGenerationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INoticeGenerationRepository _noticeGenerationRepository;
        private readonly IMapper _mapper;
        public NoticeGenerationService(IUnitOfWork unitOfWork,
            INoticeGenerationRepository noticeGenerationRepository,
            IMapper mapper)
        : base(unitOfWork, noticeGenerationRepository)
        {
            _unitOfWork = unitOfWork;
            _noticeGenerationRepository = noticeGenerationRepository;
            _mapper = mapper;
        }
        public async Task<List<Requestforproceeding>> GetNoticeDetails()
        {
            return await _noticeGenerationRepository.GetNoticeDetails();
        }
        public async Task<Leasenoticegeneration> FetchNoticeGenerationDetails(int id)
        {
            return await _noticeGenerationRepository.FetchNoticeGenerationDetails(id);
        }

        public async Task<List<Leasenoticegeneration>> GetNoticeHistoryDetails(int id)
        {
            return await _noticeGenerationRepository.GetNoticeHistoryDetails(id);
        }

        public async  Task<PagedResult<Requestforproceeding>> GetPagedRequestLetterDetails(LeaseHearingDetailsSearchDto model)
        {
            return await _noticeGenerationRepository.GetPagedRequestLetterDetails(model);
        }

        public async Task<bool> Update(int id, Leasenoticegeneration leasenoticegeneration)
        {
            var result = await _noticeGenerationRepository.FindBy(a => a.Id == id);
            Leasenoticegeneration model = result.FirstOrDefault();
            model.RequestProceedingId = leasenoticegeneration.RequestProceedingId;
            if (leasenoticegeneration.GenerateUpload == 0)
            {
                model.MeetingPlace = leasenoticegeneration.MeetingPlace;
                model.MeetingDate = leasenoticegeneration.MeetingDate;
                model.MeetingTime = leasenoticegeneration.MeetingTime;
            }
            else
                model.NoticeFileName = leasenoticegeneration.NoticeFileName;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = leasenoticegeneration.ModifiedBy;
            _noticeGenerationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Leasenoticegeneration leasenoticegeneration)
        {
            leasenoticegeneration.CreatedDate = DateTime.Now;
            _noticeGenerationRepository.Add(leasenoticegeneration);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
