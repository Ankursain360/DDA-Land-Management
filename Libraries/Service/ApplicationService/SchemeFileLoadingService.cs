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
    public class SchemeFileLoadingService : EntityService<Schemefileloading>, ISchemeFileLoadingService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ISchemeFileLoadingRepository _schemeFileLoadingRepository;
        protected readonly DataContext _dbContext;


        public SchemeFileLoadingService(IUnitOfWork unitOfWork, ISchemeFileLoadingRepository schemeFileLoadingRepository, DataContext dbContext)
       : base(unitOfWork, schemeFileLoadingRepository)
        {
            _unitOfWork = unitOfWork;
            _schemeFileLoadingRepository = schemeFileLoadingRepository;
            _dbContext = dbContext;
        }



        public async Task<List<Schemefileloading>> GetAllSchemeFileLoading()
        {
            return await _schemeFileLoadingRepository.GetAll();
        }

        public async Task<List<Schemefileloading>> GetSchemeFileLoadingUsingRepo()
        {
            return await _schemeFileLoadingRepository.GetSchemeFileloading();
        }

        public async Task<bool> Update(int id, Schemefileloading schemefileloading)
        {
            var result = await _schemeFileLoadingRepository.FindBy(a => a.Id == id);
            Schemefileloading model = result.FirstOrDefault();
            model.SchemeName = schemefileloading.SchemeName;
            model.SchemeCode = schemefileloading.SchemeCode;
            model.ModifiedDate = DateTime.Now;
            model.IsActive = schemefileloading.IsActive;
            model.ModifiedBy = 1;
            _schemeFileLoadingRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public bool CheckUniqueName(int id, Schemefileloading schemefileloading)
        {
            var result = _dbContext.Schemefileloading.Any(t => t.Id != id && t.SchemeName == schemefileloading.SchemeName);
            return result;
        }

        public async Task<Schemefileloading> FetchSingleResult(int id)
        {
            var result = await _schemeFileLoadingRepository.FindBy(a => a.Id == id);
            Schemefileloading model = result.FirstOrDefault();
            return model;
        }



        public async Task<bool> Create(Schemefileloading schemefileloading)
        {

            schemefileloading.CreatedBy = 1;
            schemefileloading.CreatedDate = DateTime.Now;
            _schemeFileLoadingRepository.Add(schemefileloading);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> CheckUniqueName(int id, string schemename)
        {
            bool result = await _schemeFileLoadingRepository.Any(id, schemename);
            //  var result1 = _dbContext.Designation.Any(t => t.Id != id && t.Name == designation.Name);
            return result;
        }


        public async Task<bool> Delete(int id)
        {
            var form = await _schemeFileLoadingRepository.FindBy(a => a.Id == id);
            Schemefileloading model = form.FirstOrDefault();
            model.IsActive = 0;
            _schemeFileLoadingRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Schemefileloading>> GetPagedSchemeFileLoading(SchemeFileLoadingSearchDto model)
        {
            return await _schemeFileLoadingRepository.GetPagedSchemeFileLoading(model);
        }

    }
}
