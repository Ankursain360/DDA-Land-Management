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
    
    public class ProposaldetailsService : EntityService<Proposaldetails>, IProposaldetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProposaldetailsRepository _proposaldetailsRepository;

        public ProposaldetailsService(IUnitOfWork unitOfWork, IProposaldetailsRepository proposaldetailsRepository)
        : base(unitOfWork, proposaldetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _proposaldetailsRepository = proposaldetailsRepository;
        }

        public async Task<List<Proposaldetails>> GetAllProposaldetails()
        {
            return await _proposaldetailsRepository.GetAllProposaldetails();
        }

        public async Task<List<Proposaldetails>> GetProposaldetailsUsingRepo()
        {
            return await _proposaldetailsRepository.GetProposaldetails();
        }

        public async Task<Proposaldetails> FetchSingleResult(int id)
        {
            var result = await _proposaldetailsRepository.FindBy(a => a.Id == id);
            Proposaldetails model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Proposaldetails proposaldetails)
        {
            var result = await _proposaldetailsRepository.FindBy(a => a.Id == id);
            Proposaldetails model = result.FirstOrDefault();
            model.Name = proposaldetails.Name;
            model.SchemeId = proposaldetails.SchemeId;
            model.Name = proposaldetails.Name;
            model.RequiredAgency = proposaldetails.RequiredAgency;

            model.ProposalFileNo = proposaldetails.ProposalFileNo;
            model.Bigha = proposaldetails.Bigha;
            model.Biswa = proposaldetails.Biswa;
            model.Biswanshi = proposaldetails.Biswanshi;
            model.Description = proposaldetails.Description;
            model.ProposalDate = proposaldetails.ProposalDate;
            model.IsActive = proposaldetails.IsActive;

            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _proposaldetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Proposaldetails proposaldetails)
        {

            proposaldetails.CreatedBy = 1;
            proposaldetails.CreatedDate = DateTime.Now;
            _proposaldetailsRepository.Add(proposaldetails);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<List<Scheme>> GetAllScheme()
        {
            List<Scheme> schemeList = await _proposaldetailsRepository.GetAllScheme();
            return schemeList;
        }

        public async Task<bool> CheckUniqueName(int id, string proposaldetails)
        {
            bool result = await _proposaldetailsRepository.Any(id, proposaldetails);
            //  var result1 = _dbContext.Designation.Any(t => t.Id != id && t.Name == designation.Name);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _proposaldetailsRepository.FindBy(a => a.Id == id);
            Proposaldetails model = form.FirstOrDefault();
            model.IsActive = 0;
            _proposaldetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }



    }
}
