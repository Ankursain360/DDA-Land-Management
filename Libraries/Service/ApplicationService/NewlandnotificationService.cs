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
using AutoMapper;
using Dto.Master;
using Service.IApplicationService;

namespace Libraries.Service.ApplicationService
{

    public class NewlandnotificationService : EntityService<Newlandnotification>,INewlandnotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewlandnotificationRepository _newlandnotificationRepository;
        private readonly IMapper _mapper;
        public NewlandnotificationService(IUnitOfWork unitOfWork, INewlandnotificationRepository newlandnotificationRepository, IMapper mapper) : base(unitOfWork, newlandnotificationRepository)
        {
            _unitOfWork = unitOfWork;
            _newlandnotificationRepository = newlandnotificationRepository;
            _mapper = mapper;
        }
        public async Task<List<Newlandnotification>> GetNewlandnotification()
        {
            return await _newlandnotificationRepository.GetNewlandnotificationdetails();

        }
        public async Task<PagedResult<Newlandnotification>> GetPagedNewlandnotificationdetails(NewlandnotificationSearchDto model)
        {
            return await _newlandnotificationRepository.GetPagedNewlandnotificationdetails(model);
        }
        public async Task<Newlandnotification> FetchSingleResult(int id)
        {
            var result = await _newlandnotificationRepository.FindBy(a => a.Id == id);
            Newlandnotification model = result.FirstOrDefault();
            return model;
        }
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
            model.ModifiedBy = 1;
            _newlandnotificationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public new async Task<bool> Create(Newlandnotification newlandnotification)
        {

            newlandnotification.CreatedBy = 1;
            newlandnotification.CreatedDate = DateTime.Now;
            _newlandnotificationRepository.Add(newlandnotification);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> Delete(int id)
        {
            var form = await _newlandnotificationRepository.FindBy(a => a.Id == id);
            Newlandnotification model = form.FirstOrDefault();
            model.IsActive = 0;
            _newlandnotificationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<List<NewlandNotificationtype>> GetNotificationType()
        {
            List<NewlandNotificationtype> notificationtypelist = await _newlandnotificationRepository.GetNotificationType();
            return notificationtypelist;
        }
    }


}

