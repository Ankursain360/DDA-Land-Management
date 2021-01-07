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
namespace Service.ApplicationService
{
  
    public class Legalmanagementsystemservice : EntityService<Legalmanagementsystem>, ILegalmanagementsystemservice
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILegalmanagementsystemRepository _legalmanagementsystemRepository;

        public Legalmanagementsystemservice(IUnitOfWork unitOfWork, ILegalmanagementsystemRepository legalmanagementsystemRepository)
        : base(unitOfWork, legalmanagementsystemRepository)
        {
            _unitOfWork = unitOfWork;
            _legalmanagementsystemRepository = legalmanagementsystemRepository;
        }

        // * *********** methods for legal report added by ishu ************
        public async Task<List<Zone>> GetZoneList()  
        {
            List<Zone> zoneList = await _legalmanagementsystemRepository.GetZoneList();
            return zoneList;
        }
        public async Task<List<Locality>> GetLocalityList(int zoneId)    
        {
            List<Locality> localityList = await _legalmanagementsystemRepository.GetLocalityList(zoneId);
            return localityList;
        }

       
        public async Task<List<Casestatus>> GetCasestatusList()
        {
            List<Casestatus> casestatusList = await _legalmanagementsystemRepository.GetCasestatusList();
            return casestatusList;
        }

        public async Task<List<Courttype>> GetCourttypeList()
        {
            List<Courttype> courttypeList = await _legalmanagementsystemRepository.GetCourttypeList();
            return courttypeList;
        }

        public async Task<List<Legalmanagementsystem>> GetFileNoList()   
        {
            List<Legalmanagementsystem> fileNoList = await _legalmanagementsystemRepository.GetFileNoList();
            return fileNoList;
        }
        public async Task<List<Legalmanagementsystem>> GetCourtCaseNoList(int filenoId)     
        {
            List<Legalmanagementsystem> caseNoList = await _legalmanagementsystemRepository.GetCourtCaseNoList(filenoId);
            return caseNoList;
        }
        public async Task<PagedResult<Legalmanagementsystem>> GetPagedLegalReport(LegalReportSearchDto model)
        {
            return await _legalmanagementsystemRepository.GetPagedLegalReport(model);
        }
        public async Task<PagedResult<Legalmanagementsystem>> GetLegalmanagementsystemReportData(HearingReportSearchDto hearingReportSearchDto)
        {
            return await _legalmanagementsystemRepository.GetLegalmanagementsystemReportData(hearingReportSearchDto);
        }
        public async Task<List<Legalmanagementsystem>> GetLegalmanagementsystemList()
        {
            List<Legalmanagementsystem> legalmanagementsytemlist = await _legalmanagementsystemRepository.GetLegalmanagementsystemList();
            return legalmanagementsytemlist;
        }
    }
}
