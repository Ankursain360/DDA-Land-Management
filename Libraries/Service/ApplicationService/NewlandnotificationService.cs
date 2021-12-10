using Dto.Search;
using Libraries.Model.Common;
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

    public class NewlandnotificationService : EntityService<Newlandnotification>,INewlandnotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewlandnotificationRepository _newlandnotificationRepository;
        public NewlandnotificationService(IUnitOfWork unitOfWork, INewlandnotificationRepository newlandnotificationRepository)
      : base(unitOfWork, newlandnotificationRepository)
        {
            _unitOfWork = unitOfWork;
            _newlandnotificationRepository = newlandnotificationRepository;
        }
        public async Task<bool> Delete(int id)
        {
            var form = await _newlandnotificationRepository.FindBy(a => a.Id == id);
            Newlandnotification model = form.FirstOrDefault();
            model.IsActive = 0;
            _newlandnotificationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<Newlandnotification> FetchSingleResult1(int id)
        {
            var result = await _newlandnotificationRepository.FindBy(a => a.Id == id);
            Newlandnotification model = result.FirstOrDefault();
            return model;
        }


        public async Task<Newlandnotification> FetchSingleResult(int id)
        {
            return await _newlandnotificationRepository.FetchSingleResult(id);
        }

        public async Task<List<Newlandnotification>> GetAllNewlandNotification()
        {
            return await _newlandnotificationRepository.GetAllNewlandNotification();
        }
        public async Task<List<NewlandNotificationtype>> GetAllNotificationType()
        {
            return await _newlandnotificationRepository.GetAllNotificationType();
        }
        public async Task<List<Newlandnotificationfilepath>> GetAllfiledetails(int id)
        {
            return await _newlandnotificationRepository.GetAllfiledetails(id);
        }
   
        public async Task<PagedResult<Newlandnotification>> GetPagedNewlandnotificationdetails(NewlandnotificationSearchDto model)
        {
            return await _newlandnotificationRepository.GetPagedNewlandnotificationdetails(model);
        }
        //public async Task<Newlandnotification> FetchSingleResult(int id)
        //{
        //    var result = await _newlandnotificationRepository.FindBy(a => a.Id == id);
        //    Newlandnotification model = result.FirstOrDefault();
        //    return model;
        //}
        public async Task<bool> Update(int id, Newlandnotification newlandnotification)
        {
            var result = await _newlandnotificationRepository.FindBy(a => a.Id == id);
            Newlandnotification model = result.FirstOrDefault();
            model.NotificationTypeId = newlandnotification.NotificationTypeId;
            model.NotificationNo = newlandnotification.NotificationNo;
            model.Date = newlandnotification.Date;
            model.GazetteNotificationFilePath = newlandnotification.GazetteNotificationFilePath;
            model.IsActive = newlandnotification.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.Remarks = newlandnotification.Remarks;
            model.ModifiedBy = 1;
            _newlandnotificationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public new async Task<bool> Create(Newlandnotification newlandnotification)
        {

            newlandnotification.CreatedBy = 1;
            newlandnotification.IsActive = 1;
            newlandnotification.CreatedDate = DateTime.Now;
            _newlandnotificationRepository.Add(newlandnotification);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> SaveNewlandNotification(Newlandnotification newlandnotification)
        {
            newlandnotification.CreatedBy = 1;
            newlandnotification.CreatedDate = DateTime.Now;
            newlandnotification.IsActive = 1;
            return await _newlandnotificationRepository.SaveNewlandNotification(newlandnotification);
        }
        public async Task<bool> DeleteNewlandnotification(int Id)
        {
            return await _newlandnotificationRepository.DeleteNewlandnotification(Id);
        }
        public async Task<bool> Savefiledetails(Newlandnotificationfilepath newlandnotificationfilepath)
        {
            newlandnotificationfilepath.CreatedBy = 1;
            newlandnotificationfilepath.CreatedDate = DateTime.Now;
            newlandnotificationfilepath.IsActive = 1;
            return await _newlandnotificationRepository.Savefiledetails(newlandnotificationfilepath);
        }
        public async Task<bool> Deletefiledetails(int Id)
        {
            return await _newlandnotificationRepository.Deletefiledetails(Id);
        }
        public async Task<List<NewlandNotificationtype>> GetNotificationType()
        {
            List<NewlandNotificationtype> notificationtypelist = await _newlandnotificationRepository.GetNotificationType();
            return notificationtypelist;
        }

        public Task<List<Newlandnotification>> GetNewlandnotification()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Any(int id, string name)
        {
            throw new NotImplementedException();
        }
    }


}

