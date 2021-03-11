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

    public class InterestrateService : EntityService<Interestrate>, IInterestrateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInterestrateRepository _InterestrateRepository;

        public InterestrateService(IUnitOfWork unitOfWork, IInterestrateRepository InterestrateRepository)
        : base(unitOfWork, InterestrateRepository)
        {
            _unitOfWork = unitOfWork;
            _InterestrateRepository = InterestrateRepository;
        }

        public async Task<List<Interestrate>> GetAllInterestrate()
        {
            return await _InterestrateRepository.GetAllInterestrate();
        }

        public async Task<List<PropertyType>> GetAllPropertyType()
        {
            List<PropertyType> list = await _InterestrateRepository.GetAllPropertyType();
            return list;
        }

        public async Task<Interestrate> FetchSingleResult(int id)
        {
            var result = await _InterestrateRepository.FindBy(a => a.Id == id);
            Interestrate model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Interestrate rate)
        {
            var result = await _InterestrateRepository.FindBy(a => a.Id == id);
            Interestrate model = result.FirstOrDefault();
            model.PropertyTypeId = rate.PropertyTypeId;
            model.InterestRate = rate.InterestRate;
            model.FromDate = rate.FromDate;
            model.ToDate = rate.ToDate;

            model.IsActive = rate.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = rate.ModifiedBy;
            _InterestrateRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Interestrate rate)
        {
            rate.CreatedBy = rate.CreatedBy;
            rate.CreatedDate = DateTime.Now;
            _InterestrateRepository.Add(rate);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<bool> Delete(int id)
        {
            var form = await _InterestrateRepository.FindBy(a => a.Id == id);
            Interestrate model = form.FirstOrDefault();
            model.IsActive = 0;
            _InterestrateRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Interestrate>> GetPagedInterestrate(InterestrateSearchDto model)
        {
            return await _InterestrateRepository.GetPagedInterestrate(model);
        }


    }
}
