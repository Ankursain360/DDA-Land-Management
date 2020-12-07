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
        Task<Demolitionstructureafterdemolitionphotofiledetails> GetDemolitionstructureafterdemolitionphotofiledetails(int Id);
        Task<Demolitionstructurebeforedemolitionphotofiledetails> GetDemolitionstructurebeforedemolitionphotofiledetails(int Id);


        Task<bool> SaveDemolitionstructure(Demolitionstructure demolitionstructure);
        Task<bool> DeleteDemolitionstructure(int Id);

        Task<bool> SaveDemolitionstructureafterdemolitionphotofiledetails(Demolitionstructureafterdemolitionphotofiledetails demolitionstructureafterdemolitionphotofiledetails);
        Task<bool> DeleteDemolitionstructureafterdemolitionphotofiledetails(int Id);
        Task<bool> SaveDemolitionstructurebeforedemolitionphotofiledetails(Demolitionstructurebeforedemolitionphotofiledetails demolitionstructurebeforedemolitionphotofiledetails);
        Task<bool> DeleteDemolitionstructurebeforedemolitionphotofiledetails(int Id);

        Task<Demolitionstructuredetails> FetchSingleResult(int id);

        Task<List<Division>> GetAllDivision(int zoneId);
        Task<List<Department>> GetAllDepartment();
        Task<PagedResult<Demolitionstructuredetails>> GetPagedDemolitionstructuredetails(DemolitionstructuredetailsDto model);
        Task<List<Demolitionstructuredetails>> GetPagedDemolitionstructuredetailsList(DemolitionstructuredetailsDto model);
        Task<List<Locality>> GetAllLocalityList(int localityId);
        Task<List<Demolitionstructure>> GetStructure();
        Task<List<Structure>> GetMasterStructure();
        Task<List<Demolitionstructuredetails>> GetDemolitionReportDataDepartmentZoneWise(int department, int zone, int division, int locality);
        Task<PagedResult<Demolitionstructuredetails>> GetPagedDemolitionReportDataDepartmentZoneWise(DemolitionReportZoneDivisionLocalityWiseSearchDto dto);
    }
}