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
    public class NewLandNotificationTypeService : EntityService<NewlandNotificationtype>, INewLandNotificationTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewLandNotificationTypeRepository _newLandNotificationTypeRepository;
        public NewLandNotificationTypeService(IUnitOfWork unitOfWork, INewLandNotificationTypeRepository newLandNotificationTypeRepository)
       : base(unitOfWork, newLandNotificationTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _newLandNotificationTypeRepository = newLandNotificationTypeRepository;

        }

        public async Task<bool> Create(NewlandNotificationtype notification)
        {
            notification.CreatedBy = 1;
            notification.CreatedDate = DateTime.Now;
            _newLandNotificationTypeRepository.Add(notification);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<NewlandNotificationtype>> GetPagedNotification(NewLandNotificationTypeSearchDto model)
        {
            return await _newLandNotificationTypeRepository.GetPagedZone(model);
        }


        public async Task<List<NewlandNotificationtype>> GetAllNotificationType()
        {
            return await _newLandNotificationTypeRepository.GetAll();
        }


        public async Task<NewlandNotificationtype> FetchSingleResult(int id)
        {
            var result = await _newLandNotificationTypeRepository.FindBy(a => a.Id == id);
            NewlandNotificationtype model = result.FirstOrDefault();
            return model;
        }


        public async Task<bool> Update(int id, NewlandNotificationtype notification)
        {
            var result = await _newLandNotificationTypeRepository.FindBy(a => a.Id == id);
            NewlandNotificationtype model = result.FirstOrDefault();
            model.NotificationType = notification.NotificationType;
            model.ModifiedDate = DateTime.Now;
            model.IsActive = notification.IsActive;
            model.ModifiedBy = 1;
            _newLandNotificationTypeRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _newLandNotificationTypeRepository.FindBy(a => a.Id == id);
            NewlandNotificationtype model = form.FirstOrDefault();
            model.IsActive = 0;
            _newLandNotificationTypeRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
