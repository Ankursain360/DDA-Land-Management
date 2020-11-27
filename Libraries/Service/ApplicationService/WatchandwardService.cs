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

       
        public async Task<List<Locality>> GetAllLocality()
        {
            List<Locality> localityList = await _watchandwardRepository.GetAllLocality();
            return localityList;
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


        //public async Task<Watchandward> FetchSingleResult(int id)
        //{
        //    var result = await _watchandwardRepository.FindBy(a => a.Id == id);
        //    Watchandward model = result.FirstOrDefault();
        //    return model;
        //}

        public async Task<Watchandward> FetchSingleResult(int id)
        {
            return await _watchandwardRepository.FetchSingleResult(id);
        }


        public async Task<bool> Update(int id, Watchandward watchandward)
        {
            var result = await _watchandwardRepository.FindBy(a => a.Id == id);
            Watchandward model = result.FirstOrDefault();

            model.Date = watchandward.Date;
            model.LocalityId = watchandward.LocalityId;
            model.KhasraId = watchandward.KhasraId;
            model.Landmark = watchandward.Landmark;
            model.Encroachment = watchandward.Encroachment;
            model.StatusOnGround = watchandward.StatusOnGround;
            model.PrimaryListNo = watchandward.PrimaryListNo;
            model.PhotoPath = watchandward.Photo != null ? watchandward.PhotoPath : model.PhotoPath;
            model.ReportFiletPath = watchandward.ReportFile != null ? watchandward.ReportFiletPath : model.ReportFiletPath;


            model.Remarks = watchandward.Remarks;
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


        public async Task<PagedResult<Watchandward>> GetWatchandwardReportData(WatchandwardSearchDto watchandwardSearchDto)
        {
            return await _watchandwardRepository.GetWatchandwardReportData(watchandwardSearchDto);
        }

        public async Task<PagedResult<Watchandward>> GetPagedWatchandward(WatchandwardSearchDto model)
        {
            return await _watchandwardRepository.GetPagedWatchandward(model);
        }


        //*****multiple files methods*********Added by ishu


        public async Task<bool> SaveWatchandwardphotofiledetails(Watchandwardphotofiledetails watchandwardphotofiledetails)
        {
            watchandwardphotofiledetails.CreatedBy = 1;
            watchandwardphotofiledetails.CreatedDate = DateTime.Now;
            watchandwardphotofiledetails.IsActive = 1;
            return await _watchandwardRepository.SaveWatchandwardphotofiledetails(watchandwardphotofiledetails);
        }

        public async Task<bool> SaveWatchandwardreportfiledetails(Watchandwardreportfiledetails watchandwardreportfiledetails)
        {
            watchandwardreportfiledetails.CreatedBy = 1;
            watchandwardreportfiledetails.CreatedDate = DateTime.Now;
            watchandwardreportfiledetails.IsActive = 1;
            return await _watchandwardRepository.SaveWatchandwardreportfiledetails(watchandwardreportfiledetails);
        }
        public async Task<Watchandwardphotofiledetails> GetWatchandwardphotofiledetails(int Id)
        {
            return await _watchandwardRepository.GetWatchandwardphotofiledetails(Id);
        }
        public async Task<Watchandwardreportfiledetails> GetWatchandwardreportfiledetails(int Id)
        {
            return await _watchandwardRepository.GetWatchandwardreportfiledetails(Id);
        }
      
       
        public async Task<bool> DeleteWatchandwardphotofiledetails(int Id)
        {
            return await _watchandwardRepository.DeleteWatchandwardphotofiledetails(Id);
        }

        public async Task<bool> DeleteWatchandwardreportfiledetails(int Id)
        {
            return await _watchandwardRepository.DeleteWatchandwardreportfiledetails(Id);
        }

        public async Task<List<Propertyregistration>> GetAllPrimaryList()
        {
            return await _watchandwardRepository.GetAllPrimaryList();
        }

        public async Task<Propertyregistration> FetchSingleResultOnPrimaryList(int propertyId)
        {
            return await _watchandwardRepository.FetchSingleResultOnPrimaryList(propertyId);
        }

        public async Task<bool> UpdateBeforeApproval(int id, Watchandward watchandward)
        {
            var result = await _watchandwardRepository.FindBy(a => a.Id == id);
            Watchandward model = result.FirstOrDefault();

            model.Status = watchandward.Status;
            model.PendingAt = watchandward.PendingAt;
            _watchandwardRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

       
    }
}
