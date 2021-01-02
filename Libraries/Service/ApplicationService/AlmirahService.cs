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
    public class AlmirahService : EntityService<Almirah>, IAlmirahService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAlmirahRepository _almirahRepository;
        protected readonly DataContext _dbContext;


        public AlmirahService(IUnitOfWork unitOfWork, IAlmirahRepository almirahRepository, DataContext dbContext)
       : base(unitOfWork, almirahRepository)
        {
            _unitOfWork = unitOfWork;
            _almirahRepository = almirahRepository;
            _dbContext = dbContext;
        }

        public async Task<List<Almirah>> GetAllAlmirah()
        {
            return await _almirahRepository.GetAll();
        }

        public async Task<List<Almirah>> GetAlmirahUsingReport()
        {
            return await _almirahRepository.GetAlmirah();
        }

        public async Task<bool> Update(int id, Almirah almirah)
        {
            var result = await _almirahRepository.FindBy(a => a.Id == id);
            Almirah model = result.FirstOrDefault();
            model.AlmirahNo = almirah.AlmirahNo;
            model.ModifiedDate = DateTime.Now;
            model.IsActive = almirah.IsActive;
            model.ModifiedBy = 1;
            _almirahRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public bool CheckUniqueName(int id, Almirah almirah)
        {
            var result = _dbContext.Almirah.Any(t => t.Id != id && t.AlmirahNo == almirah.AlmirahNo);
            return result;
        }

        public async Task<Almirah> FetchSingleResult(int id)
        {
            var result = await _almirahRepository.FindBy(a => a.Id == id);
            Almirah model = result.FirstOrDefault();
            return model;
        }



        public async Task<bool> Create(Almirah almirah)
        {

            almirah.CreatedBy = 1;
            almirah.CreatedDate = DateTime.Now;
            _almirahRepository.Add(almirah);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> CheckUniqueName(int id, string almirah)
        {
            bool result = await _almirahRepository.Any(id, almirah);

            return result;
        }


        public async Task<bool> Delete(int id)
        {
            var form = await _almirahRepository.FindBy(a => a.Id == id);
            Almirah model = form.FirstOrDefault();
            model.IsActive = 0;
            _almirahRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Almirah>> GetPagedAlmirah(AlmirahSearchDto model)
        {
            return await _almirahRepository.GetPagedAlmirah(model);
        }

     
    }
}
