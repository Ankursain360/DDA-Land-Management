using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
namespace Libraries.Repository.EntityRepository
{
    public class NewlandAppealdetailRepository : GenericRepository<Newlandappealdetail>, INewlandAppealdetailRepository
    {
        public NewlandAppealdetailRepository(DataContext dbcontext) : base(dbcontext)
        { }

        public async Task<List<Newlandappealdetail>> GetNewlandappealdetails()
        {
            return await _dbContext.Newlandappealdetail.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<PagedResult<Newlandappealdetail>> GetPagedNewlandAppealdetails(NewlandAppealdetailSearchDto model)
        {
            var data = await _dbContext.Newlandappealdetail
                  .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.appealNo) || x.AppealNo.Contains(model.appealNo))
                    && (string.IsNullOrEmpty(model.enmSno) || x.EnmSno.Contains(model.enmSno)))
                 .GetPaged<Newlandappealdetail>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DEMANDLISTNO"):
                        data = null;
                        data = await _dbContext.Newlandappealdetail
                            .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.appealNo) || x.AppealNo.Contains(model.appealNo))
                    && (string.IsNullOrEmpty(model.enmSno) || x.EnmSno.Contains(model.enmSno)))
                           .OrderBy(s => s.DemandListNo)
                           .GetPaged<Newlandappealdetail>(model.PageNumber, model.PageSize);

                        break;
                    case ("APPEAL"):
                        data = null;
                        data = await _dbContext.Newlandappealdetail
                            .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.appealNo) || x.AppealNo.Contains(model.appealNo))
                    && (string.IsNullOrEmpty(model.enmSno) || x.EnmSno.Contains(model.enmSno)))
                           .OrderBy(s => s.AppealNo)
                           .GetPaged<Newlandappealdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("LAWYER"):
                        data = null;
                        data = await _dbContext.Newlandappealdetail
                            .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.appealNo) || x.AppealNo.Contains(model.appealNo))
                    && (string.IsNullOrEmpty(model.enmSno) || x.EnmSno.Contains(model.enmSno)))
                           .OrderBy(s => s.PanelLawer)
                           .GetPaged<Newlandappealdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Newlandappealdetail
                            .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.appealNo) || x.AppealNo.Contains(model.appealNo))
                    && (string.IsNullOrEmpty(model.enmSno) || x.EnmSno.Contains(model.enmSno)))
                           .OrderBy(s => s.DateOfAppeal)
                           .GetPaged<Newlandappealdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandappealdetail
                         .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.appealNo) || x.AppealNo.Contains(model.appealNo))
                    && (string.IsNullOrEmpty(model.enmSno) || x.EnmSno.Contains(model.enmSno)))
                           .OrderByDescending(x => x.IsActive)
                           .GetPaged<Newlandappealdetail>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DEMANDLISTNO"):
                        data = null;
                        data = await _dbContext.Newlandappealdetail
                            .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.appealNo) || x.AppealNo.Contains(model.appealNo))
                    && (string.IsNullOrEmpty(model.enmSno) || x.EnmSno.Contains(model.enmSno)))
                           .OrderByDescending(s => s.DemandListNo)
                           .GetPaged<Newlandappealdetail>(model.PageNumber, model.PageSize);

                        break;
                    case ("APPEAL"):
                        data = null;
                        data = await _dbContext.Newlandappealdetail
                            .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.appealNo) || x.AppealNo.Contains(model.appealNo))
                    && (string.IsNullOrEmpty(model.enmSno) || x.EnmSno.Contains(model.enmSno)))
                           .OrderByDescending(s => s.AppealNo)
                           .GetPaged<Newlandappealdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("LAWYER"):
                        data = null;
                        data = await _dbContext.Newlandappealdetail
                            .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.appealNo) || x.AppealNo.Contains(model.appealNo))
                    && (string.IsNullOrEmpty(model.enmSno) || x.EnmSno.Contains(model.enmSno)))
                           .OrderByDescending(s => s.PanelLawer)
                           .GetPaged<Newlandappealdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Newlandappealdetail
                            .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.appealNo) || x.AppealNo.Contains(model.appealNo))
                    && (string.IsNullOrEmpty(model.enmSno) || x.EnmSno.Contains(model.enmSno)))
                           .OrderByDescending(s => s.DateOfAppeal)
                           .GetPaged<Newlandappealdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandappealdetail
                         .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.appealNo) || x.AppealNo.Contains(model.appealNo))
                    && (string.IsNullOrEmpty(model.enmSno) || x.EnmSno.Contains(model.enmSno)))
                           .OrderBy(x => x.IsActive)
                           .GetPaged<Newlandappealdetail>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;
        }


    }
}

