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
  public  class AnnexureAService : EntityService<Fixingdemolition>, IAnnexureAService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAnnexureARepository _annexureARepository;
        public AnnexureAService(IUnitOfWork unitOfWork, IAnnexureARepository annexureARepository)
      : base(unitOfWork, annexureARepository)
        {
            _unitOfWork = unitOfWork;
            _annexureARepository = annexureARepository;
        }
        public async Task<List<Demolitionchecklist>> GetDemolitionchecklist()
        {
            return await _annexureARepository.GetDemolitionchecklist();
        }

        public async Task<List<EncroachmentRegisteration>> GetAllRequestForFixingDemolitionList(int approved)
        {
            return await _annexureARepository.GetAllRequestForFixingDemolitionList(approved);
        }
        public async Task<List<Demolitionprogram>> GetDemolitionprogram()
        {
            return await _annexureARepository.GetDemolitionprogram();
        }
        public async Task<List<Demolitiondocument>> GetDemolitiondocument()
        {
            return await _annexureARepository.GetDemolitiondocument();
        }
        public async Task<List<Fixingdemolition>> GetFixingdemolition(int id)
        {
            return await _annexureARepository.GetFixingdemolition(id);
        }
        public async Task<bool> Create(Fixingdemolition model)
        {
            model.CreatedBy = 1;
            model.CreatedDate = DateTime.Now;
            model.IsActive = 1;
            _annexureARepository.Add(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        //public async Task<bool> SaveFixingdemolition(Fixingdemolition fixingdemolition)
        //{
        //    fixingdemolition.CreatedBy = 1;
        //    fixingdemolition.CreatedDate = DateTime.Now;
        //    fixingdemolition.IsActive = 1;
        //    return await _annexureARepository.SaveFixingdemolition(fixingdemolition);
        //}
        public async Task<bool> Savefixingchecklist(Fixingchecklist fixingchecklist)
        {
            fixingchecklist.CreatedBy = 1;
            fixingchecklist.CreatedDate = DateTime.Now;
            fixingchecklist.IsActive = 1;
            return await _annexureARepository.Savefixingchecklist(fixingchecklist);
        }
        public async Task<List<Fixingchecklist>> Getfixingchecklist(int Id)
        {
            return await _annexureARepository.Getfixingchecklist(Id);
        }

        public async Task<List<Fixingprogram>> Getfixingprogram(int Id)
        {
            return await _annexureARepository.Getfixingprogram(Id);
        }

        public async Task<List<Fixingdocument>> Getfixingdocument(int Id)
        {
            return await _annexureARepository.Getfixingdocument(Id);
        }
        public async Task<bool> SaveFixingprogram(Fixingprogram fixingprogram)
        {
            fixingprogram.CreatedBy = 1;
            fixingprogram.CreatedDate = DateTime.Now;
            fixingprogram.IsActive = 1;
            return await _annexureARepository.SaveFixingprogram(fixingprogram);
        }
        public async Task<bool> SaveFixingdocument(Fixingdocument fixingdocument)
        {
            fixingdocument.CreatedBy = 1;
            fixingdocument.CreatedDate = DateTime.Now;
            fixingdocument.IsActive = 1;
            return await _annexureARepository.SaveFixingdocument(fixingdocument);
        }

        public async Task<PagedResult<EncroachmentRegisteration>> GetPagedDetails(AnnexureASearchDto model, int approved, int zoneId)
        {
            return await _annexureARepository.GetPagedDetails(model,approved, zoneId);
        }

        public async Task<bool> UpdateBeforeApproval(int id, Fixingdemolition fixingdemolition)
        {
            var result = await _annexureARepository.FindBy(a => a.Id == id);
            Fixingdemolition model = result.FirstOrDefault();

            model.ApprovedStatus = fixingdemolition.ApprovedStatus;
            model.PendingAt = fixingdemolition.PendingAt;
            _annexureARepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Fixingdemolition> FetchSingleResult(int id)
        {
            return await _annexureARepository.FetchSingleResult(id);
        }

        public async Task<Fixingdocument> GetAnnexureAfiledetails(int id)
        {
            return await _annexureARepository.GetAnnexureAfiledetails(id);
        }

        public async Task<bool> RollBackEntry(int id)
        {
            var result = await _annexureARepository.FindBy(a => a.Id == id);
            Fixingdemolition model = result.FirstOrDefault();
            _annexureARepository.Delete(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> RollBackEntryFixingdocument(int id)
        {
            return await _annexureARepository.RollBackEntryFixingdocument(id);
        }

        public async Task<bool> RollBackEntryFixingchecklist(int id)
        {
            return await _annexureARepository.RollBackEntryFixingchecklist(id);
        }

        public async Task<bool> RollBackEntryFixingprogram(int id)
        {
            return await _annexureARepository.RollBackEntryFixingprogram(id);
        }
    }
}
