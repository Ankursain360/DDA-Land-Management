using AutoMapper;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Microsoft.AspNetCore.Identity;
using Model.Entity;
using Repository.IEntityRepository;
using Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


using System.Linq;

namespace Service.ApplicationService
{
   public class ApprovalCompleteService : EntityService<Approvalproccess>, IApprovalCompleteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApprovalCompleteRepository _userrightRepository;

        public ApprovalCompleteService(IUnitOfWork unitOfWork, IApprovalCompleteRepository userrightRepository)
 : base(unitOfWork, userrightRepository)
        {
            _unitOfWork = unitOfWork;
            _userrightRepository = userrightRepository;
        }


        public async Task<List<ApprovalCompleteListDataDto>> GetApprovalCompleteModule(ApprovalCompleteSearchDto model)
        {
            return await _userrightRepository.GetApprovalCompleteModule(model);
        }

        public async Task<List<ApprovalCompleteListDataDto>> BindModuleName()
        {
            return await _userrightRepository.BindModuleName();
        }

    }
}
