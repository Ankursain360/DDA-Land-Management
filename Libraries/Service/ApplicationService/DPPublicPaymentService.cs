
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
    public class DPPublicPaymentService : EntityService<Demandletters>, IDPPublicPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDPPublicPaymentRepository _dPPublicPaymentRepository;
     
        public DPPublicPaymentService(IUnitOfWork unitOfWork, IDPPublicPaymentRepository dPPublicPaymentRepository)
        : base(unitOfWork, dPPublicPaymentRepository)
        {
            _unitOfWork = unitOfWork;
            _dPPublicPaymentRepository = dPPublicPaymentRepository;
            
        }

        public async Task<Damagepayeeregister> FetchDamagePayeeRegisterDetails(int userId)
        {
            return await _dPPublicPaymentRepository.FetchDamagePayeeRegisterDetails(userId);
        }
        public async Task<List<Demandletters>> GetDemandDetails(string FileNo)
        {
            return await _dPPublicPaymentRepository.GetDemandDetails(FileNo);
        }
    }
}