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

    public class ClassificationOfLandService : EntityService<Classificationofland>, IClassificationOfLandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClassificationOfLandRepository _classificationoflandRepository;

        public ClassificationOfLandService(IUnitOfWork unitOfWork, IClassificationOfLandRepository classificationoflandRepository)
        : base(unitOfWork, classificationoflandRepository)
        {
            _unitOfWork = unitOfWork;
            _classificationoflandRepository = classificationoflandRepository;

        }

        public async Task<List<Classificationofland>> GetAllLandUse()
        {
            return await _classificationoflandRepository.GetAll();
        }


        public async Task<Classificationofland> FetchSingleResult(int id)
        {
            var result = await _classificationoflandRepository.FindBy(a => a.Id == id);
            Classificationofland model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Classificationofland classificationofland)
        {
            var result = await _classificationoflandRepository.FindBy(a => a.Id == id);
            Classificationofland model = result.FirstOrDefault();
            model.Name = classificationofland.Name;
            model.ModifiedDate = DateTime.Now;
            model.IsActive = classificationofland.IsActive;
            model.ModifiedBy = 1;
            _classificationoflandRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Classificationofland classificationofland)
        {

            classificationofland.CreatedBy = 1;
            classificationofland.CreatedDate = DateTime.Now;
            _classificationoflandRepository.Add(classificationofland);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<bool> CheckUniqueName(int id, string classificationofland)
        {
            bool result = await _classificationoflandRepository.Any(id, classificationofland);
            //  var result1 = _dbContext.Classificationofland.Any(t => t.Id != id && t.Name == classificationofland.Name);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _classificationoflandRepository.FindBy(a => a.Id == id);
            Classificationofland model = form.FirstOrDefault();
            model.IsActive = 0;
            _classificationoflandRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }


    }
}
