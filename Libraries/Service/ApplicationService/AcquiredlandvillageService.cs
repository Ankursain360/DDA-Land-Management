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
  public  class AcquiredlandvillageService : EntityService<Acquiredlandvillage>, IAcquiredlandvillageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAcquiredlandvillageRepository _acquiredlandvillageRepository;
        public AcquiredlandvillageService(IUnitOfWork unitOfWork, IAcquiredlandvillageRepository acquiredlandvillageRepository)
      : base(unitOfWork, acquiredlandvillageRepository)
        {
            _unitOfWork = unitOfWork;
            _acquiredlandvillageRepository = acquiredlandvillageRepository;
        }





        public async Task<bool> Delete(int id)
        {
            var form = await _acquiredlandvillageRepository.FindBy(a => a.Id == id);
            Acquiredlandvillage model = form.FirstOrDefault();
            model.IsActive = 0;
            _acquiredlandvillageRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Acquiredlandvillage> FetchSingleResult(int id)
        {
            var result = await _acquiredlandvillageRepository.FindBy(a => a.Id == id);
            Acquiredlandvillage model = result.FirstOrDefault();
            return model;
        }

        public async Task<List<District>> GetAllDistrict()
        {
            List<District> districtList = await _acquiredlandvillageRepository.GetAllDistrict();
            return districtList;
        }

        public async Task<List<Acquiredlandvillage>> GetAcquiredlandvillage()
        {

            return await _acquiredlandvillageRepository.GetAcquiredlandvillage();
        }

        public async Task<List<Tehsil>> GetAllTehsil()
        {
            List<Tehsil> tehsilList = await _acquiredlandvillageRepository.GetAllTehsil();
            return tehsilList;
        }
       
        public async Task<List<Zone>> GetAllZone()
        {
            List<Zone> zoneList = await _acquiredlandvillageRepository.GetAllZone();
            return zoneList;
        }

        public async Task<List<Acquiredlandvillage>> GetACquiredlandvillageUsingRepo()
        {
            return await _acquiredlandvillageRepository.GetAcquiredlandvillage();
        }

        public async Task<bool> Update(int id, Acquiredlandvillage acquiredlandvillage)
        {
            var result = await _acquiredlandvillageRepository.FindBy(a => a.Id == id);
            Acquiredlandvillage model = result.FirstOrDefault();
           
            model.Name = acquiredlandvillage.Name;
            model.Code = acquiredlandvillage.Code;
            model.DistrictId = acquiredlandvillage.DistrictId;
            model.TehsilId = acquiredlandvillage.TehsilId;
            model.YearofConsolidation = acquiredlandvillage.YearofConsolidation;
            model.TotalNoOfSheet = acquiredlandvillage.TotalNoOfSheet;

            model.ZoneId = acquiredlandvillage.ZoneId;
            model.Acquired = acquiredlandvillage.Acquired;
            model.Circle = acquiredlandvillage.Circle;
            model.WorkingVillage = acquiredlandvillage.WorkingVillage;
            model.VillageType = acquiredlandvillage.VillageType;
            model.IsActive = acquiredlandvillage.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = acquiredlandvillage.ModifiedBy;
            _acquiredlandvillageRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Acquiredlandvillage acquiredlandvillage)
        {
            acquiredlandvillage.CreatedBy = acquiredlandvillage.CreatedBy;
            acquiredlandvillage.CreatedDate = DateTime.Now;

            _acquiredlandvillageRepository.Add(acquiredlandvillage);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<PagedResult<Acquiredlandvillage>> GetPagedAcquiredlandvillage(AcquiredLandVillageSearchDto model)
        {
            return await _acquiredlandvillageRepository.GetPagedAcquiredlandvillage(model);
        }
        public async Task<PagedResult<Acquiredlandvillage>> GetPagedVillageReport(VillageReportSearchDto model)
        {
            return await _acquiredlandvillageRepository.GetPagedVillageReport(model);

        }
        public async Task<List<Acquiredlandvillage>> GetAllVillageList()
        {
            List<Acquiredlandvillage> villageList = await _acquiredlandvillageRepository.GetAllVillageList();
            return villageList;
        }

    }
}
