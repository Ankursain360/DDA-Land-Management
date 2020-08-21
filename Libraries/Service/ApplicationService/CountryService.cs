using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Libraries.Repository.IEntityRepository;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Libraries.Service.ApplicationService
{
    public class CountryService : EntityService<Country>, ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICountryRepository _countryRepository;

        public CountryService(IUnitOfWork unitOfWork, ICountryRepository countryRepository)
        : base(unitOfWork, countryRepository)
        {
            _unitOfWork = unitOfWork;
            _countryRepository = countryRepository;
        }

        public async Task<List<Country>> GetAllCountry()
        {
            return await _countryRepository.GetAll();
        }

        public async Task<List<Country>> GetCountryUsingRepo()
        {
            return await _countryRepository.GetCountry();
        }
    }
}