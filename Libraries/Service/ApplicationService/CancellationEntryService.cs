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
    public class CancellationEntryService : EntityService<Requestforproceeding>, ICancellationEntryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRequestforproceedingRepository _requestRepository;

        public CancellationEntryService(IUnitOfWork unitOfWork, IRequestforproceedingRepository requestRepository)
     : base(unitOfWork, requestRepository)
        {
            _unitOfWork = unitOfWork;
            _requestRepository = requestRepository;
        }

        public async Task<List<UserBindDropdownDto>> BindUsernameNameList()
        {
            return await _requestRepository.BindUsernameNameList();
        }

        public async Task<List<Allotmententry>> GetAllAllotment()
        {
            List<Allotmententry> villageList = await _requestRepository.GetAllAllotment();
            return villageList;
        }

        public async Task<List<Honble>> GetAllHonble()
        {
            List<Honble> villageList = await _requestRepository.GetAllHonble();
            return villageList;
        }

        public async Task<List<Requestforproceeding>> GetAllRequestForProceeding()
        {

            return await _requestRepository.GetAllRequestForProceeding();
        }


        public async Task<bool> Create(Requestforproceeding requestforproceeding)
        {

            requestforproceeding.CreatedBy = 1;
            requestforproceeding.PendingAt = 0;
            requestforproceeding.CreatedDate = DateTime.Now;
            _requestRepository.Add(requestforproceeding);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<List<Requestforproceeding>> GetRequestUsingRepo()
        {
            return await _requestRepository.GetAllRequestForProceeding();
        }

        public async Task<bool> Update(int id, Requestforproceeding scheme)
        {
            var result = await _requestRepository.FindBy(a => a.Id == id);
            Requestforproceeding model = result.FirstOrDefault();
            model.AllotmentId = scheme.AllotmentId;
            model.LetterReferenceNo = scheme.LetterReferenceNo;
            model.Subject = scheme.Subject;
            model.GroundOfViolations = scheme.GroundOfViolations;
            model.DateOfCancellationofLease = scheme.DateOfCancellationofLease;
            model.DemandLetter = scheme.DemandLetter;
            model.Noc = scheme.Noc;
            model.CancellationOrder = scheme.CancellationOrder;
            model.UserId = scheme.UserId;

            model.HonebleLgOrCommon = scheme.HonebleLgOrCommon;
            model.ProceedingEvictionPossession = scheme.ProceedingEvictionPossession;
            model.CourtCaseifAny = scheme.CourtCaseifAny;
            //  model.Remarks = scheme.Remarks;

            model.IsActive = scheme.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _requestRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Requestforproceeding> FetchSingleResult(int id)
        {
            var result = await _requestRepository.FindBy(a => a.Id == id);
            Requestforproceeding model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _requestRepository.FindBy(a => a.Id == id);
            Requestforproceeding model = form.FirstOrDefault();
            model.IsActive = 0;
            _requestRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Requestforproceeding>> GetPagedRequestForProceeding(RequestForProceedingSearchDto model)
        {
            return await _requestRepository.GetPagedRequestForProceeding(model);
        }



    }
}
