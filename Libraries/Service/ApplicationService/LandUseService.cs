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

namespace Libraries.Service.ApplicationService
{

    public class LandUseService : EntityService<Landuse>, ILandUseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILandUseRepository _landuseRepository;

        public LandUseService(IUnitOfWork unitOfWork, ILandUseRepository landuseRepository)
        : base(unitOfWork, landuseRepository)
        {
            _unitOfWork = unitOfWork;
            _landuseRepository = landuseRepository;

        }

        public async Task<List<Landuse>> GetAllLanduse()
        {
            return await _landuseRepository.GetAll();
        }


        public async Task<Landuse> FetchSingleResult(int id)
        {
            var result = await _landuseRepository.FindBy(a => a.Id == id);
            Landuse model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Landuse landuse)
        {
            var result = await _landuseRepository.FindBy(a => a.Id == id);
            Landuse model = result.FirstOrDefault();
            model.Name = landuse.Name;
            model.ModifiedDate = DateTime.Now;
            model.IsActive = landuse.IsActive;
            model.ModifiedBy = 1;
            _landuseRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Landuse landuse)
        {

            landuse.CreatedBy = 1;
            landuse.CreatedDate = DateTime.Now;
            _landuseRepository.Add(landuse);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<bool> CheckUniqueName(int id, string landuse)
        {
            bool result = await _landuseRepository.Any(id, landuse);
            //  var result1 = _dbContext.Landuse.Any(t => t.Id != id && t.Name == landuse.Name);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _landuseRepository.FindBy(a => a.Id == id);
            Landuse model = form.FirstOrDefault();
            model.IsActive = 0;
            _landuseRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public Task<List<Landuse>> GetAllLandUse()
        {
            throw new NotImplementedException();
        }
    }
}
