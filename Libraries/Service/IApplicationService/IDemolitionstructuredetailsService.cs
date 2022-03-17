using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IDemolitionstructuredetailsService : IEntityService<Demolitionstructuredetails>
    {
        Task<List<Zone>> GetAllZone(int departmentId); // To Get all data 
        Task<List<Department>> GetAllDepartment(); // To Get all data 
        Task<bool> Update(int id, Demolitionstructuredetails demolitionstructuredetails); // To Upadte Particular data 
        Task<bool> Create(Demolitionstructuredetails demolitionstructuredetails);
        Task<Demolitionstructuredetails> FetchSingleResult(int id);  // To fetch Particular data 
        Task<bool> Delete(int id);    //To Delete Data 
        Task<List<Locality>> GetAllLocalityList(int divisionId);
        Task<PagedResult<Demolitionstructuredetails>> GetPagedDemolitionstructuredetails(DemolitionstructuredetailsDto model);
        Task<List<Demolitionstructuredetails>> GetPagedDemolitionstructuredetailsList(DemolitionstructuredetailsDto model);
        Task<List<Division>> GetAllDivisionList(int zone);
        Task<List<Demolitionstructuredetails>> GetAllDemolitionstructuredetails();

        Task<bool> SaveDemolitionstructure(Demolitionstructure demolitionstructure);
        Task<bool> DeleteDemolitionstructure(int Id);


        Task<bool> SaveDemolitionstructureafterdemolitionphotofiledetails(Demolitionstructureafterdemolitionphotofiledetails demolitionstructureafterdemolitionphotofiledetails);
        Task<bool> DeleteDemolitionstructureafterdemolitionphotofiledetails(int Id);
        Task<bool> SaveDemolitionstructurebeforedemolitionphotofiledetails(Demolitionstructurebeforedemolitionphotofiledetails demolitionstructurebeforedemolitionphotofiledetails);
        Task<bool> DeleteDemolitionstructurebeforedemolitionphotofiledetails(int Id);
        Task<List<Demolitionstructure>> GetDemolitionstructure(int demostructuredId);
        Task<Demolitionstructureafterdemolitionphotofiledetails> GetAfterphotofile(int Id);

        Task<Demolitionstructurebeforedemolitionphotofiledetails> GetBeforephotofile(int Id);
        Task<List<Demolitionstructure>> GetStructure();
        Task<List<Structure>> GetMasterStructure();
        Task<List<Demolitionstructuredetails>> GetAllDemolitionstructuredetailsList();

        Task<PagedResult<Demolitionstructuredetails>> GetPagedDemolitionReportDataDepartmentZoneWise(DemolitionReportZoneDivisionLocalityWiseSearchDto demolitionReportZoneDivisionLocalityWiseSearchDto);
        Task<List<Demolitionstructuredetails>> GetDemolitionReportDataDepartmentZoneWise(int department, int zone, int division, int locality);

        //added by ishu 11june2021


        Task<bool> SaveDemolishedstructurerpt(Demolishedstructurerpt rpt);
        Task<bool> SaveAreareclaimedrpt(Areareclaimedrpt rpt);
        Task<List<Demolishedstructurerpt>> GetAlldemolitionrptdetails(int id);
        Task<bool> Deletedemolitionrptdetails(int Id);
        Task<List<Areareclaimedrpt>> GetAllArearptdetails(int id);
        Task<bool> Deletedearearptdetails(int Id);

        //added by ishu 17 june 2021
        Task<PagedResult<Fixingdemolition>> GetPagedDemolitiondiary(DemolitionstructuredetailsDto1 model, int userId, int approved);
        Task<Demolitionstructuredetails> FetchSingleResultonId(int id);
        Task<Fixingdemolition> FetchSingleResultOfFixingDemolition(int id);

        Task<List<DemolitionDashboardDto>> GetDashboardData(int userId, int roleId);

        Task<PagedResult<Fixingdemolition>> GetDashboardListData(DemolitionDasboardDataDto model);
        Task<List<Fixingdemolition>> DownloadDasboarddata(string filter, int Userid);
        Task<string> Getusername(int Userid);
        
    }
}
