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
    public class OtherlandnotificationService : EntityService<Otherlandnotification>, IOtherlandnotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOtherlandnotificationRepository _otherlandnotificationRepository;
        public OtherlandnotificationService(IUnitOfWork unitOfWork, IOtherlandnotificationRepository otherlandnotificationRepository)
: base(unitOfWork, otherlandnotificationRepository)
        {
            _unitOfWork = unitOfWork;
            _otherlandnotificationRepository = otherlandnotificationRepository;
        }



        public async Task<List<Otherlandnotification>> GetOtherlandnotificationUsingRepo()
        {
            return await _otherlandnotificationRepository.GetOtherlandnotification();
        }

        public async Task<Otherlandnotification> FetchSingleResult(int id)
        {
            var result = await _otherlandnotificationRepository.FindBy(a => a.Id == id);
            Otherlandnotification model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Otherlandnotification otherlandnotification)
        {
            var result = await _otherlandnotificationRepository.FindBy(a => a.Id == id);
            Otherlandnotification model = result.FirstOrDefault();
            model.LandType = otherlandnotification.LandType;
            model.NotificationNumber = otherlandnotification.NotificationNumber;
            model.IsActive = otherlandnotification.IsActive;

            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _otherlandnotificationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Otherlandnotification otherlandnotification)
        {

            otherlandnotification.CreatedBy = 1;
            otherlandnotification.CreatedDate = DateTime.Now;
            _otherlandnotificationRepository.Add(otherlandnotification);
            return await _unitOfWork.CommitAsync() > 0;
        }




        public async Task<bool> Delete(int id)
        {
            var form = await _otherlandnotificationRepository.FindBy(a => a.Id == id);
            Otherlandnotification model = form.FirstOrDefault();
            model.IsActive = 0;
            _otherlandnotificationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<List<Otherlandnotification>> GetOtherlandnotification()
        {
            return await _otherlandnotificationRepository.GetOtherlandnotification();
        }



        public async Task<PagedResult<Otherlandnotification>> GetPagedOtherlandnotification(OtherlandnotificationSearchDto model)
        {
            return await _otherlandnotificationRepository.GetPagedOtherlandnotification(model);
        }


    }
}
