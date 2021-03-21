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
        //********* save in table  Allotmententry  **********


        public async Task<int> SaveAllotmentDetails(Allotmententry entry)
        {
            entry.CreatedBy = entry.CreatedBy;
            entry.CreatedDate = DateTime.Now;
            entry.IsActive = 1;
            //return await _oldAllotmentEntryRepository.SaveAllotmentDetails(entry);
             var result= await _oldAllotmentEntryRepository.SaveAllotmentDetails(entry);
            var id = entry.Id;
            return id;
        }
        public async Task<List<Allotmententry>> GetAllAllotmententry(int id)
        {
            return await _oldAllotmentEntryRepository.GetAllAllotmententry(id);
        }
        public async Task<bool> DeleteEntry(int Id)
        {
            return await _oldAllotmentEntryRepository.DeleteEntry(Id);
        }

        //********* save in table  possesionplan  **********
        public async Task<bool> SavepossessionDetails(Possesionplan entry)
        {
            entry.CreatedBy = entry.CreatedBy;
            entry.CreatedDate = DateTime.Now;
            entry.IsActive = 1;
            return await _oldAllotmentEntryRepository.SavepossessionDetails(entry);

        }
        //public async Task<List<Possesionplan>> GetAllPossesionplan(int id)
        //{
        //return await _oldAllotmentEntryRepository.GetAllPossesionplan(id);
        //}
        //public async Task<bool> DeletePlan(int Id)
        //{
        // return await _jaraidetailRepository.DeletePlan(Id);
        //}



        
    }
}
