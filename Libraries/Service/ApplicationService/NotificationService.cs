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

namespace Libraries.Service.ApplicationService
{

    public class NotificationService : EntityService<Notification>, INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(IUnitOfWork unitOfWork, INotificationRepository notificationRepository)
        : base(unitOfWork, notificationRepository)
        {
            _unitOfWork = unitOfWork;
            _notificationRepository = notificationRepository;

        }

        public async Task<List<Notification>> GetAllNotification()
        {
            return await _notificationRepository.GetAll();
        }


        public async Task<Notification> FetchSingleResult(int id)
        {
            var result = await _notificationRepository.FindBy(a => a.Id == id);
            Notification model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Notification notification)
        {
            var result = await _notificationRepository.FindBy(a => a.Id == id);
            Notification model = result.FirstOrDefault();
            model.Name = notification.Name;
            model.ModifiedDate = DateTime.Now;
            model.IsActive = notification.IsActive;
            model.ModifiedBy = 1;
            _notificationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Notification notification)
        {

            notification.CreatedBy = 1;
            notification.CreatedDate = DateTime.Now;
            _notificationRepository.Add(notification);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<bool> CheckUniqueName(int id, string notification)
        {
            bool result = await _notificationRepository.Any(id, notification);
            //  var result1 = _dbContext.Notification.Any(t => t.Id != id && t.Name == notification.Name);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _notificationRepository.FindBy(a => a.Id == id);
            Notification model = form.FirstOrDefault();
            model.IsActive = 0;
            _notificationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
