

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
        public async Task<List<ApiSaveEncroachmentRegisterDto>> GetAllEncroachmentRegisterAPIdata(ApiEncroachmentRegisterParmsDto dto)

        {
            List<ApiSaveEncroachmentRegisterDto> listData = new List<ApiSaveEncroachmentRegisterDto>();


            var Data = await _dbContext.EncroachmentRegisteration
                                       .Include(x => x.Department)
                                       .Include(x => x.OtherDepartmentNavigation)
                                       .Include(x => x.Division)
                                       .Include(x => x.Locality)
                                       .Include(x => x.WatchWard)
                                       .Include(x => x.Zone)
                                       .Include(x=>x.KhasraNoNavigation)
                                     .Where(a => (a.IsActive == 1)
                                     && a.EncrochmentDate == (dto.date == "" ? a.EncrochmentDate : Convert.ToDateTime(dto.date))
                                     && (string.IsNullOrEmpty(dto.department) || a.Department.Name.Contains(dto.department))
                                     && (string.IsNullOrEmpty(dto.zone) || a.Zone.Name.Contains(dto.zone))
                                     && (string.IsNullOrEmpty(dto.division) || a.Division.Name.Contains(dto.division))
                                     && (string.IsNullOrEmpty(dto.locality) || a.Locality.Name.Contains(dto.locality))
                                     && (string.IsNullOrEmpty(dto.locality) || a.Locality.Name.Contains(dto.locality))
                                     && (string.IsNullOrEmpty(dto.khasrano) || a.KhasraNoNavigation.KhasraNo.Contains(dto.khasrano))
                                    )
                                  .OrderByDescending(a => a.Id)
                                  .ToListAsync();
            if (Data != null && Data.Count > 0)
            {
                for (int i = 0; i < Data.Count; i++)
                {

                    listData.Add(new ApiSaveEncroachmentRegisterDto()
                    {
                        Id = Data[i].Id,
                        RefNo = Data[i].RefNo==null|| Data[i].RefNo == "0"?"NA": Data[i].RefNo,
                        EncrochmentDate = Data[i].EncrochmentDate,
                        DepartmentId = Data[i].DepartmentId==0|| Data[i].DepartmentId == null ? 0 : Data[i].DepartmentId,
                        ZoneId = Data[i].ZoneId == 0 || Data[i].ZoneId == null ? 0 : Data[i].ZoneId,
                        DivisionId = Data[i].DivisionId == 0 || Data[i].DivisionId == null ? 0 : Data[i].DivisionId,
                        LocalityId = Data[i].LocalityId == 0 || Data[i].LocalityId == null ? 0 : Data[i].LocalityId,
                        KhasraNo = Data[i].KhasraNoNavigation.KhasraNo == null || Data[i].KhasraNoNavigation.KhasraNo == "" ? "NA" : Data[i].KhasraNoNavigation.KhasraNo,
                        DepartmentName = Data[i].DepartmentId == 0|| Data[i].DepartmentId == null ? "NA": Data[i].Department.Name,
                        ZoneName = Data[i].ZoneId == 0 || Data[i].ZoneId == null ? "NA" : Data[i].Zone.Name,
                        DivisionName = Data[i].DivisionId == 0 || Data[i].DivisionId == null ? "NA" : Data[i].Division.Name,
                        LocalityName = Data[i].LocalityId == 0 || Data[i].LocalityId == null ? "NA" : Data[i].Locality.Name,

                        Area = Data[i].Area == null ? 0 : Data[i].Area,
                        AreaUnit = Data[i].AreaUnit,
                        IsEncroachment = Data[i].IsEncroachment == "" ? "NA" : Data[i].IsEncroachment,
                        IsPossession = Data[i].IsPossession,
                        EncroacherName= Data[i].EncroacherName == ""|| Data[i].EncroacherName ==null ? "NA" : Data[i].IsEncroachment,//null data err 
                        Remarks = Data[i].Remarks,
                        
                    });
                }
            }
            return listData;
        }
    }
}
