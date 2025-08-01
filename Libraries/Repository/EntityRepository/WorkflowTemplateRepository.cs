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
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
    public class WorkflowTemplateRepository : GenericRepository<WorkflowTemplate>, IWorkflowTemplateRepository
    {

        public WorkflowTemplateRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<PagedResult<WorkflowTemplate>> GetPagedWorkflowTemplate(WorkflowTemplateSearchDto model)
        {

            var data = await _dbContext.WorkflowTemplate
                                            .Include(x => x.Module)
                                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                             && (string.IsNullOrEmpty(model.version) || x.Version.Contains(model.version))
                                             && x.ModuleId == (model.moduleid == 0 ? x.ModuleId : model.moduleid)
                                             )
                                            .GetPaged<WorkflowTemplate>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                if (model.SortBy.ToUpper() == "ISACTIVE")
                {
                    data = await _dbContext.WorkflowTemplate
                                            .Include(x => x.Module)
                                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                             && (string.IsNullOrEmpty(model.version) || x.Version.Contains(model.version))
                                             && x.ModuleId == (model.moduleid == 0 ? x.ModuleId : model.moduleid)
                                             )
                                            .OrderByDescending(s => s.IsActive)
                                            .GetPaged<WorkflowTemplate>(model.PageNumber, model.PageSize);
                }
                else
                {
                    data = await _dbContext.WorkflowTemplate
                                            .Include(x => x.Module)
                                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                             && (string.IsNullOrEmpty(model.version) || x.Version.Contains(model.version))
                                             && x.ModuleId == (model.moduleid == 0 ? x.ModuleId : model.moduleid)
                                             )
                                            .OrderBy(s =>
                                            (model.SortBy.ToUpper() == "NAME" ? s.Name
                                            : model.SortBy.ToUpper() == "MODULE" ? s.Module.Name
                                            : s.Module.Name)
                                            )
                                            .GetPaged<WorkflowTemplate>(model.PageNumber, model.PageSize);
                }
            }
            else if (SortOrder == 2)
            {
                data = null;
                if (model.SortBy.ToUpper() == "ISACTIVE")
                {
                    data = await _dbContext.WorkflowTemplate
                                            .Include(x => x.Module)
                                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                             && (string.IsNullOrEmpty(model.version) || x.Version.Contains(model.version))
                                             && x.ModuleId == (model.moduleid == 0 ? x.ModuleId : model.moduleid)
                                             )
                                            .OrderBy(s => s.IsActive)
                                            .GetPaged<WorkflowTemplate>(model.PageNumber, model.PageSize);
                }
                else
                {
                    data = await _dbContext.WorkflowTemplate
                                            .Include(x => x.Module)
                                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                             && (string.IsNullOrEmpty(model.version) || x.Version.Contains(model.version))
                                             && x.ModuleId == (model.moduleid == 0 ? x.ModuleId : model.moduleid)
                                             )
                                            .OrderByDescending(s =>
                                            (model.SortBy.ToUpper() == "NAME" ? s.Name
                                            : model.SortBy.ToUpper() == "MODULE" ? s.Module.Name
                                            : s.Module.Name)
                                            )
                                            .GetPaged<WorkflowTemplate>(model.PageNumber, model.PageSize);
                }
            }
            return data;
        }

        public async Task<bool> Any(int id, string name)

        {
            return await _dbContext.Designation.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }

        public async Task<List<Module>> GetAllModuleList()
        {
            return await _dbContext.Module.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<List<Approvalstatus>> GetApprovalStatusListData()
        {
            List<Approvalstatus> listData = new List<Approvalstatus>();
            var Data = await _dbContext.Approvalstatus
                                    .Where(x => x.IsActive == 1)
                                    .ToListAsync();
            if (Data != null)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    listData.Add(new Approvalstatus()
                    {
                        Id= Data[i].Id,
                        Name = Data[i].Name,
                        idcode = string.Format("{0}|{1}", Data[i].Id, Data[i].StatusCode)
                    });
                }
            }

            return listData;
        }

        public int ProcessGuidBasisCount(string processGuid)
        {
            var count = (from f in _dbContext.WorkflowTemplate
                         where f.ProcessGuid == processGuid
                         orderby f.Id descending
                         select f.Id).Count();

            return count;
        }

        public async Task<WorkflowTemplate> FetchSingleResultOnProcessGuid(string processguid)
        {
            return await _dbContext.WorkflowTemplate
                                    .Where(x => x.ProcessGuid == processguid && x.EffectiveDate <= DateTime.Now
                                    && x.IsActive == 1
                                    )
                                    .OrderByDescending(x => x.Id)
                                    .Take(1)
                                    .FirstOrDefaultAsync();
        }

        public async Task<WorkflowTemplate> FetchSingleResultOnProcessGuidWithVersion(string processguid, string version)
        {
            return await _dbContext.WorkflowTemplate
                                   .Where(x => x.ProcessGuid == processguid 
                                   && x.Version == version
                                   && x.IsActive == 1
                                   )
                                   .OrderByDescending(x => x.Id)
                                   .Take(1)
                                   .FirstOrDefaultAsync();
        }

        public async Task<List<WorkflowTemplate>> GetWorkFlowDataOnGuid(string processguid)
        {
            return await _dbContext.WorkflowTemplate
                                     .Where(x => x.ProcessGuid == processguid
                                     && x.IsActive == 1
                                     )
                                     .OrderByDescending(x => x.Id)
                                     .ToListAsync();
        }

        public async Task<int> GetStatusCodeFromId(int id)
        {
            var data =await (from f in _dbContext.Approvalstatus
                        where f.Id == id
                      select f.StatusCode).FirstOrDefaultAsync();

            return data;
        }
    }
}
