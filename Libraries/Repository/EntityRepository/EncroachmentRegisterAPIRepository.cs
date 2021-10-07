

using Dto.Master;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Model.Entity;
using Repository.Common;
using Repository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class EncroachmentRegisterAPIRepository : GenericRepository<EncroachmentRegisteration>, IEncroachmentRegisterAPIRepository
    {
        public EncroachmentRegisterAPIRepository(DataContext dbContext) : base(dbContext)
        {

        }
        
        public async Task<Zone> GetZonecode(int? zoneId)
        {
            return await _dbContext.Zone.Where(x => x.IsActive == 1 && x.Id ==zoneId).FirstOrDefaultAsync();
        }
        
        public async Task<List<APIGetDepartmentListDto>> GetDepartmentDropDownList()
        {
            List<APIGetDepartmentListDto> listData = new List<APIGetDepartmentListDto>();

            var Data = await _dbContext.Department.Where(x => x.IsActive == 1).ToListAsync();

            if (Data != null && Data.Count > 0)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    listData.Add(new APIGetDepartmentListDto()
                    {
                        Id = Data[i].Id,
                        Name = Data[i].Name,
                        IsActive = Data[i].IsActive
                    });
                }
            }
            return listData;
        }
       
       
        public async Task<List<ApiGetZoneListDto>> GetZoneDropDownList(int departmentId)
        {
            List<ApiGetZoneListDto> listData = new List<ApiGetZoneListDto>();

            var Data = await _dbContext.Zone.Where(x => x.DepartmentId == departmentId && x.IsActive == 1).ToListAsync();

            if (Data != null && Data.Count > 0)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    listData.Add(new ApiGetZoneListDto()
                    {
                        Id = Data[i].Id,
                        Name = Data[i].Name,
                        Code = Data[i].Code,
                        IsActive = Data[i].IsActive
                    });
                }
            }
            return listData;
        }
        
       
        public async Task<List<ApiGetDivisionListDto>> GetDivisionDropDownList(int zoneId)
        {
            List<ApiGetDivisionListDto> listData = new List<ApiGetDivisionListDto>();

            var Data = await _dbContext.Division.Where(x => x.ZoneId == zoneId && x.IsActive == 1).ToListAsync();

            if (Data != null && Data.Count > 0)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    listData.Add(new ApiGetDivisionListDto()
                    {
                        Id = Data[i].Id,
                        Name = Data[i].Name,
                        Code = Data[i].Code,
                        IsActive = Data[i].IsActive
                    });
                }
            }
            return listData;
        }
        public async Task<List<ApiGetLocalityListDto>> GetLocalityDropDownList(int divisionId)
        {
            List<ApiGetLocalityListDto> listData = new List<ApiGetLocalityListDto>();

            var Data = await _dbContext.Locality.Where(x => x.DivisionId == divisionId && x.IsActive == 1).ToListAsync();

            if (Data != null && Data.Count > 0)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    listData.Add(new ApiGetLocalityListDto()
                    {
                        Id = Data[i].Id,
                        Name = Data[i].Name,
                        Code = Data[i].LocalityCode,
                        IsActive = Data[i].IsActive
                    });
                }
            }
            return listData;
        }

        public async Task<List<APIGetKhasraListDto>> GetKhasraDropDownList()
        {
            List<APIGetKhasraListDto> listData = new List<APIGetKhasraListDto>();

            var Data = await _dbContext.Khasra.Where(x => x.IsActive == 1).ToListAsync();

            if (Data != null && Data.Count > 0)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    listData.Add(new APIGetKhasraListDto()
                    {
                        Id = Data[i].Id,
                        Name = Data[i].Name,
                        IsActive = Data[i].IsActive
                    });
                }
            }
            return listData;
        }

    }
}
