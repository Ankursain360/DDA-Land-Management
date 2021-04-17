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
   
   public class Newlandus17plotService : EntityService<Newlandus17plot>, INewlandus17plotService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewlandus17plotRepository _newlandus17plotRepository;

        public Newlandus17plotService(IUnitOfWork unitOfWork, INewlandus17plotRepository newlandus17plotRepository)
: base(unitOfWork, newlandus17plotRepository)
        {
            _unitOfWork = unitOfWork;
            _newlandus17plotRepository = newlandus17plotRepository;
        }
        public async Task<bool> Update(int id, Newlandus17plot us17)
        {
            var result = await _newlandus17plotRepository.FindBy(a => a.Id == id);
            Newlandus17plot model = result.FirstOrDefault();
            model.NotificationId = us17.NotificationId;
            model.VillageId = us17.VillageId;
            model.KhasraId = us17.KhasraId;
            model.Bigha = us17.Bigha;
            model.Biswa = us17.Biswa;
            model.Biswanshi = us17.Biswanshi;
            model.Remarks = us17.Remarks;
            model.IsActive = us17.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = us17.ModifiedBy;
            _newlandus17plotRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> Create(Newlandus17plot us17)
        {
            us17.CreatedBy = us17.CreatedBy;
            us17.CreatedDate = DateTime.Now;
            us17.IsActive = 1;

            _newlandus17plotRepository.Add(us17);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<Newlandus17plot> FetchSingleResult(int id)
        {
            var result = await _newlandus17plotRepository.FindBy(a => a.Id == id);
            Newlandus17plot model = result.FirstOrDefault();
            return model;
        }
        public async Task<bool> Delete(int id)
        {
            var form = await _newlandus17plotRepository.FindBy(a => a.Id == id);
            Newlandus17plot model = form.FirstOrDefault();
            model.IsActive = 0;
            _newlandus17plotRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<PagedResult<Newlandus17plot>> GetPagedUS17Plot(Newlandus17plotSearchDto model)
        {
            return await _newlandus17plotRepository.GetPagedUS17Plot(model);
        }
        public async Task<List<Newlandus17plot>> GetAllUS17Plot()
        {
            return await _newlandus17plotRepository.GetAllUS17Plot();
        }
        public async Task<List<LandNotification>> GetAllNotification()
        {
            List<LandNotification> notificationList = await _newlandus17plotRepository.GetAllNotification();
            return notificationList;
        }
        public async Task<List<Newlandvillage>> GetAllVillage()
        {
            List<Newlandvillage> villageList = await _newlandus17plotRepository.GetAllVillage();
            return villageList;
        }
        public async Task<List<Newlandkhasra>> GetAllKhasra(int? villageId)
        {
            List<Newlandkhasra> khasraList = await _newlandus17plotRepository.GetAllKhasra(villageId);
            return khasraList;
        }
        public async Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId)
        {
            return await _newlandus17plotRepository.FetchSingleKhasraResult(khasraId);
        }
        public async Task<PagedResult<Newlandus17plot>> GetAllFetchNotificationDetails(NewLandNotification17ListSearchDto model)
        {
            return await _newlandus17plotRepository.GetAllFetchNotificationDetails(model);
        }

    }
}
