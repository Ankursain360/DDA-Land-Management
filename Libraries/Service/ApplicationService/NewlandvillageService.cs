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
    public class NewlandvillageService : EntityService<Newlandvillage>, INewlandvillageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewlandvillageRepository _newlandvillageRepository;
        public NewlandvillageService(IUnitOfWork unitOfWork, INewlandvillageRepository newlandvillageRepository)
      : base(unitOfWork, newlandvillageRepository)
        {
            _unitOfWork = unitOfWork;
            _newlandvillageRepository = newlandvillageRepository;
        }





        public async Task<bool> Delete(int id)
        {
            var form = await _newlandvillageRepository.FindBy(a => a.Id == id);
            Newlandvillage model = form.FirstOrDefault();
            model.IsActive = 0;
            _newlandvillageRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Newlandvillage> FetchSingleResult(int id)
        {
            var result = await _newlandvillageRepository.FindBy(a => a.Id == id);
            Newlandvillage model = result.FirstOrDefault();
            return model;
        }

        public async Task<List<District>> GetAllDistrict()
        {
            List<District> districtList = await _newlandvillageRepository.GetAllDistrict();
            return districtList;
        }
        public async Task<PagedResult<Newlandvillage>> GetPagedNewlandvillage(NewlandvillageSearchDto model)
        {
            return await _newlandvillageRepository.GetPagedNewlandvillage(model);
        }
        public async Task<List<Newlandvillage>> GetNewlandvillage()
        {

            return await _newlandvillageRepository.GetNewlandvillage();
        }

        public async Task<List<Tehsil>> GetAllTehsil()
        {
            List<Tehsil> tehsilList = await _newlandvillageRepository.GetAllTehsil();
            return tehsilList;
        }

        public async Task<List<Zone>> GetAllZone()
        {
            List<Zone> zoneList = await _newlandvillageRepository.GetAllZone();
            return zoneList;
        }

        public async Task<List<Newlandvillage>> GetNewlandvillageUsingRepo()
        {
            return await _newlandvillageRepository.GetNewlandvillage();
        }

        public async Task<bool> Update(int id, Newlandvillage newlandvillage)
        {
            var result = await _newlandvillageRepository.FindBy(a => a.Id == id);
            Newlandvillage model = result.FirstOrDefault();

            model.Name = newlandvillage.Name;
            model.Code = newlandvillage.Code;
            model.DistrictId = newlandvillage.DistrictId;
            model.TehsilId = newlandvillage.TehsilId;
            model.YearofConsolidation = newlandvillage.YearofConsolidation;
            model.TotalNoOfSheet = newlandvillage.TotalNoOfSheet;

            model.ZoneId = newlandvillage.ZoneId;
            model.Acquired = newlandvillage.Acquired;
            model.Circle = newlandvillage.Circle;
            model.WorkingVillage = newlandvillage.WorkingVillage;
            model.VillageType = newlandvillage.VillageType;
            model.IsActive = newlandvillage.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = newlandvillage.ModifiedBy;
            _newlandvillageRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Newlandvillage newlandvillage)
        {
            newlandvillage.CreatedBy = newlandvillage.CreatedBy;
            newlandvillage.CreatedDate = DateTime.Now;

            _newlandvillageRepository.Add(newlandvillage);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public Task<List<Newlandvillage>> GetAllVillageList()
        {
            throw new NotImplementedException();
        }

        public Task<List<Newlandvillage>> GetACquiredlandvillageUsingRepo()
        {
            throw new NotImplementedException();
        }
    }
}
