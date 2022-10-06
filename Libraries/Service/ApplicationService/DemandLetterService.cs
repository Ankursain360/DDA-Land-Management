using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Master;
using AutoMapper;

namespace Service.ApplicationService
{
    public class DemandLetterService : EntityService<Demandletters>, IDemandLetterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDemandLetterRepository _demandLetterRepository;
        private readonly IMapper _mapper;

        public DemandLetterService(IUnitOfWork unitOfWork, IDemandLetterRepository demandLetterRepository, IMapper mapper)
    : base(unitOfWork, demandLetterRepository)
        {
            _unitOfWork = unitOfWork;
            _demandLetterRepository = demandLetterRepository;
            _mapper = mapper;
        }

        public async Task<List<Demandletters>> GetAllDemandletter()
        {
            List<Demandletters> DamageList = await _demandLetterRepository.GetAllDemandletter();
            return DamageList;
        }

        public async Task<PagedResult<Demandletters>> GetPagedDemandletter(DemandletterSearchDto model)
        {
            return await _demandLetterRepository.GetPagedDemandletter(model);
        }
        public async Task<PagedResult<Demandletters>> GetPagedDuplicateDemandletter(DuplicateDemandLetterSearchDto model)
        {
            return await _demandLetterRepository.GetPagedDuplicateDemandletter(model);
        }
        public async Task<PagedResult<Demandletters>> GetDefaultListingReportData(DefaulterListingReportSearchDto defaulterListingReportSearchDto)
        {
            return await _demandLetterRepository.GetDefaultListingReportData(defaulterListingReportSearchDto);
        }

        public async Task<bool> Create(Demandletters demandletter)
        {
            demandletter.CreatedBy = 1;
            demandletter.CreatedDate = DateTime.Now;
            demandletter.IsActive = 1;


            _demandLetterRepository.Add(demandletter);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Demandletters> FetchSingleResult(int id)
        {
            var result = await _demandLetterRepository.FindBy(a => a.Id == id);
            Demandletters model = result.FirstOrDefault();
            return model;
        }




        public async Task<bool> Update(int id, Demandletters demandletter)
        {
            var result = await _demandLetterRepository.FindBy(a => a.Id == id);
            Demandletters model = result.FirstOrDefault();
            model.FileNo = demandletter.FileNo;
            model.Name = demandletter.Name;
            model.FatherName = demandletter.FatherName;
            model.Address = demandletter.Address;
            model.GenerateDate = DateTime.Now;
            model.UndersignedDate = demandletter.UndersignedDate;
            model.UndersignedTime = demandletter.UndersignedTime;
            model.DepositDue = demandletter.DepositDue;
            model.UptoDate = demandletter.UptoDate;
            model.DemandPeriodFromDate = demandletter.DemandPeriodFromDate;
            model.DemandPeriodToDate = demandletter.DemandPeriodToDate;
            model.LocalityId = demandletter.LocalityId;
            model.PropertyNo = demandletter.PropertyNo;
            model.DamageCharges = demandletter.DamageCharges;
            model.Penalty = demandletter.Penalty;
            model.InterestAmount = demandletter.InterestAmount;
            model.ReliefAmount = demandletter.ReliefAmount;
            model.ModifiedBy = 1;
            _demandLetterRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<List<Demandletters>> BindPropertyNoList()
        {

            return await _demandLetterRepository.BindPropertyNoList();

        }

        public async Task<PagedResult<Demandletters>> GetPagedDemandletterReport(DemandletterreportSearchDto model)
        {
            return await _demandLetterRepository.GetPagedDemandletterReport(model);
        }


        public async Task<List<Demandletters>> GetDemandLetterReportList(DownloadDemandLetterReportDto report)
        {
            return await _demandLetterRepository.GetDemandLetterReportList(report);
        }

        /*-----------------Relief Report Start------------------*/
        public async Task<PagedResult<Demandletters>> GetPagedReliefReport(ReliefReportSearchDto model)
        {
            return await _demandLetterRepository.GetPagedReliefReport(model);
        }

        public async Task<List<Demandletters>> BindFileNoList()
        {
            return await _demandLetterRepository.BindFileNoList();
        }

        public async Task<List<Locality>> BindLoclityList()
        {
            return await _demandLetterRepository.BindLoclityList();
        }
        /*-----------------Relief Report End------------------*/

        //*******   Penalty Imposition Report**********

        public async Task<List<Locality>> GetLocalityList()
        {
            List<Locality> localityList = await _demandLetterRepository.GetLocalityList();
            return localityList;
        }
        public async Task<List<PropertyType>> GetPropertyType()
        {
            return await _demandLetterRepository.GetPropertyType();
        }
        public async Task<List<Demandletters>> GetFileNoList()
        {
            List<Demandletters> fileNoList = await _demandLetterRepository.GetFileNoList();
            return fileNoList;
        }

        public async Task<PagedResult<Demandletters>> GetPagedPenaltyImpositionReport(PenaltyImpositionReportSearchDto model)
        {
            return await _demandLetterRepository.GetPagedPenaltyImpositionReport(model);
        }
        public async Task<PagedResult<Demandletters>> GetPagedImpositionReportOfCharges(ImpositionOfChargesSearchDto model)
        {
            return await _demandLetterRepository.GetPagedImpositionReportOfCharges(model);
        }
        /*-----------------Demand Collection Ledger Report Start------------------*/
        public async Task<PagedResult<Demandletters>> GetPagedDemandCollectionLedgerReport(DemandCollectionLedgerSearchDto model)
        {
            return await _demandLetterRepository.GetPagedDemandCollectionLedgerReport(model);
        }

        public async Task<List<DemandCollectionLedgerListDataDto>> GetPagedDemandCollectionLedgerReport1(DemandCollectionLedgerSearchDto model)
        {
            return await _demandLetterRepository.GetPagedDemandCollectionLedgerReport1(model);
        }
        /*-----------------Demand Collection Ledger Report Start------------------*/

        public async Task<List<DuesVsPaidAmountDto>> GetDuesVsPaidAmountListDto(DuesVsPaidAmountSearchDto model)
        {
            return await _demandLetterRepository.GetDuesVsPaidAmountListDto(model);
        }

        public async Task<List<FileNODto>> GetFileAutoCompleteDetails(string prefix)
        {
            var fileno = await _demandLetterRepository.GetFileAutoCompleteDetails(prefix);
            List<FileNODto> ds = new List<FileNODto>();

            foreach (var item in fileno)
            {
                FileNODto files = new FileNODto();
                files.FileNo = item.FileNo;
                files.Id = item.Id;
                ds.Add(files);
            }
            
            return ds;
          
        }
        public async Task<DemandAutoFillDto> GetFileNODetail(int fileid)
        {
            var fileno = await _demandLetterRepository.GetFileDetails(fileid);
            DemandAutoFillDto dto = new DemandAutoFillDto();
            dto.propertyNo = fileno.PropertyNo;
            dto.plotAreaSqYard = Convert.ToString(fileno.PlotAreaSqYard);
            dto.payeeName = fileno.Damagepayeepersonelinfo.Select(x => x.Name).FirstOrDefault();
            dto.fatherName = fileno.Damagepayeepersonelinfo.Select(x => x.FatherName).FirstOrDefault();
            dto.address = fileno.Damagepayeepersonelinfo.Select(x => x.Address).FirstOrDefault(); 

            return dto;

        }
        public async Task<Encrochmenttype> FetchResultEncroachmentType(DateTime date1)
        {
            return await _demandLetterRepository.FetchResultEncroachmentType(date1);
        }
        public async Task<List<Resratelisttypea>> RateListTypeA(DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _demandLetterRepository.RateListTypeA(date1, localityId, subEncroachersId);
        }
        public async Task<List<Resratelisttypeb>> RateListTypeB(DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _demandLetterRepository.RateListTypeB(date1, localityId, subEncroachersId);
        }
        public async Task<List<Resratelisttypec>> RateListTypeC(DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _demandLetterRepository.RateListTypeC(date1, localityId, subEncroachersId);
        }

        public async Task<List<Resratelisttypeb>> RateListTypeBSpecific(DateTime dateTimeSpecific, DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _demandLetterRepository.RateListTypeBSpecific(dateTimeSpecific, date1, localityId, subEncroachersId);
        }
        public async Task<List<Resratelisttypea>> RateListTypeASpecific(DateTime specificDateTime, DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _demandLetterRepository.RateListTypeASpecific(specificDateTime, date1, localityId, subEncroachersId);
        }
        public async Task<Comencrochmenttype> FetchResultCOMEncroachmentType(DateTime date1)
        {
            return await _demandLetterRepository.FetchResultCOMEncroachmentType(date1);
        }
        public async Task<List<Comratelisttypea>> ComRateListTypeA(DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _demandLetterRepository.ComRateListTypeA(date1, localityId, subEncroachersId);
        }
        public async Task<List<Comratelisttypeb>> ComRateListTypeB(DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _demandLetterRepository.ComRateListTypeB(date1, localityId, subEncroachersId);
        }
        public async Task<List<Comratelisttypec>> ComRateListTypeC(DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _demandLetterRepository.ComRateListTypeC(date1, localityId, subEncroachersId);
        }
        public async Task<List<Comratelisttypeb>> ComRateListTypeBSpecific(DateTime dateTimeSpecific, DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _demandLetterRepository.ComRateListTypeBSpecific(dateTimeSpecific, date1, localityId, subEncroachersId);
        }

        public async Task<List<Comratelisttypea>> ComRateListTypeASpecific(DateTime specificDateTime, DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _demandLetterRepository.ComRateListTypeASpecific(specificDateTime, date1, localityId, subEncroachersId);
        }
    }
}
