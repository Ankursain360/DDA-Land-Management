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

    public class LicenceFeesService : EntityService<Licencefees>, ILicenceFeesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILicenceFeesRepository _licenceFeesRepository;

        public LicenceFeesService(IUnitOfWork unitOfWork, ILicenceFeesRepository LicenceFeesRepository)
        : base(unitOfWork, LicenceFeesRepository)
        {
            _unitOfWork = unitOfWork;
            _licenceFeesRepository = LicenceFeesRepository;
        }

        public async Task<List<Licencefees>> GetAllLicencefees()
        {
            return await _licenceFeesRepository.GetAllLicencefees();
        }

        public async Task<List<Leasepurpose>> GetAllLeasepurpose()
        {
            List<Leasepurpose> leasePurposeList = await _licenceFeesRepository.GetAllLeasepurpose();
            return leasePurposeList;
        }

        public async Task<List<Leasesubpurpose>> GetAllLeaseSubpurpose(int purposeUseId)
        {
            List<Leasesubpurpose> leaseSubPurposeList = await _licenceFeesRepository.GetAllLeaseSubpurpose(purposeUseId);
            return leaseSubPurposeList;
        }

        public async Task<Licencefees> FetchSingleResult(int id)
        {
            var result = await _licenceFeesRepository.FindBy(a => a.Id == id);
            Licencefees model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Licencefees licencefees)
        {
            var result = await _licenceFeesRepository.FindBy(a => a.Id == id);
            Licencefees model = result.FirstOrDefault();
            model.LeasePurposesTypeId = licencefees.LeasePurposesTypeId;
            model.LeaseSubPurposeId = licencefees.LeaseSubPurposeId;
            model.LicenceFees = licencefees.LicenceFees;
            model.FromDate = licencefees.FromDate;
            model.ToDate = licencefees.ToDate;

            model.IsActive = licencefees.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = licencefees.ModifiedBy;
            _licenceFeesRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Licencefees licencefees)
        {
            licencefees.CreatedBy = licencefees.CreatedBy;
            licencefees.CreatedDate = DateTime.Now;
            _licenceFeesRepository.Add(licencefees);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<bool> Delete(int id)
        {
            var form = await _licenceFeesRepository.FindBy(a => a.Id == id);
            Licencefees model = form.FirstOrDefault();
            model.IsActive = 0;
            _licenceFeesRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Licencefees>> GetPagedLicencefees(LicencefeesSearchDto model)
        {
            return await _licenceFeesRepository.GetPagedLicencefees(model);
        }


    }
}
