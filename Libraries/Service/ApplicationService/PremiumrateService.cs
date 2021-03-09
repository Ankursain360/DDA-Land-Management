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
     
    public class PremiumrateService : EntityService<Premiumrate>, IPremiumrateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPremiumrateRepository _premiumrateRepository;

        public PremiumrateService(IUnitOfWork unitOfWork, IPremiumrateRepository PremiumrateRepository)
        : base(unitOfWork, PremiumrateRepository)
        {
            _unitOfWork = unitOfWork;
            _premiumrateRepository = PremiumrateRepository;
        }

        public async Task<List<Premiumrate>> GetAllPremiumrate()
        {
            return await _premiumrateRepository.GetAllPremiumrate();
        }



        public async Task<Premiumrate> FetchSingleResult(int id)
        {
            var result = await _premiumrateRepository.FindBy(a => a.Id == id);
            Premiumrate model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Premiumrate rate)
        {
            var result = await _premiumrateRepository.FindBy(a => a.Id == id);
            Premiumrate model = result.FirstOrDefault();
            model.PropertyTypeId = rate.PropertyTypeId;
            model.PremiumRate = rate.PremiumRate;
            model.FromDate = rate.FromDate;
            model.ToDate = rate.ToDate;
            
            model.IsActive = rate.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _premiumrateRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Premiumrate rate)
        {
            rate.CreatedBy = 1;
            rate.CreatedDate = DateTime.Now;
            _premiumrateRepository.Add(rate);
            return await _unitOfWork.CommitAsync() > 0;
        }

       

        public async Task<bool> Delete(int id)
        {
            var form = await _premiumrateRepository.FindBy(a => a.Id == id);
            Premiumrate model = form.FirstOrDefault();
            model.IsActive = 0;
            _premiumrateRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Premiumrate>> GetPagedPremiumrate(PremiumrateSearchDto model)
        {
            return await _premiumrateRepository.GetPagedPremiumrate(model);
        }


    }
}
