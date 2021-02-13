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

using Dto.Search;



namespace Libraries.Service.ApplicationService
{
  public  class Undersection6plotService : EntityService<Undersection6plot>, IUndersection6plotService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUndersection6plotRepository _undersection6plotRepository;

        public Undersection6plotService(IUnitOfWork unitOfWork, IUndersection6plotRepository undersection6plotRepository)
: base(unitOfWork, undersection6plotRepository)
        {
            _unitOfWork = unitOfWork;
            _undersection6plotRepository = undersection6plotRepository;
        }





        public async Task<bool> Delete(int id)
        {
            var form = await _undersection6plotRepository.FindBy(a => a.Id == id);
            Undersection6plot model = form.FirstOrDefault();
            model.IsActive = 0;
            _undersection6plotRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Undersection6plot> FetchSingleResult(int id)
        {
            var result = await _undersection6plotRepository.FindBy(a => a.Id == id);
            Undersection6plot model = result.FirstOrDefault();
            return model;
        }

        public async Task<List<Undersection6>> GetAllNotificationNo()
        {
            List<Undersection6> notificationList = await _undersection6plotRepository.GetAllNotificationNo();
            return notificationList;
        }
        public async Task<List<Khasra>> BindKhasra(int? villageId)
        {
            List<Khasra> khasraList = await _undersection6plotRepository.BindKhasra(villageId);
            return khasraList;
        }


        public async Task<List<Acquiredlandvillage>> GetAllVillage()
        {
            List<Acquiredlandvillage> villageList = await _undersection6plotRepository.GetAllVillage();
            return villageList;
        }


        public async Task<List<Undersection6plot>> GetAllUndersection6Plot()
        {

            return await _undersection6plotRepository.GetAllUndersection6Plot();
        }



        public async Task<List<Undersection6plot>> GetUndersection6PlotUsingRepo()
        {
            return await _undersection6plotRepository.GetAllUndersection6Plot();
        }

        public async Task<bool> Update(int id, Undersection6plot undersection4plot)
        {
            var result = await _undersection6plotRepository.FindBy(a => a.Id == id);
            Undersection6plot model = result.FirstOrDefault();
            model.UnderSection6Id = undersection4plot.UnderSection6Id;
            model.VillageId = undersection4plot.VillageId;
            model.KhasraId = undersection4plot.KhasraId;
          
            model.IsActive = undersection4plot.IsActive;
            model.Bigha = undersection4plot.Bigha;
            model.Biswa = undersection4plot.Biswa;
            model.Biswanshi = undersection4plot.Biswanshi;

            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _undersection6plotRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Undersection6plot undersection6plot)
        {
            undersection6plot.CreatedBy = 1;
            undersection6plot.CreatedDate = DateTime.Now;


            _undersection6plotRepository.Add(undersection6plot);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<PagedResult<Undersection6plot>> GetPagedNoUndersection6plot(NotificationUndersection6plotDto model)
        {
            return await _undersection6plotRepository.GetPagedNoUndersection6plot(model);
        }








    }
}
