
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{

    public class WatchWardAPIService : EntityService<Watchandward>, IWatchWardAPIService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWatchWardAPIRepository _watchWardAPIRepository;

        public WatchWardAPIService(IUnitOfWork unitOfWork, IWatchWardAPIRepository watchWardAPIRepository) :
            base(unitOfWork, watchWardAPIRepository)
        {
            _unitOfWork = unitOfWork;
            _watchWardAPIRepository = watchWardAPIRepository;
        }

        public async Task<bool> Create(ApiSaveWatchandwardDto dto)
        {
            Watchandward model = new Watchandward();
            model.RefNo = dto.RefNo;
            model.Date = dto.Date;
            model.PrimaryListNo = dto.PrimaryListNo;
            model.Landmark = dto.Landmark;
            model.Encroachment = dto.Encroachment;
            model.StatusOnGround = dto.StatusOnGround;
            model.IsActive = dto.IsActive;
            model.PhotoPath = dto.PhotoPath;
            model.Latitude = dto.Latitude;
            model.Longitude = dto.Longitude;
            model.Remarks = dto.Remarks;
            model.ApprovalZoneId = dto.ApprovalZoneId;
            model.IsActive = 1;
            model.CreatedBy = 1;
            model.CreatedDate = DateTime.Now;
            _watchWardAPIRepository.Add(model);
            var result = await _unitOfWork.CommitAsync() > 0;
            dto.Id = model.Id;
            return result;
        }
       

        public async Task<bool> UpdateBeforeApproval(ApiSaveWatchandwardDto dto)
        {
            var result = await _watchWardAPIRepository.FindBy(a => a.Id == dto.Id);
            
            Watchandward model = result.FirstOrDefault();
            model.ApprovedStatus = dto.ApprovedStatus;
            model.PendingAt = dto.PendingAt;
            model.ModifiedBy = dto.CreatedBy;
            model.ModifiedDate = DateTime.Now;
            _watchWardAPIRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<List<APIGetPrimaryListNoListDto>> GetPrimaryListNoList()
        {
            return await _watchWardAPIRepository.GetPrimaryListNoList();
        }

        //*****multiple files methods*********Added by ishu

        public async Task<bool> SaveWatchandwardphotofiledetails(Watchandwardphotofiledetails item)
        {
          
            item.CreatedBy = 1;
            item.CreatedDate = DateTime.Now;
            item.IsActive = 1;
            return await _watchWardAPIRepository.SaveWatchandwardphotofiledetails(item);
        }
       

        public async Task<bool> SaveWatchandwardreportfiledetails(Watchandwardreportfiledetails item)
        {
            item.CreatedBy = 1;
            item.CreatedDate = DateTime.Now;
            item.IsActive = 1;
            return await _watchWardAPIRepository.SaveWatchandwardreportfiledetails(item);
        }

        public async Task<List<ApiSaveWatchandwardDto>> GetAllWatchandward(ApiWatchWardParmsDto dto)
        {
            return await _watchWardAPIRepository.GetAllWatchandward(dto);
        }
        public async Task<Userprofile> GetUserOngivenUserId(int userId)
        {
            return await _watchWardAPIRepository.GetUserOngivenUserId(userId);
        }
    }
}
