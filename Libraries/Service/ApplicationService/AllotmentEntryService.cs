using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Libraries.Repository.IEntityRepository;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Dto.Search;
namespace Libraries.Service.ApplicationService
{
    public class AllotmentEntryService : EntityService<Allotmententry>, IAllotmentEntryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAllotmentEntryRepository _allotmentEntryRepository;
        public AllotmentEntryService(IUnitOfWork unitOfWork, IAllotmentEntryRepository allotmentEntryRepository)
      : base(unitOfWork, allotmentEntryRepository)
        {
            _unitOfWork = unitOfWork;
            _allotmentEntryRepository = allotmentEntryRepository;
        }





        public async Task<bool> Delete(int id)
        {
            var form = await _allotmentEntryRepository.FindBy(a => a.Id == id);
            Allotmententry model = form.FirstOrDefault();
            model.IsActive = 0;
            _allotmentEntryRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Allotmententry> FetchSingleResult(int id)
        {
            var result = await _allotmentEntryRepository.FindBy(a => a.Id == id);
            Allotmententry model = result.FirstOrDefault();
            return model;
        }



        public async Task<List<Allotmententry>> GetAllAllotmententry()
        {

            return await _allotmentEntryRepository.GetAllAllotmententry();
        }



       


        public async Task<List<Leaseapplication>> GetAllLeaseapplication()
        {
            List<Leaseapplication> leaseappList = await _allotmentEntryRepository.GetAllLeaseapplication();
            return leaseappList;
        }
        public async Task<List<Leasetype>> GetAllLeasetype()
        {
            List<Leasetype> leaseTypeList = await _allotmentEntryRepository.GetAllLeasetype();
            return leaseTypeList;
        }
        
        public async Task<List<Leasepurpose>> GetAllLeasepurpose()
        {
            List<Leasepurpose> leasePurposeList = await _allotmentEntryRepository.GetAllLeasepurpose();
            return leasePurposeList;
        }

        public async Task<List<Leasesubpurpose>> GetAllLeaseSubpurpose(int purposeUseId)
        {
            List<Leasesubpurpose> leaseSubPurposeList = await _allotmentEntryRepository.GetAllLeaseSubpurpose(purposeUseId);
            return leaseSubPurposeList;
        }


        public async Task<List<Allotmententry>> GetAllotmententryUsingRepo()
        {
            return await _allotmentEntryRepository.GetAllAllotmententry();
        }
        public async Task<PagedResult<Allotmententry>> GetPagedAllotmententry(AllotmentEntrySearchDto model)
        {
            return await _allotmentEntryRepository.GetPagedAllotmententry(model);
        }

        public async Task<bool> Update(int id, Allotmententry allotmententry)
        {
            var result = await _allotmentEntryRepository.FindBy(a => a.Id == id);
            Allotmententry model = result.FirstOrDefault();
            model.ApplicationId = allotmententry.ApplicationId;
            model.Name = allotmententry.Name;
            model.Address = allotmententry.Address;
            model.ContactNo = allotmententry.ContactNo;
            model.LandAreaSqMt = allotmententry.LandAreaSqMt;



            model.AllotmentDate = allotmententry.AllotmentDate;
            model.TotalArea = allotmententry.TotalArea;
            model.PhaseNo = allotmententry.PhaseNo;

            model.SectorNo = allotmententry.SectorNo;
            model.PlotNo = allotmententry.PlotNo;
            model.PocketNo = allotmententry.PocketNo;
            model.NoOfYears = allotmententry.NoOfYears;
            model.AmountLicFee = allotmententry.AmountLicFee;
            model.BuildingArea = allotmententry.BuildingArea;

            model.IsActive = allotmententry.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _allotmentEntryRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Allotmententry allotmententry)
        {
            allotmententry.CreatedBy = allotmententry.CreatedBy;
            allotmententry.CreatedDate = DateTime.Now;
            _allotmentEntryRepository.Add(allotmententry);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<Leaseapplication> FetchSingleLeaseapplicationResult(int? applicationId)
        {
            return await _allotmentEntryRepository.FetchSingleLeaseapplicationResult(applicationId);
        }

        public async Task<Allotmententry> FetchSingleCalculationDetails(int? LeasesTypeId)
        {
            return await _allotmentEntryRepository.FetchSingleCalculationDetails(LeasesTypeId);
        }
    }
}

