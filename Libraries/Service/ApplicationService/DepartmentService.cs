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
using AutoMapper;
using Dto.Master;

namespace Libraries.Service.ApplicationService
{

    public class DepartmentService : EntityService<Department>, IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        public DepartmentService(IUnitOfWork unitOfWork,
            IDepartmentRepository departmentRepository,
            IMapper mapper)
        : base(unitOfWork, departmentRepository)
        {
            _unitOfWork = unitOfWork;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<List<Department>> GetAllDepartment()
        {
            return await _departmentRepository.GetAll();
        }

        public async Task<List<DepartmentDto>> GetDepartment()
        {
            var departments = await _departmentRepository.FindBy(a => a.IsActive == 1);
            var result = _mapper.Map<List<DepartmentDto>>(departments);
            return result;
        }

        public async Task<PagedResult<Department>> GetPagedDepartment(DepartmentSearchDto model)
        {
            return await _departmentRepository.GetPagedDepartment(model);
        }

        public async Task<List<Department>> GetDepartmentUsingRepo()
        {
            return await _departmentRepository.GetDepartment();
        }

        public async Task<Department> FetchSingleResult(int id)
        {
            var result = await _departmentRepository.FindBy(a => a.Id == id);
            Department model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Department department)
        {
            var result = await _departmentRepository.FindBy(a => a.Id == id);
            Department model = result.FirstOrDefault();
            model.Name = department.Name;
            model.IsActive = department.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _departmentRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Department department)
        {

            department.CreatedBy = 1;
            department.CreatedDate = DateTime.Now;
            _departmentRepository.Add(department);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<bool> CheckUniqueName(int id, string department)
        {
            bool result = await _departmentRepository.Any(id, department);
            //  var result1 = _dbContext.Designation.Any(t => t.Id != id && t.Name == designation.Name);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _departmentRepository.FindBy(a => a.Id == id);
            Department model = form.FirstOrDefault();
            model.IsActive = 0;
            _departmentRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
