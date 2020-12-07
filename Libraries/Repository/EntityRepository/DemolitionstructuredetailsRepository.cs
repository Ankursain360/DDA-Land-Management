using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class DemolitionStructureDetailsRepository : GenericRepository<Demolitionstructuredetails>, IDemolitionstructuredetailsRepository
    {
        public DemolitionStructureDetailsRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<bool> DeleteDemolitionstructure(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Demolitionstructure.Where(x => x.DemolitionStructureDetailsId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        

        public async Task<bool> DeleteDemolitionstructureafterdemolitionphotofiledetails(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Demolitionstructureafterdemolitionphotofiledetails.Where(x => x.DemolitionStructureDetailsId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<bool> DeleteDemolitionstructurebeforedemolitionphotofiledetails(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Demolitionstructurebeforedemolitionphotofiledetails.Where(x => x.DemolitionStructureId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<Demolitionstructuredetails> FetchSingleResult(int id)
        {
            return await _dbContext.Demolitionstructuredetails
                            .Include(x => x.Demolitionstructureafterdemolitionphotofiledetails)
                            .Include(x => x.Demolitionstructurebeforedemolitionphotofiledetails)

                            .Where(x => x.Id == id)
                            .FirstOrDefaultAsync();
        }

        public async Task<List<Department>> GetAllDepartment()
        {
            return await _dbContext.Department.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<List<Division>> GetAllDivision(int zoneId)
        {
            return await _dbContext.Division.Where(x => x.ZoneId == zoneId && x.IsActive == 1).ToListAsync();
        }

        public async Task<List<Demolitionstructuredetails>> GetAllDemolitionstructuredetails()
        {
            return await _dbContext.Demolitionstructuredetails.Where(x => x.IsActive == 1).ToListAsync();
        }



        public async Task<List<Locality>> GetAllLocalityList(int divisionId)
        {
            return await _dbContext.Locality.Where(x => x.DivisionId == divisionId && x.IsActive == 1).ToListAsync();
        }

        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            return await _dbContext.Zone.Where(x => x.DepartmentId == departmentId && x.IsActive == 1).ToListAsync();
        }

        //public async Task<List<Demolitionstructure>> GetDemolitionstructure(int demostructuredId)
        //{
        //    return await _dbContext.Demolitionstructure.Where(x => x.DemolitionStructureDetailsId == demostructuredId && x.IsActive == 1).ToListAsync();
        //}





        public async Task<Demolitionstructureafterdemolitionphotofiledetails> GetDemolitionstructureafterdemolitionphotofiledetails(int Id)
        {
            return await _dbContext.Demolitionstructureafterdemolitionphotofiledetails.Where(x => x.Id == Id && x.IsActive == 1).FirstOrDefaultAsync();
        }
        public async Task<Demolitionstructureafterdemolitionphotofiledetails> GetDemolitionstructurebeforedemolitionphotofiledetails(int Id)
        {
            return await _dbContext.Demolitionstructureafterdemolitionphotofiledetails.Where(x => x.Id == Id && x.IsActive == 1).FirstOrDefaultAsync();
        }

        public async Task<PagedResult<Demolitionstructuredetails>> GetPagedDemolitionstructuredetails(DemolitionstructuredetailsDto model)
        {
            return await _dbContext.Demolitionstructuredetails.Include(x => new { x.Locality, x.Zone, x.Department, x.Division }).Where(x => x.IsActive == 1).GetPaged(model.PageNumber, model.PageSize);
        }
        public async Task<List<Demolitionstructuredetails>> GetPagedDemolitionstructuredetailsList(DemolitionstructuredetailsDto model)
        {
            try
            {
                return await _dbContext.Demolitionstructuredetails.Include(x => x.Department).Include(x => x.Zone).Include(x => x.Division).Include(x => x.Locality).Where(x => x.IsActive == 1).ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> SaveDemolitionstructure(Demolitionstructure demolitionstructure)
        {
            _dbContext.Demolitionstructure.Add(demolitionstructure);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }





        public async Task<bool> SaveDemolitionstructureafterdemolitionphotofiledetails(Demolitionstructureafterdemolitionphotofiledetails demolitionstructureafterdemolitionphotofiledetails)
        {
            _dbContext.Demolitionstructureafterdemolitionphotofiledetails.Add(demolitionstructureafterdemolitionphotofiledetails);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }


        public async Task<bool> SaveDemolitionstructurebeforedemolitionphotofiledetails(Demolitionstructurebeforedemolitionphotofiledetails demolitionstructurebeforedemolitionphotofiledetails)
        {
            _dbContext.Demolitionstructurebeforedemolitionphotofiledetails.Add(demolitionstructurebeforedemolitionphotofiledetails);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        Task<Demolitionstructurebeforedemolitionphotofiledetails> IDemolitionstructuredetailsRepository.GetDemolitionstructurebeforedemolitionphotofiledetails(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Demolitionstructure>> GetStructure()
        {
            return await _dbContext.Demolitionstructure.Include(x=>x.Structure).Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Demolitionstructure>> GetDemolitionstructure(int demostructuredId)
        {
            return await _dbContext.Demolitionstructure.Where(x => x.DemolitionStructureDetailsId == demostructuredId && x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Structure>> GetMasterStructure()
        {
            return await _dbContext.Structure.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<PagedResult<Demolitionstructuredetails>> GetPagedDemolitionReportDataDepartmentZoneWise(DemolitionReportZoneDivisionLocalityWiseSearchDto dto)//added by shalini
        {
            var data = await _dbContext.Demolitionstructuredetails
                .Include(x => x.Locality)
                .Include(x => x.Department)
                .Include(x => x.Zone)
                .Include(x => x.Division)
                .Where(x => (x.DepartmentId == (dto.departmentId == 0 ? x.DepartmentId : dto.departmentId))
                && (x.ZoneId == (dto.zoneId == 0 ? x.ZoneId : dto.zoneId))
                && (x.DivisionId == (dto.divisionId == 0 ? x.DivisionId : dto.divisionId))
                && (x.LocalityId == (dto.localityId == 0 ? x.LocalityId : dto.localityId)) && (x.IsActive == 1)).OrderByDescending(x => x.Id).GetPaged(dto.PageNumber, dto.PageSize);
            return data;
        }

        public async Task<List<Demolitionstructuredetails>> GetDemolitionReportDataDepartmentZoneWise(int department, int zone, int division, int locality)
        {
            var data = await _dbContext.Demolitionstructuredetails
                .Include(x => x.Locality)
                .Include(x => x.Department)
                .Include(x => x.Zone)
                .Include(x => x.Division)
                .Where(x => (x.DepartmentId == (department == 0 ? x.DepartmentId : department))
                && (x.ZoneId == (zone == 0 ? x.ZoneId : zone))
                && (x.DivisionId == (division == 0 ? x.DivisionId : division))
                && (x.LocalityId == (locality == 0 ? x.LocalityId : locality))
                && (x.IsActive == 1))
                .OrderByDescending(x => x.Id).ToListAsync();
            return data;
        }
    }
}
