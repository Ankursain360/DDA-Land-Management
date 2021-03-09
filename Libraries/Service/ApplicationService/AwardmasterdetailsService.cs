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
   public class AwardmasterdetailsService : EntityService<Awardmasterdetail>,IAwardmasterdetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAwardmasterdetailsRepository _awardmasterdetailsRepository;
        private readonly IMapper _mapper;
        public AwardmasterdetailsService(IUnitOfWork unitOfWork, IAwardmasterdetailsRepository awardmasterdetailsRepository, IMapper mapper) : base(unitOfWork, awardmasterdetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _awardmasterdetailsRepository = awardmasterdetailsRepository;
            _mapper = mapper;
        }
        public async Task<List<Awardmasterdetail>> Getawardmasterdetails()
        {
            return await _awardmasterdetailsRepository.Getawardmasterdetails();
          
        }
        public async Task<PagedResult<Awardmasterdetail>> GetPagedawardmasterdetails(AwardMasterDetailsSearchDto model)
        {
            return await _awardmasterdetailsRepository.GetPagedawardmasterdetails(model);
        }
        public async Task<Awardmasterdetail> FetchSingleResult(int id)
        {
            var result = await _awardmasterdetailsRepository.FindBy(a => a.Id == id);
            Awardmasterdetail model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Awardmasterdetail awardmasterdetail)
        {
            var result = await _awardmasterdetailsRepository.FindBy(a => a.Id == id);
            Awardmasterdetail model = result.FirstOrDefault();
            model.AwardNumber = awardmasterdetail.AwardNumber;
            model.AwardDate = awardmasterdetail.AwardDate;
          
            model.Compensation = awardmasterdetail.Compensation;
            model.VillageId = awardmasterdetail.VillageId;
            model.Us6id = awardmasterdetail.Us6id;
            model.Us4id = awardmasterdetail.Us4id;
            model.Us17id = awardmasterdetail.Us17id;
            model.Type = awardmasterdetail.Type;
            model.Remarks = awardmasterdetail.Remarks;
            model.Rate4 = awardmasterdetail.Rate4;
            model.Rate3 = awardmasterdetail.Rate3;
            model.Rate2 = awardmasterdetail.Rate2;
            model.Rate1 = awardmasterdetail.Rate1;
            model.Purpose = awardmasterdetail.Purpose;
            model.ProposalId = awardmasterdetail.ProposalId;
            model.Nature = awardmasterdetail.Nature;
            model.IsActive = awardmasterdetail.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _awardmasterdetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public new async Task<bool> Create(Awardmasterdetail awardmasterdetail)
        {

            awardmasterdetail.CreatedBy = 1;
            awardmasterdetail.CreatedDate = DateTime.Now;
            _awardmasterdetailsRepository.Add(awardmasterdetail);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<bool> CheckUniqueName(int id, string AwardNumber)
        {
            bool result = await _awardmasterdetailsRepository.Any(id, AwardNumber);
        
        
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _awardmasterdetailsRepository.FindBy(a => a.Id == id);
            Awardmasterdetail model = form.FirstOrDefault();
            model.IsActive = 0;
            _awardmasterdetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<List<Acquiredlandvillage>> Getvillage()
        {
            List<Acquiredlandvillage> villageList = await _awardmasterdetailsRepository.Getvillage();
            return villageList;
        }
        public async Task<List<Proposaldetails>> GetPurposal()
        {
            List<Proposaldetails> purposalList = await _awardmasterdetailsRepository.GetPurposal();
            return purposalList;
        }
        public async Task<List<Undersection17>> Getundersection17()
        {
            List<Undersection17> section17List = await _awardmasterdetailsRepository.Getundersection17();
            return section17List;
        }
        public async Task<List<Undersection6>> Getundersection6()
        {
            List<Undersection6> section6List = await _awardmasterdetailsRepository.Getundersection6();
            return section6List;
        }
        public async Task<List<Undersection4>> Getundersection4()
        {
            List<Undersection4> section4List = await _awardmasterdetailsRepository.Getundersection4();
            return section4List;
        }

    }
}
