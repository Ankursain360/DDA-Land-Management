using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Microsoft.EntityFrameworkCore;
using Libraries.Repository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{
    public class Undersection17Service : EntityService<Undersection17>, IUndersection17Service
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUndersection17Repository _undersection17Repository;
        public Undersection17Service(IUnitOfWork unitOfWork, IUndersection17Repository undersection17Repository)
      : base(unitOfWork, undersection17Repository)
        {
            _unitOfWork = unitOfWork;
            _undersection17Repository = undersection17Repository;
        }





        public async Task<bool> Delete(int id)
        {
            var form = await _undersection17Repository.FindBy(a => a.Id == id);
            Undersection17 model = form.FirstOrDefault();
            model.IsActive = 0;
            _undersection17Repository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Undersection17> FetchSingleResult(int id)
        {
            var result = await _undersection17Repository.FindBy(a => a.Id == id);
            Undersection17 model = result.FirstOrDefault();
            return model;
        }



        public async Task<List<Undersection17>> GetAllUndersection17()
        {

            return await _undersection17Repository.GetAllUndersection17();
        }

        public async Task<List<LandNotification>> GetAllLandNotification()
        {
            List<LandNotification> landnotificationList = await _undersection17Repository.GetAllLandNotification();
            return landnotificationList;
        }

        public async Task<List<Undersection6>> GetAllUndersection6()
        {
            List<Undersection6> undersection6List = await _undersection17Repository.GetAllUndersection6();
            return undersection6List;
        }

        public async Task<List<Undersection17>> GetUndersection17UsingRepo()
        {
            return await _undersection17Repository.GetAllUndersection17();
        }

        public async Task<bool> Update(int id, Undersection17 undersection17)
        {
            var result = await _undersection17Repository.FindBy(a => a.Id == id);
            Undersection17 model = result.FirstOrDefault();
            model.LandNotificationId = undersection17.LandNotificationId;
            model.NotificationDate = undersection17.NotificationDate;
            model.UnderSection6Id = undersection17.UnderSection6Id;
            

            

            model.IsActive = undersection17.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _undersection17Repository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Undersection17 undersection17)
        {
            undersection17.CreatedBy = 1;
            undersection17.CreatedDate = DateTime.Now;

            _undersection17Repository.Add(undersection17);
            return await _unitOfWork.CommitAsync() > 0;
        }








    }
}
