using Dto.Search;
using Libraries.Model.Common;
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
    public class DemolitionstructuredetailsService : EntityService<Demolitionstructuredetails>, IDemolitionstructuredetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDemolitionstructuredetailsRepository _demolitionstructuredetailsRepository;
        public DemolitionstructuredetailsService(IUnitOfWork unitOfWork, IDemolitionstructuredetailsRepository demolitionstructuredetailsRepository)
      : base(unitOfWork, demolitionstructuredetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _demolitionstructuredetailsRepository = demolitionstructuredetailsRepository;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _demolitionstructuredetailsRepository.FindBy(a => a.Id == id);
            Demolitionstructuredetails model = form.FirstOrDefault();
            model.IsActive = 0;
            _demolitionstructuredetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Demolitionstructuredetails> FetchSingleResult(int id)
        {
            return await _demolitionstructuredetailsRepository.FetchSingleResult(id);
        }

        public async Task<List<Department>> GetAllDepartment()
        {
            return await _demolitionstructuredetailsRepository.GetAllDepartment();
        }
        public async Task<List<Division>> GetAllDivisionList(int zone)
        {
            return await _demolitionstructuredetailsRepository.GetAllDivision(zone);
        }
        public async Task<List<Demolitionstructuredetails>> GetAllDemolitionstructuredetails()
        {
            return await _demolitionstructuredetailsRepository.GetAllDemolitionstructuredetails();
        }
        public async Task<List<Demolitionstructuredetails>> GetAllDemolitionstructuredetailsList()
        {
            return await _demolitionstructuredetailsRepository.GetAllDemolitionstructuredetailsList();
        }
        public async Task<List<Fixingdemolition>> GetAllDemolitionstructuredetailsList1()
        {
            return await _demolitionstructuredetailsRepository.GetAllDemolitionstructuredetailsList1();
        }
        public async Task<List<Locality>> GetAllLocalityList(int divisionId)
        {
            return await _demolitionstructuredetailsRepository.GetAllLocalityList(divisionId);
        }

        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            return await _demolitionstructuredetailsRepository.GetAllZone(departmentId);
        }
        public async Task<PagedResult<Demolitionstructuredetails>> GetPagedDemolitionstructuredetails(DemolitionstructuredetailsDto model)
        {
            return await _demolitionstructuredetailsRepository.GetPagedDemolitionstructuredetails(model);
        }
        public async Task<List<Demolitionstructuredetails>> GetPagedDemolitionstructuredetailsList(DemolitionstructuredetailsDto model)
        {
            return await _demolitionstructuredetailsRepository.GetPagedDemolitionstructuredetailsList(model);
        }
        public async Task<PagedResult<Demolitionstructuredetails>> GetPagedDemolitionReportDataDepartmentZoneWise(DemolitionReportZoneDivisionLocalityWiseSearchDto dto)
        {
            return await _demolitionstructuredetailsRepository.GetPagedDemolitionReportDataDepartmentZoneWise(dto);
        }
        public async Task<List<Demolitionstructuredetails>> GetDemolitionReportDataDepartmentZoneWise(int department, int zone, int division, int locality)
        {
            return await _demolitionstructuredetailsRepository.GetDemolitionReportDataDepartmentZoneWise(department, zone, division, locality);
        }
        public async Task<bool> Update(int id, Demolitionstructuredetails demolitionstructuredetails)
        {
            var result = await _demolitionstructuredetailsRepository.FindBy(a => a.Id == id);
            Demolitionstructuredetails model = result.FirstOrDefault();
            model.DepartmentId = demolitionstructuredetails.DepartmentId;
            model.ZoneId = demolitionstructuredetails.ZoneId;
            model.DivisionId = demolitionstructuredetails.DivisionId;
            model.LocalityId = demolitionstructuredetails.LocalityId;
            model.FileNo = demolitionstructuredetails.FileNo;
            model.Date = demolitionstructuredetails.Date;
            model.Area = demolitionstructuredetails.Area;
            model.PoliceStation = demolitionstructuredetails.PoliceStation;
            model.PoliceStation = demolitionstructuredetails.PoliceStation;
            model.NameOfAreaSite = demolitionstructuredetails.NameOfAreaSite;
            model.EncroachmentSinceDate = demolitionstructuredetails.EncroachmentSinceDate;
            model.DateOfApprovalDemolition = demolitionstructuredetails.DateOfApprovalDemolition;
            model.NameOfEncroacherIfAny = demolitionstructuredetails.NameOfEncroacherIfAny;
            model.Remarks = demolitionstructuredetails.Remarks;
            model.StartOfDemolitionActionDate = demolitionstructuredetails.StartOfDemolitionActionDate;
            model.EndOfDemolitionActionDate = demolitionstructuredetails.EndOfDemolitionActionDate;
            model.AreaReclaimed = demolitionstructuredetails.AreaReclaimed;
            model.DemilitionReportPath = demolitionstructuredetails.DemilitionReportPath;
            model.DemolitionStatus = demolitionstructuredetails.DemolitionStatus;
            model.DemolitionRemarks = demolitionstructuredetails.DemolitionRemarks;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = demolitionstructuredetails.ModifiedBy;
            model.IsActive = 1;
            _demolitionstructuredetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> Create(Demolitionstructuredetails demolitionstructuredetails)
        {
            demolitionstructuredetails.IsActive = 1;
            demolitionstructuredetails.CreatedDate = DateTime.Now;
            _demolitionstructuredetailsRepository.Add(demolitionstructuredetails);
            return await _unitOfWork.CommitAsync() > 0;
        }
        //public async Task<List<Khasra>> GetAllKhasraList(int localityId)
        //{
        //    return await _encroachmentRegisterationRepository.GetAllKhasraList(localityId);
        //}
        public async Task<bool> SaveDemolitionstructure(Demolitionstructure demolitionstructure)
        {
            demolitionstructure.CreatedBy = 1;
            demolitionstructure.CreatedDate = DateTime.Now;
            demolitionstructure.IsActive = 1;
            return await _demolitionstructuredetailsRepository.SaveDemolitionstructure(demolitionstructure);
        }
        public async Task<bool> DeleteDemolitionstructure(int Id)
        {
            return await _demolitionstructuredetailsRepository.DeleteDemolitionstructure(Id);
        }


        public async Task<bool> SaveDemolitionstructureafterdemolitionphotofiledetails(Demolitionstructureafterdemolitionphotofiledetails demolitionstructureafterdemolitionphotofiledetails)
        {
            demolitionstructureafterdemolitionphotofiledetails.CreatedBy = 1;
            demolitionstructureafterdemolitionphotofiledetails.CreatedDate = DateTime.Now;
            demolitionstructureafterdemolitionphotofiledetails.IsActive = 1;
            return await _demolitionstructuredetailsRepository.SaveDemolitionstructureafterdemolitionphotofiledetails(demolitionstructureafterdemolitionphotofiledetails);
        }

        public async Task<bool> DeleteDemolitionstructureafterdemolitionphotofiledetails(int Id)
        {
            return await _demolitionstructuredetailsRepository.DeleteDemolitionstructureafterdemolitionphotofiledetails(Id);
        }
        public async Task<bool> SaveDemolitionstructurebeforedemolitionphotofiledetails(Demolitionstructurebeforedemolitionphotofiledetails demolitionstructurebeforedemolitionphotofiledetails)
        {
            demolitionstructurebeforedemolitionphotofiledetails.CreatedBy = 1;
            demolitionstructurebeforedemolitionphotofiledetails.CreatedDate = DateTime.Now;
            demolitionstructurebeforedemolitionphotofiledetails.IsActive = 1;
            return await _demolitionstructuredetailsRepository.SaveDemolitionstructurebeforedemolitionphotofiledetails(demolitionstructurebeforedemolitionphotofiledetails);
        }

        public async Task<bool> DeleteDemolitionstructurebeforedemolitionphotofiledetails(int Id)
        {
            return await _demolitionstructuredetailsRepository.DeleteDemolitionstructurebeforedemolitionphotofiledetails(Id);
        }



        public async Task<List<Demolitionstructure>> GetDemolitionstructure(int Id)
        {
            return await _demolitionstructuredetailsRepository.GetDemolitionstructure(Id);
        }



        public async Task<Demolitionstructureafterdemolitionphotofiledetails> GetAfterphotofile(int Id)
        {
            return await _demolitionstructuredetailsRepository.GetAfterphotofile(Id);
        }

        public async Task<Demolitionstructurebeforedemolitionphotofiledetails> GetBeforephotofile(int Id)
        {
            return await _demolitionstructuredetailsRepository.GetBeforephotofile(Id);
        }

        public async Task<List<Demolitionstructure>> GetStructure()
        {
            return await _demolitionstructuredetailsRepository.GetStructure();
        }
        public async Task<List<Structure>> GetMasterStructure()
        {
            return await _demolitionstructuredetailsRepository.GetMasterStructure();
        }

        //********* rpt  Details **********added by ishu



        public async Task<bool> SaveDemolishedstructurerpt(Demolishedstructurerpt rpt)
        {
            rpt.CreatedBy = rpt.CreatedBy;
            rpt.CreatedDate = DateTime.Now;
            rpt.IsActive = 1;
            return await _demolitionstructuredetailsRepository.SaveDemolishedstructurerpt(rpt);
        }
        public async Task<bool> SaveAreareclaimedrpt(Areareclaimedrpt rpt)
        {
            rpt.CreatedBy = rpt.CreatedBy;
            rpt.CreatedDate = DateTime.Now;
            rpt.IsActive = 1;
            return await _demolitionstructuredetailsRepository.SaveAreareclaimedrpt(rpt);
        }

        public async Task<List<Demolishedstructurerpt>> GetAlldemolitionrptdetails(int id)
        {
            return await _demolitionstructuredetailsRepository.GetAlldemolitionrptdetails(id);
        }
        public async Task<bool> Deletedemolitionrptdetails(int Id)
        {
            return await _demolitionstructuredetailsRepository.Deletedemolitionrptdetails(Id);
        }
        public async Task<List<Areareclaimedrpt>> GetAllArearptdetails(int id)
        {
            return await _demolitionstructuredetailsRepository.GetAllArearptdetails(id);
        }
        public async Task<bool> Deletedearearptdetails(int Id)
        {
            return await _demolitionstructuredetailsRepository.Deletedearearptdetails(Id);
        }

        //added by ishu 17/6/2021
        public async Task<PagedResult<Fixingdemolition>> GetPagedDemolitiondiary(DemolitionstructuredetailsDto1 model, int userId, int approved, int zoneId, int deprtId,int roleId)
        {
            return await _demolitionstructuredetailsRepository.GetPagedDemolitiondiary(model, userId, approved,zoneId,deprtId,roleId);
        }
        public async Task<Demolitionstructuredetails> FetchSingleResultonId(int id)
        {
            return await _demolitionstructuredetailsRepository.FetchSingleResultonId(id);
        }
        public async Task<Fixingdemolition> FetchSingleResultOfFixingDemolition(int id)
        {
            return await _demolitionstructuredetailsRepository.FetchSingleResultOfFixingDemolition(id);
        }
        public async Task<List<DemolitionDashboardDto>> GetDashboardData(int userId, int roleId , int DeptId , int ZoneId)
        {
            return await _demolitionstructuredetailsRepository.GetDashboardData(userId, roleId, DeptId, ZoneId);
        }
        public async Task<List<EncroachmentRegisterDashboardDto>> GetEncroachmentRegistersDashboardData(int userId, int roleId, int DeptId, int ZoneId)
        {
            return await _demolitionstructuredetailsRepository.GetEncroachmentRegistersDashboardData(userId, roleId, DeptId, ZoneId);
        }
        public async Task<PagedResult<Fixingdemolition>> GetDashboardListData(DemolitionDasboardDataDto model , int DeptId, int ZoneId,int roleId)
        {
            return await _demolitionstructuredetailsRepository.GetDashboardListData(model,DeptId,ZoneId,roleId);
        }
        public async Task<PagedResult<EncroachmentRegisteration>> GetAllEncroachmentRagistrationDashboardListData(DemolitionDasboardDataDto model, int DeptId, int ZoneId, int roleId)
        {
            return await _demolitionstructuredetailsRepository.GetAllEncroachmentRagistrationDashboardListData(model, DeptId, ZoneId, roleId);
        }

        public async Task<List<Fixingdemolition>> DownloadDasboarddata(string filter, int Userid)
        {
            return await _demolitionstructuredetailsRepository.DownloadDasboarddata(filter, Userid);
        }
        public async Task<List<EncroachmentRegisteration>> DownloadEncroachmentDashboard(string filter, int Userid)
        {
            return await _demolitionstructuredetailsRepository.DownloadEncroachmentDashboard(filter, Userid);
        }
        public async Task<string> Getusername(string Userid)
        {
            return await _demolitionstructuredetailsRepository.Getusername(Userid);
        }
        public async Task<List<Demolitionstructuredetails>> GetAllDemolitionReport(DemolitionReportZoneDivisionLocalityWiseSearchDto model)
        {
            return await _demolitionstructuredetailsRepository.GetAllDemolitionReport(model);
        }
        
    }

}
