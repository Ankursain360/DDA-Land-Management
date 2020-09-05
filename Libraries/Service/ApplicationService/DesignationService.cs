using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Libraries.Repository.IEntityRepository;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Libraries.Service.ApplicationService
{
    
    public class DesignationService : EntityService<Designation>, IDesignationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDesignationRepository _designationRepository;

        public DesignationService(IUnitOfWork unitOfWork, IDesignationRepository designationRepository)
        : base(unitOfWork, designationRepository)
        {
            _unitOfWork = unitOfWork;
            _designationRepository = designationRepository;
        }

        public async Task<List<Designation>> GetAllDesignation()
        {
            return await _designationRepository.GetAll();
        }

        public async Task<List<Designation>> GetDesignationUsingRepo()
        {
            return await _designationRepository.GetDesignation();
        }

        public async Task<bool> Update(int id, Designation designation)
        {
          var  result =await _designationRepository.FindBy(a => a.Id == id);
            Designation model = result.FirstOrDefault();
            model.Name = designation.Name;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _designationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
