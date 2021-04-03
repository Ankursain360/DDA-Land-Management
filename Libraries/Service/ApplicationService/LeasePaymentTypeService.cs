using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{

    public class LeasePaymentTypeService : EntityService<Leasepaymenttype>, ILeasePaymentTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILeasePaymentTypeRepository _LeasePaymentTypeRepository;

        public LeasePaymentTypeService(IUnitOfWork unitOfWork, ILeasePaymentTypeRepository LeasePaymentTypeRepository) : base(unitOfWork, LeasePaymentTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _LeasePaymentTypeRepository = LeasePaymentTypeRepository;
        }

        public async Task<List<Leasepaymenttype>> GetAllLeasepaymenttype()
        {
            return await _LeasePaymentTypeRepository.GetAllLeasepaymenttype();
        }


        public async Task<Leasepaymenttype> FetchSingleResult(int id)
        {
            var result = await _LeasePaymentTypeRepository.FindBy(a => a.Id == id);
            Leasepaymenttype model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Leasepaymenttype rent)
        {
            var result = await _LeasePaymentTypeRepository.FindBy(a => a.Id == id);
            Leasepaymenttype model = result.FirstOrDefault();
            model.Name = rent.Name;


            model.IsActive = rent.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = rent.ModifiedBy;
            _LeasePaymentTypeRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Leasepaymenttype rate)
        {
            rate.CreatedBy = rate.CreatedBy;

            rate.CreatedDate = DateTime.Now;
            _LeasePaymentTypeRepository.Add(rate);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<bool> Delete(int id)
        {
            var form = await _LeasePaymentTypeRepository.FindBy(a => a.Id == id);
            Leasepaymenttype model = form.FirstOrDefault();
            model.IsActive = 0;
            _LeasePaymentTypeRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Leasepaymenttype>> GetPagedLeasepaymenttype(LeasePaymentTypeSearchDto model)
        {
            return await _LeasePaymentTypeRepository.GetPagedLeasepaymenttype(model);
        }
    }
}
