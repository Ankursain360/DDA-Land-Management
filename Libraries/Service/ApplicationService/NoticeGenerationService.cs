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

       
    }
}
