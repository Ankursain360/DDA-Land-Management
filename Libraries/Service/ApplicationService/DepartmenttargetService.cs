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
   public class DepartmenttargetService : EntityService<Departmenttarget>,IDepartmenttargetService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDepartmenttargetRepository _departmenttargetRepository;
            private readonly IMapper _mapper;
        public DepartmenttargetService(IUnitOfWork unitOfWork,
            IDepartmenttargetRepository departmenttargetRepository,
            IMapper mapper)
        : base(unitOfWork, departmenttargetRepository)
        {
            _unitOfWork = unitOfWork;
            _departmenttargetRepository = (IDepartmenttargetRepository)departmenttargetRepository;
            _mapper = mapper;
        }

        public async Task<List<Departmenttarget>> GetAllDepartmenttarget()
        {
            return await _departmenttargetRepository.GetAllDepartmenttarget();
        }

        public async Task<List<DepartmenttargetDto>> GetDepartmenttarget()
        {
            var departments = await _departmenttargetRepository.FindBy(a => a.IsActive == 1);
            var result = _mapper.Map<List<DepartmenttargetDto>>(departments);
            return result;
        }

        public async Task<PagedResult<Departmenttarget>> GetPagedDepartmenttarget(DepartmentTargetSearchDto model)
        {
            return await _departmenttargetRepository.GetPagedDepartmenttarget(model);
        }

        public async Task<List<Departmenttarget>> GetDepartmenttargetUsingRepo()
        {
            return await _departmenttargetRepository.GetDepartmenttarget();
        }

        public async Task<Departmenttarget> FetchSingleResult(int id)
        {
            var result = await _departmenttargetRepository.FindBy(a => a.Id == id);
            Departmenttarget model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Departmenttarget departmenttarget)
        {
            var result = await _departmenttargetRepository.FindBy(a => a.Id == id);
            Departmenttarget model = result.FirstOrDefault();
            model.DepartmentId = departmenttarget.DepartmentId;
            model.FilesToBeDone = departmenttarget.FilesToBeDone;
            model.WeeklyToBeDone = departmenttarget.WeeklyToBeDone;
            model.IsActive = departmenttarget.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _departmenttargetRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Departmenttarget departmenttarget)
        {

            departmenttarget.CreatedBy = 1;
            departmenttarget.CreatedDate = DateTime.Now;
            _departmenttargetRepository.Add(departmenttarget);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<bool> CheckUniqueName(int id, string departmenttarget)
        {
            bool result = await _departmenttargetRepository.Any(id, departmenttarget);
            //  var result1 = _dbContext.Designation.Any(t => t.Id != id && t.Name == designation.Name);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _departmenttargetRepository.FindBy(a => a.Id == id);
            Departmenttarget model = form.FirstOrDefault();
            model.IsActive = 0;
            _departmenttargetRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> CheckUniqueName(int Id, string Name, int DepartmentId)
        {
            bool result = await _departmenttargetRepository.AnyName(Id, Name, DepartmentId);
            return result;
        }
        public async Task<bool> CheckUniqueCode(int id, int code)
        {
            bool result = await _departmenttargetRepository.AnyCode(id, code);
            return result;
        }
        public async Task<List<Department>> GetAllDepartment()
        {
            List<Department> departmentList = await _departmenttargetRepository.GetAllDepartment();
            return departmentList;
        }
        public async Task<List<WeeklyFileReportListDataDto>> GetPagedWeeklyFileReport(WeeklyFileReportSearchDto model, int UserId)
        {
            return await _departmenttargetRepository.GetPagedWeeklyFileReport(model, UserId);
        }
    }
}
