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

        public async  Task<Encrochmenttype> FetchResultEncroachmentType(DateTime date1)
        {
            return await _damgecalculationRepository.FetchResultEncroachmentType(date1);
        }

        public async Task<List<Resratelisttypea>> RateListTypeA(DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _damgecalculationRepository.RateListTypeA(date1, localityId, subEncroachersId);
        }
        public async Task<List<Resratelisttypeb>> RateListTypeB(DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _damgecalculationRepository.RateListTypeB(date1, localityId, subEncroachersId);
        }
        public async Task<List<Resratelisttypec>> RateListTypeC(DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _damgecalculationRepository.RateListTypeC(date1, localityId, subEncroachersId);
        }

        public async Task<List<Resratelisttypeb>> RateListTypeBSpecific(DateTime dateTimeSpecific, DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _damgecalculationRepository.RateListTypeBSpecific(dateTimeSpecific,date1, localityId, subEncroachersId);
        }

        public async Task<List<Resratelisttypea>> RateListTypeASpecific(DateTime specificDateTime, DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _damgecalculationRepository.RateListTypeASpecific(specificDateTime,date1, localityId, subEncroachersId);
        }

        public async Task<Comencrochmenttype> FetchResultCOMEncroachmentType(DateTime date1)
        {
            return await _damgecalculationRepository.FetchResultCOMEncroachmentType(date1);
        }
        public async Task<List<Comratelisttypea>> ComRateListTypeA(DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _damgecalculationRepository.ComRateListTypeA(date1, localityId, subEncroachersId);
        }
        public async Task<List<Comratelisttypeb>> ComRateListTypeB(DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _damgecalculationRepository.ComRateListTypeB(date1, localityId, subEncroachersId);
        }
        public async Task<List<Comratelisttypec>> ComRateListTypeC(DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _damgecalculationRepository.ComRateListTypeC(date1, localityId, subEncroachersId);
        }
        public async Task<List<Comratelisttypeb>> ComRateListTypeBSpecific(DateTime dateTimeSpecific, DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _damgecalculationRepository.ComRateListTypeBSpecific(dateTimeSpecific, date1, localityId, subEncroachersId);
        }

        public async Task<List<Comratelisttypea>> ComRateListTypeASpecific(DateTime specificDateTime, DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _damgecalculationRepository.ComRateListTypeASpecific(specificDateTime, date1, localityId, subEncroachersId);
        }
    }
}
