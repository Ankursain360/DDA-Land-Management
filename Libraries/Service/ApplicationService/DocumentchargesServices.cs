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
     
    public class DocumentchargesServices : EntityService<Documentcharges>, IDocumentchargesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDocumentchargesRepository _documentchargesRepository;

        public DocumentchargesServices(IUnitOfWork unitOfWork, IDocumentchargesRepository documentchargesRepository)
        : base(unitOfWork, documentchargesRepository)
        {
            _unitOfWork = unitOfWork;
            _documentchargesRepository = documentchargesRepository;
        }

        public async Task<List<Documentcharges>> GetAllDocumentcharges()
        {
            return await _documentchargesRepository.GetAllDocumentcharges();
        }

        public async Task<List<PropertyType>> GetAllPropertyType()
        {
            List<PropertyType> list = await _documentchargesRepository.GetAllPropertyType();
            return list;
        }

        public async Task<Documentcharges> FetchSingleResult(int id)
        {
            var result = await _documentchargesRepository.FindBy(a => a.Id == id);
            Documentcharges model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Documentcharges charge)
        {
            var result = await _documentchargesRepository.FindBy(a => a.Id == id);
            Documentcharges model = result.FirstOrDefault();
            model.PropertyTypeId = charge.PropertyTypeId;
            model.DocumentCharge = charge.DocumentCharge;
            model.FromDate = charge.FromDate;
            model.ToDate = charge.ToDate;

            model.IsActive = charge.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = charge.ModifiedBy;
            _documentchargesRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Documentcharges charge)
        {
            charge.CreatedBy = charge.CreatedBy;
            charge.CreatedDate = DateTime.Now;
            _documentchargesRepository.Add(charge);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<bool> Delete(int id)
        {
            var form = await _documentchargesRepository.FindBy(a => a.Id == id);
            Documentcharges model = form.FirstOrDefault();
            model.IsActive = 0;
            _documentchargesRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Documentcharges>> GetPagedDocumentcharges(DocumentchargesSearchDto model)
        {
            return await _documentchargesRepository.GetPagedDocumentcharges(model);
        }


    }
}
