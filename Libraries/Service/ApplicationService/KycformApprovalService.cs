

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
    public class KycformApprovalService : EntityService<Kycform>, IKycformApprovalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IKycformApprovalRepository _kycformApprovalRepository;

        public KycformApprovalService(IUnitOfWork unitOfWork, IKycformApprovalRepository kycformApprovalRepository)
        : base(unitOfWork, kycformApprovalRepository)
        {
            _unitOfWork = unitOfWork;
            _kycformApprovalRepository = kycformApprovalRepository;

        }

       
        //public async Task<List<Zone>> GetAllZoneList()
        //{
        //    List<Zone> List = await _kycformRepository.GetAllZoneList();
        //    return List;
        //}
       
       

       

       

       
        //public async Task<PagedResult<Kycform>> GetPagedKycform(KycformSearchDto model)
        //{

        //    return await _kycformRepository.GetPagedKycform(model);

        //}

       


    }
}
