using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
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
        public Task<bool> CheckUniqueName(int id, string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Locality> FetchSingleResult(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Zone>> GetAllDepartment()
        {
            throw new NotImplementedException();
        }

        public Task<List<Locality>> GetAllLocality()
        {
            throw new NotImplementedException();
        }

        public Task<List<Zone>> GetAllZone()
        {
            throw new NotImplementedException();
        }

        public Task<List<Locality>> GetLocalityUsingRepo()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(int id, Locality locality)
        {
            throw new NotImplementedException();
        }

        Task<bool> ILocalityService.Create(Locality locality)
        {
            throw new NotImplementedException();
        }
    }
}
