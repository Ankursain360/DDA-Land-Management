using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Microsoft.EntityFrameworkCore;
using Libraries.Repository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Service.ApplicationService
{

    public class VillageService : EntityService<Village>, IVillageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVillageRepository _villageRepository;
        public VillageService(IUnitOfWork unitOfWork, IVillageRepository villageRepository)
        : base(unitOfWork, villageRepository)
        {
            _unitOfWork = unitOfWork;
            _villageRepository = villageRepository;
        }
        public async Task<List<Village>> GetAllVillage()
        {
            return await _villageRepository.GetVillage();
        }
        public async Task<PagedResult<Village>> GetPagedVillage(VillageSearchDto model)
        {
            return await _villageRepository.GetPagedVillage(model);
        }

        public async Task<List<Village>> GetVillageUsingRepo()
        {
            return await _villageRepository.GetVillage();
        }

        public async Task<bool> Update(int id, Village village)
        {
            var result = await _villageRepository.FindBy(a => a.Id == id);
            Village model = result.FirstOrDefault();
            model.Name = village.Name;
            model.DepartmentId = village.DepartmentId;
            model.ZoneId = village.ZoneId;
            model.DivisionId = village.DivisionId;
           
            model.IsActive = village.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _villageRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Village village)
        {

            village.CreatedBy = 1;
            village.CreatedDate = DateTime.Now;
            _villageRepository.Add(village);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<Village> FetchSingleResult(int id)
        {
            var result = await _villageRepository.FindBy(a => a.Id == id);
            Village model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _villageRepository.FindBy(a => a.Id == id);
            Village model = form.FirstOrDefault();
            model.IsActive = 0;
            _villageRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            List<Zone> zoneList = await _villageRepository.GetAllZone(departmentId);
            return zoneList;
        }
        public async Task<bool> CheckUniqueName(int id, string name)
        {
            bool result = await _villageRepository.Any(id, name);
            return result;
        }

        public async Task<List<Division>> GetAllDivisionList(int departmentId)
        {
            List<Division> divisionList = await _villageRepository.GetAllDivisionList(departmentId);
            return divisionList;
        }

        public async Task<List<Department>> GetAllDepartment()
        {
            List<Department> departmentList = await _villageRepository.GetAllDepartmentList();
            return departmentList;
        }
    }
}
