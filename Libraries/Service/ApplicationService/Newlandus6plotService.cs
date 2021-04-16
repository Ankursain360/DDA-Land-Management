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
    public class Newlandus6plotService : EntityService<Newlandus6plot>, INewlandus6plotService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewlandus6plotRepository _newlandus6plotRepository;

        public Newlandus6plotService(IUnitOfWork unitOfWork, INewlandus6plotRepository newlandus6plotRepository)
: base(unitOfWork, newlandus6plotRepository)
        {
            _unitOfWork = unitOfWork;
            _newlandus6plotRepository = newlandus6plotRepository;
        }
        public async Task<bool> Update(int id, Newlandus6plot us6)
        {
            var result = await _newlandus6plotRepository.FindBy(a => a.Id == id);
            Newlandus6plot model = result.FirstOrDefault();
            model.NotificationId = us6.NotificationId;
            model.VillageId = us6.VillageId;
            model.KhasraId = us6.KhasraId;
            model.Bigha = us6.Bigha;
            model.Biswa = us6.Biswa;
            model.Biswanshi = us6.Biswanshi;
            model.Remarks = us6.Remarks;
            model.IsActive = us6.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = us6.ModifiedBy;
            _newlandus6plotRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> Create(Newlandus6plot us6)
        {
            us6.CreatedBy = us6.CreatedBy;
            us6.CreatedDate = DateTime.Now;
            us6.IsActive = 1;

            _newlandus6plotRepository.Add(us6);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<Newlandus6plot> FetchSingleResult(int id)
        {
            var result = await _newlandus6plotRepository.FindBy(a => a.Id == id);
            Newlandus6plot model = result.FirstOrDefault();
            return model;
        }
        public async Task<bool> Delete(int id)
        {
            var form = await _newlandus6plotRepository.FindBy(a => a.Id == id);
            Newlandus6plot model = form.FirstOrDefault();
            model.IsActive = 0;
            _newlandus6plotRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<PagedResult<Newlandus6plot>> GetPagedUS6Plot(Newlandus6plotSearchDto model)
        {
            return await _newlandus6plotRepository.GetPagedUS6Plot(model);
        }
        public async Task<List<Newlandus6plot>> GetAllUS6Plot()
        {
            return await _newlandus6plotRepository.GetAllUS6Plot();
        }
        public async Task<List<LandNotification>> GetAllNotification()
        {
            List<LandNotification> notificationList = await _newlandus6plotRepository.GetAllNotification();
            return notificationList;
        }
        public async Task<List<Newlandvillage>> GetAllVillage()
        {
            List<Newlandvillage> villageList = await _newlandus6plotRepository.GetAllVillage();
            return villageList;
        }
        public async Task<List<Newlandkhasra>> GetAllKhasra(int? villageId)
        {
            List<Newlandkhasra> khasraList = await _newlandus6plotRepository.GetAllKhasra(villageId);
            return khasraList;
        }
        public async Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId)
        {
            return await _newlandus6plotRepository.FetchSingleKhasraResult(khasraId);
        }
        public async Task<List<Newlandus6plot>> GetAllFetchNotification6Details(int? NotificationId)
        {
            return await _newlandus6plotRepository.GetAllFetchNotification6Details(NotificationId);
        }

    }
}
