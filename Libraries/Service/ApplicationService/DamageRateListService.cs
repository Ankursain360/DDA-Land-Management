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

namespace Libraries.Service.ApplicationService
{

    public class DamageRateListService : EntityService<Resratelisttypea>, IDamageRateListService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDamageRateListRepository _damageRateListRepository;

        public DamageRateListService(IUnitOfWork unitOfWork, IDamageRateListRepository damageRateListRepository)
        : base(unitOfWork, damageRateListRepository)
        {
            _unitOfWork = unitOfWork;
            _damageRateListRepository = damageRateListRepository;
        }


    }
}
