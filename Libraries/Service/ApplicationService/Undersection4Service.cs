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
    public class  Undersection4Service : EntityService<Undersection4>, IUndersection4service
    {
        private readonly IUnitOfWork _unitOfWork; 
        private readonly IUndersection4Repository _undersection4Repository;
        public Undersection4Service(IUnitOfWork unitOfWork, IUndersection4Repository undersection4Repository)
     : base(unitOfWork, undersection4Repository)
        {
            _unitOfWork = unitOfWork;
            _undersection4Repository = undersection4Repository;
        }




        public async Task<bool> Delete(int id)
        {
            var form = await _undersection4Repository.FindBy(a => a.Id == id);
            Undersection4 model = form.FirstOrDefault();
            model.IsActive = 0;
            _undersection4Repository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Undersection4> FetchSingleResult(int id)
        {
            var result = await _undersection4Repository.FindBy(a => a.Id == id);
            Undersection4 model = result.FirstOrDefault();
            return model;
        }

        public async Task<List<Proposaldetails>> GetAllProposal()
        {
            List<Proposaldetails> purposeList = await _undersection4Repository.GetAllProposal();
            return purposeList;
        }

        public async Task<List<Undersection4>> GetAllUndersection4()
        {

            return await _undersection4Repository.GetAllUndersection4();
        }

       
      
        public async Task<List<Undersection4>> GetUndersection4UsingRepo()
        {
            return await _undersection4Repository.GetAllUndersection4();
        }

        public async Task<bool> Update(int id, Undersection4 undersection4)
        {
            var result = await _undersection4Repository.FindBy(a => a.Id == id);
            Undersection4 model = result.FirstOrDefault();
            model.ProposalId = undersection4.ProposalId;
            model.Number = undersection4.Number;
            model.Ndate = undersection4.Ndate;
            model.Npurpose = undersection4.Npurpose;
            model.TypeDetails = undersection4.TypeDetails;
            model.BoundaryDescription = undersection4.BoundaryDescription;
            model.DocumentName = undersection4.DocumentName;
            model.IsActive = undersection4.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = undersection4.ModifiedBy;
            _undersection4Repository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Undersection4 undersection4)
        {
            undersection4.CreatedDate = DateTime.Now;
            _undersection4Repository.Add(undersection4);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<PagedResult<Undersection4>> GetPagedUndersection4details(Undersection4SearchDto model)
        {
            return await _undersection4Repository.GetPagedUndersection4details(model);
        }




    }
}
