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
using Dto.Search;
using Dto.Master;

namespace Libraries.Service.ApplicationService
{

    public class DamageRateListService : EntityService<Resratelisttypea>, IDamageRateListService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDamageRateListRepository _damageRateListRepository;

        public DamageRateListService(IUnitOfWork unitOfWork, IDamageRateListRepository damageRateListRepository)
        : base(unitOfWork, damageRateListRepository)
        {
            _unitOfWork = unitOfWork;
            _damageRateListRepository = damageRateListRepository;
        }

        public async Task<bool> Create(DamageRateListCreateDto damageRateListCreateDto)
        {
            return await _damageRateListRepository.Create(damageRateListCreateDto);
        }

        public async Task<PropertyType> FetchSinglePropertyType(int id)
        {
            return await _damageRateListRepository.FetchSinglePropertyType(id);
        }

        public async Task<dynamic> FetchSingleResult(int id, int EncroachmentTypeId, int LocalityId, int PropertypeId)
        {
            return await _damageRateListRepository.FetchSingleResult(id, EncroachmentTypeId, LocalityId, PropertypeId);
        }

        public async Task<List<DateRangeConcatedListDto>> GetDateRangeDropdownListCommercial()
        {
            return await _damageRateListRepository.GetDateRangeDropdownListCommercial();
        }

        public async Task<List<DateRangeConcatedListDto>> GetDateRangeDropdownListResidential()
        {
            return await _damageRateListRepository.GetDateRangeDropdownListResidential();
        }

        public async Task<List<Locality>> GetLocalities()
        {
            return await _damageRateListRepository.GetLocalities();
        }

        public async Task<List<PropertyType>> GetPropertyTypes()
        {
            return await _damageRateListRepository.GetPropertyTypes();
        }

        public async Task<List<DamageRateListDataDto>> GetSearchResultPagedData(DamageRateListSearchDto model, List<DamageRateListDataDto> damageRateListDataDtos)
        {
            return await _damageRateListRepository.GetSearchResultPagedData( model, damageRateListDataDtos);
        }

        public async Task<bool> Update(DamageRateListCreateDto damageRateListCreateDto)
        {
            return await _damageRateListRepository.Update(damageRateListCreateDto);
        }
    }
}
