using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.Common;

namespace Libraries.Repository.EntityRepository
{
    public class ExtensionRepository : GenericRepository<Extension>, IExtensionRepository
    {

        public ExtensionRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<Possesionplan> GetAllotteeDetails(int userId)
        {
            return await _dbContext.Possesionplan
                                    .Include(x => x.Allotment)
                                    .Include(x => x.Allotment.Application)
                                    .Include(x => x.Allotment.LeasePurposesType)
                                    .Where(x => x.Allotment.Application.UserId == userId)
                                    .FirstOrDefaultAsync();
        }

        public async Task<List<Documentchecklist>> GetDocumentChecklistDetails(int servicetypeid)
        {
            return await _dbContext.Documentchecklist
                                        .Include(x => x.ServiceType)
                                        .Where(x => x.ServiceTypeId == servicetypeid)
                                        .OrderByDescending(x => x.IsMandatory)
                                        .ToListAsync();
        }

        public async Task<PagedResult<Extension>> GetPagedExtensionServiceDetails(ExtensionServiceSearchDto model)
        {
            var data = await _dbContext.Extension
                                         .Include(x => x.Allotment)
                                         .Include(x => x.Allotment.Application)
                                         .Where(x => (x.Allotment.Application.RefNo != null ? x.Allotment.Application.RefNo.Contains(model.refno == "" ? x.Allotment.Application.RefNo : model.refno) : true)
                                         && (x.Allotment.Application.Name != null ? x.Allotment.Application.Name.Contains(model.name == "" ? x.Allotment.Application.Name : model.name) : true)
                                        )
                                         .GetPaged<Extension>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                if (model.SortBy.ToUpper() == "ISACTIVE")
                {
                    data = await _dbContext.Extension
                                         .Include(x => x.Allotment)
                                         .Include(x => x.Allotment.Application)
                                         .Where(x => (x.Allotment.Application.RefNo != null ? x.Allotment.Application.RefNo.Contains(model.refno == "" ? x.Allotment.Application.RefNo : model.refno) : true)
                                         && (x.Allotment.Application.Name != null ? x.Allotment.Application.Name.Contains(model.name == "" ? x.Allotment.Application.Name : model.name) : true)
                                        )
                                        .OrderByDescending(s => s.IsActive)
                                         .GetPaged<Extension>(model.PageNumber, model.PageSize);
                }
                else
                {
                    data = null;
                    data = await _dbContext.Extension
                                         .Include(x => x.Allotment)
                                         .Include(x => x.Allotment.Application)
                                         .Where(x => (x.Allotment.Application.RefNo != null ? x.Allotment.Application.RefNo.Contains(model.refno == "" ? x.Allotment.Application.RefNo : model.refno) : true)
                                         && (x.Allotment.Application.Name != null ? x.Allotment.Application.Name.Contains(model.name == "" ? x.Allotment.Application.Name : model.name) : true)
                                        )
                                       .OrderBy(s =>
                                       (model.SortBy.ToUpper() == "REFNO" ? (s.Allotment == null ? null : s.Allotment.Application == null ? null : s.Allotment.Application.RefNo)
                                       : model.SortBy.ToUpper() == "SOCIETYNAME" ? (s.Allotment == null ? null : s.Allotment.Application == null ? null : s.Allotment.Application.Name)
                                       : (s.Allotment == null ? null : s.Allotment.Application == null ? null : s.Allotment.Application.RefNo))
                                       )
                                        .GetPaged<Extension>(model.PageNumber, model.PageSize);
                }

            }
            else if (SortOrder == 2)
            {
                if (model.SortBy.ToUpper() == "ISACTIVE")
                {
                    data = await _dbContext.Extension
                                         .Include(x => x.Allotment)
                                         .Include(x => x.Allotment.Application)
                                         .Where(x => (x.Allotment.Application.RefNo != null ? x.Allotment.Application.RefNo.Contains(model.refno == "" ? x.Allotment.Application.RefNo : model.refno) : true)
                                         && (x.Allotment.Application.Name != null ? x.Allotment.Application.Name.Contains(model.name == "" ? x.Allotment.Application.Name : model.name) : true)
                                        )
                                        .OrderBy(s => s.IsActive)
                                         .GetPaged<Extension>(model.PageNumber, model.PageSize);
                }
                else
                {
                    data = null;
                    data = await _dbContext.Extension
                                         .Include(x => x.Allotment)
                                         .Include(x => x.Allotment.Application)
                                         .Where(x => (x.Allotment.Application.RefNo != null ? x.Allotment.Application.RefNo.Contains(model.refno == "" ? x.Allotment.Application.RefNo : model.refno) : true)
                                         && (x.Allotment.Application.Name != null ? x.Allotment.Application.Name.Contains(model.name == "" ? x.Allotment.Application.Name : model.name) : true)
                                        )
                                       .OrderByDescending(s =>
                                       (model.SortBy.ToUpper() == "REFNO" ? (s.Allotment == null ? null : s.Allotment.Application == null ? null : s.Allotment.Application.RefNo)
                                       : model.SortBy.ToUpper() == "SOCIETYNAME" ? (s.Allotment == null ? null : s.Allotment.Application == null ? null : s.Allotment.Application.Name)
                                       : (s.Allotment == null ? null : s.Allotment.Application == null ? null : s.Allotment.Application.RefNo))
                                       )
                                        .GetPaged<Extension>(model.PageNumber, model.PageSize);
                }

            }
            return data;
        }

        public async Task<bool> SaveAllotteeServiceDocuments(List<Allotteeservicesdocument> allotteeservicesdocuments)
        {
            await _dbContext.Allotteeservicesdocument.AddRangeAsync(allotteeservicesdocuments);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
    }
}
