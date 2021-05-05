using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Dto.Search;

namespace Libraries.Service.ApplicationService
{
    public class ModuleService : EntityService<Module>, IModuleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModuleRepository _moduleRepository;

        public ModuleService(IUnitOfWork unitOfWork, IModuleRepository moduleRepository)
        : base(unitOfWork, moduleRepository)
        {
            _unitOfWork = unitOfWork;
            _moduleRepository = moduleRepository;
        }

        public async Task<List<Module>> GetAllModule()
        {
            return await _moduleRepository.GetAllModule();
        }

        public async Task<List<Module>> GetActiveModule()
        {
            return await _moduleRepository.FindBy(a => a.IsActive == 1);
        }

        public async Task<List<Module>> GetModuleUsingRepo()
        {
            return await _moduleRepository.GetAllModule();
        }

        public async Task<Module> FetchSingleResult(int id)
        {
            var result = await _moduleRepository.FindBy(a => a.Id == id);
            Module model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Module module)
        {
            var result = await _moduleRepository.FindBy(a => a.Id == id);
            Module model = result.FirstOrDefault();
            model.Name = module.Name;
            model.Description = module.Description;
            model.Url = module.Url;
            model.Icon = module.Icon;
            model.Target = module.Target;
            model.ModifiedDate = DateTime.Now;
            model.IsActive = module.IsActive;
            model.ModifiedBy = 1;
            model.ModuleCategoryId = module.ModuleCategoryId;
            _moduleRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Module module)
        {
            module.Guid = Guid.NewGuid().ToString();
            module.CreatedBy = 1;
            module.CreatedDate = DateTime.Now;
            _moduleRepository.Add(module);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> CheckUniqueName(int id, string module)
        {
            bool result = await _moduleRepository.Any(id, module);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _moduleRepository.FindBy(a => a.Id == id);
            Module model = form.FirstOrDefault();
            model.IsActive = 0;
            _moduleRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Module>> GetPagedModule(ModuleSearchDto model)
        {
            return await _moduleRepository.GetPagedModule(model);
        }

        public Task<Module> GetModuleByGuid(string guid)
        {
            return _moduleRepository.GetModuleByGuid(guid);
        }

        public async Task<List<Menuactionrolemap>> ModuleFromMenuRoleActionMap(int roleId)
        {
            return await _moduleRepository.ModuleFromMenuRoleActionMap(roleId);
        }

        public async Task<List<ModuleCategory>> GetModuleCategory()
        {
            List<ModuleCategory> ModuleCategoryList = await _moduleRepository.GetModuleCategory();
            return ModuleCategoryList;
        }



    }
}
