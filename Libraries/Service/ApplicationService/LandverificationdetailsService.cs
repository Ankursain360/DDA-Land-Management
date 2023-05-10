using Dto.Master;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.EntityRepository;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.ApplicationService
{
    public class LandverificationdetailsService : EntityService<LandVerificationDetails>, ILandverificationdetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILandverificationdetailsRepository _landverificationdetailsRepository;

        public LandverificationdetailsService(IUnitOfWork unitOfWork, ILandverificationdetailsRepository landverificationdetailsRepository):
            base(unitOfWork,landverificationdetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _landverificationdetailsRepository = landverificationdetailsRepository;
        }
        public async Task<bool> Create(landverificationdetailsDto dto)
        {
            LandVerificationDetails model = new LandVerificationDetails();
            model.VillageId = dto.VillageId;
            model.Khasraid = dto.Khasraid;
            model.CreatedBy = dto.createdby;
            model.CreatedDate = DateTime.Now;
            model.IsActive = 1;
            model.AckID = dto.AckID;
            _landverificationdetailsRepository.Add(model);
            var result = await _unitOfWork.CommitAsync() > 0;
            dto.Id = model.Id;
            dto.createdby = model.CreatedBy;
            return result;
        }

        public async Task<int> SaveLandVillagedetails(LandVerificationVillageDetails landVerificationVillage)
        {
            landVerificationVillage.CreatedDate = DateTime.Now;
            return await _landverificationdetailsRepository.SaveLandVillagedetails(landVerificationVillage);
        }

        public async Task<int> SaveSignatureData(LandVerificationSignatureData signatureData)
        {
            signatureData.CreatedDate = DateTime.Now;
            return await _landverificationdetailsRepository.SaveSignatureData(signatureData);
        }
    }
}
