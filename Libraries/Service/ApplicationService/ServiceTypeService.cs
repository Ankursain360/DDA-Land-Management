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
    public class ServiceTypeService : EntityService<Servicetype>, IServiceTypeService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceTypeRepository _serviceTypeRepository;
        protected readonly DataContext _dbContext;


        public ServiceTypeService(IUnitOfWork unitOfWork, IServiceTypeRepository serviceTypeRepository, DataContext dbContext)
       : base(unitOfWork, serviceTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _serviceTypeRepository = serviceTypeRepository;
            _dbContext = dbContext;
        }



        public async Task<List<Servicetype>> GetAllServicetype()
        {
            return await _serviceTypeRepository.GetAll();
        }

        public async Task<List<Servicetype>> GetServicetypeUsingRepo()
        {
            return await _serviceTypeRepository.GetServicetype();
        }

        public async Task<bool> Update(int id, Servicetype servicetype)
        {
            var result = await _serviceTypeRepository.FindBy(a => a.Id == id);
            Servicetype model = result.FirstOrDefault();
            model.Name = servicetype.Name;
            model.Url = servicetype.Url;
            model.Timeline = servicetype.Timeline;
            model.ModifiedDate = DateTime.Now;
            model.IsActive = servicetype.IsActive;
            model.ModifiedBy = 1;
            _serviceTypeRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<Servicetype> FetchSingleResult(int id)
        {
            var result = await _serviceTypeRepository.FindBy(a => a.Id == id);
            Servicetype model = result.FirstOrDefault();
            return model;
        }



        public async Task<bool> Create(Servicetype servicetype)
        {

            servicetype.CreatedBy = 1;
            servicetype.CreatedDate = DateTime.Now;
            _serviceTypeRepository.Add(servicetype);
            return await _unitOfWork.CommitAsync() > 0;
        }




        public async Task<bool> Delete(int id)
        {
            var form = await _serviceTypeRepository.FindBy(a => a.Id == id);
            Servicetype model = form.FirstOrDefault();
            model.IsActive = 0;
            _serviceTypeRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Servicetype>> GetPagedServicetype(ServiceTypeSearchDto model)
        {
            return await _serviceTypeRepository.GetPagedServicetype(model);
        }

    }
}

