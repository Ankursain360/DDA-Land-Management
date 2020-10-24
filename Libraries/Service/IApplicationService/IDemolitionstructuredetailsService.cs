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
        Task<List<Division>> GetAllDivisionList(int zone);
        Task<List<Demolitionstructuredetails>> GetAllDemolitionstructuredetails();

        Task<bool> SaveDemolitionstructure(Demolitionstructure demolitionstructure);
        Task<bool> DeleteDemolitionstructure(int Id);
        
       
        Task<bool> SaveDemolitionstructureafterdemolitionphotofiledetails(Demolitionstructureafterdemolitionphotofiledetails demolitionstructureafterdemolitionphotofiledetails);
        Task<bool> DeleteDemolitionstructureafterdemolitionphotofiledetails(int Id);
        Task<bool> SaveDemolitionstructurebeforedemolitionphotofiledetails(Demolitionstructurebeforedemolitionphotofiledetails demolitionstructurebeforedemolitionphotofiledetails);
        Task<bool> DeleteDemolitionstructurebeforedemolitionphotofiledetails(int Id);
        Task<List<Demolitionstructure>> GetDemolitionstructure(int demostructuredId);
        Task<Demolitionstructureafterdemolitionphotofiledetails> GetDemolitionstructureafterdemolitionphotofiledetails(int Id);
        Task<Demolitionstructurebeforedemolitionphotofiledetails> GetDemolitionstructurebeforedemolitionphotofiledetails(int Id);
        Task<List<Structure>> GetStructure();

        //Task<PagedResult<EncroachmentRegisteration>> GetEncroachmentReportData(EnchroachmentSearchDto enchroachmentSearchDto);
        //Task<List<EncroachmentRegisteration>> GetEncroachmentRegisterationReportData(int department, int zone, int division, int locality, DateTime fromdate, DateTime todate);

    }
}
