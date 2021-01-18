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
   public class DemolitiondocumentService : EntityService<Demolitiondocument>, IDemolitiondocumentService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDemolitiondocumentRepository _demolitiondocumentRepository;

        public DemolitiondocumentService(IUnitOfWork unitOfWork, IDemolitiondocumentRepository demolitiondocumentRepository)
  : base(unitOfWork, demolitiondocumentRepository)
        {
            _unitOfWork = unitOfWork;
            _demolitiondocumentRepository = demolitiondocumentRepository;
        }






        public async Task<List<Demolitiondocument>> GetDemolitiondocumentUsingRepo()
        {
            return await _demolitiondocumentRepository.GetDemolitiondocument();
        }

        public async Task<Demolitiondocument> FetchSingleResult(int id)
        {
            var result = await _demolitiondocumentRepository.FindBy(a => a.Id == id);
            Demolitiondocument model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Demolitiondocument demolitiondocument)
        {
            var result = await _demolitiondocumentRepository.FindBy(a => a.Id == id);
            Demolitiondocument model = result.FirstOrDefault();
            model.DocumentName = demolitiondocument.DocumentName;
            model.IsMandatory = demolitiondocument.IsMandatory;
            model.IsActive = demolitiondocument.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _demolitiondocumentRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Demolitiondocument demolitiondocument)
        {

            demolitiondocument.CreatedBy = 1;
            demolitiondocument.CreatedDate = DateTime.Now;
            _demolitiondocumentRepository.Add(demolitiondocument);
            return await _unitOfWork.CommitAsync() > 0;
        }




        public async Task<bool> Delete(int id)
        {
            var form = await _demolitiondocumentRepository.FindBy(a => a.Id == id);
            Demolitiondocument model = form.FirstOrDefault();
            model.IsActive = 0;
            _demolitiondocumentRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<List<Demolitiondocument>> GetDemolitiondocument()
        {
            return await _demolitiondocumentRepository.GetDemolitiondocument();
        }



        public async Task<PagedResult<Demolitiondocument>> GetPagedDemolitiondocument(DemolitiondocumentSearchDto model)
        {
            return await _demolitiondocumentRepository.GetPagedDemolitiondocument(model);
        }















    }
}
