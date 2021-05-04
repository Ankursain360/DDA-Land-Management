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
    public class ModuleCategoryService : EntityService<ModuleCategory>, IModuleCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModuleCategoryRepository _modulecategoryRepository;
        protected readonly DataContext _dbContext;


        public ModuleCategoryService(IUnitOfWork unitOfWork, IModuleCategoryRepository modulecategoryRepository, DataContext dbContext)
       : base(unitOfWork, modulecategoryRepository)
        {
            _unitOfWork = unitOfWork;
            _modulecategoryRepository = modulecategoryRepository;
            _dbContext = dbContext;
        }

        public async Task<List<ModuleCategory>> GetAllModuleCategory()
        {
            return await _modulecategoryRepository.GetAll();
        }

        public async Task<List<ModuleCategory>> GetModuleCategoryUsingReport()
        {
            return await _modulecategoryRepository.GetModuleCategory();
        }

        public async Task<bool> Update(int id, ModuleCategory modulecategory)
        {
            var result = await _modulecategoryRepository.FindBy(a => a.Id == id);
            ModuleCategory model = result.FirstOrDefault();
            model.CategoryName = modulecategory.CategoryName;
            model.ModifiedDate = DateTime.Now;
            model.IsActive = modulecategory.IsActive;
            model.ModifiedBy = 1;
            _modulecategoryRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public bool CheckUniqueName(int id, ModuleCategory moduleCategory)
        {
            var result = _dbContext.ModuleCategory.Any(t => t.Id != id && t.CategoryName ==moduleCategory.CategoryName);
            return result;
        }

        public async Task<ModuleCategory> FetchSingleResult(int id)
        {
            var result = await _modulecategoryRepository.FindBy(a => a.Id == id);
            ModuleCategory model = result.FirstOrDefault();
            return model;
        }



        public async Task<bool> Create(ModuleCategory moduleCategory)
        {

            moduleCategory.CreatedBy = 1;
            moduleCategory.CreatedDate = DateTime.Now;
            _modulecategoryRepository.Add(moduleCategory);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> CheckUniqueName(int id, string moduleCategory)
        {
            bool result = await _modulecategoryRepository.Any(id, moduleCategory);

            return result;
        }


        public async Task<bool> Delete(int id)
        {
            var form = await _modulecategoryRepository.FindBy(a => a.Id == id);
            ModuleCategory model = form.FirstOrDefault();
            model.IsActive = 0;
            _modulecategoryRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<ModuleCategory>> GetPagedModuleCategory(ModuleCategorySearchDto model)
        {
            return await _modulecategoryRepository.GetPagedModuleCategory(model);
        }


    }
}
