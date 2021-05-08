using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Model.Entity;
using Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{
    public class MonthlyRosterService : EntityService<MonthlyRoaster>, IMonthlyRosterService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMonthlyRosterRepository _monthlyRosterRepository;
        protected readonly DataContext _dbContext;
        public MonthlyRosterService(IUnitOfWork unitOfWork, IMonthlyRosterRepository monthlyRosterRepository, DataContext dbContext)
       : base(unitOfWork, monthlyRosterRepository)
        {
            _unitOfWork = unitOfWork;
            _monthlyRosterRepository = monthlyRosterRepository;
            _dbContext = dbContext;
        }
        public async Task<bool> Create(MonthlyRoaster monthlyRoaster)
        {
            _monthlyRosterRepository.Add(monthlyRoaster);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<List<Department>> GetAllDepartmentList()
        {
            return (await _monthlyRosterRepository.GetAllDepartmentList());
        }

        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            return (await _monthlyRosterRepository.GetAllZone(departmentId));
        }

        public async Task<List<Division>> GetAllDivisionList(int zoneId)
        {
            return (await _monthlyRosterRepository.GetAllDivisionList(zoneId));
        }

        public async Task<List<Locality>> GetAllLocalityList(int divisionId)
        {
            return (await _monthlyRosterRepository.GetAllLocalityList(divisionId));
        }

        public async Task<List<Userprofile>> SecurityGuardList()
        {
            return (await _monthlyRosterRepository.SecurityGuardList());
        }

        public async Task<List<Propertyregistration>> GetPrimaryListNoList(int divisionId, int departmentId, int zoneId, int localityId)
        {
            return await _monthlyRosterRepository.GetPrimaryListNoList(divisionId, departmentId, zoneId, localityId);
        }

        public async Task<PagedResult<MonthlyRoaster>> GetAllRoasterDetails(MonthlyRoasterSearchDto monthlyRoasterSearchDto)
        {
            return await _monthlyRosterRepository.GetAllRoasterDetails(monthlyRoasterSearchDto);
        }
        public async Task<bool> Update(int id, MonthlyRoaster monthlyRoaster)
        {
            var result = await _monthlyRosterRepository.FindBy(a => a.Id == id);
            var model = result.FirstOrDefault();
            model.Month = monthlyRoaster.Month;
            model.Year = monthlyRoaster.Year;
            model.ZoneId = monthlyRoaster.ZoneId;
            model.DivisionId = monthlyRoaster.DivisionId;
            model.LocalityId = monthlyRoaster.LocalityId;
            model.Template = monthlyRoaster.Template;
            model.ModifiedBy = monthlyRoaster.ModifiedBy;
            model.ModifiedDate = DateTime.Now;
            model.UserprofileId = monthlyRoaster.UserprofileId;
            _monthlyRosterRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<MonthlyRoaster> GetMonthlyRoasterById(int id)
        {
            return (await _monthlyRosterRepository.FindBy(x => x.Id == id)).SingleOrDefault();
        }

        public async Task<bool> DeleteRoaster(int id)
        {
            var result = await _monthlyRosterRepository.FindBy(a => a.Id == id);
            var model = result.FirstOrDefault();
            model.IsActive = 0;
            _monthlyRosterRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<List<MonthlyRoaster>> GetAllmonthlyrosterlist()
        {
            return await _monthlyRosterRepository.GetAllmonthlyrosterlist();
        }
    }
}
