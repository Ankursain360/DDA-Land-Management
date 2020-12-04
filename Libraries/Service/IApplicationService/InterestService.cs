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

namespace Libraries.Service.IApplicationService
{

    public class InterestService : EntityService<Interest>, IInterestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInterestRepository _interestRepository;

        public InterestService(IUnitOfWork unitOfWork, IInterestRepository interestRepository)
        : base(unitOfWork, interestRepository)
        {
            _unitOfWork = unitOfWork;
            _interestRepository = interestRepository;

        }

        public async Task<List<Interest>> GetAllInterest()
        {
            return await _interestRepository.GetAllDetails();
        }

        public async Task<List<PropertyType>> GetDropDownList()
        {
            List<PropertyType> propertytypeList = await _interestRepository.GetPropertyTypeList();
            return propertytypeList;
        }

        public async Task<Interest> FetchSingleResult(int id)
        {
            var result = await _interestRepository.FindBy(a => a.Id == id);
            Interest model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Interest interest)
        {
            var result = await _interestRepository.FindBy(a => a.Id == id);
            Interest model = result.FirstOrDefault();
            model.FromDate = interest.FromDate;
            model.ToDate = interest.ToDate;
            model.PropertyId = interest.PropertyId;
            model.Percentage = interest.Percentage;
            model.IsActive = interest.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _interestRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Interest interest)
        {
            interest.CreatedBy = 1;
            interest.CreatedDate = DateTime.Now;
            _interestRepository.Add(interest);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _interestRepository.FindBy(a => a.Id == id);
            Interest model = form.FirstOrDefault();
            model.IsActive = 0;
            _interestRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public object GetFromDateData(int propertyId)
        {
            return _interestRepository.GetFromDateData(propertyId);
        }

        public async Task<PagedResult<Interest>> GetPagedInterest(InterestSearchDto model)
        {
            return await _interestRepository.GetPagedInterest(model);
        }

        public int IsRecordExist(int propertyId)
        {
            return _interestRepository.IsRecordExist(propertyId);
        }
    }
}
