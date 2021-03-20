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

    public class OldAllotmentEntryService : EntityService<Leaseapplication>, IOldAllotmentEntryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOldAllotmentEntryRepository _oldAllotmentEntryRepository;
        private readonly IMapper _mapper;
        public OldAllotmentEntryService(IUnitOfWork unitOfWork,
            IOldAllotmentEntryRepository oldAllotmentEntryRepository,
            IMapper mapper)
        : base(unitOfWork, oldAllotmentEntryRepository)
        {
            _unitOfWork = unitOfWork;
            _oldAllotmentEntryRepository = oldAllotmentEntryRepository;
            _mapper = mapper;
        }
        public async Task<List<PropertyType>> GetAllPropertyType()
        {
            List<PropertyType> list = await _oldAllotmentEntryRepository.GetAllPropertyType();
            return list;
        }
        public async Task<List<Leasetype>> GetAllLeaseType()
        {
            List<Leasetype> list = await _oldAllotmentEntryRepository.GetAllLeaseType();
            return list;
        }
        public async Task<List<Leasepurpose>> GetAllLeasepurpose()
        {
            List<Leasepurpose> list = await _oldAllotmentEntryRepository.GetAllLeasepurpose();
            return list;
        }

        public async Task<List<Leasesubpurpose>> GetAllLeaseSubpurpose(int purposeId)
        {
            List<Leasesubpurpose> zoneList = await _oldAllotmentEntryRepository.GetAllLeaseSubpurpose(purposeId);
            return zoneList;
        }
       
        public async Task<bool> Create(Leaseapplication lease)
        {
            lease.CreatedDate = DateTime.Now;
            _oldAllotmentEntryRepository.Add(lease);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
