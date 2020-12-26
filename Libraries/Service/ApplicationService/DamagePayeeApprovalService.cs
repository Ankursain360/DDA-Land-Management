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

namespace Service.ApplicationService
{
  
    public class DamagePayeeApprovalService : EntityService<Damagepayeeregistertemp>, IDamagePayeeApprovalService
    {
        private readonly IUnitOfWork _unitOfWork;
      //  private readonly IDamagepayeeregisterRepository _damagepayeeregisterRepository;
        private readonly IDamagePayeeApprovalRepository _damagePayeeApprovalRepository;


        public DamagePayeeApprovalService(IUnitOfWork unitOfWork, IDamagePayeeApprovalRepository damagePayeeApprovalRepository)
        : base(unitOfWork, damagePayeeApprovalRepository)
        {
            _unitOfWork = unitOfWork;
            _damagePayeeApprovalRepository = damagePayeeApprovalRepository;
        }

    }
}
