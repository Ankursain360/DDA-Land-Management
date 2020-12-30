using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.ApplicationService
{
    public class CaseyearService : EntityService<Caseyear>, ICaseyearService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICaseyearRepository _caseyearRepository;

        public CaseyearService(IUnitOfWork unitOfWork, ICaseyearRepository caseyearRepository)
       : base(unitOfWork, caseyearRepository)
        {
            _unitOfWork = unitOfWork;
            _caseyearRepository = caseyearRepository;
        }

        public async Task<List<Caseyear>> GetAllCaseyear()
        {
            List<Caseyear> DamageList = await _caseyearRepository.GetAllCaseyear();
            return DamageList;
        }

        public async Task<bool> Create(Caseyear caseyear)
        {
            caseyear.CreatedBy = 1;
            caseyear.CreatedDate = DateTime.Now;


            _caseyearRepository.Add(caseyear);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Caseyear>> GetPagedCaseyear(CaseyearSearchDto model)
        {
            return await _caseyearRepository.GetPagedCaseyear(model);
        }


        public async Task<Caseyear> FetchSingleResult(int id)
        {
            var result = await _caseyearRepository.FindBy(a => a.Id == id);
            Caseyear model = result.FirstOrDefault();
            return model;
        }


        public async Task<bool> Update(int id, Caseyear caseyear)
        {
            var result = await _caseyearRepository.FindBy(a => a.Id == id);
            Caseyear model = result.FirstOrDefault();

            model.Name = caseyear.Name;
            model.IsActive = caseyear.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _caseyearRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _caseyearRepository.FindBy(a => a.Id == id);
            Caseyear model = form.FirstOrDefault();
            model.IsActive = 0;
            _caseyearRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }


    }
}
