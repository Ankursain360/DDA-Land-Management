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
    public class Newlandus4plotService : EntityService<Newlandus4plot>, INewlandus4plotService
    {
            private readonly IUnitOfWork _unitOfWork;
            private readonly INewlandus4plotRepository _newlandus4plotRepository;

            public Newlandus4plotService(IUnitOfWork unitOfWork, INewlandus4plotRepository newlandus4plotRepository)
    : base(unitOfWork, newlandus4plotRepository)
            {
                _unitOfWork = unitOfWork;
            _newlandus4plotRepository = newlandus4plotRepository;
            }
        public async Task<bool> Update(int id, Newlandus4plot us4)
        {
            var result = await _newlandus4plotRepository.FindBy(a => a.Id == id);
            Newlandus4plot model = result.FirstOrDefault();
            model.NotificationId = us4.NotificationId;
            model.VillageId = us4.VillageId;
            model.KhasraId = us4.KhasraId;
            model.ABigha = us4.ABigha;
            model.ABiswa = us4.ABiswa;
            model.ABiswanshi = us4.ABiswanshi;
            model.Bigha = us4.Bigha;
            model.Biswa = us4.Biswa;
            model.Biswanshi = us4.Biswanshi;
            model.Remarks = us4.Remarks;
            model.IsActive = us4.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = us4.ModifiedBy;
            _newlandus4plotRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> Create(Newlandus4plot us4)
        {
            us4.CreatedBy = us4.CreatedBy;
            us4.CreatedDate = DateTime.Now;
            us4.IsActive = 1;

            _newlandus4plotRepository.Add(us4);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<Newlandus4plot> FetchSingleResult(int id)
        {
            var result = await _newlandus4plotRepository.FindBy(a => a.Id == id);
            Newlandus4plot model = result.FirstOrDefault();
            return model;
        }
        public async Task<bool> Delete(int id)
        {
            var form = await _newlandus4plotRepository.FindBy(a => a.Id == id);
            Newlandus4plot model = form.FirstOrDefault();
            model.IsActive = 0;
            _newlandus4plotRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<PagedResult<Newlandus4plot>> GetPagedUS4Plot(Newlandus4plotSearchDto model)
        {
            return await _newlandus4plotRepository.GetPagedUS4Plot(model);
        }
        public async Task<List<Newlandus4plot>> GetAllUS4Plot()
        {
            return await _newlandus4plotRepository.GetAllUS4Plot();
        }
        public async Task<List<LandNotification>> GetAllNotification()
        {
            List<LandNotification> notificationList = await _newlandus4plotRepository.GetAllNotification();
            return notificationList;
        }
        public async Task<List<Newlandvillage>> GetAllVillage()
        {
            List<Newlandvillage> villageList = await _newlandus4plotRepository.GetAllVillage();
            return villageList;
        }
        public async Task<List<Newlandkhasra>> GetAllKhasra(int? villageId)
        {
            List<Newlandkhasra> khasraList = await _newlandus4plotRepository.GetAllKhasra(villageId);
            return khasraList;
        }
        public async Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId)
        {
            return await _newlandus4plotRepository.FetchSingleKhasraResult(khasraId);
        }
        public async Task<Newlandkhasra> FetchSingleKhasra1Result(int? khasraId)
        {
            return await _newlandus4plotRepository.FetchSingleKhasraResult(khasraId);

        }
        //public async Task<PagedResult<Newlandus4plot>> GetAllFetchNotificationDetails(NewLandNotification4ListSearchDto model)
        //{
        //    return await _newlandus4plotRepository.GetAllFetchNotificationDetails(model);
        //}
        public async Task<PagedResult<Newlandus4plot>> GetAllFetchNotificationDetails(NewLandNotification4ListSearchDto model)
        {
            return await _newlandus4plotRepository.GetAllFetchNotificationDetails(model);
        }

    }
}
