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

    public class NewlandProposaldetailsService : EntityService<Newlandacquistionproposaldetails>, INewlandProposaldetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewlandProposaldetailsRepository _newlandProposaldetailsRepository;

        public NewlandProposaldetailsService(IUnitOfWork unitOfWork, INewlandProposaldetailsRepository newlandProposaldetailsRepository)
        : base(unitOfWork, newlandProposaldetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _newlandProposaldetailsRepository = newlandProposaldetailsRepository;
        }

        public async Task<List<Newlandacquistionproposaldetails>> GetAllProposaldetails()
        {
            return await _newlandProposaldetailsRepository.GetAllProposaldetails();
        }

        public async Task<List<Newlandacquistionproposaldetails>> GetProposaldetailsUsingRepo()
        {
            return await _newlandProposaldetailsRepository.GetProposaldetails();
        }

        public async Task<Newlandacquistionproposaldetails> FetchSingleResult(int id)
        {
            var result = await _newlandProposaldetailsRepository.FindBy(a => a.Id == id);
            Newlandacquistionproposaldetails model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Newlandacquistionproposaldetails newlandacquistionproposaldetails)
        {
            var result = await _newlandProposaldetailsRepository.FindBy(a => a.Id == id);
            Newlandacquistionproposaldetails model = result.FirstOrDefault();
            model.Name = newlandacquistionproposaldetails.Name;
            model.SchemeId = newlandacquistionproposaldetails.SchemeId;
            model.Name = newlandacquistionproposaldetails.Name;
            model.RequiredAgency = newlandacquistionproposaldetails.RequiredAgency;

            model.ProposalFileNo = newlandacquistionproposaldetails.ProposalFileNo;
            model.Bigha = newlandacquistionproposaldetails.Bigha;
            model.Biswa = newlandacquistionproposaldetails.Biswa;
            model.Biswanshi = newlandacquistionproposaldetails.Biswanshi;
            model.Description = newlandacquistionproposaldetails.Description;
            model.ProposalDate = newlandacquistionproposaldetails.ProposalDate;
            model.IsActive = newlandacquistionproposaldetails.IsActive;

            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _newlandProposaldetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Newlandacquistionproposaldetails newlandacquistionproposaldetails)
        {

            newlandacquistionproposaldetails.CreatedBy = 1;
            newlandacquistionproposaldetails.CreatedDate = DateTime.Now;
            _newlandProposaldetailsRepository.Add(newlandacquistionproposaldetails);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<List<Scheme>> GetAllScheme()
        {
            List<Scheme> schemeList = await _newlandProposaldetailsRepository.GetAllScheme();
            return schemeList;
        }

        public async Task<bool> CheckUniqueName(int id, string newlandacquistionproposaldetails)
        {
            bool result = await _newlandProposaldetailsRepository.Any(id, newlandacquistionproposaldetails);
            //  var result1 = _dbContext.Designation.Any(t => t.Id != id && t.Name == designation.Name);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _newlandProposaldetailsRepository.FindBy(a => a.Id == id);
            Newlandacquistionproposaldetails model = form.FirstOrDefault();
            model.IsActive = 0;
            _newlandProposaldetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Newlandacquistionproposaldetails>> GetPagedProposaldetails(NewlandacquistionproposaldetailsSearchDto model)
        {
            return await _newlandProposaldetailsRepository.GetPagedProposaldetails(model);
        }

    }
}

