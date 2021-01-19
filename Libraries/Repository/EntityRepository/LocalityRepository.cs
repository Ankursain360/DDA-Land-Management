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
                             && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode))
                              && (string.IsNullOrEmpty(model.landmark) || x.Landmark.Contains(model.landmark))
                               && (string.IsNullOrEmpty(model.address) || x.Address.Contains(model.address)))
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
                       data.Results= data.Results.OrderBy(x => x.Department.Name).ToList(); 
                        break;
                    case ("ZONE"):
                        data.Results = data.Results.OrderBy(x => x.Zone.Name).ToList(); 
                        break;
                    case ("DIVISION"):
                        data.Results = data.Results.OrderBy(x => x.Division.Name).ToList(); 
                        break;
                    case ("LOCALITY"):
                        data.Results = data.Results.OrderBy(x => x.Name).ToList();
                        break;
                    case ("LOCALITYCODE"):
                        data.Results = data.Results.OrderBy(x => x.LocalityCode).ToList();
                        break;
                    case ("ISACTIVE"):
                        data.Results = data.Results.OrderBy(x => x.IsActive).ToList();
                        break;
                }
            }
            else if(SortOrder==2)
            {
                switch (model.SortBy.ToUpper())
                {
                   
                    case ("DEPARTMENT"):
                        data.Results = data.Results.OrderByDescending(x => x.Department.Name).ToList();
                        break;
                    case ("ZONE"):
                        data.Results = data.Results.OrderByDescending(x => x.Zone.Name).ToList();
                        break;
                    case ("DIVISION"):
                        data.Results = data.Results.OrderByDescending(x => x.Division.Name).ToList();
                        break;
                    case ("LOCALITY"):
                        data.Results = data.Results.OrderByDescending(x => x.Name).ToList();
                        break;
                    case ("LOCALITYCODE"):
                        data.Results = data.Results.OrderByDescending(x => x.LocalityCode).ToList();
                        break;
                    case ("ISACTIVE"):
                        data.Results = data.Results.OrderByDescending(x => x.IsActive).ToList();
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
