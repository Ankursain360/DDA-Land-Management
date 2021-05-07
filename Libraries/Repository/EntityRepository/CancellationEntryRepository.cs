using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Dto.Master;
using Repository.Common;


namespace Libraries.Repository.EntityRepository
{
    public class CancellationEntryRepository : GenericRepository<Cancellationentry>, ICancellationEntryRepository
    {
        public CancellationEntryRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Cancellationentry>> GetAllRequestForProceeding()
        {
            return await _dbContext.Cancellationentry
                                    .Include(x => x.Allotment)
                                     .Include(x => x.Allotment.Application)
                                    .Include(x => x.HonebleLgOrCommonNavigation)
                                    .OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<List<Honble>> GetAllHonble()
        {
            List<Honble> villageList = await _dbContext.Honble.Where(x => x.IsActive == 1).ToListAsync();
            return villageList;
        }

        public async Task<List<Allotmententry>> GetAllAllotment()
        {
            List<Allotmententry> villageList = await _dbContext.Allotmententry.Include(x => x.Application).
                                                                                Include(x => x.LeasePurposesType)
                                                                                .Where(x => (x.ApplicationId == x.Application.Id 
                                                                                && x.IsActive == 1 
                                                                                && x.LeasePurposesTypeId == x.LeasePurposesType.Id))
                                                                                .ToListAsync();
            return villageList;
        }



        public async Task<PagedResult<Cancellationentry>> GetPagedCancellationEntry(CancellationEntrySearchDto model)
        {
            var data = await _dbContext.Cancellationentry
                                         .Include(x => x.Allotment)
                                         .Include(x => x.Allotment.Application)
                                         .Where(x =>  (x.Allotment.Application.RefNo != null ? x.Allotment.Application.RefNo.Contains(model.refno == "" ? x.Allotment.Application.RefNo : model.refno) : true)
                                         && (x.Allotment.Application.Name != null ? x.Allotment.Application.Name.Contains(model.name == "" ? x.Allotment.Application.Name : model.name) : true)
                                        )
                                         .GetPaged<Cancellationentry>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                if (model.SortBy.ToUpper() == "ISACTIVE")
                {
                    data = await _dbContext.Cancellationentry
                                         .Include(x => x.Allotment)
                                         .Include(x => x.Allotment.Application)
                                         .Where(x => (x.Allotment.Application.RefNo != null ? x.Allotment.Application.RefNo.Contains(model.refno == "" ? x.Allotment.Application.RefNo : model.refno) : true)
                                         && (x.Allotment.Application.Name != null ? x.Allotment.Application.Name.Contains(model.name == "" ? x.Allotment.Application.Name : model.name) : true)
                                        )
                                        .OrderByDescending(s => s.IsActive)                                        
                                         .GetPaged<Cancellationentry>(model.PageNumber, model.PageSize);
                }
                else
                {
                    data = null;
                    data = await _dbContext.Cancellationentry
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
                                        .GetPaged<Cancellationentry>(model.PageNumber, model.PageSize);
                }

            }
            else if (SortOrder == 2)
            {
                if (model.SortBy.ToUpper() == "ISACTIVE")
                {
                    data = await _dbContext.Cancellationentry
                                         .Include(x => x.Allotment)
                                         .Include(x => x.Allotment.Application)
                                         .Where(x => (x.Allotment.Application.RefNo != null ? x.Allotment.Application.RefNo.Contains(model.refno == "" ? x.Allotment.Application.RefNo : model.refno) : true)
                                         && (x.Allotment.Application.Name != null ? x.Allotment.Application.Name.Contains(model.name == "" ? x.Allotment.Application.Name : model.name) : true)
                                        )
                                        .OrderBy(s => s.IsActive)
                                         .GetPaged<Cancellationentry>(model.PageNumber, model.PageSize);
                }
                else
                {
                    data = null;
                    data = await _dbContext.Cancellationentry
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
                                        .GetPaged<Cancellationentry>(model.PageNumber, model.PageSize);
                }

            }
            return data;
        }
        public async Task<List<UserBindDropdownDto>> BindUsernameNameList()
        {
            try
            {
                var data = await _dbContext.LoadStoredProcedure("BindUserZoneDropDown")
                                            .WithOutParams()
                                            .ExecuteStoredProcedureAsync<UserBindDropdownDto>();

                return (List<UserBindDropdownDto>)data;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Allotmententry> FetchAllottmentDetails(int allottmentId)
        {
            return await _dbContext.Allotmententry
                                    .Include(x => x.Application)
                                    .Include(x => x.LeasePurposesType)
                                    .Where(x => x.Id == allottmentId)
                                    .FirstOrDefaultAsync();
        }
    }
}
