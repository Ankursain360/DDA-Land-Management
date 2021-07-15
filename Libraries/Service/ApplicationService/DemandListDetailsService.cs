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
    public class DemandListDetailsService : EntityService<Demandlistdetails>, IDemandListDetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDemandListDetailsRepository _demandListDetailsRepository;

        public DemandListDetailsService(IUnitOfWork unitOfWork, IDemandListDetailsRepository demandListDetailsRepository)
        : base(unitOfWork, demandListDetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _demandListDetailsRepository = demandListDetailsRepository;
        }

        public async Task<PagedResult<Demandlistdetails>> GetPagedDMSFileUploadList(DemandListDetailsSearchDto model)
        {
            return await _demandListDetailsRepository.GetPagedDMSFileUploadList(model);
        }

        public async Task<bool> Create(Demandlistdetails demandlistdetails)
        {
            try
            {
                demandlistdetails.IsActive = 1;
                demandlistdetails.CreatedDate = DateTime.Now;
                _demandListDetailsRepository.Add(demandlistdetails);
                return await _unitOfWork.CommitAsync() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<Demandlistdetails> FetchSingleResult(int id)
        {
            return await _demandListDetailsRepository.FetchSingleResult(id);
        }

        public async Task<bool> Update(int id, Demandlistdetails demandlistdetails)
        {
            var result = await _demandListDetailsRepository.FindBy(a => a.Id == id);
            Demandlistdetails model = result.FirstOrDefault();
            model.DemandListNo = demandlistdetails.DemandListNo;
            model.Enmsno = demandlistdetails.Enmsno;
            model.Lacno = demandlistdetails.Lacno;
            model.LacfileNo = demandlistdetails.LacfileNo;
            model.Lbno = demandlistdetails.Lbno;
            model.LbrefDate = demandlistdetails.LbrefDate;
            model.Rfano = demandlistdetails.Rfano;
            model.Slpno = demandlistdetails.Slpno;
            model.NotificationDate = demandlistdetails.NotificationDate;
            model.DdafileNo = demandlistdetails.DdafileNo;
            model.BalanceInterestCase = demandlistdetails.BalanceInterestCase;
            model.PayableAppealable = demandlistdetails.PayableAppealable;
            model.AwardDate = demandlistdetails.AwardDate;
            model.AwardNo = demandlistdetails.AwardNo;
            model.VillageId = demandlistdetails.VillageId;
            model.KhasraNoId = demandlistdetails.KhasraNoId;
            model.PartyName = demandlistdetails.PartyName;
            model.EnhancedRatePerBigha = demandlistdetails.EnhancedRatePerBigha;
            model.ExistingRatePerBigha = demandlistdetails.ExistingRatePerBigha;
            model.CourtInvolves = demandlistdetails.CourtInvolves;
            model.PayableAmt = demandlistdetails.PayableAmt;
            model.ApealableAmt = demandlistdetails.ApealableAmt;
            model.JundgementDate = demandlistdetails.JundgementDate;
            model.ReasonForNonPay = demandlistdetails.ReasonForNonPay;
            model.Remarks = demandlistdetails.Remarks;
            model.TotalAmount = demandlistdetails.TotalAmount;
            model.ENMDocumentName = demandlistdetails.ENMDocumentName;
            model.ModifiedBy = demandlistdetails.ModifiedBy;
            model.IsActive = demandlistdetails.IsActive;
            model.ModifiedDate = DateTime.Now;
            _demandListDetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Delete(int id, int userId)
        {
            var result = await _demandListDetailsRepository.FindBy(a => a.Id == id);
            Demandlistdetails model = result.FirstOrDefault();
            model.IsActive = 0;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = userId;
            _demandListDetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public int GetLocalityByName(string name)
        {
            return _demandListDetailsRepository.GetLocalityByName(name);
        }

        public int GetKhasraByName(string name)
        {
            return _demandListDetailsRepository.GetKhasraByName(name);
        }

        public async Task<bool> CheckUniqueName(int id, string fileNo)
        {
            return await _demandListDetailsRepository.Any(id, fileNo);
        }
        public async Task<List<Acquiredlandvillage>> GetVillageList()
        {
            return await _demandListDetailsRepository.GetVillageList();
        }

        public async Task<List<Khasra>> GetKhasraList(int id)
        {
            return await _demandListDetailsRepository.GetKhasraList( id);
        }
        public async Task<List<Demandlistdetails>> GetAllDemandlistdetails()
        {
            return await _demandListDetailsRepository.GetAllDemandlistdetails();
        }
        //*********  appeal Details **********

        public async Task<bool> SaveAppeal(Appealdetail appealdetail)
        {
            appealdetail.CreatedBy = appealdetail.CreatedBy;
            appealdetail.CreatedDate = DateTime.Now;
            appealdetail.IsActive = 1;
            return await _demandListDetailsRepository.SaveAppeal(appealdetail);
        }
        public async Task<List<Appealdetail>> GetAllAppeal(int id)
        {
            return await _demandListDetailsRepository.GetAllAppeal(id);
        }
        public async Task<bool> DeleteAppeal(int Id)
        {
            return await _demandListDetailsRepository.DeleteAppeal(Id);
        }

        public async Task<Appealdetail> FetchSingleAppeal(int id)
        {
            return await _demandListDetailsRepository.FetchSingleAppeal(id);
        }
        public async Task<bool> UpdateAppeal(int id, Appealdetail appealdetail)
        {

            return await _demandListDetailsRepository.UpdateAppeal(id, appealdetail);
        }
        //*********  Payment Details **********

        public async Task<bool> SavePayment(Paymentdetail paymentdetail)
        {
            paymentdetail.CreatedBy = paymentdetail.CreatedBy;
            paymentdetail.CreatedDate = DateTime.Now;
            paymentdetail.IsActive = 1;
            return await _demandListDetailsRepository.SavePayment(paymentdetail);
        }
        public async Task<List<Paymentdetail>> GetAllPayment(int id)
        {
            return await _demandListDetailsRepository.GetAllPayment(id);
        }
        public async Task<bool> Deletepayment(int Id)
        {
            return await _demandListDetailsRepository.Deletepayment(Id);
        }

        public async Task<Paymentdetail> FetchSinglePayment(int id)
        {
            return await _demandListDetailsRepository.FetchSinglePayment(id);
        }
        public async Task<bool> UpdatePayment(int id, Paymentdetail paymentdetail)
        {

            return await _demandListDetailsRepository.UpdatePayment(id, paymentdetail);
        }
    }
}
