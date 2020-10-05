using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Microsoft.EntityFrameworkCore;
using Libraries.Repository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Service.ApplicationService
{
   public class EnhancecompensationService: EntityService<Enhancecompensation>, IEnhancecompensationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEnhancecompensationRepository _EnhancecompensationRepository;

        public EnhancecompensationService(IUnitOfWork unitOfWork, IEnhancecompensationRepository enhancecompensationRepository)
: base(unitOfWork, enhancecompensationRepository)
        {
            _unitOfWork = unitOfWork;
            _EnhancecompensationRepository = enhancecompensationRepository;
        }





        public async Task<bool> Delete(int id)
        {
            var form = await _EnhancecompensationRepository.FindBy(a => a.Id == id);
            Enhancecompensation model = form.FirstOrDefault();
            model.IsActive = 0;
            _EnhancecompensationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Enhancecompensation> FetchSingleResult(int id)
        {
            var result = await _EnhancecompensationRepository.FindBy(a => a.Id == id);
            Enhancecompensation model = result.FirstOrDefault();
            return model;
        }



        public async Task<List<Khasra>> BindKhasra()
        {
            List<Khasra> khasraList = await _EnhancecompensationRepository.BindKhasra();
            return khasraList;
        }
        public async Task<List<Acquiredlandvillage>> GetAllVillage()
        {
            List<Acquiredlandvillage> villageList = await _EnhancecompensationRepository.GetAllVillage();
            return villageList;
        }


        public async Task<List<Enhancecompensation>> GetAllEnhancecompensation()
        {

            return await _EnhancecompensationRepository.GetAllEnhancecompensation();
        }



        public async Task<List<Enhancecompensation>> GetEnhancecompensationUsingRepo()
        {
            return await _EnhancecompensationRepository.GetAllEnhancecompensation();
        }

        public async Task<bool> Update(int id, Enhancecompensation enhancecompensation)
        {
            var result = await _EnhancecompensationRepository.FindBy(a => a.Id == id);
            Enhancecompensation model = result.FirstOrDefault();

            model.DemandListNo = enhancecompensation.DemandListNo;
            model.Enmsno = enhancecompensation.Enmsno;
            model.LacfileNo = enhancecompensation.LacfileNo;
            model.Lbno = enhancecompensation.Lbno;
            model.LbrefDate = enhancecompensation.LbrefDate;
            model.DdafileNo = enhancecompensation.DdafileNo;
            model.Lacno = enhancecompensation.Lacno;
            model.Rfano = enhancecompensation.Rfano;
            model.Slpno = enhancecompensation.Slpno;
            model.Payable = enhancecompensation.Payable;
            model.JudgementDate = enhancecompensation.JudgementDate;
            model.CaseCourt = enhancecompensation.CaseCourt;
            model.PartyName = enhancecompensation.PartyName;
            model.DemandStatus = enhancecompensation.DemandStatus;


            model.VillageId = enhancecompensation.VillageId;
            model.KhasraId = enhancecompensation.KhasraId;
         
          
            model.Bigha = enhancecompensation.Bigha;
            model.Biswa = enhancecompensation.Biswa;
            model.Biswanshi = enhancecompensation.Biswanshi;
            model.AmountPaid = enhancecompensation.AmountPaid;
            model.ChequeDate = enhancecompensation.ChequeDate;
            model.ChequeNo = enhancecompensation.ChequeNo;
            model.BankName = enhancecompensation.BankName;
            model.VoucherNo = enhancecompensation.VoucherNo;
            model.PercentPaid = enhancecompensation.PercentPaid;
            model.PaymentStatus = enhancecompensation.PaymentStatus;
            model.AppealNo = enhancecompensation.AppealNo;
            model.AppealDept = enhancecompensation.AppealDept;
            model.DateOfAppeal = enhancecompensation.DateOfAppeal;
            model.PanelLawer = enhancecompensation.PanelLawer;
            model.IsActive = enhancecompensation.IsActive;
            model.Remarks = enhancecompensation.Remarks;
        


            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _EnhancecompensationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Enhancecompensation enhancecompensation)
        {
            enhancecompensation.CreatedBy = 1;
            enhancecompensation.CreatedDate = DateTime.Now;
            enhancecompensation.IsActive = 1;

            _EnhancecompensationRepository.Add(enhancecompensation);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<PagedResult<Enhancecompensation>> GetPagedEnhancecompensation(EnhancecompensationSearchDto model)
        {
            return await _EnhancecompensationRepository.GetPagedEnhancecompensation(model);
        }



    }
}
