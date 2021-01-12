using Dto.Search;
using Libraries.Model;
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
    public class MutationDetailsService : EntityService<Mutationdetailstemp>, IMutationDetailsService

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMutationDetailsRepository _mutationDetailsRepository;
        protected readonly DataContext _dbContext;

        public MutationDetailsService(IUnitOfWork unitOfWork, IMutationDetailsRepository mutationDetailsRepository, DataContext dbContext)
       : base(unitOfWork, mutationDetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _mutationDetailsRepository = mutationDetailsRepository;
            _dbContext = dbContext;
        }

        public async Task<bool> Create(Mutationdetailstemp details)
        {
            Mutationdetailstemp model = new Mutationdetailstemp();
            model.FileNo = details.FileNo;
            model.DamagePayeeRegisterId = details.DamagePayeeRegisterId;
            model.MutationPurpose = details.MutationPurpose;
            model.PurchaseDate = details.PurchaseDate;
            model.AtsfilePath = details.AtsfilePath;
            model.GpafilePath = details.GpafilePath;
            model.MoneyfilePathNew = details.MoneyfilePathNew;
            model.SignatureSpecimenFilePath = details.SignatureSpecimenFilePath;
            model.DeathCertificatePath = details.DeathCertificatePath;
            model.RelationshipUploadPath = details.RelationshipUploadPath;
            model.AffidevitLegalUploadPath = details.AffidevitLegalUploadPath;
            model.NonObjectHeirUploadPath = details.NonObjectHeirUploadPath;
            model.SpecimenSignLegalUpload = details.SpecimenSignLegalUpload;
            model.IsAddressProof = details.IsAddressProof;
            model.AddressProofFilePath = details.AddressProofFilePath;
            model.AffidavitFilePath = details.AffidavitFilePath;
            model.IndemnityFilePath = details.IndemnityFilePath;
            model.Declaration1 = details.Declaration1;
            model.ApprovedStatus = details.ApprovedStatus;
            model.IsActive = 1;

            model.CreatedDate = DateTime.Now;
            model.CreatedBy = details.CreatedBy;
            _mutationDetailsRepository.Add(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<List<Mutationdetails>> GetAllMutationDetails()
        {
            return await _mutationDetailsRepository.GetAllMutationDetails();
        }

        public async Task<List<Locality>> GetLocalityList()
        {
            List<Locality> localityList = await _mutationDetailsRepository.GetLocalityList();
            return localityList;
        }
        public async Task<List<District>> GetDistrictList()
        {
            List<District> districtList = await _mutationDetailsRepository.GetDistrictList();
            return districtList;
        }

        public async Task<bool> Update(int id, Mutationdetailstemp details)
        {
            var result = await _mutationDetailsRepository.FindBy(a => a.Id == id);
            Mutationdetailstemp model = result.FirstOrDefault();
            model.FileNo = details.FileNo;
            model.DamagePayeeRegisterId = details.DamagePayeeRegisterId;
            model.MutationPurpose = details.MutationPurpose;
            model.PurchaseDate = details.PurchaseDate;
            model.AtsfilePath = details.AtsfilePath;
            model.GpafilePath = details.GpafilePath;
            model.MoneyfilePathNew = details.MoneyfilePathNew;
            model.SignatureSpecimenFilePath = details.SignatureSpecimenFilePath;
            model.DeathCertificatePath = details.DeathCertificatePath;
            model.RelationshipUploadPath = details.RelationshipUploadPath;
            model.AffidevitLegalUploadPath = details.AffidevitLegalUploadPath;
            model.NonObjectHeirUploadPath = details.NonObjectHeirUploadPath;
            model.SpecimenSignLegalUpload = details.SpecimenSignLegalUpload;
            model.IsAddressProof = details.IsAddressProof;
            model.AddressProofFilePath = details.AddressProofFilePath;
            model.AffidavitFilePath = details.AffidavitFilePath;
            model.IndemnityFilePath = details.IndemnityFilePath;
            model.Declaration1 = details.Declaration1;
            model.ApprovedStatus = details.ApprovedStatus;
            model.IsActive = 1;

            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = details.ModifiedBy;
            _mutationDetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        //public async Task<Mutationdetails> GetPhotoPropFile(int id)
        //{
        //    return await _mutationDetailsRepository.GetPhotoPropFile(id);

        //}

        //public async Task<Mutationdetails> SaveMutationAtsFilePath(int id)
        //{
        //    return await _mutationDetailsRepository.SaveMutationAtsFilePath(id);
        //}
        //public async Task<Mutationdetails> SaveMutationGPAFilePath(int id)
        //{
        //    return await _mutationDetailsRepository.SaveMutationGPAFilePath(id);
        //}
        //public async Task<Mutationdetails> SaveMutationMoneyReceiptFilePath(int id)
        //{
        //    return await _mutationDetailsRepository.SaveMutationMoneyReceiptFilePath(id);
        //}
        //public async Task<Mutationdetails> SaveMutationSignSPCFilePath(int id)
        //{
        //    return await _mutationDetailsRepository.SaveMutationSignSPCFilePath(id);
        //}
        //public async Task<Mutationdetails> SaveMutationAddressProofFilePath(int id)
        //{
        //    return await _mutationDetailsRepository.SaveMutationAddressProofFilePath(id);
        //}
        //public async Task<Mutationdetails> SaveMutationAffitDevitFilePath(int id)
        //{
        //    return await _mutationDetailsRepository.SaveMutationAffitDevitFilePath(id);
        //}
        //public async Task<Mutationdetails> SaveMutationIndemnityFilePath(int id)
        //{
        //    return await _mutationDetailsRepository.SaveMutationIndemnityFilePath(id);
        //}

        //public async Task<bool> SaveMutationOldDamage(Mutationolddamageassesse oldDamage)
        //{
        //    oldDamage.CreatedBy = 1;
        //    oldDamage.CreatedDate = DateTime.Now;
        //    oldDamage.IsActive = 1;
        //    return await _mutationDetailsRepository.SaveMutationOldDamage(oldDamage);
        //}
        public async Task<Damagepayeeregister> FetchDamageResult(int Id)
        {
            return await _mutationDetailsRepository.FetchDamageResult(Id);
        }

        public async Task<PagedResult<Damagepayeeregister>> GetPagedSubsitutionMutationDetails(SubstitutionMutationDetailsDto model)
        {
            return await _mutationDetailsRepository.GetPagedSubsitutionMutationDetails(model);
        }

        public async Task<List<Damagepayeepersonelinfo>> GetPersonalInfo(int id)
        {
            return await _mutationDetailsRepository.GetPersonalInfo(id);
        }

        public async Task<List<Allottetype>> GetAllottetype(int id)
        {
            return await _mutationDetailsRepository.GetAllottetype(id);
        }

        public async Task<Mutationdetailstemp> FetchMutationSingleResult(int id)
        {
            return await _mutationDetailsRepository.FetchMutationSingleResult(id);
        }
        public async Task<Mutationdetailstemp> FetchSingleResultMutationId(int id)
        {
            return await _mutationDetailsRepository.FetchSingleResultMutationId(id);
        }
        public async Task<Damagepayeepersonelinfo> GetPersonelInfoFile(int id)
        {
            return await _mutationDetailsRepository.GetPersonelInfoFile(id);
        }
        public async Task<Allottetype> GetAlloteeTypeFile(int id)
        {
            return await _mutationDetailsRepository.GetAlloteeTypeFile(id);
        }
    }
}
