using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Libraries.Repository.IEntityRepository;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Libraries.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libraries.Service.ApplicationService
{

    public class ZoneService : EntityService<Zone>, IZoneService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IZoneRepository _zoneRepository;
        
        public ZoneService(IUnitOfWork unitOfWork, IZoneRepository zoneRepository)
        : base(unitOfWork, zoneRepository)
        {
            _unitOfWork = unitOfWork;
            _zoneRepository = zoneRepository;
           
        }

        public async Task<List<Zone>> GetAllZone()
        {
            return await _zoneRepository.GetAll();
        }

        public async Task<List<Zone>> GetAllDetails()
        {
            return await _zoneRepository.GetAllDetails();
        }
        public async Task<List<Zone>> GetZoneUsingRepo()
        {
            return await _zoneRepository.GetZone();
        }

        public async Task<IEnumerable<SelectListItem>> GetDropDownList()
        {
            var result =await _zoneRepository.GetDepartmentList();
            return (IEnumerable<SelectListItem>)result ;
                //Designation.Select(x => new { x.Id, x.Name }).ToList();
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
            model.Name = zone.Name;
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
    }
}
