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
  public class DivisionService : EntityService<Division>, IDivisionService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDivisionRepository _divisionRepository;
        protected readonly DataContext _dbContext;

        public DivisionService(IUnitOfWork unitOfWork, IDivisionRepository divisionRepository, DataContext dbContext)
       : base(unitOfWork, divisionRepository)
        {
            _unitOfWork = unitOfWork;
            _divisionRepository = divisionRepository;
            _dbContext = dbContext;
        }



        public async Task<List<Division>> GetAllDivision()
        {
            return await _divisionRepository.GetAll();
        }

        public async Task<List<Division>> GetDivisionUsingRepo()
        {
            return await _divisionRepository.GetDivisions();
        }

        public async Task<Division> FetchSingleResult(int id)
        {
            var result = await _divisionRepository.FindBy(a => a.Id == id);
            Division model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Division division)
        {
            var result = await _divisionRepository.FindBy(a => a.Id == id);
            Division model = result.FirstOrDefault();
            model.Name = division.Name;
            model.Code = division.Code;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _divisionRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Division division)
        {

            division.CreatedBy = 1;
            division.CreatedDate = DateTime.Now;
            _divisionRepository.Add(division);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<bool> CheckUniqueName(int id, string division)
        {
            // var name = designation;
            bool result = await _divisionRepository.Any(id, division);
            //  var result1 = _dbContext.Designation.Any(t => t.Id != id && t.Name == designation.Name);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _divisionRepository.FindBy(a => a.Id == id);
            Division model = form.FirstOrDefault();
            model.IsActive = 0;
            _divisionRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<List<Department>> GetAllDepartment()
        {
            List<Department> departmentList = await _divisionRepository.GetAllDepartment();
            return departmentList;
        }


        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            List<Zone> zoneList = await _divisionRepository.GetAllZone(departmentId);
            return zoneList;
        }


    }
}
