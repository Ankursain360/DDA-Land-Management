using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ubiety.Dns.Core.Records.NotUsed;

namespace Libraries.Repository.EntityRepository
{
    public class LocalityRepository:GenericRepository<Locality>,ILocalityRepository
    {
        public LocalityRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Locality>> GetPagedLocality(LocalitySearchDto model)
        {
            var data= await _dbContext.Locality
                        .Include(x => x.Zone)
                        .Include(x => x.Department)
                        .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                             .OrderByDescending(s => s.IsActive)
                            .ThenBy(s => s.Zone.Name)
                            .ThenBy(s => s.Division.Name)
                            .ThenBy(s => s.Name).GetPaged<Locality>(model.PageNumber, model.PageSize); ;
                      
            int SortOrder = (int)model.SortOrder;
            if (SortOrder==1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DEPARTMENT"):
                        data = null;
                        data = await _dbContext.Locality
                        .Include(x => x.Zone)
                        .Include(x => x.Department)
                        .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                            .OrderBy(x => x.Department.Name)
                            .GetPaged<Locality>(model.PageNumber, model.PageSize); ;
                        break;
                    case ("ZONE"):
                        data = null;
                        data = await _dbContext.Locality
                        .Include(x => x.Zone)
                        .Include(x => x.Department)
                        .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                            .OrderBy(x => x.Zone.Name)
                            .GetPaged<Locality>(model.PageNumber, model.PageSize); ;
                     
                        break;
                    case ("DIVISION"):
                        data = null;
                        data = await _dbContext.Locality
                        .Include(x => x.Zone)
                        .Include(x => x.Department)
                        .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                            .OrderBy(x => x.Division.Name)
                            .GetPaged<Locality>(model.PageNumber, model.PageSize); ;
                      
                        break;
                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Locality
                        .Include(x => x.Zone)
                        .Include(x => x.Department)
                        .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                            .OrderBy(x =>x.Name)
                            .GetPaged<Locality>(model.PageNumber, model.PageSize); ;

                       
                        break;
                    case ("LOCALITYCODE"):
                        data = null;
                        data = await _dbContext.Locality
                        .Include(x => x.Zone)
                        .Include(x => x.Department)
                        .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                            .OrderBy(x => x.LocalityCode)
                            .GetPaged<Locality>(model.PageNumber, model.PageSize); ;
                        break;
                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.Locality
                        .Include(x => x.Zone)
                        .Include(x => x.Department)
                        .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                            .OrderByDescending(x => x.IsActive)
                            .GetPaged<Locality>(model.PageNumber, model.PageSize); ;
                      
                        break;
                }
            }
            else if(SortOrder==2)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("DEPARTMENT"):
                        data = null;
                        data = await _dbContext.Locality
                        .Include(x => x.Zone)
                        .Include(x => x.Department)
                        .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                            .OrderByDescending(x => x.Department.Name)
                            .GetPaged<Locality>(model.PageNumber, model.PageSize); ;
                        break;
                    case ("ZONE"):
                        data = null;
                        data = await _dbContext.Locality
                        .Include(x => x.Zone)
                        .Include(x => x.Department)
                        .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                            .OrderByDescending(x => x.Zone.Name)
                            .GetPaged<Locality>(model.PageNumber, model.PageSize); ;

                        break;
                    case ("DIVISION"):
                        data = null;
                        data = await _dbContext.Locality
                        .Include(x => x.Zone)
                        .Include(x => x.Department)
                        .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                            .OrderByDescending(x => x.Division.Name)
                            .GetPaged<Locality>(model.PageNumber, model.PageSize); ;

                        break;
                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Locality
                        .Include(x => x.Zone)
                        .Include(x => x.Department)
                        .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                            .OrderByDescending(x => x.Name)
                            .GetPaged<Locality>(model.PageNumber, model.PageSize); ;


                        break;
                    case ("LOCALITYCODE"):
                        data = null;
                        data = await _dbContext.Locality
                        .Include(x => x.Zone)
                        .Include(x => x.Department)
                        .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                            .OrderByDescending(x => x.LocalityCode)
                            .GetPaged<Locality>(model.PageNumber, model.PageSize); ;
                        break;
                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.Locality
                        .Include(x => x.Zone)
                        .Include(x => x.Department)
                        .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                            .OrderBy(x => x.IsActive)
                            .GetPaged<Locality>(model.PageNumber, model.PageSize); ;

                        break;
                }
            }
            return data;
        }
        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            List<Zone> zoneList = await _dbContext.Zone.Where(x=>x.DepartmentId== departmentId && x.IsActive == 1).ToListAsync();
            return zoneList;
        }
        public async Task<List<Department>> GetAllDepartment()
        {
            List<Department> departmentList = await _dbContext.Department.Where(x => x.IsActive == 1).ToListAsync();
            return departmentList;
        }
        public async Task<bool> AnyName(int Id, string Name, int DepartmentId, int DivisionId, int ZoneId)
        {
            return await _dbContext.Locality.AnyAsync(t => t.Id != Id && t.DepartmentId == DepartmentId&& t.IsActive == 1 && t.DivisionId == DivisionId && t.ZoneId == ZoneId && t.Name.ToLower() == Name.ToLower());
        }
        public async Task<bool> AnyCode(int id, string code)
        {
            return await _dbContext.Locality.AnyAsync(t => t.Id != id && t.LocalityCode.ToLower() == code.ToLower());
        }

        public async Task<List<Locality>> GetAllLocality()
        {
            var data = await _dbContext.Locality.Include(x => x.Zone).Include(x => x.Department).OrderByDescending(x => x.Id).ToListAsync();
            return data;
        }

        public async Task<List<Division>> GetAllDivisionList(int zone)
        {
            var data = await _dbContext.Division.Where(x => x.ZoneId== zone && x.IsActive == 1).ToListAsync();
            return data;
        }
    }
}
