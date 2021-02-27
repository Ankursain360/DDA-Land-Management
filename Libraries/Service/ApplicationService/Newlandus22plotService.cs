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
    public class Newlandus22plotService : EntityService<Newlandus22plot>, INewlandus22plotService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewlandus22plotRepository _newlandus22plotRepository;

        public Newlandus22plotService(IUnitOfWork unitOfWork, INewlandus22plotRepository newlandus22plotRepository)
: base(unitOfWork, newlandus22plotRepository)
        {
            _unitOfWork = unitOfWork;
            _newlandus22plotRepository = newlandus22plotRepository;
        }
        public async Task<bool> Update(int id, Newlandus22plot us22)
        {
            var result = await _newlandus22plotRepository.FindBy(a => a.Id == id);
            Newlandus22plot model = result.FirstOrDefault();
            model.NotificationId = us22.NotificationId;
            model.VillageId = us22.VillageId;
            model.KhasraId = us22.KhasraId;
            model.Bigha = us22.Bigha;
            model.Biswa = us22.Biswa;
            model.Biswanshi = us22.Biswanshi;

            model.Us4Id = us22.Us4Id;
            model.Us6Id = us22.Us6Id;
            model.Us17Id = us22.Us17Id;

            model.Remarks = us22.Remarks;
            model.IsActive = us22.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = us22.ModifiedBy;
            _newlandus22plotRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> Create(Newlandus22plot us22)
        {
            us22.CreatedBy = us22.CreatedBy;
            us22.CreatedDate = DateTime.Now;
            us22.IsActive = 1;

            _newlandus22plotRepository.Add(us22);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<Newlandus22plot> FetchSingleResult(int id)
        {
            var result = await _newlandus22plotRepository.FindBy(a => a.Id == id);
            Newlandus22plot model = result.FirstOrDefault();
            return model;
        }
        public async Task<bool> Delete(int id)
        {
            var form = await _newlandus22plotRepository.FindBy(a => a.Id == id);
            Newlandus22plot model = form.FirstOrDefault();
            model.IsActive = 0;
            _newlandus22plotRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<PagedResult<Newlandus22plot>> GetPagedUS22Plot(Newlandus22plotSearchDto model)
        {
            return await _newlandus22plotRepository.GetPagedUS22Plot(model);
        }
        public async Task<List<Newlandus22plot>> GetAllUS22Plot()
        {
            return await _newlandus22plotRepository.GetAllUS22Plot();
        }
        public async Task<List<Newlandus4plot>> GetAllUS4Plot(int? notificationId)
        {
            List<Newlandus4plot> notificationList = await _newlandus22plotRepository.GetAllUS4Plot(notificationId);
            return notificationList;
        }
        public async Task<List<Newlandus6plot>> GetAllUS6Plot(int? notificationId)
        {
            List<Newlandus6plot> notificationList = await _newlandus22plotRepository.GetAllUS6Plot(notificationId);
            return notificationList;
        }
        public async Task<List<Newlandus17plot>> GetAllUS17Plot(int? notificationId)
        {
            List<Newlandus17plot> notificationList = await _newlandus22plotRepository.GetAllUS17Plot(notificationId);
            return notificationList;
        }
        public async Task<Newlandus4plot> FetchUS4Plot(int? notificationId)
        {
            return await _newlandus22plotRepository.FetchUS4Plot(notificationId);
        }
        public async Task<Newlandus6plot> FetchUS6Plot(int? notificationId)
        {
            return await _newlandus22plotRepository.FetchUS6Plot(notificationId);
        }
        public async Task<Newlandus17plot> FetchUS17Plot(int? notificationId)
        {
            return await _newlandus22plotRepository.FetchUS17Plot(notificationId);
        }
        public async Task<List<LandNotification>> GetAllNotification()
        {
            List<LandNotification> notificationList = await _newlandus22plotRepository.GetAllNotification();
            return notificationList;
        }
        public async Task<List<Newlandvillage>> GetAllVillage()
        {
            List<Newlandvillage> villageList = await _newlandus22plotRepository.GetAllVillage();
            return villageList;
        }
        public async Task<List<Newlandkhasra>> GetAllKhasra(int? villageId)
        {
            List<Newlandkhasra> khasraList = await _newlandus22plotRepository.GetAllKhasra(villageId);
            return khasraList;
        }
        public async Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId)
        {
            return await _newlandus22plotRepository.FetchSingleKhasraResult(khasraId);
        }

    }
}
