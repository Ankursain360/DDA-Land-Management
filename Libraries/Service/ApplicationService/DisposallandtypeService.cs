using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dto.Search;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{
    
     public class DisposallandtypeService : EntityService<Disposallandtype>, IDisposallandtypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDisposallandtypeRepository _disposallandtypeRepository;

        public DisposallandtypeService(IUnitOfWork unitOfWork, IDisposallandtypeRepository disposallandtypeRepository)
        : base(unitOfWork, disposallandtypeRepository)
        {
            _unitOfWork = unitOfWork;
            _disposallandtypeRepository = disposallandtypeRepository;
        }

        public async Task<List<Disposallandtype>> GetAllDisposallandtype()
        {
            return await _disposallandtypeRepository.GetAll();
        }

        public async Task<List<Disposallandtype>> GetDisposallandtypeUsingRepo()
        {
            return await _disposallandtypeRepository.GetDisposallandtype();
        }

        public async Task<Disposallandtype> FetchSingleResult(int id)
        {
            var result = await _disposallandtypeRepository.FindBy(a => a.Id == id);
            Disposallandtype model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Disposallandtype disposallandtype)
        {
            var result = await _disposallandtypeRepository.FindBy(a => a.Id == id);
            Disposallandtype model = result.FirstOrDefault();
            model.Name = disposallandtype.Name;
            model.LandCode = disposallandtype.LandCode;
            model.RecState = disposallandtype.RecState;
            model.Remarks = disposallandtype.Remarks;
                model.IsActive = disposallandtype.IsActive;

            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _disposallandtypeRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Disposallandtype disposallandtype)
        {

            disposallandtype.CreatedBy = 1;
            disposallandtype.CreatedDate = DateTime.Now;
            _disposallandtypeRepository.Add(disposallandtype);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<bool> CheckUniqueName(int id, string disposallandtype)
        {
            bool result = await _disposallandtypeRepository.Any(id, disposallandtype);
            //  var result1 = _dbContext.Designation.Any(t => t.Id != id && t.Name == designation.Name);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _disposallandtypeRepository.FindBy(a => a.Id == id);
            Disposallandtype model = form.FirstOrDefault();
            model.IsActive = 0;
            _disposallandtypeRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Disposallandtype>> GetPagedDisposalLandType(DisposalLandTypeSearchDto model)
        {
            return await _disposallandtypeRepository.GetPagedDisposalLandType(model);
        }






    }
}
