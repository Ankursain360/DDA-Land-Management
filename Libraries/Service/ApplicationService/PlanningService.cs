using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.ApplicationService
{
    public class PlanningService : EntityService<Planning>, IPlanningService
    {
        public readonly IPlanningRepositry _planningRepositry;
        private readonly IUnitOfWork _unitOfWork;
        public PlanningService(IUnitOfWork unitOfWork, IPlanningRepositry planningRepositry)
        : base(unitOfWork, planningRepositry)
        {
            _unitOfWork = unitOfWork;
            _planningRepositry = planningRepositry;
        }
        public async Task<bool> Create(Planning planning)
        {
            planning.CreatedDate = DateTime.Now;
            planning.CreatedBy = 1;
            planning.IsActive = 1;
            planning.IsVerify = 0;
            _planningRepositry.Add(planning);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> CreateProperties(List<PlanningProperties> planningProperties)
        {
            return await _planningRepositry.CreateProperties(planningProperties);
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _planningRepositry.FindBy(a => a.Id == id);
            Planning model = result.FirstOrDefault();
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 0;
            model.IsActive = 0;
            _planningRepositry.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<List<int>> FetchPlannedProperties(int id)
        {
            return await _planningRepositry.FetchPlannedProperties(id);
        }

        public async Task<Planning> FetchSingleResult(int id)
        {
            return (await _planningRepositry.FindBy(x => x.Id == id)).FirstOrDefault();
        }

        public async Task<List<int>> FetchUnplannedProperties(int id)
        {
            return await _planningRepositry.FetchUnplannedProperties(id);
        }

        public async Task<List<Department>> GetAllDepartment()
        {
            return await _planningRepositry.GetAllDepartment();
        }

        public async Task<List<Division>> GetAllDivision(int ZoneId)
        {
            return await _planningRepositry.GetAllDivision(ZoneId);
        }

        public async Task<List<Zone>> GetAllZone(int DepartmentId)
        {
            return await _planningRepositry.GetAllZone(DepartmentId);
        }

        public async Task<PagedResult<Planning>> GetPagedPlanning(PlanningSearchDto dto)
        {
            return await _planningRepositry.GetPagedPlanning(dto);
        }

        public async Task<List<Propertyregistration>> GetPlannedProperties(int departmentId, int zoneId, int divisionId)
        {
            return await _planningRepositry.GetPlannedProperties(departmentId, zoneId, divisionId);
        }

        public async Task<List<Propertyregistration>> GetUnplannedProperties(int departmentId, int zoneId, int divisionId)
        {
            return await _planningRepositry.GetUnplannedProperties(departmentId, zoneId, divisionId);
        }

        public async Task<PagedResult<Planning>> GetUnverifiedPagedPlanning(PlanningSearchDto dto)
        {
            return await _planningRepositry.GetUnverifiedPagedPlanning(dto);
        }

        public async Task<bool> Update(int id, Planning planning)
        {
            var result = await _planningRepositry.FindBy(a => a.Id == id);
            Planning model = result.FirstOrDefault();
            model.DepartmentId = planning.DepartmentId;
            model.ZoneId = planning.ZoneId;
            model.DivisionId = planning.DivisionId;
            model.Remarks = planning.Remarks;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _planningRepositry.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> VerifyProperties(int PlanningId)
        {
            return await _planningRepositry.DeleteProperties(PlanningId);
        }

        public async Task<bool> VerifyProperty(int id, Planning planning)
        {
            var result = await _planningRepositry.FindBy(a => a.Id == id);
            Planning model = result.FirstOrDefault();
            model.IsVerify = planning.IsVerify;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _planningRepositry.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
