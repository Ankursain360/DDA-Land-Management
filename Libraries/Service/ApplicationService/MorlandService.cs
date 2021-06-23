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
    public class MorlandService : EntityService<Morland>, IMorlandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMorlandRepository _morlandRepository;
        public MorlandService(IUnitOfWork unitOfWork, IMorlandRepository morlandRepository)
      : base(unitOfWork, morlandRepository)
        {
            _unitOfWork = unitOfWork;
            _morlandRepository = morlandRepository;
        }

        public async Task<PagedResult<Morland>> GetPagedMorland(MorLandsSearchDto model)
        {
            return await _morlandRepository.GetPagedMorland(model);
        }



        public async Task<bool> Delete(int id)
        {
            var form = await _morlandRepository.FindBy(a => a.Id == id);
            Morland model = form.FirstOrDefault();
            model.IsActive = 0;
            _morlandRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Morland> FetchSingleResult(int id)
        {
            var result = await _morlandRepository.FindBy(a => a.Id == id);
            Morland model = result.FirstOrDefault();
            return model;
        }



        public async Task<List<Morland>> GetAllMorland()
        {

            return await _morlandRepository.GetAllMorland();
        }

        public async Task<List<Otherlandnotification>> GetAllLandNotification()
        {
            List<Otherlandnotification> landnotificationList = await _morlandRepository.GetAllLandNotification();
            return landnotificationList;
        }

        
        

        public async Task<List<Morland>> GetMorlandUsingRepo()
        {
            return await _morlandRepository.GetAllMorland();
        }

        public async Task<bool> Update(int id, Morland morland)
        {
            var result = await _morlandRepository.FindBy(a => a.Id == id);
            Morland model = result.FirstOrDefault();
            model.LandNotificationId = morland.LandNotificationId;
            model.NotificationDate = morland.NotificationDate;
            model.PropertySiteNo = morland.PropertySiteNo;
            model.Name = morland.Name;
            model.SiteDescription = morland.SiteDescription;
            model.StatusOfLand = morland.StatusOfLand;
            model.OccupiedBy = morland.OccupiedBy;
            model.Developed = morland.Developed;
            model.LandType = morland.LandType;
            model.Remarks = morland.Remarks;
            model.GOINotificationDocumentName = morland.GOINotificationDocumentName;
            model.IsActive = morland.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = morland.ModifiedBy;
            _morlandRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Morland morland)
        {
            morland.CreatedDate = DateTime.Now;           
            _morlandRepository.Add(morland);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<bool> CheckUniqueName(int id, string Name)
        {
            bool result = await _morlandRepository.Any(id, Name);
            return result;
        }




    }
}
