using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{
   
    public class StructureService : EntityService<Structure>, IStructureService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStructureRepository _structureRepository;

        public StructureService(IUnitOfWork unitOfWork, IStructureRepository structureRepository)
        : base(unitOfWork, structureRepository)
        {
            _unitOfWork = unitOfWork;
            _structureRepository = structureRepository;
        }

        public async Task<List<Structure>> GetAllStructure()
        {
            return await _structureRepository.GetAllStructure();
        }

       

        public async Task<List<Structure>> GetStructureUsingRepo()
        {
            return await _structureRepository.GetAllStructure();
        }

        public async Task<Structure> FetchSingleResult(int id)
        {
            var result = await _structureRepository.FindBy(a => a.Id == id);
            Structure model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Structure structure)
        {
            var result = await _structureRepository.FindBy(a => a.Id == id);
            Structure model = result.FirstOrDefault();
            model.Name = structure.Name;
           
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _structureRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Structure structure)
        {
            structure.CreatedBy = 1;
            structure.CreatedDate = DateTime.Now;
            _structureRepository.Add(structure);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> CheckUniqueName(int id, string structure)
        {
            bool result = await _structureRepository.Any(id, structure);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _structureRepository.FindBy(a => a.Id == id);
            Structure model = form.FirstOrDefault();
            model.IsActive = 0;
            _structureRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Structure>> GetPagedStructure(StructureSearchDto model)
        {
            return await _structureRepository.GetPagedStructure(model);
        }

       
    }
}
