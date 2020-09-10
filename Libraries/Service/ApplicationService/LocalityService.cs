using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{
    public class LocalityService : EntityService<Locality>, ILocalityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILocalityRepository _localityRepository;
        public LocalityService(IUnitOfWork unitOfWork, ILocalityRepository localityRepository)
        : base(unitOfWork, localityRepository)
        {
            _unitOfWork = unitOfWork;
            _localityRepository = localityRepository;
        }
        public async Task<bool> CheckUniqueName(int id, string name)
        {
            bool result= await _localityRepository.Any(id, name);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _localityRepository.FindBy(a => a.Id == id);
            Locality model = form.FirstOrDefault();
            model.IsActive = 0;
            _localityRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Locality> FetchSingleResult(int id)
        {
            var result = await _localityRepository.FindBy(a => a.Id == id);
            Locality model = result.FirstOrDefault();
            return model;
        }

        public async Task<List<Department>> GetAllDepartment()
        {
            List<Department> departmentList = await _localityRepository.GetAllDepartment();
            return departmentList;
        }

        public async Task<List<Locality>> GetAllLocality()
        {
            return await _localityRepository.GetAllLocality();
        }

        public async Task<List<Zone>> GetAllZone( int departmentId)
        {
            List<Zone> zoneList = await _localityRepository.GetAllZone(departmentId);
            return zoneList;
        }

        public async Task<List<Locality>> GetLocalityUsingRepo()
        {
            return await _localityRepository.GetAllLocality();
        }

        public async Task<bool> Update(int id, Locality locality)
        {
            var result = await _localityRepository.FindBy(a => a.Id == id);
            Locality model = result.FirstOrDefault();
            model.Name = locality.Name;
            model.ZoneId = locality.ZoneId;
            model.IsActive = locality.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _localityRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Locality locality)
        {
            locality.CreatedBy = 1;
            locality.CreatedDate = DateTime.Now;
            _localityRepository.Add(locality);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
