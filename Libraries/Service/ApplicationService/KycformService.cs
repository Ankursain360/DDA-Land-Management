using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ApplicationService
{
     public class KycformService : EntityService<Kycform>, IKycformService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IKycformRepository _kycformRepository;
        
        public KycformService(IUnitOfWork unitOfWork, IKycformRepository kycformRepository)
        : base(unitOfWork, kycformRepository)
        {
            _unitOfWork = unitOfWork;
            _kycformRepository = kycformRepository;
            
        }

    }
}
