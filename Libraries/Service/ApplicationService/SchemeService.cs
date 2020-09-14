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

namespace LibrariesService.ApplicationService
{
   public class SchemeService: EntityService<Scheme>, ISchemeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISchemeRepository _schemeRepository;

        public SchemeService(IUnitOfWork unitOfWork, ISchemeRepository schemeRepository)
       : base(unitOfWork, schemeRepository)
        {
            _unitOfWork = unitOfWork;
            _schemeRepository = schemeRepository;
        }

        public async Task<List<Scheme>> GetAllScheme()
        {

            return await _schemeRepository.GetAllScheme();
        }





        public async Task<List<Scheme>> GetSchemeUsingRepo()
        {
            return await _schemeRepository.GetAllScheme();
        }

        public async Task<bool> Update(int id, Scheme scheme)
        {
            var result = await _schemeRepository.FindBy(a => a.Id == id);
            Scheme model = result.FirstOrDefault();
            model.Name = scheme.Name;
            model.Code = scheme.Code;
            model.IsActive = scheme.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _schemeRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Scheme scheme)
        {

            scheme.CreatedBy = 1;
            scheme.CreatedDate = DateTime.Now;
            _schemeRepository.Add(scheme);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<Scheme> FetchSingleResult(int id)
        {
            var result = await _schemeRepository.FindBy(a => a.Id == id);
            Scheme model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _schemeRepository.FindBy(a => a.Id == id);
            Scheme model = form.FirstOrDefault();
            model.IsActive = 0;
            _schemeRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> CheckUniqueName(int id, string name)
        {
            bool result = await _schemeRepository.Any(id, name);
            return result;
        }

    }
}
