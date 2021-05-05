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

    public class PropertyTypeService : EntityService<PropertyType>, IPropertyTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPropertyTypeRepository _PropertyTypeRepository;

        public PropertyTypeService(IUnitOfWork unitOfWork, IPropertyTypeRepository PropertyTypeRepository) : base(unitOfWork, PropertyTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _PropertyTypeRepository = PropertyTypeRepository;
        }

        public async Task<List<PropertyType>> GetAllPropertyType()
        {
            return await _PropertyTypeRepository.GetAllPropertyType();
        }

        public async Task<List<PropertyType>> GetAllPropertyTypeList()
        {
            return await _PropertyTypeRepository.GetAllPropertyTypeList();
        }


        public async Task<PropertyType> FetchSingleResult(int id)
        {
            var result = await _PropertyTypeRepository.FindBy(a => a.Id == id);
            PropertyType model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, PropertyType rent)
        {
            var result = await _PropertyTypeRepository.FindBy(a => a.Id == id);
            PropertyType model = result.FirstOrDefault();
            model.Name = rent.Name;
            

            model.IsActive = rent.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = rent.ModifiedBy;
            _PropertyTypeRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(PropertyType rate)
        {
            rate.CreatedBy = rate.CreatedBy;

            rate.CreatedDate = DateTime.Now;
            _PropertyTypeRepository.Add(rate);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<bool> Delete(int id)
        {
            var form = await _PropertyTypeRepository.FindBy(a => a.Id == id);
            PropertyType model = form.FirstOrDefault();
            model.IsActive = 0;
            _PropertyTypeRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<PropertyType>> GetPagedPropertyType(PropertyTypeSearchDto model)
        {
            return await _PropertyTypeRepository.GetPagedPropertyType(model);
        }
    }
}
