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
  
      public class LdolandService : EntityService<Ldoland>, ILdolandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILdolandRepository _ldolandRepository;

        public LdolandService(IUnitOfWork unitOfWork, ILdolandRepository ldolandRepository)
        : base(unitOfWork, ldolandRepository)
        {
            _unitOfWork = unitOfWork;
            _ldolandRepository = ldolandRepository;
        }

        public async Task<List<Ldoland>> GetAllLdoland()
        {
            return await _ldolandRepository.GetAllLdoland();
        }
        public async Task<List<Ldoland>> GetAllLdolandList(LdolandSearchDto model)
        {
            return await _ldolandRepository.GetAllLdolandList(model);
        }
        public async Task<List<Ldoland>> GetLdolandUsingRepo()
        {
            return await _ldolandRepository.GetLdoland();
        }

        public async Task<Ldoland> FetchSingleResult(int id)
        {
            var result = await _ldolandRepository.FindBy(a => a.Id == id);
            Ldoland model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Ldoland ldoland)
        {
            var result = await _ldolandRepository.FindBy(a => a.Id == id);
            Ldoland model = result.FirstOrDefault();

            model.OtherLandNotificationId = ldoland.OtherLandNotificationId;
            model.NotificationDate = ldoland.NotificationDate;
            model.SerialNumber = ldoland.SerialNumber;
            model.PropertySiteNo = ldoland.PropertySiteNo;
            model.Location = ldoland.Location;
            model.SiteDescription = ldoland.SiteDescription;
            model.Area = ldoland.Area;           
            model.StatusOfLand = ldoland.StatusOfLand;
            model.OccupiedBy = ldoland.OccupiedBy;
            model.DateofPossession = ldoland.DateofPossession;
            model.DateOfLandResume = ldoland.DateOfLandResume;
            model.GOINotificationDocumentName = ldoland.GOINotificationDocumentName;
            model.OrderDocumentName = ldoland.OrderDocumentName;
            model.PossessionDocumentName = ldoland.PossessionDocumentName;
            model.Remarks = ldoland.Remarks;
            model.IsActive = ldoland.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = ldoland.ModifiedBy;
            _ldolandRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Ldoland ldoland)
        {

            ldoland.CreatedBy = ldoland.CreatedBy;
            ldoland.CreatedDate = DateTime.Now;
            _ldolandRepository.Add(ldoland);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<List<LandNotification>> GetAllLandNotification()
        {
            List<LandNotification> landNotificationList = await _ldolandRepository.GetAllLandNotification();
            return landNotificationList;
        }
        public async Task<List<Otherlandnotification>> GetAllOtherLandNotification()
        {
            List<Otherlandnotification> List = await _ldolandRepository.GetAllOtherLandNotification();
            return List;
        }
        
        //public async Task<List<Serialnumber>> GetAllSerialnumber()
        //{
        //    List<Serialnumber> serialnumberList = await _ldolandRepository.GetAllSerialnumber();
        //    return serialnumberList;
        //}

        //public async Task<bool> CheckUniqueName(int id, string page)
        //{
        //    bool result = await _pageRepository.Any(id, page);
        //    //  var result1 = _dbContext.Designation.Any(t => t.Id != id && t.Name == designation.Name);
        //    return result;
        //}

        public async Task<bool> Delete(int id)
        {
            var form = await _ldolandRepository.FindBy(a => a.Id == id);
            Ldoland model = form.FirstOrDefault();
            model.IsActive = 0;
            _ldolandRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        
        public async Task<PagedResult<Ldoland>> GetPagedLdoland(LdolandSearchDto model)
        {
            return await _ldolandRepository.GetPagedLdoland(model);
        }

    }
}
