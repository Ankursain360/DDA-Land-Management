
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

    public class NewlandnotificationdetailsService : EntityService<Newlandnotificationdetails>, INewlandnotificationdetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewlandnotificationdetailsRepository _newlandnotificationdetailsRepository;

        public NewlandnotificationdetailsService(IUnitOfWork unitOfWork, 
            INewlandnotificationdetailsRepository newlandnotificationdetailsRepository)
        : base(unitOfWork, newlandnotificationdetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _newlandnotificationdetailsRepository = newlandnotificationdetailsRepository;
        }

        public async Task<List<Newlandnotificationdetails>> GetAllNotifications()
        {
            return await _newlandnotificationdetailsRepository.GetAllNotifications();
        }

        public async Task<Newlandnotificationdetails> FetchSingleResult(int id)
        {
            var result = await _newlandnotificationdetailsRepository.FindBy(a => a.Id == id);
            Newlandnotificationdetails model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Newlandnotificationdetails notification)
        {
            var result = await _newlandnotificationdetailsRepository.FindBy(a => a.Id == id);
            Newlandnotificationdetails model = result.FirstOrDefault();

            model.NotificationTypeId = notification.NotificationTypeId;
            model.NotificationNo = notification.NotificationNo;
            model.VillageId = notification.VillageId;
            model.KhasraId = notification.KhasraId;
            model.Bigha = notification.Bigha;
            model.Biswa = notification.Biswa;
            model.Biswanshi = notification.Biswanshi;
            model.Remarks = notification.Remarks;
            model.IsActive = notification.IsActive;
           
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = notification.ModifiedBy;
            _newlandnotificationdetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Newlandnotificationdetails notification)
        {
            notification.CreatedBy = notification.CreatedBy;
            notification.CreatedDate = DateTime.Now;
            _newlandnotificationdetailsRepository.Add(notification);
            return await _unitOfWork.CommitAsync() > 0;
        }

       

        public async Task<bool> Delete(int id)
        {
            var form = await _newlandnotificationdetailsRepository.FindBy(a => a.Id == id);
            Newlandnotificationdetails model = form.FirstOrDefault();
            model.IsActive = 0;
            _newlandnotificationdetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Newlandnotificationdetails>> GetPagedNotifications(NewlandnotificationdetailsSearchDto model)

        {
            return await _newlandnotificationdetailsRepository.GetPagedNotifications(model);
        }

        public async Task<List<NewlandNotificationtype>> GetAllNotificationType()
        {
            List<NewlandNotificationtype> notificationList = await _newlandnotificationdetailsRepository.GetAllNotificationType();
            return notificationList;
        }
        public async Task<List<Newlandvillage>> GetAllVillage()
        {
            List<Newlandvillage> villageList = await _newlandnotificationdetailsRepository.GetAllVillage();
            return villageList;
        }
        public async Task<List<Newlandkhasra>> GetAllKhasra(int? villageId)
        {
            List<Newlandkhasra> khasraList = await _newlandnotificationdetailsRepository.GetAllKhasra(villageId);
            return khasraList;
        }
        public async Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId)
        {
            return await _newlandnotificationdetailsRepository.FetchSingleKhasraResult(khasraId);
        }
    }
}
