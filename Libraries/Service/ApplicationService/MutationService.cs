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
    public class MutationService : EntityService<Mutation>, IMutationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMutationRepository _mutationRepository;

        public MutationService(IUnitOfWork unitOfWork, IMutationRepository mutationRepository)
        : base(unitOfWork, mutationRepository)
        {
            _unitOfWork = unitOfWork;
            _mutationRepository = mutationRepository;
        }

        public async Task<PagedResult<Mutation>> GetPagedDMSFileUploadList(DemandListDetailsSearchDto model)
        {
            return await _mutationRepository.GetPagedDMSFileUploadList(model);
        }

        public async Task<bool> Create(Mutation mutation)
        {
            try
            {
                mutation.IsActive = 1;
                mutation.CreatedDate = DateTime.Now;
                _mutationRepository.Add(mutation);
                return await _unitOfWork.CommitAsync() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<Mutation> FetchSingleResult(int id)
        {
            return await _mutationRepository.FetchSingleResult(id);
        }

        public async Task<bool> Update(int id, Mutation mutation)
        {
            var result = await _mutationRepository.FindBy(a => a.Id == id);
            Mutation model = result.FirstOrDefault();
            //model.DemandListNo = mutation.DemandListNo;
            //model.Enmsno = mutation.Enmsno;
            //model.Lacno = mutation.Lacno;
            //model.LacfileNo = mutation.LacfileNo;
            //model.Lbno = mutation.Lbno;
            //model.LbrefDate = mutation.LbrefDate;
            //model.Rfano = mutation.Rfano;
            //model.Slpno = mutation.Slpno;
            //model.NotificationDate = mutation.NotificationDate;
            //model.DdafileNo = mutation.DdafileNo;
            //model.BalanceInterestCase = mutation.BalanceInterestCase;
            //model.PayableAppealable = mutation.PayableAppealable;
            //model.AwardDate = mutation.AwardDate;
            //model.AwardNo = mutation.AwardNo;
            //model.VillageId = mutation.VillageId;
            //model.KhasraNoId = mutation.KhasraNoId;
            //model.PartyName = mutation.PartyName;
            //model.EnhancedRatePerBigha = mutation.EnhancedRatePerBigha;
            //model.ExistingRatePerBigha = mutation.ExistingRatePerBigha;
            //model.CourtInvolves = mutation.CourtInvolves;
            //model.PayableAmt = mutation.PayableAmt;
            //model.ApealableAmt = mutation.ApealableAmt;
            //model.JundgementDate = mutation.JundgementDate;
            //model.ReasonForNonPay = mutation.ReasonForNonPay;
            //model.Remarks = mutation.Remarks;
            //model.TotalAmount = mutation.TotalAmount;
            model.IsActive = mutation.IsActive;
            model.ModifiedDate = DateTime.Now;
            _mutationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Delete(int id, int userId)
        {
            var result = await _mutationRepository.FindBy(a => a.Id == id);
            Mutation model = result.FirstOrDefault();
            model.IsActive = 0;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = userId;
            _mutationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public int GetLocalityByName(string name)
        {
            return _mutationRepository.GetLocalityByName(name);
        }

        public int GetKhasraByName(string name)
        {
            return _mutationRepository.GetKhasraByName(name);
        }

        public async Task<bool> CheckUniqueName(int id, string fileNo)
        {
            return await _mutationRepository.Any(id, fileNo);
        }
        public async Task<List<Acquiredlandvillage>> GetVillageList()
        {
            return await _mutationRepository.GetVillageList();
        }

        public async Task<List<Khasra>> GetKhasraList(int id)
        {
            return await _mutationRepository.GetKhasraList(id);
        }
    }
}
