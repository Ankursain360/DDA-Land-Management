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
    public class LeasepurposeService : EntityService<Leasepurpose>, ILeasepurposeService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILeasepurposeRepository _LeasepurposeRepository;
        protected readonly DataContext _dbContext;


        public LeasepurposeService(IUnitOfWork unitOfWork, ILeasepurposeRepository LeasepurposeRepository, DataContext dbContext)
      : base(unitOfWork, LeasepurposeRepository)
        {
            _unitOfWork = unitOfWork;
            _LeasepurposeRepository = LeasepurposeRepository;
            _dbContext = dbContext;
        }



        public async Task<List<Leasepurpose>> GetLeasepurposes()
        {
            return await _LeasepurposeRepository.GetAll();
        }

        public async Task<List<Leasepurpose>> GetLeasepurposeUsingRepo()
        {
            return await _LeasepurposeRepository.GetLeasepurposes();
        }

        public async Task<bool> Update(int id, Leasepurpose Leasepurpose)
        {
            var result = await _LeasepurposeRepository.FindBy(a => a.Id == id);
            Leasepurpose model = result.FirstOrDefault();
            model.PurposeUse = Leasepurpose.PurposeUse;
          
            model.IsActive = Leasepurpose.IsActive;
            model.ModifiedBy = 1;
            _LeasepurposeRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<Leasepurpose> FetchSingleResult(int id)
        {
            var result = await _LeasepurposeRepository.FindBy(a => a.Id == id);
            Leasepurpose model = result.FirstOrDefault();
            return model;
        }



        public async Task<bool> Create(Leasepurpose Leasepurpose)
        {

            Leasepurpose.CreatedBy = 1;
            Leasepurpose.CreatedDate = DateTime.Now;
            _LeasepurposeRepository.Add(Leasepurpose);
            return await _unitOfWork.CommitAsync() > 0;
        }




        public async Task<bool> Delete(int id)
        {
            var form = await _LeasepurposeRepository.FindBy(a => a.Id == id);
            Leasepurpose model = form.FirstOrDefault();
            model.IsActive = 0;
            _LeasepurposeRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Leasepurpose>> GetpagedLeasepurpose(LeasepurposeSearchDto model)
        {
            return await _LeasepurposeRepository.GetpagedLeasepurpose(model);
        }

    }
}