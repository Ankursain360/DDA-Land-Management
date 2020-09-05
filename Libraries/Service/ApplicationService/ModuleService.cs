using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
            return await _moduleRepository.GetAll();
        }

        public async Task<List<Module>> GetModuleUsingRepo()
        {
            return await _moduleRepository.GetModule();
        }
    }
}
