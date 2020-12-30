using Dto.Search;
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
    public class DamageCalculationService : EntityService<Damagecalculation>, IDamageCalculationService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDamageCalculationRepository _damgecalculationRepository;

        public DamageCalculationService(IUnitOfWork unitOfWork, IDamageCalculationRepository damagepayeecalculationRepository)
      : base(unitOfWork, damagepayeecalculationRepository)
        {
            _unitOfWork = unitOfWork;
            _damgecalculationRepository = damagepayeecalculationRepository;
        }
        public async Task<List<PropertyType>> GetPropertyTypes()
        {
            List<PropertyType> localityList = await _damgecalculationRepository.GetPropertyType();
            return localityList;
        }


        public async Task<List<Locality>> GetLocalities()
        {
            List<Locality> localityList = await _damgecalculationRepository.GetLocalities();
            return localityList;
        }

    }
}
