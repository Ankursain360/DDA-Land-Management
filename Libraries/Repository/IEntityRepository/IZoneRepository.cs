﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libraries.Repository.IEntityRepository
{
    public interface IZoneRepository : IGenericRepository<Zone>
    {
        
        Task<bool> Any(int id,int DepartmentId, string name);
        Task<bool> anyCode(int id, int DepartmentId, string name);
        Task<List<Department>> GetDepartmentList();
        Task<List<Zone>> GetAllDetails();
        Task<List<Zone>> GetAllZone();
        Task<PagedResult<Zone>> GetPagedZone(ZoneSearchDto model);
        Task<PagedResult<Zone>> DecisionSupportSystemZone(ZoneSearchDto model); 
    }
}