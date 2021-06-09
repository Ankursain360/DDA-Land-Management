using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Libraries.Repository.IEntityRepository;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Libraries.Model;
using Dto.Search;
using AutoMapper;
using Dto.Master;

namespace Libraries.Service.ApplicationService
{
    public class NewlandawardmasterdetailsService : EntityService<Newlandawardmasterdetail>, INewlandawardmasterdetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewlandawardmasterdetailRepository _newlandawardmasterdetailsRepository;
        private readonly IMapper _mapper;
        public NewlandawardmasterdetailsService(IUnitOfWork unitOfWork, INewlandawardmasterdetailRepository newlandawardmasterdetailsRepository, IMapper mapper) : base(unitOfWork, newlandawardmasterdetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _newlandawardmasterdetailsRepository = newlandawardmasterdetailsRepository;
            _mapper = mapper;
        }
        public async Task<List<Newlandawardmasterdetail>> Getawardmasterdetails()
        {
            return await _newlandawardmasterdetailsRepository.Getawardmasterdetails();
            
        }
        public async Task<PagedResult<Newlandawardmasterdetail>> GetPagedawardmasterdetails(NewlandawardmasterSearchDto model)
        {
            return await _newlandawardmasterdetailsRepository.GetPagedawardmasterdetails(model);
        }
        public async Task<Newlandawardmasterdetail> FetchSingleResult(int id)
        {
            var result = await _newlandawardmasterdetailsRepository.FindBy(a => a.Id == id);
            Newlandawardmasterdetail model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Newlandawardmasterdetail newlandawardmasterdetail)
        {
            var result = await _newlandawardmasterdetailsRepository.FindBy(a => a.Id == id);
            Newlandawardmasterdetail model = result.FirstOrDefault();
            model.AwardNumber = newlandawardmasterdetail.AwardNumber;
            model.AwardDate = newlandawardmasterdetail.AwardDate;
            //model.Awardplotdetails = awardmasterdetail.Awardplotdetails;
            model.Compensation = newlandawardmasterdetail.Compensation;
            model.VillageId = newlandawardmasterdetail.VillageId;
            model.Us6id = newlandawardmasterdetail.Us6id;
            model.Us4id = newlandawardmasterdetail.Us4id;
            model.Us17id = newlandawardmasterdetail.Us17id;
            model.Type = newlandawardmasterdetail.Type;
            model.Remarks = newlandawardmasterdetail.Remarks;
            model.Rate4 = newlandawardmasterdetail.Rate4;
            model.Rate3 = newlandawardmasterdetail.Rate3;
            model.Rate2 = newlandawardmasterdetail.Rate2;
            model.Rate1 = newlandawardmasterdetail.Rate1;
            model.Purpose = newlandawardmasterdetail.Purpose;
            model.ProposalId = newlandawardmasterdetail.ProposalId;
            model.Nature = newlandawardmasterdetail.Nature;
            model.DocumentName = newlandawardmasterdetail.DocumentName;
            model.IsActive = newlandawardmasterdetail.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = newlandawardmasterdetail.ModifiedBy;
            _newlandawardmasterdetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public new async Task<bool> Create(Newlandawardmasterdetail newlandawardmasterdetail)
        {
            newlandawardmasterdetail.CreatedDate = DateTime.Now;
            _newlandawardmasterdetailsRepository.Add(newlandawardmasterdetail);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<bool> CheckUniqueName(int id, string AwardNumber)
        {
            bool result = await _newlandawardmasterdetailsRepository.Any(id, AwardNumber);
            //  var result1 = _dbContext.Designation.Any(t => t.Id != id && t.Name == designation.Name);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _newlandawardmasterdetailsRepository.FindBy(a => a.Id == id);
            Newlandawardmasterdetail model = form.FirstOrDefault();
            model.IsActive = 0;
            _newlandawardmasterdetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<List<Newlandvillage>> Getvillage()
        {
            List<Newlandvillage> villageList = await _newlandawardmasterdetailsRepository.Getvillage();
            return villageList;
        }
        public async Task<List<Proposaldetails>> GetPurposal()
        {
            List<Proposaldetails> purposalList = await _newlandawardmasterdetailsRepository.GetPurposal();
            return purposalList;
        }
        public async Task<List<Undersection17>> Getundersection17()
        {
            List<Undersection17> section17List = await _newlandawardmasterdetailsRepository.Getundersection17();
            return section17List;
        }
        public async Task<List<Undersection6>> Getundersection6()
        {
            List<Undersection6> section6List = await _newlandawardmasterdetailsRepository.Getundersection6();
            return section6List;
        }
        public async Task<List<Undersection4>> Getundersection4()
        {
            List<Undersection4> section4List = await _newlandawardmasterdetailsRepository.Getundersection4();
            return section4List;
        }

    }
}
