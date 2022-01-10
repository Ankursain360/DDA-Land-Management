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
    public class CancellationEntryService : EntityService<Cancellationentry>, ICancellationEntryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICancellationEntryRepository _cancellationEntryRepository;

        public CancellationEntryService(IUnitOfWork unitOfWork, ICancellationEntryRepository cancellationEntryRepository) : base(unitOfWork, cancellationEntryRepository)
        {
            _unitOfWork = unitOfWork;
            _cancellationEntryRepository = cancellationEntryRepository;
        }

        public async Task<List<UserBindDropdownDto>> BindUsernameNameList()
        {
            return await _cancellationEntryRepository.BindUsernameNameList();
        }

        public async Task<List<Allotmententry>> GetAllAllotment()
        {
            List<Allotmententry> villageList = await _cancellationEntryRepository.GetAllAllotment();
            return villageList;
        }

        public async Task<List<Honble>> GetAllHonble()
        {
            List<Honble> villageList = await _cancellationEntryRepository.GetAllHonble();
            return villageList;
        }

        public async Task<List<Cancellationentry>> GetAllRequestForProceeding()
        {

            return await _cancellationEntryRepository.GetAllRequestForProceeding();
        }


        public async Task<bool> Create(Cancellationentry cancellationentry)
        {

            cancellationentry.CreatedBy = 1;
            cancellationentry.CreatedDate = DateTime.Now;
            _cancellationEntryRepository.Add(cancellationentry);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<List<Cancellationentry>> GetRequestUsingRepo()
        {
            return await _cancellationEntryRepository.GetAllRequestForProceeding();
        }

        public async Task<bool> Update(int id, Cancellationentry cancellationentry)
        {
            var result = await _cancellationEntryRepository.FindBy(a => a.Id == id);
            Cancellationentry model = result.FirstOrDefault();
            model.AllotmentId = cancellationentry.AllotmentId;
            model.Subject = cancellationentry.Subject;
            model.GroundOfViolations = cancellationentry.GroundOfViolations;
            model.DateOfCancellationofLease = cancellationentry.DateOfCancellationofLease;
            model.DemandLetter = cancellationentry.DemandLetter;
            model.Noc = cancellationentry.Noc;
            model.CancellationOrder = cancellationentry.CancellationOrder;
            model.HonebleLgOrCommon = cancellationentry.HonebleLgOrCommon;
            model.ProceedingEvictionPossession = cancellationentry.ProceedingEvictionPossession;
            model.CourtCaseifAny = cancellationentry.CourtCaseifAny;
            model.IsActive = cancellationentry.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _cancellationEntryRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Cancellationentry> FetchSingleResult(int id)
        {
            var result = await _cancellationEntryRepository.FindBy(a => a.Id == id);
            Cancellationentry model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _cancellationEntryRepository.FindBy(a => a.Id == id);
            Cancellationentry model = form.FirstOrDefault();
            model.IsActive = 0;
            _cancellationEntryRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Cancellationentry>> GetPagedCancellationEntry(CancellationEntrySearchDto model)
        {
            return await _cancellationEntryRepository.GetPagedCancellationEntry(model);
        }

        public async Task<Allotmententry> FetchAllottmentDetails(int allottmentId)
        {
            return await _cancellationEntryRepository.FetchAllottmentDetails(allottmentId);
        }
    }
}
