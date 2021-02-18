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

  public class BooktransferlandService : EntityService<Booktransferland>, IBooktransferlandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBooktransferlandRepository _booktransferlandRepository;

        public BooktransferlandService(IUnitOfWork unitOfWork, IBooktransferlandRepository booktransferlandRepository)
        : base(unitOfWork, booktransferlandRepository)
        {
            _unitOfWork = unitOfWork;
            _booktransferlandRepository = booktransferlandRepository;
        }

        public async Task<List<Booktransferland>> GetAllBooktransferland()
        {
            return await _booktransferlandRepository.GetAllBooktransferland();
        }

        public async Task<List<Booktransferland>> GetBooktransferlandUsingRepo()
        {
            return await _booktransferlandRepository.GetBooktransferland();
        }

        public async Task<Booktransferland> FetchSingleResult(int id)
        {
            var result = await _booktransferlandRepository.FindBy(a => a.Id == id);
            Booktransferland model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Booktransferland booktransferland)
        {
            var result = await _booktransferlandRepository.FindBy(a => a.Id == id);
            Booktransferland model = result.FirstOrDefault();

            model.LandNotificationId= booktransferland.LandNotificationId;

            model.NotificationDate = booktransferland.NotificationDate;
            model.LocalityId = booktransferland.LocalityId;
            model.KhasraId = booktransferland.KhasraId;


            model.Part = booktransferland.Part;
            model.Area  = booktransferland.Area;
           


            model.StatusOfLand = booktransferland.StatusOfLand;
            model.DateofPossession = booktransferland.DateofPossession;
            model.Remarks = booktransferland.Remarks;


            model.IsActive = booktransferland.IsActive;

            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _booktransferlandRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Booktransferland booktransferland)
        {

            booktransferland.CreatedBy = 1;
            booktransferland.CreatedDate = DateTime.Now;
            _booktransferlandRepository.Add(booktransferland);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<List<LandNotification>> GetAllLandNotification()
        {
            List<LandNotification> landNotificationList = await _booktransferlandRepository.GetAllLandNotification();
            return landNotificationList;
        }
       
        public async Task<List<Acquiredlandvillage>> GetAllLocality()
        {
            List<Acquiredlandvillage> localityList = await _booktransferlandRepository.GetAllLocality();
            return localityList;
        }
        public async Task<List<Khasra>> GetAllKhasra()
        {
            List<Khasra> khasraList = await _booktransferlandRepository.GetAllKhasra();
            return khasraList;
        }



        public async Task<bool> Delete(int id)
        {
            var form = await _booktransferlandRepository.FindBy(a => a.Id == id);
            Booktransferland model = form.FirstOrDefault();
            model.IsActive = 0;
            _booktransferlandRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<PagedResult<Booktransferland>> GetPagedBooktransferland(BooktransferlandSearchDto model)
        {
            return await _booktransferlandRepository.GetPagedBooktransferland(model);
        }

    }
}
