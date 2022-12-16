using Dto.Search;
using Libraries.Model.Common;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IDemolitionstructuredetailsRepository :IGenericRepository<Demolitionstructuredetails>
    {
        Task<List<Demolitionstructuredetails>> GetAllDemolitionstructuredetails();
        Task<List<Zone>> GetAllZone(int departmentId);
        Task<List<Demolitionstructure>> GetDemolitionstructure(int demostructuredId);
        //Task<Demolitionstructureafterdemolitionphotofiledetails> GetDemolitionstructureafterdemolitionphotofiledetails(int Id);
        //Task<Demolitionstructurebeforedemolitionphotofiledetails> GetDemolitionstructurebeforedemolitionphotofiledetails(int Id);

        Task<Demolitionstructureafterdemolitionphotofiledetails> GetAfterphotofile(int Id);
       
        Task<Demolitionstructurebeforedemolitionphotofiledetails> GetBeforephotofile(int Id);
       
        Task<bool> SaveDemolitionstructure(Demolitionstructure demolitionstructure);
        Task<bool> DeleteDemolitionstructure(int Id);

        Task<bool> SaveDemolitionstructureafterdemolitionphotofiledetails(Demolitionstructureafterdemolitionphotofiledetails demolitionstructureafterdemolitionphotofiledetails);
        Task<bool> DeleteDemolitionstructureafterdemolitionphotofiledetails(int Id);
        Task<bool> SaveDemolitionstructurebeforedemolitionphotofiledetails(Demolitionstructurebeforedemolitionphotofiledetails demolitionstructurebeforedemolitionphotofiledetails);
        Task<bool> DeleteDemolitionstructurebeforedemolitionphotofiledetails(int Id);

        Task<Demolitionstructuredetails> FetchSingleResult(int id);

        Task<List<Division>> GetAllDivision(int zoneId);
        Task<List<Department>> GetAllDepartment();
        Task<List<Demolitionstructuredetails>> GetAllDemolitionstructuredetailsList();
        Task<List<Fixingdemolition>> GetAllDemolitionstructuredetailsList1();
        Task<PagedResult<Demolitionstructuredetails>> GetPagedDemolitionstructuredetails(DemolitionstructuredetailsDto model);
        Task<List<Demolitionstructuredetails>> GetPagedDemolitionstructuredetailsList(DemolitionstructuredetailsDto model);
        Task<List<Locality>> GetAllLocalityList(int localityId);
        Task<List<Demolitionstructure>> GetStructure();
        Task<List<Structure>> GetMasterStructure();
        Task<List<Demolitionstructuredetails>> GetDemolitionReportDataDepartmentZoneWise(int department, int zone, int division, int locality);
        Task<PagedResult<Demolitionstructuredetails>> GetPagedDemolitionReportDataDepartmentZoneWise(DemolitionReportZoneDivisionLocalityWiseSearchDto dto);

        //added by ishu 11june2021
       
        Task<bool> SaveDemolishedstructurerpt(Demolishedstructurerpt rpt);
        Task<bool> SaveAreareclaimedrpt(Areareclaimedrpt rpt);
        Task<List<Demolishedstructurerpt>> GetAlldemolitionrptdetails(int id);
        Task<bool> Deletedemolitionrptdetails(int Id);
        Task<List<Areareclaimedrpt>> GetAllArearptdetails(int id);
        Task<bool> Deletedearearptdetails(int Id);
        //added by ishu 17 june 2021
        Task<PagedResult<Fixingdemolition>> GetPagedDemolitiondiary(DemolitionstructuredetailsDto1 model, int userId, int approved, int zoneId, int deprtId , int roleId);
        Task<Demolitionstructuredetails> FetchSingleResultonId(int id);
        Task<Fixingdemolition> FetchSingleResultOfFixingDemolition(int id); 
        Task<List<DemolitionDashboardDto>> GetDashboardData(int userId, int roleId);
        Task<PagedResult<Fixingdemolition>> GetDashboardListData(DemolitionDasboardDataDto model);
        Task<List<Fixingdemolition>> DownloadDasboarddata(string filter, int Userid);
        Task<string> Getusername(int Userid);
        Task<List<Demolitionstructuredetails>> GetAllDemolitionReport(DemolitionReportZoneDivisionLocalityWiseSearchDto dto);



    }
}