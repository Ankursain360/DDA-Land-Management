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
  public  class Undersection4PlotService : EntityService<Undersection4plot>, IUndersection4PlotService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnderSection4PlotRepository _undersection4plotRepository;
        public Undersection4PlotService(IUnitOfWork unitOfWork, IUnderSection4PlotRepository undersection4plotRepository)
   : base(unitOfWork, undersection4plotRepository)
        {
            _unitOfWork = unitOfWork;
            _undersection4plotRepository = undersection4plotRepository;
        }




        public async Task<bool> Delete(int id)
        {
            var form = await _undersection4plotRepository.FindBy(a => a.Id == id);
            Undersection4plot model = form.FirstOrDefault();
            model.IsActive = 0;
            _undersection4plotRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Undersection4plot> FetchSingleResult(int id)
        {
            var result = await _undersection4plotRepository.FindBy(a => a.Id == id);
            Undersection4plot model = result.FirstOrDefault();
            return model;
        }

        public async Task<List<Undersection4>> GetAllNotificationNo()
        {
            List<Undersection4> notificationList = await _undersection4plotRepository.GetAllNotificationNo();
            return notificationList;
        }
        public async Task<List<Khasra>> BindKhasra()
        {
            List<Khasra> khasraList = await _undersection4plotRepository.BindKhasra();
            return khasraList;
        }
        public async Task<List<Acquiredlandvillage>> GetAllVillage()
        {
            List<Acquiredlandvillage>  villageList = await _undersection4plotRepository.GetAllVillage();
            return villageList;
        }


        public async Task<List<Undersection4plot>> GetAllUndersection4Plot()
        {

            return await _undersection4plotRepository.GetAllUndersection4Plot();
        }



        public async Task<List<Undersection4plot>> GetUndersection4PlotUsingRepo()
        {
            return await _undersection4plotRepository.GetAllUndersection4Plot();
        }

        public async Task<bool> Update(int id, Undersection4plot undersection4plot)
        {
            var result = await _undersection4plotRepository.FindBy(a => a.Id == id);
            Undersection4plot model = result.FirstOrDefault();
            model.UnderSection4Id = undersection4plot.UnderSection4Id;
           model.VillageId = undersection4plot.VillageId;
          //  model.KhasraId = undersection4plot.KhasraId;
            model.Bigha = undersection4plot.Bigha;
            model.Biswa = undersection4plot.Biswa;
            model.Biswanshi = undersection4plot.Biswanshi;

            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _undersection4plotRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Undersection4plot undersection4plot)
        {
            undersection4plot.CreatedBy = 1;
            undersection4plot.CreatedDate = DateTime.Now;
            undersection4plot.IsActive = 1;

            _undersection4plotRepository.Add(undersection4plot);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<PagedResult<Undersection4plot>> GetPagedNoUndersection4plot(NotificationUndersection4plotDto model)
        {
            return await _undersection4plotRepository.GetPagedNoUndersection4plot(model);
        }







    }
}
