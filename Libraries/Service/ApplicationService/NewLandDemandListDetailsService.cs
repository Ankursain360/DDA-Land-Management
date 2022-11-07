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
    public class NewLandDemandListDetailsService : EntityService<Newlanddemandlistdetails>, INewLandDemandListDetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewLandDemandListDetailsRepository _newLandDemandListDetailsRepository;

        public NewLandDemandListDetailsService(IUnitOfWork unitOfWork, INewLandDemandListDetailsRepository newLandDemandListDetailsRepository)
        : base(unitOfWork, newLandDemandListDetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _newLandDemandListDetailsRepository = newLandDemandListDetailsRepository;
        }

        public async Task<PagedResult<Newlanddemandlistdetails>> GetPagedDMSFileUploadList(NewLandDemandListDetailsSearchDto model)
        {
            return await _newLandDemandListDetailsRepository.GetPagedDMSFileUploadList(model);
        }

        public async Task<bool> Create(Newlanddemandlistdetails newlanddemandlistdetails)
        {
            try
            {
                newlanddemandlistdetails.IsActive = 1;
                newlanddemandlistdetails.CreatedDate = DateTime.Now;
                _newLandDemandListDetailsRepository.Add(newlanddemandlistdetails);
                return await _unitOfWork.CommitAsync() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<Newlanddemandlistdetails> FetchSingleResult(int id)
        {
            return await _newLandDemandListDetailsRepository.FetchSingleResult(id);
        }

        public async Task<bool> Update(int id, Newlanddemandlistdetails newlanddemandlistdetails)
        {
            var result = await _newLandDemandListDetailsRepository.FindBy(a => a.Id == id);
            Newlanddemandlistdetails model = result.FirstOrDefault();
            model.DemandListNo = newlanddemandlistdetails.DemandListNo;
            model.Enmsno = newlanddemandlistdetails.Enmsno;
            model.Lacno = newlanddemandlistdetails.Lacno;
            model.LacfileNo = newlanddemandlistdetails.LacfileNo;
            model.Lbno = newlanddemandlistdetails.Lbno;
            model.LbrefDate = newlanddemandlistdetails.LbrefDate;
            model.Rfano = newlanddemandlistdetails.Rfano;
            model.Slpno = newlanddemandlistdetails.Slpno;
            model.NotificationDate = newlanddemandlistdetails.NotificationDate;
            model.DdafileNo = newlanddemandlistdetails.DdafileNo;
            model.BalanceInterestCase = newlanddemandlistdetails.BalanceInterestCase;
            model.PayableAppealable = newlanddemandlistdetails.PayableAppealable;
            model.AwardDate = newlanddemandlistdetails.AwardDate;
            model.AwardNo = newlanddemandlistdetails.AwardNo;
            model.VillageId = newlanddemandlistdetails.VillageId;
            model.KhasraNoId = newlanddemandlistdetails.KhasraNoId;
            model.PartyName = newlanddemandlistdetails.PartyName;
            model.EnhancedRatePerBigha = newlanddemandlistdetails.EnhancedRatePerBigha;
            model.ExistingRatePerBigha = newlanddemandlistdetails.ExistingRatePerBigha;
            model.CourtInvolves = newlanddemandlistdetails.CourtInvolves;
            model.PayableAmt = newlanddemandlistdetails.PayableAmt;
            model.ApealableAmt = newlanddemandlistdetails.ApealableAmt;
            model.JundgementDate = newlanddemandlistdetails.JundgementDate;
            model.ReasonForNonPay = newlanddemandlistdetails.ReasonForNonPay;
            model.Remarks = newlanddemandlistdetails.Remarks;
            model.TotalAmount = newlanddemandlistdetails.TotalAmount;
            model.ENMDocumentName = newlanddemandlistdetails.ENMDocumentName;
            model.ModifiedBy = newlanddemandlistdetails.ModifiedBy;
            model.IsActive = newlanddemandlistdetails.IsActive;
            model.ModifiedDate = DateTime.Now;
            _newLandDemandListDetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Delete(int id, int userId)
        {
            var result = await _newLandDemandListDetailsRepository.FindBy(a => a.Id == id);
            Newlanddemandlistdetails model = result.FirstOrDefault();
            model.IsActive = 0;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = userId;
            _newLandDemandListDetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public int GetLocalityByName(string name)
        {
            return _newLandDemandListDetailsRepository.GetLocalityByName(name);
        }

        public int GetKhasraByName(string name)
        {
            return _newLandDemandListDetailsRepository.GetKhasraByName(name);
        }

        public async Task<bool> CheckUniqueName(int id, string fileNo)
        {
            return await _newLandDemandListDetailsRepository.Any(id, fileNo);
        }
        public async Task<List<Newlandvillage>> GetVillageList()
        {
            return await _newLandDemandListDetailsRepository.GetVillageList();
        }

        public async Task<List<Newlandkhasra>> GetKhasraList(int id)
        {
            return await _newLandDemandListDetailsRepository.GetKhasraList(id);
        }
        public async Task<List<Newlanddemandlistdetails>> GetAllDemandlistdetails()
        {
            return await _newLandDemandListDetailsRepository.GetAllDemandlistdetails();
        }
        public async Task<List<Newlanddemandlistdetails>> GetAllDMSFileUploadListList(NewLandDemandListDetailsSearchDto model)
        {
            return await _newLandDemandListDetailsRepository.GetAllDMSFileUploadListList(model);
        }

        //*********  appeal Details **********

        public async Task<bool> SaveAppeal(Newlandappealdetail newlandappealdetail)
        {
            newlandappealdetail.CreatedBy = newlandappealdetail.CreatedBy;
            newlandappealdetail.CreatedDate = DateTime.Now;
            newlandappealdetail.IsActive = 1;
            return await _newLandDemandListDetailsRepository.SaveAppeal(newlandappealdetail);
        }
        public async Task<List<Newlandappealdetail>> GetAllAppeal(int id)
        {
            return await _newLandDemandListDetailsRepository.GetAllAppeal(id);
        }
        public async Task<bool> DeleteAppeal(int Id)
        {
            return await _newLandDemandListDetailsRepository.DeleteAppeal(Id);
        }

        public async Task<Newlandappealdetail> FetchSingleAppeal(int id)
        {
            return await _newLandDemandListDetailsRepository.FetchSingleAppeal(id);
        }
        public async Task<bool> UpdateAppeal(int id, Newlandappealdetail newlandappealdetail)
        {

            return await _newLandDemandListDetailsRepository.UpdateAppeal(id, newlandappealdetail);
        }
        //*********  Payment Details **********

        public async Task<bool> SavePayment(Newlandpaymentdetail newlandpaymentdetail)
        {
            newlandpaymentdetail.CreatedBy = newlandpaymentdetail.CreatedBy;
            newlandpaymentdetail.CreatedDate = DateTime.Now;
            newlandpaymentdetail.IsActive = 1;
            return await _newLandDemandListDetailsRepository.SavePayment(newlandpaymentdetail);
        }
        public async Task<List<Newlandpaymentdetail>> GetAllPayment(int id)
        {
            return await _newLandDemandListDetailsRepository.GetAllPayment(id);
        }
        public async Task<bool> Deletepayment(int Id)
        {
            return await _newLandDemandListDetailsRepository.Deletepayment(Id);
        }

        public async Task<Newlandpaymentdetail> FetchSinglePayment(int id)
        {
            return await _newLandDemandListDetailsRepository.FetchSinglePayment(id);
        }
        public async Task<bool> UpdatePayment(int id, Newlandpaymentdetail newlandpaymentdetail)
        {

            return await _newLandDemandListDetailsRepository.UpdatePayment(id, newlandpaymentdetail);
        }
        public async Task<Newlandpaymentdetail> GetPaymentProofDocument(int Id)
        {
            return await _newLandDemandListDetailsRepository.GetPaymentProofDocument(Id);
        }
    }
}
