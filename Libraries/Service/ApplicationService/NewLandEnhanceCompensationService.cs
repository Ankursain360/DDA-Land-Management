using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Libraries.Repository.IEntityRepository;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Dto.Search;
namespace Libraries.Service.ApplicationService
{
    public class NewLandEnhanceCompensationService : EntityService<Newlandenhancecompensation>, INewLandEnhanceCompensationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewLandEnhanceCompensationRepository _newLandEnhanceCompensationRepository;
        public NewLandEnhanceCompensationService(IUnitOfWork unitOfWork, INewLandEnhanceCompensationRepository newLandEnhanceCompensationRepository)
      : base(unitOfWork, newLandEnhanceCompensationRepository)
        {
            _unitOfWork = unitOfWork;
            _newLandEnhanceCompensationRepository = newLandEnhanceCompensationRepository;
        }





        public async Task<bool> Delete(int id)
        {
            var form = await _newLandEnhanceCompensationRepository.FindBy(a => a.Id == id);
            Newlandenhancecompensation model = form.FirstOrDefault();
            model.IsActive = 0;
            _newLandEnhanceCompensationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Newlandenhancecompensation> FetchSingleResult(int id)
        {
            var result = await _newLandEnhanceCompensationRepository.FindBy(a => a.Id == id);
            Newlandenhancecompensation model = result.FirstOrDefault();
            return model;
        }



        public async Task<List<Newlandenhancecompensation>> GetAllNewlandenhancecompensation()
        {

            return await _newLandEnhanceCompensationRepository.GetAllNewlandenhancecompensation();
        }


        public async Task<List<Newlandvillage>> GetAllVillage()
        {
            List<Newlandvillage> villageList = await _newLandEnhanceCompensationRepository.GetAllVillage();
            return villageList;
        }
        public async Task<List<Newlandkhasra>> GetAllKhasra(int? villageId)
        {
            List<Newlandkhasra> khasraList = await _newLandEnhanceCompensationRepository.GetAllKhasra(villageId);
            return khasraList;
        }
        public async Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId)
        {
            return await _newLandEnhanceCompensationRepository.FetchSingleKhasraResult(khasraId);
        }



        public async Task<List<Newlandenhancecompensation>> GetNewlandenhancecompensationUsingRepo()
        {
            return await _newLandEnhanceCompensationRepository.GetAllNewlandenhancecompensation();
        }
        public async Task<PagedResult<Newlandenhancecompensation>> GetPagedNewlandenhancecompensation(NewlandenhancecompensationSearchDto model)
        {
            return await _newLandEnhanceCompensationRepository.GetPagedNewlandenhancecompensation(model);
        }

        public async Task<bool> Update(int id, Newlandenhancecompensation newlandenhancecompensation)
        {
            var result = await _newLandEnhanceCompensationRepository.FindBy(a => a.Id == id);
            Newlandenhancecompensation model = result.FirstOrDefault();
            model.DemandListNo = newlandenhancecompensation.DemandListNo;
            model.EnmSno = newlandenhancecompensation.EnmSno;
            model.LacfileNo = newlandenhancecompensation.LacfileNo;
            model.Lbno = newlandenhancecompensation.Lbno;
            model.Lbrefdate = newlandenhancecompensation.Lbrefdate;
            model.DdafileNo = newlandenhancecompensation.DdafileNo;
            model.Lacno = newlandenhancecompensation.Lacno;
            model.Rfano = newlandenhancecompensation.Rfano;
            model.CourtCaseNo = newlandenhancecompensation.CourtCaseNo;
            model.PayableAppealable = newlandenhancecompensation.PayableAppealable;
            model.DateOfJudgement = newlandenhancecompensation.DateOfJudgement;
            model.CaseInvolesWhichCourt = newlandenhancecompensation.CaseInvolesWhichCourt;
            model.PartyName = newlandenhancecompensation.PartyName;
            model.VillageId = newlandenhancecompensation.VillageId; 
            model.KhasraId = newlandenhancecompensation.KhasraId;
            model.Bigha = newlandenhancecompensation.Bigha;
            model.Biswa = newlandenhancecompensation.Biswa;
            model.Biswanshi = newlandenhancecompensation.Biswanshi;           
            model.Remarks = newlandenhancecompensation.Remarks;
            model.ENMDocumentName = newlandenhancecompensation.ENMDocumentName;
            model.IsActive = newlandenhancecompensation.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = newlandenhancecompensation.ModifiedBy;
            _newLandEnhanceCompensationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Newlandenhancecompensation newlandenhancecompensation)
        {
            newlandenhancecompensation.CreatedDate = DateTime.Now;
            _newLandEnhanceCompensationRepository.Add(newlandenhancecompensation);
            return await _unitOfWork.CommitAsync() > 0;
        }

        //public async Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId)
        //{
        //    return await _newLandEnhanceCompensationRepository.FetchSingleKhasraResult(khasraId);
        //}


    }
}

