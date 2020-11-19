using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Service.IApplicationService;
using System;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{
    public class MonthlyRosterService : EntityService<MonthlyRoaster>, IMonthlyRosterService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMonthlyRosterRepository _monthlyRosterService;
        protected readonly DataContext _dbContext;

        public MonthlyRosterService(IUnitOfWork unitOfWork, IMonthlyRosterRepository monthlyRosterService, DataContext dbContext)
       : base(unitOfWork, monthlyRosterService)
        {
            _unitOfWork = unitOfWork;
            _monthlyRosterService = monthlyRosterService;
            _dbContext = dbContext;
        }
        public async Task<bool> Create(MonthlyRoaster monthlyRoaster)
        {

            monthlyRoaster.CreatedBy = 1;
            monthlyRoaster.CreatedDate = DateTime.Now;
            _monthlyRosterService.Add(monthlyRoaster);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public void Create(MonthlyRoster entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(MonthlyRoster entity)
        {
            throw new NotImplementedException();
        }

        public void Update(MonthlyRoster entity)
        {
            throw new NotImplementedException();
        }
    }
}
