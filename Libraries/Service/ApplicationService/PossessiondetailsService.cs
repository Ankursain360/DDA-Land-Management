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
using Dto.Master;

namespace Libraries.Service.ApplicationService
{
  public  class PossessiondetailsService : EntityService<Possessiondetails>, IPossessiondetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPossessiondetailsRepository _possessiondetailsRepository;

        public PossessiondetailsService(IUnitOfWork unitOfWork, IPossessiondetailsRepository possessiondetailsRepository)
: base(unitOfWork, possessiondetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _possessiondetailsRepository = possessiondetailsRepository;
        }


        public async Task<bool> Delete(int id)
        {
            var form = await _possessiondetailsRepository.FindBy(a => a.Id == id);
            Possessiondetails model = form.FirstOrDefault();
            model.IsActive = 0;
            _possessiondetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Possessiondetails> FetchSingleResult(int id)
        {
            var result = await _possessiondetailsRepository.FindBy(a => a.Id == id);
            Possessiondetails model = result.FirstOrDefault();
            return model;
        }

    
        public async Task<List<Khasra>> BindKhasra(int? villageId)
        {
            List<Khasra> khasraList = await _possessiondetailsRepository.BindKhasra(villageId);
            return khasraList;
        }


        public async Task<List<Acquiredlandvillage>> GetAllVillage()
        {
            List<Acquiredlandvillage> villageList = await _possessiondetailsRepository.GetAllVillage();
            return villageList;
        }


        public async Task<List<Possessiondetails>> GetAllPossessiondetails()
        {

            return await _possessiondetailsRepository.GetAllPossessiondetails();
        }



        public async Task<List<Possessiondetails>> GetPossessiondetailsUsingRepo()
        {
            return await _possessiondetailsRepository.GetAllPossessiondetails();
        }

        public async Task<bool> Update(int id, Possessiondetails possessiondetails)
        {
            var result = await _possessiondetailsRepository.FindBy(a => a.Id == id);
            Possessiondetails model = result.FirstOrDefault();
          
            model.VillageId = possessiondetails.VillageId;
            model.KhasraId = possessiondetails.KhasraId;
            model.IsActive = possessiondetails.IsActive;
            model.PlotNo = possessiondetails.PlotNo;
            model.PossDate = possessiondetails.PossDate;
            model.PossType = possessiondetails.PossType;
            model.Bigha = possessiondetails.Bigha;
            model.Biswa = possessiondetails.Biswa;
            model.ReasonNonPoss = possessiondetails.ReasonNonPoss;
            model.IsActive = possessiondetails.IsActive;
            model.Remarks = possessiondetails.Remarks;
            model.DocumentName = possessiondetails.DocumentName;
            model.PossessionTaken = possessiondetails.PossessionTaken;
            model.Reason = possessiondetails.Reason;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _possessiondetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Possessiondetails possessiondetails)
        {
            possessiondetails.CreatedBy = 1;
            possessiondetails.CreatedDate = DateTime.Now;


            _possessiondetailsRepository.Add(possessiondetails);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<PagedResult<Possessiondetails>> GetPagedNoPossessiondetails(PossessiondetailsSearchDto model)
        {
            return await _possessiondetailsRepository.GetPagedNoPossessiondetails(model);
        }





        public async Task<Khasra> FetchSingleKhasraResult(int? khasraId)
        {
            return await _possessiondetailsRepository.FetchSingleKhasraResult(khasraId);
        }

        public async Task<PagedResult<Possessiondetails>> GetPagedPossessionReport(PossessionReportSearchDto model)
        {
            return await _possessiondetailsRepository.GetPagedPossessionReport(model);
        }

        public async Task<List<PossessionReportDtoProfile>> BindPossessionDateList()
        {
            return await _possessiondetailsRepository.BindPossessionDateList();
        }

        public async Task<List<VillageAndKhasraDetailListDto>> GetPagedvillageAndKhasradetailsList(VillageAndKhasraDetailsSearchDto model)
        {
            return await _possessiondetailsRepository.GetPagedvillageAndKhasradetailsList(model);
        }

    }
}
