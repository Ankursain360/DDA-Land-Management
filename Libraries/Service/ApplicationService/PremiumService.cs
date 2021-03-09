using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{
    public class PremiumService : EntityService<Premiumrate>, IPremiumService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPremiumRepository _PremiumRepository;


        public PremiumService(IUnitOfWork unitOfWork, IPremiumRepository premiumRepository)
          : base(unitOfWork, premiumRepository)
        {
            _unitOfWork = unitOfWork;
            _PremiumRepository = premiumRepository;
        }

    }
}
