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

namespace Libraries.Service.ApplicationService
{

    public class RateService : EntityService<Rate>, IRateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRateRepository _rateRepository;

        public RateService(IUnitOfWork unitOfWork, IRateRepository rateRepository)
        : base(unitOfWork, rateRepository)
        {
            _unitOfWork = unitOfWork;
            _rateRepository = rateRepository;

        }

        public async Task<List<Rate>> GetAllRate()
        {
            return await _rateRepository.GetAllDetails();
        }

        public async Task<List<PropertyType>> GetDropDownList()
        {
            List<PropertyType> propertytypeList = await _rateRepository.GetPropertyTypeList();
            return propertytypeList;
        }

        public async Task<Rate> FetchSingleResult(int id)
        {
            var result = await _rateRepository.FindBy(a => a.Id == id);
            Rate model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Rate rate)
        {
            var result = await _rateRepository.FindBy(a => a.Id == id);
            Rate model = result.FirstOrDefault();
            model.FromDate = rate.FromDate;
            model.ToDate = rate.ToDate;
            model.PropertyId = rate.PropertyId;
            model.RatePercentage = rate.RatePercentage;
            model.Scheme = rate.Scheme;
            model.IsActive = rate.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _rateRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Rate rate)
        {

            rate.CreatedBy = 1;
            rate.CreatedDate = DateTime.Now;
            _rateRepository.Add(rate);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _rateRepository.FindBy(a => a.Id == id);
            Rate model = form.FirstOrDefault();
            model.IsActive = 0;
            _rateRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public object GetFromDateData(int propertyId)
        {
            return  _rateRepository.GetFromDateData(propertyId);
        }

        public async Task<PagedResult<Rate>> GetPagedRate(RateSearchDto model)
        {
            return await _rateRepository.GetPagedRate(model);
        }

        public int IsRecordExist(int propertyId)
        {
            return _rateRepository.IsRecordExist(propertyId);
        }
    }
}

