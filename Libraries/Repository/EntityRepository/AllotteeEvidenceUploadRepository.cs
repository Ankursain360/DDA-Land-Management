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
    public class AllotteeEvidenceUploadRepository : GenericRepository<Allotteeevidenceupload>, IAllotteeEvidenceUploadRepository
    {

        public AllotteeEvidenceUploadRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Requestforproceeding>> GetAllotteeEvidenceDetails()
        {
            return await _dbContext.Requestforproceeding
                                        .Include(x => x.Allotment)
                                        .Include(x => x.Allotment.Application)
                                        .Where(x => x.IsSend == 1)
                                        .ToListAsync();
        }

        public async Task<Allotteeevidenceupload> FetchAllotteeEvidenceUploadDetails(int id)
        {
            return await _dbContext.Allotteeevidenceupload
                                    .Include(x => x.RequestProceeding)
                                    //   .Include(x => x.RequestProceeding.Allotment)
                                    //    .Include(x => x.RequestProceeding.Allotment.Application)
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
        }

        public async Task<List<Allotteeevidenceupload>> GetAllotteeEvidenceHistoryDetails(int id)
        {
            return await _dbContext.Allotteeevidenceupload
                                    .Include(x => x.RequestProceeding)
                                    .Where(x => x.RequestProceedingId == id)
                                    .ToListAsync();
        }

        public async Task<PagedResult<Requestforproceeding>> GetPagedRequestLetterDetails(AllotteeEvidenceSearchDto model)
        {
            var data = await _dbContext.Requestforproceeding
                                        .Include(x => x.Allotment)
                                        .Include(x => x.Allotment.Application)
                                        .Where(x => x.IsActive == 1
                                        && (x.Allotment.Application.RefNo != null ? x.Allotment.Application.RefNo.Contains(model.refno == "" ? x.Allotment.Application.RefNo : model.refno) : true)
                                        && (x.Allotment.Application.Name != null ? x.Allotment.Application.Name.Contains(model.name == "" ? x.Allotment.Application.Name : model.name) : true)
                                        && x.IsSend == 1
                                        )
                                        .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                data = await _dbContext.Requestforproceeding
                                        .Include(x => x.Allotment)
                                        .Include(x => x.Allotment.Application)
                                        .Where(x => x.IsActive == 1
                                        && (x.Allotment.Application.RefNo != null ? x.Allotment.Application.RefNo.Contains(model.refno == "" ? x.Allotment.Application.RefNo : model.refno) : true)
                                        && (x.Allotment.Application.Name != null ? x.Allotment.Application.Name.Contains(model.name == "" ? x.Allotment.Application.Name : model.name) : true)
                                        && x.IsSend == 1
                                        )
                                       .OrderBy(s =>
                                       (model.SortBy.ToUpper() == "REFNO" ? (s.Allotment == null ? null : s.Allotment.Application == null ? null : s.Allotment.Application.RefNo)
                                       : model.SortBy.ToUpper() == "SOCIETYNAME" ? (s.Allotment == null ? null : s.Allotment.Application == null ? null : s.Allotment.Application.Name)
                                       : (s.Allotment == null ? null : s.Allotment.Application == null ? null : s.Allotment.Application.RefNo))
                                       )
                                        .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);


            }
            else if (SortOrder == 2)
            {
                data = null;
                data = await _dbContext.Requestforproceeding
                                        .Include(x => x.Allotment)
                                        .Include(x => x.Allotment.Application)
                                        .Where(x => x.IsActive == 1
                                        && (x.Allotment.Application.RefNo != null ? x.Allotment.Application.RefNo.Contains(model.refno == "" ? x.Allotment.Application.RefNo : model.refno) : true)
                                        && (x.Allotment.Application.Name != null ? x.Allotment.Application.Name.Contains(model.name == "" ? x.Allotment.Application.Name : model.name) : true)
                                        && x.IsSend == 1
                                        )
                                       .OrderByDescending(s =>
                                       (model.SortBy.ToUpper() == "REFNO" ? (s.Allotment == null ? null : s.Allotment.Application == null ? null : s.Allotment.Application.RefNo)
                                       : model.SortBy.ToUpper() == "SOCIETYNAME" ? (s.Allotment == null ? null : s.Allotment.Application == null ? null : s.Allotment.Application.Name)
                                       : (s.Allotment == null ? null : s.Allotment.Application == null ? null : s.Allotment.Application.RefNo))
                                       )
                                        .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);
            }
            return data;
        }
    }
}
