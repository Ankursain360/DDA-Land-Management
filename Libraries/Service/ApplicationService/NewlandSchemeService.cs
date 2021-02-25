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
    public class NewlandSchemeService : EntityService<Newlandscheme>, INewlandSchemeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewlandSchemeRepository _schemeRepository;


        public NewlandSchemeService(IUnitOfWork unitOfWork, INewlandSchemeRepository schemeRepository)
  : base(unitOfWork, schemeRepository)
        {
            _unitOfWork = unitOfWork;
            _schemeRepository = schemeRepository;
        }


        public async Task<List<Newlandscheme>> GetAllScheme()
        {

            return await _schemeRepository.GetAllScheme();
        }


        public async Task<List<Newlandscheme>> GetSchemeUsingRepo()
        {
            return await _schemeRepository.GetAllScheme();
        }

        public async Task<bool> Update(int id, Newlandscheme scheme)
        {
            var result = await _schemeRepository.FindBy(a => a.Id == id);
            Newlandscheme model = result.FirstOrDefault();
            model.Name = scheme.Name;
            model.Code = scheme.Code;
            model.SchemeDate = scheme.SchemeDate;
            model.FileNo = scheme.FileNo;
            model.Description = scheme.Description;
            model.IsActive = scheme.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _schemeRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Newlandscheme scheme)
        {

            scheme.CreatedBy = 1;
            scheme.CreatedDate = DateTime.Now;
            _schemeRepository.Add(scheme);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<Newlandscheme> FetchSingleResult(int id)
        {
            var result = await _schemeRepository.FindBy(a => a.Id == id);
            Newlandscheme model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _schemeRepository.FindBy(a => a.Id == id);
            Newlandscheme model = form.FirstOrDefault();
            model.IsActive = 0;
            _schemeRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> CheckUniqueName(int id, string name)
        {
            bool result = await _schemeRepository.Any(id, name);
            return result;
        }


        public async Task<PagedResult<Newlandscheme>> GetPagedScheme(NewlandschemeSearchDto model)
        {
            return await _schemeRepository.GetPagedScheme(model);
        }







    }
}
