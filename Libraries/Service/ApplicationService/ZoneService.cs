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

    public class ZoneService : EntityService<Zone>, IZoneService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IZoneRepository _zoneRepository;
        private readonly IMapper _mapper;
        public ZoneService(IUnitOfWork unitOfWork,
            IZoneRepository zoneRepository,
            IMapper mapper)
        : base(unitOfWork, zoneRepository)
        {
            _unitOfWork = unitOfWork;
            _zoneRepository = zoneRepository;
            _mapper = mapper;
        }
        public async Task<List<Zone>> GetAllDetails()
        {
            return await _zoneRepository.GetAllDetails();
        }

        public async Task<List<ZoneDto>> GetZone()
        {
            var zones = await _zoneRepository.FindBy(a => a.IsActive == 1);
            var result = _mapper.Map<List<ZoneDto>>(zones);
            return result;
        }

        public async Task<List<Department>> GetDropDownList()
        {
            List<Department> departmentList = await _zoneRepository.GetDepartmentList();
            return departmentList;
        }
        public async Task<Zone> FetchSingleResult(int id)
        {
            var result = await _zoneRepository.FindBy(a => a.Id == id);
            Zone model = result.FirstOrDefault();
            return model;
        }
        public async Task<bool> Update(int id, Zone zone)
        {
            var result = await _zoneRepository.FindBy(a => a.Id == id);
            Zone model = result.FirstOrDefault();
            model.DepartmentId = zone.DepartmentId;
            model.Name = zone.Name;
            model.Code = zone.Code;
            model.IsActive = zone.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _zoneRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Zone zone)
        {

            zone.CreatedBy = 1;
            zone.CreatedDate = DateTime.Now;
            _zoneRepository.Add(zone);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<bool> CheckUniqueName(int id, string zone)
        {
            bool result = await _zoneRepository.Any(id, zone);
            return result;
        }
        public async Task<bool> CheckUniqueCode(int id, string code)
        {
            bool result = await _zoneRepository.anyCode(id, code);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _zoneRepository.FindBy(a => a.Id == id);
            Zone model = form.FirstOrDefault();
            model.IsActive = 0;
            _zoneRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Zone>> GetPagedZone(ZoneSearchDto model)
        {
            return await _zoneRepository.GetPagedZone(model);
        }
    }
}
