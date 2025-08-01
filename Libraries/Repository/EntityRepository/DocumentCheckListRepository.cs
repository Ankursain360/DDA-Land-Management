﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
    public class DocumentCheckListRepository : GenericRepository<Documentchecklist>, IDocumentCheckListRepository
    {

        public DocumentCheckListRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<bool> Any(int id, string name, int ServiceTypeId)
        {
            return await _dbContext.Documentchecklist.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower() && t.ServiceTypeId == ServiceTypeId);
        }
        public async Task<Documentchecklist> FetchSingleResult(int id)
        {
            return await _dbContext.Documentchecklist
                                        .Include(x => x.ServiceType)
                                        .Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();
        }

        public async Task<List<Documentchecklist>> GetAllDocumentchecklist()
        {
            return await _dbContext.Documentchecklist.Include(x => x.ServiceType).ToListAsync();
        }

        public async Task<PagedResult<Documentchecklist>> GetPagedDocumentChecklistData(DocumentChecklistSearchDto model)
        {
            var data = await _dbContext.Documentchecklist
                                        .Include(x => x.ServiceType)
                                        .Where(x => x.ServiceTypeId == (model.serviceId == 0 ? x.ServiceTypeId : model.serviceId)
                                        && (x.Name != null ? x.Name.Contains(model.name == "" ? x.Name : model.name) : true)
                                        )
                                        .GetPaged<Documentchecklist>(model.PageNumber, model.PageSize);
            //int SortOrder = (model.SortBy.ToUpper() == "ISACTIVE") && ((int)model.SortOrder == 1) ? 2 : (model.SortBy.ToUpper() == "ISACTIVE") && ((int)model.SortOrder == 1) ? 1 : (int)model.SortOrder;
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                if(model.SortBy.ToUpper() == "ISACTIVE")
                {
                    data = await _dbContext.Documentchecklist
                                   .Include(x => x.ServiceType)
                                   .Where(x => x.ServiceTypeId == (model.serviceId == 0 ? x.ServiceTypeId : model.serviceId)
                                   && (x.Name != null ? x.Name.Contains(model.name == "" ? x.Name : model.name) : true)
                                   )
                                   .OrderByDescending(s => s.IsActive)
                                   .GetPaged<Documentchecklist>(model.PageNumber, model.PageSize);
                }
                else
                {
                    data = await _dbContext.Documentchecklist
                                   .Include(x => x.ServiceType)
                                   .Where(x => x.ServiceTypeId == (model.serviceId == 0 ? x.ServiceTypeId : model.serviceId)
                                   && (x.Name != null ? x.Name.Contains(model.name == "" ? x.Name : model.name) : true)
                                   )
                                   .OrderBy(s =>
                                   (model.SortBy.ToUpper() == "NAME" ? s.Name
                                   : model.SortBy.ToUpper() == "SERVICETYPE" ? (s.ServiceType == null ? null : s.ServiceType.Name)
                                  // : model.SortBy.ToUpper() == "ISACTIVE" ? s.IsActive
                                   : s.Name)
                                   )
                                   .GetPaged<Documentchecklist>(model.PageNumber, model.PageSize);
                }
               
            }
            else if (SortOrder == 2)
            {
                data = null;
                if (model.SortBy.ToUpper() == "ISACTIVE")
                {
                    data = await _dbContext.Documentchecklist
                                   .Include(x => x.ServiceType)
                                   .Where(x => x.ServiceTypeId == (model.serviceId == 0 ? x.ServiceTypeId : model.serviceId)
                                   && (x.Name != null ? x.Name.Contains(model.name == "" ? x.Name : model.name) : true)
                                   )
                                   .OrderBy(s => s.IsActive)
                                   .GetPaged<Documentchecklist>(model.PageNumber, model.PageSize);
                }
                else
                {
                    data = await _dbContext.Documentchecklist
                                   .Include(x => x.ServiceType)
                                   .Where(x => x.ServiceTypeId == (model.serviceId == 0 ? x.ServiceTypeId : model.serviceId)
                                   && (x.Name != null ? x.Name.Contains(model.name == "" ? x.Name : model.name) : true)
                                   )
                                   .OrderByDescending(s =>
                                   (model.SortBy.ToUpper() == "NAME" ? s.Name
                                   : model.SortBy.ToUpper() == "SERVICETYPE" ? (s.ServiceType == null ? null : s.ServiceType.Name)
                                   // : model.SortBy.ToUpper() == "ISACTIVE" ? s.IsActive
                                   : s.Name)
                                   )
                                   .GetPaged<Documentchecklist>(model.PageNumber, model.PageSize);
                }
            }
            return data;
        }
        public async Task<List<Servicetype>> GetServiceTypeList()
        {
            return await _dbContext.Servicetype.Where(x => x.IsActive == 1).ToListAsync();
        }
    }


}
