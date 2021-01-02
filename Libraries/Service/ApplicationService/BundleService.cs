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
    public class BundleService : EntityService<Bundle>, IBundleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBundleRepository _bundleRepository;
        protected readonly DataContext _dbContext;


        public BundleService(IUnitOfWork unitOfWork, IBundleRepository bundleRepository, DataContext dbContext)
       : base(unitOfWork, bundleRepository)
        {
            _unitOfWork = unitOfWork;
            _bundleRepository = bundleRepository;
            _dbContext = dbContext;
        }

        public async Task<List<Bundle>> GetAllBundle()
        {
            return await _bundleRepository.GetAll();
        }

        public async Task<List<Bundle>> GetBundleUsingReport()
        {
            return await _bundleRepository.GetBundle();
        }

        public async Task<bool> Update(int id, Bundle bundle)
        {
            var result = await _bundleRepository.FindBy(a => a.Id == id);
            Bundle model = result.FirstOrDefault();
            model.BundleNo = bundle.BundleNo;
            model.ModifiedDate = DateTime.Now;
            model.IsActive = bundle.IsActive;
            model.ModifiedBy = 1;
            _bundleRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public bool CheckUniqueName(int id, Bundle bundle)
        {
            var result = _dbContext.Bundle.Any(t => t.Id != id && t.BundleNo == bundle.BundleNo);
            return result;
        }

        public async Task<Bundle> FetchSingleResult(int id)
        {
            var result = await _bundleRepository.FindBy(a => a.Id == id);
            Bundle model = result.FirstOrDefault();
            return model;
        }



        public async Task<bool> Create(Bundle bundle)
        {

            bundle.CreatedBy = 1;
            bundle.CreatedDate = DateTime.Now;
            _bundleRepository.Add(bundle);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> CheckUniqueName(int id, string bundle)
        {
            bool result = await _bundleRepository.Any(id, bundle);

            return result;
        }


        public async Task<bool> Delete(int id)
        {
            var form = await _bundleRepository.FindBy(a => a.Id == id);
            Bundle model = form.FirstOrDefault();
            model.IsActive = 0;
            _bundleRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Bundle>> GetPagedBundle(BundleSearchDto model)
        {
            return await _bundleRepository.GetPagedBundle(model);
        }

     
    }
}
