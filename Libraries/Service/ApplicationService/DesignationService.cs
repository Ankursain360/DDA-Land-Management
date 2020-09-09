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

        public async Task<Designation> FetchSingleResult(int id)
        {
            var result = await _designationRepository.FindBy(a => a.Id == id);
            Designation model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Designation designation)
        {
            var result = await _designationRepository.FindBy(a => a.Id == id);
            Designation model = result.FirstOrDefault();
            model.Name = designation.Name;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _designationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Designation designation)
        {

            designation.CreatedBy = 1;
            designation.CreatedDate = DateTime.Now;
            _designationRepository.Add(designation);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<bool> CheckUniqueName(int id, string designation)
        {
            bool result = await _designationRepository.Any(id, designation);
          //  var result1 = _dbContext.Designation.Any(t => t.Id != id && t.Name == designation.Name);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _designationRepository.FindBy(a => a.Id == id);
            Designation model = form.FirstOrDefault();
            model.IsActive = 0;
            _designationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
