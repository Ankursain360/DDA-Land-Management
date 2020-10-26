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
            return await _dbContext.Demolitionstructuredetails.Include(x => x.Locality).Where(x => x.IsActive == 1).GetPaged(model.PageNumber, model.PageSize);
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

        public async Task<List<Structure>> GetStructure()
        {
            return await _dbContext.Structure.Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Demolitionstructure>> GetDemolitionstructure(int demostructuredId)
        {
            return await _dbContext.Demolitionstructure.Where(x => x.DemolitionStructureDetailsId == demostructuredId && x.IsActive == 1).ToListAsync();
        }
    }
}
