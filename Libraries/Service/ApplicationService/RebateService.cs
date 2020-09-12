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

namespace Libraries.Service.ApplicationService
{

    public class RebateService : EntityService<Rebate>, IRebateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRebateRepository _rebateRepository;

        public RebateService(IUnitOfWork unitOfWork, IRebateRepository rebateRepository)
        : base(unitOfWork, rebateRepository)
        {
            _unitOfWork = unitOfWork;
            _rebateRepository = rebateRepository;

        }

        public async Task<List<Rebate>> GetAllRebate()
        {
            return await _rebateRepository.GetAllDetails();
        }

        public async Task<List<PropertyType>> GetDropDownList()
        {
            List<PropertyType> propertytypeList = await _rebateRepository.GetPropertyTypeList();
            return propertytypeList;
        }

        public async Task<Rebate> FetchSingleResult(int id)
        {
            var result = await _rebateRepository.FindBy(a => a.Id == id);
            Rebate model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Rebate rebate)
        {
            var result = await _rebateRepository.FindBy(a => a.Id == id);
            Rebate model = result.FirstOrDefault();
            model.FromDate = rebate.FromDate;
            model.ToDate = rebate.ToDate;
            model.IsRebateOn = rebate.IsRebateOn;
            model.RebatePercentage = rebate.RebatePercentage;
            model.IsActive = rebate.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _rebateRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Rebate rebate)
        {

            rebate.CreatedBy = 1;
            rebate.CreatedDate = DateTime.Now;
            _rebateRepository.Add(rebate);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _rebateRepository.FindBy(a => a.Id == id);
            Rebate model = form.FirstOrDefault();
            model.IsActive = 0;
            _rebateRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public object GetFromDateData(int propertyId)
        {
            return _rebateRepository.GetFromDateData(propertyId);
        }
    }
}
