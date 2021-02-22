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

namespace Libraries.Service.ApplicationService
{

    public class NewLandProposalPlotDetailsService : EntityService<Newlandacquistionproposalplotdetails>, INewLandProposalPlotDetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewLandProposalPlotDetailsRepository _newLandProposalPlotDetailsRepository;

        public NewLandProposalPlotDetailsService(IUnitOfWork unitOfWork, INewLandProposalPlotDetailsRepository newLandProposalPlotDetailsRepository)
        : base(unitOfWork, newLandProposalPlotDetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _newLandProposalPlotDetailsRepository = newLandProposalPlotDetailsRepository;
        }

        public async Task<List<Newlandacquistionproposalplotdetails>> GetAllProposalplotdetails()
        {
            return await _newLandProposalPlotDetailsRepository.GetAllProposalplotdetails();
        }

        public async Task<List<Newlandacquistionproposalplotdetails>> GetProposalplotdetailsUsingRepo()
        {
            return await _newLandProposalPlotDetailsRepository.GetAllProposalplotdetails();
        }

        public async Task<Newlandacquistionproposalplotdetails> FetchSingleResult(int id)
        {
            var result = await _newLandProposalPlotDetailsRepository.FindBy(a => a.Id == id);
            Newlandacquistionproposalplotdetails model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Newlandacquistionproposalplotdetails newlandacquistionproposalplotdetails)
        {
            var result = await _newLandProposalPlotDetailsRepository.FindBy(a => a.Id == id);
            Newlandacquistionproposalplotdetails model = result.FirstOrDefault();

            model.ProposaldetailsId = newlandacquistionproposalplotdetails.ProposaldetailsId;
            model.AcquiredlandvillageId = newlandacquistionproposalplotdetails.AcquiredlandvillageId;
            model.KhasraId = newlandacquistionproposalplotdetails.KhasraId;


            model.Bigha = newlandacquistionproposalplotdetails.Bigha;
            model.Biswa = newlandacquistionproposalplotdetails.Biswa;
            model.Biswanshi = newlandacquistionproposalplotdetails.Biswanshi;
            model.IsActive = newlandacquistionproposalplotdetails.IsActive;

            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = newlandacquistionproposalplotdetails.ModifiedBy;
            _newLandProposalPlotDetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Newlandacquistionproposalplotdetails newlandacquistionproposalplotdetails)
        {

            newlandacquistionproposalplotdetails.CreatedBy = newlandacquistionproposalplotdetails.CreatedBy;
            newlandacquistionproposalplotdetails.CreatedDate = DateTime.Now;
            _newLandProposalPlotDetailsRepository.Add(newlandacquistionproposalplotdetails);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<List<Newlandacquistionproposaldetails>> GetAllProposaldetails()
        {
            List<Newlandacquistionproposaldetails> proposaldetailsList = await _newLandProposalPlotDetailsRepository.GetAllProposaldetails();
            return proposaldetailsList;
        }

        public async Task<List<Newlandvillage>> GetAllVillageList()
        {
            List<Newlandvillage> villageList = await _newLandProposalPlotDetailsRepository.GetAllVillageList();
            return villageList;
        }
        public async Task<List<Newlandkhasra>> GetAllKhasraList(int? villageId)
        {
            List<Newlandkhasra> khasraList = await _newLandProposalPlotDetailsRepository.GetAllKhasraList(villageId);
            return khasraList;
        }
        public async Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId)
        {
            return await _newLandProposalPlotDetailsRepository.FetchSingleKhasraResult(khasraId);
        }



        public async Task<bool> Delete(int id)
        {
            var form = await _newLandProposalPlotDetailsRepository.FindBy(a => a.Id == id);
            Newlandacquistionproposalplotdetails model = form.FirstOrDefault();
            model.IsActive = 0;
            _newLandProposalPlotDetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Newlandacquistionproposalplotdetails>> GetPagedProposalplotdetails(NewLandProposalplotdetailSearchDto model)
        {
            return await _newLandProposalPlotDetailsRepository.GetPagedProposalplotdetails(model);
        }
      
    }
}
