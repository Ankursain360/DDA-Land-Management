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
using AutoMapper;
using Dto.Master;
using Libraries.Repository.EntityRepository;

namespace Libraries.Service.ApplicationService
{
    public class CaseyearService : EntityService<Caseyear>, ICaseyearService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICaseyearRepository _caseyearRepository;
        private readonly IMapper _mapper;
        public CaseyearService(IUnitOfWork unitOfWork, ICaseyearRepository caseyearRepository, IMapper mapper) : base(unitOfWork, caseyearRepository)
        {
            _unitOfWork = unitOfWork;
            _caseyearRepository = caseyearRepository;
            _mapper = mapper;
        }

        public async Task<List<Caseyear>> GetAllcaseyear()
        {
            return await _caseyearRepository.GetAll();
        }

        public async Task<List<CaseyearSearchDto>> Getcaseyear()
        {
            var caseyear = await _caseyearRepository.FindBy(a => a.IsActive == 1);
            var result = _mapper.Map<List<CaseyearSearchDto>>(caseyear);
            return result;
        }

         public async Task<List<Caseyear>> GetcaseyearUsingRepo()
        {
            return await _caseyearRepository.GetAllCaseyear();
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

        public new async Task<bool> Create(Caseyear caseyear)
        {

            caseyear.CreatedBy = 1;
            caseyear.CreatedDate = DateTime.Now;
            _caseyearRepository.Add(caseyear);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<bool> CheckUniqueName(int id, string caseyear)
        {
            bool result = await _caseyearRepository.Any(id, caseyear);
            //  var result1 = _dbContext.Designation.Any(t => t.Id != id && t.Name == designation.Name);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _caseyearRepository.FindBy(a => a.Id == id);
            Caseyear model = form.FirstOrDefault();
            model.IsActive = 0;
            _caseyearRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Caseyear>> GetPagedcaseyear(CaseyearSearchDto model)
        {
            return await _caseyearRepository.GetPagedCaseyear(model);
        }
    }
}
