﻿using Dto.Search;
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
  
    public class ProposalplotdetailsService : EntityService<Proposalplotdetails>, IProposalplotdetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProposalplotdetailsRepository _proposalplotdetailsRepository;

        public ProposalplotdetailsService(IUnitOfWork unitOfWork, IProposalplotdetailsRepository proposalplotdetailsRepository)
        : base(unitOfWork, proposalplotdetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _proposalplotdetailsRepository = proposalplotdetailsRepository;
        }

        public async Task<List<Proposalplotdetails>> GetAllProposalplotdetails()
        {
            return await _proposalplotdetailsRepository.GetAllProposalplotdetails();
        }
        public async Task<List<Proposalplotdetails>> GetAllProposalplotdetailsList(ProposalplotdetailSearchDto model)
        {
            return await _proposalplotdetailsRepository.GetAllProposalplotdetailsList(model);
        }
        public async Task<List<Proposalplotdetails>> GetProposalplotdetailsUsingRepo()
        {
            return await _proposalplotdetailsRepository.GetProposalplotdetails();
        }

        public async Task<Proposalplotdetails> FetchSingleResult(int id)
        {
            var result = await _proposalplotdetailsRepository.FindBy(a => a.Id == id);
            Proposalplotdetails model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Proposalplotdetails proposalplotdetails)
        {
            var result = await _proposalplotdetailsRepository.FindBy(a => a.Id == id);
            Proposalplotdetails model = result.FirstOrDefault();
          
            model.ProposaldetailsId = proposalplotdetails.ProposaldetailsId;
            model.AcquiredlandvillageId = proposalplotdetails.AcquiredlandvillageId;
            model.KhasraId = proposalplotdetails.KhasraId;

          
            model.Bigha = proposalplotdetails.Bigha;
            model.Biswa = proposalplotdetails.Biswa;
            model.Biswanshi = proposalplotdetails.Biswanshi;
            model.IsActive = proposalplotdetails.IsActive;

            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = proposalplotdetails.ModifiedBy;
            _proposalplotdetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Proposalplotdetails proposalplotdetails)
        {

            proposalplotdetails.CreatedBy = proposalplotdetails.CreatedBy;
            proposalplotdetails.CreatedDate = DateTime.Now;
            _proposalplotdetailsRepository.Add(proposalplotdetails);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<List<Proposaldetails>> GetAllProposaldetails()
        {
            List<Proposaldetails> proposaldetailsList = await _proposalplotdetailsRepository.GetAllProposaldetails();
            return proposaldetailsList;
        }
        public async Task<List<Acquiredlandvillage>> GetAllVillage()
        {
            List<Acquiredlandvillage> villageList = await _proposalplotdetailsRepository.GetAllVillage();
            return villageList;
        }
        public async Task<List<Khasra>> GetAllKhasra(int? villageId)
        {
            List<Khasra> khasraList = await _proposalplotdetailsRepository.GetAllKhasra(villageId);
            return khasraList;
        }

        public async Task<Khasra> FetchSingleKhasraResult(int? khasraId)
        {
            return await _proposalplotdetailsRepository.FetchSingleKhasraResult(khasraId);
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _proposalplotdetailsRepository.FindBy(a => a.Id == id);
            Proposalplotdetails model = form.FirstOrDefault();
            model.IsActive = 0;
            _proposalplotdetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Proposalplotdetails>> GetPagedProposalplotdetails(ProposalplotdetailSearchDto model)
        {
            return await _proposalplotdetailsRepository.GetPagedProposalplotdetails(model);
        }

    }
}
