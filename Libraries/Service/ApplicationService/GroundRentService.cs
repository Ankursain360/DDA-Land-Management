using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{

    public class GroundRentService : EntityService<Groundrent>, IGroundRentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroundRentRepository _groundRentRepository;

        public GroundRentService(IUnitOfWork unitOfWork, IGroundRentRepository groundRentRepository)  : base(unitOfWork, groundRentRepository)
        {
            _unitOfWork = unitOfWork;
            _groundRentRepository = groundRentRepository;
        }

        public async Task<List<Groundrent>> GetAllGroundRent()
        {
            return await _groundRentRepository.GetAllGroundRent();
        }
        public async Task<List<PropertyType>> GetAllPropertyTypeList()
        {
            return await _groundRentRepository.GetAllPropertyTypeList();
        }


        public async Task<Groundrent> FetchSingleResult(int id)
        {
            var result = await _groundRentRepository.FindBy(a => a.Id == id);
            Groundrent model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Groundrent rent)
        {
            var result = await _groundRentRepository.FindBy(a => a.Id == id);
            Groundrent model = result.FirstOrDefault();
            model.PropertyTypeId = rent.PropertyTypeId;
            model.GroundRate = rent.GroundRate;
            model.FromDate = rent.FromDate;
            model.ToDate = rent.ToDate;

            model.IsActive = rent.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = model.ModifiedBy;
            _groundRentRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Groundrent rate)
        {
            rate.CreatedBy = rate.CreatedBy;
           
            rate.CreatedDate = DateTime.Now;
            _groundRentRepository.Add(rate);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<bool> Delete(int id)
        {
            var form = await _groundRentRepository.FindBy(a => a.Id == id);
            Groundrent model = form.FirstOrDefault();
            model.IsActive = 0;
            _groundRentRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Groundrent>> GetPagedGroundRent(GroundrentSearchDto model)
        {
            return await _groundRentRepository.GetPagedGroundRent(model);
        }
    }
}
