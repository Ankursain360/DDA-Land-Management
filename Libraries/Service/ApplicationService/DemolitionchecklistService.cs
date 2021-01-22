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


namespace Libraries.Service.ApplicationService
{
   public class DemolitionchecklistService : EntityService<Demolitionchecklist>, IDemolitionchecklistService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDemolitionchecklistRepository _demolitionchecklistRepository;

        public DemolitionchecklistService(IUnitOfWork unitOfWork, IDemolitionchecklistRepository demolitionchecklistRepository)
  : base(unitOfWork, demolitionchecklistRepository)
        {
            _unitOfWork = unitOfWork;
            _demolitionchecklistRepository = demolitionchecklistRepository;
        }

        public async Task<List<Demolitionchecklist>> GetDemolitionchecklistUsingRepo()
        {
            return await _demolitionchecklistRepository.GetDemolitionchecklist();
        }

        public async Task<Demolitionchecklist> FetchSingleResult(int id)
        {
            var result = await _demolitionchecklistRepository.FindBy(a => a.Id == id);
            Demolitionchecklist model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Demolitionchecklist Demolitionchecklist)
        {
            var result = await _demolitionchecklistRepository.FindBy(a => a.Id == id);
            Demolitionchecklist model = result.FirstOrDefault();
            model.ChecklistDescription = Demolitionchecklist.ChecklistDescription;
            model.IsActive = Demolitionchecklist.IsActive;

            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _demolitionchecklistRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Demolitionchecklist demolitionchecklist)
        {

            demolitionchecklist.CreatedBy = 1;
            demolitionchecklist.CreatedDate = DateTime.Now;
            _demolitionchecklistRepository.Add(demolitionchecklist);
            return await _unitOfWork.CommitAsync() > 0;
        }


      

        public async Task<bool> Delete(int id)
        {
            var form = await _demolitionchecklistRepository.FindBy(a => a.Id == id);
            Demolitionchecklist model = form.FirstOrDefault();
            model.IsActive = 0;
            _demolitionchecklistRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<List<Demolitionchecklist>> GetDemolitionchecklist()
        {
            return await _demolitionchecklistRepository.GetDemolitionchecklist();
        }



        public async Task<PagedResult<Demolitionchecklist>> GetPagedDemolitionchecklist(DemolitionchecklistSearchDto model)
        {
            return await _demolitionchecklistRepository.GetPagedDemolitionchecklist(model);
        }

    }
}
