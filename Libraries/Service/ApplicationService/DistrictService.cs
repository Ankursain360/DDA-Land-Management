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
  public  class DistrictService:EntityService<District>, IDistrictService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistrictRepository _districtRepository;
        protected readonly DataContext _dbContext;


        public DistrictService(IUnitOfWork unitOfWork, IDistrictRepository districtRepository, DataContext dbContext)
       : base(unitOfWork, districtRepository)
        {
            _unitOfWork = unitOfWork;
            _districtRepository = districtRepository;
            _dbContext = dbContext;
        }



        public async Task<List<District>> GetAllDistrict()
        {
            return await _districtRepository.GetAll();
        }

        public async Task<List<District>> GetDistrictUsingRepo()
        {
            return await _districtRepository.GetDistricts();
        }

        public async Task<bool> Update(int id, District district)
        {
            var result = await _districtRepository.FindBy(a => a.Id == id);
            District model = result.FirstOrDefault();
            model.Name = district.Name;
            model.Code = district.Code;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _districtRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public bool CheckUniqueName(int id, District district)
        {
            var result = _dbContext.District.Any(t => t.Id != id && t.Name == district.Name);
            return result;
        }

        public async Task<District> FetchSingleResult(int id)
        {
            var result = await _districtRepository.FindBy(a => a.Id == id);
            District model = result.FirstOrDefault();
            return model;
        }



        public async Task<bool> Create(District district)
        {

            district.CreatedBy = 1;
            district.CreatedDate = DateTime.Now;
            _districtRepository.Add(district);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<bool> Delete(int id)
        {
            var form = await _districtRepository.FindBy(a => a.Id == id);
            District model = form.FirstOrDefault();
            model.IsActive = 0;
            _districtRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<District>> GetPagedDistrict(DistrictSearchDto model)
        {
            return await _districtRepository.GetPagedDistrict(model);
        }

    }
}
