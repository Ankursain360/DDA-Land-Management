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

    public class WatchandwardService : EntityService<Watchandward>, IWatchandwardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWatchandwardRepository _watchandwardRepository;

        public WatchandwardService(IUnitOfWork unitOfWork, IWatchandwardRepository watchandwardRepository)
        : base(unitOfWork, watchandwardRepository)
        {
            _unitOfWork = unitOfWork;
            _watchandwardRepository = watchandwardRepository;
        }
       
        public async Task<List<Village>> GetAllVillage()
        {
            List<Village> villageList = await _watchandwardRepository.GetAllVillage();
            return villageList;
        }
        public async Task<List<Khasra>> GetAllKhasra()
        {
            List<Khasra> khasraList = await _watchandwardRepository.GetAllKhasra();
            return khasraList;
        }

        public async Task<List<Watchandward>> GetAllWatchandward()
        {
            return await _watchandwardRepository.GetAllWatchandward();
        }

        public async Task<List<Watchandward>> GetWatchandwardUsingRepo()
        {
            return await _watchandwardRepository.GetWatchandward();
        }

        public async Task<Watchandward> FetchSingleResult(int id)
        {
            var result = await _watchandwardRepository.FindBy(a => a.Id == id);
            Watchandward model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Watchandward watchandward)
        {
            var result = await _watchandwardRepository.FindBy(a => a.Id == id);
            Watchandward model = result.FirstOrDefault();
           
            model.Date= watchandward.Date;
            model.VillageId= watchandward.VillageId;
            model.KhasraId= watchandward.KhasraId;
            model.Landmark= watchandward.Landmark;
            model.Encroachment= watchandward.Encroachment;
            model.StatusOnGround= watchandward.StatusOnGround;
          
            model.PhotoPath= watchandward.Photo != null ? watchandward.PhotoPath : model.PhotoPath;
            model.ReportFiletPath = watchandward.ReportFile != null ? watchandward.ReportFiletPath : model.ReportFiletPath;

            
            model.Remarks= watchandward.Remarks;
            model.IsActive = watchandward.IsActive;

            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _watchandwardRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Watchandward watchandward)
        {

            watchandward.CreatedBy = 1;
            watchandward.CreatedDate = DateTime.Now;
            _watchandwardRepository.Add(watchandward);
            return await _unitOfWork.CommitAsync() > 0;
        }

        //public async Task<List<Module>> GetAllModule()
        //{
        //    List<Module> moduleList = await _pageRepository.GetAllModule();
        //    return moduleList;
        //}

      

        public async Task<bool> Delete(int id)
        {
            var form = await _watchandwardRepository.FindBy(a => a.Id == id);
            Watchandward model = form.FirstOrDefault();
            model.IsActive = 0;
            _watchandwardRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Watchandward>> GetPagedWatchandward(WatchandwardSearchDto model)
        {
            return await _watchandwardRepository.GetPagedWatchandward(model);
        }
      
    }
}
