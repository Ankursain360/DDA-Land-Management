using Dto.Search;
using Libraries.Model;
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
    public class NewdamagepayeeregistrationService : EntityService<Newdamagepayeeregistration>, INewdamagepayeeregistrationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewdamagepayeeregistrationRepository damagepayeeregistration;
        protected readonly DataContext _dbContext;


        public NewdamagepayeeregistrationService(IUnitOfWork unitOfWork, INewdamagepayeeregistrationRepository _damagepayeeregistration, DataContext dbContext)
       : base(unitOfWork, _damagepayeeregistration)
        {
            _unitOfWork = unitOfWork;
            damagepayeeregistration = _damagepayeeregistration;
            _dbContext = dbContext;
        }
        //public async Task<bool> Any(int id, string name)
        //{
        //    return await damagepayeeregistration.Any(id,name);
        //} 

        public async Task<List<Approvalstatus>> GetAllApprovalStatus()
        {
            return await damagepayeeregistration.GetAllApprovalStatus();
        }

        public async Task<List<New_Damage_Colony>> GetAllColony(int villageId)
        {
            return await damagepayeeregistration.GetAllColony(villageId);
        }

        public async Task<List<Newdamagepayeeregistration>> GetAllDamagePayee()
        {
            return await damagepayeeregistration.GetAllDamagePayee();

        }

        public async Task<List<District>> GetAllDistrict()
        {
            return await damagepayeeregistration.GetAllDistrict();
        }

        public async Task<List<Acquiredlandvillage>> GetAllVillage(int districtId)
        {
            return await damagepayeeregistration.GetAllVillage(districtId);
        }

        public async Task<PagedResult<Newdamagepayeeregistration>> GetPagedDamagePayee(DamagePayeeSearchDto model)
        {
            return await damagepayeeregistration.GetPagedDamagePayee(model);
        }
    }
}
