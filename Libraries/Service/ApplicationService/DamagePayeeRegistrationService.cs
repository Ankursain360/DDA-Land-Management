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
    public class DamagePayeeRegistrationService : EntityService<Payeeregistration>, IDamagePayeeRegistrationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDamagePayeeRegistrationRepository _damagePayeeRegistrationRepository;

        public DamagePayeeRegistrationService(IUnitOfWork unitOfWork, IDamagePayeeRegistrationRepository damagePayeeRegistrationRepository)
        : base(unitOfWork, damagePayeeRegistrationRepository)
        {
            _unitOfWork = unitOfWork;
            _damagePayeeRegistrationRepository = damagePayeeRegistrationRepository;
        }

        public async Task<List<Payeeregistration>> GetAllPayeeregistration()
        {
            return await _damagePayeeRegistrationRepository.GetAllPayeeregistration();
        }


        public async Task<List<Payeeregistration>> GetStructureUsingRepo()
        {
            return await _damagePayeeRegistrationRepository.GetAllPayeeregistration();
        }

        public async Task<Payeeregistration> FetchSingleResult(int id)
        {
            var result = await _damagePayeeRegistrationRepository.FindBy(a => a.Id == id);
            Payeeregistration model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Payeeregistration payeeregistration)
        {
            var result = await _damagePayeeRegistrationRepository.FindBy(a => a.Id == id);
            Payeeregistration model = result.FirstOrDefault();
            model.Name = payeeregistration.Name;
            model.MobileNumber = payeeregistration.MobileNumber;
            model.EmailId = payeeregistration.EmailId;
            model.IsVerified = payeeregistration.IsVerified;
            model.IsActive = payeeregistration.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _damagePayeeRegistrationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Payeeregistration payeeregistration)
        {
            payeeregistration.CreatedBy = 1;
            payeeregistration.CreatedDate = DateTime.Now;
            _damagePayeeRegistrationRepository.Add(payeeregistration);
            return await _unitOfWork.CommitAsync() > 0;
        }

       

        public async Task<bool> Delete(int id)
        {
            var form = await _damagePayeeRegistrationRepository.FindBy(a => a.Id == id);
            Payeeregistration model = form.FirstOrDefault();
            model.IsActive = 0;
            _damagePayeeRegistrationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Payeeregistration>> GetPagedDamagePayeeRegistration(DamagePayeeRegistrationSearchDto model)
        {
            return await _damagePayeeRegistrationRepository.GetPagedDamagePayeeRegistration(model);
        }


    }
}
