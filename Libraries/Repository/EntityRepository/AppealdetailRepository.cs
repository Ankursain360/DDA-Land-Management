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
    public class AppealdetailRepository : GenericRepository<Appealdetail>, IAppealdetailRepository
    {
        public AppealdetailRepository(DataContext dbcontext) : base(dbcontext)
        { }

        public async Task<List<Appealdetail>> GetAppealdetail()
        {
            return await _dbContext.Appealdetail.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<PagedResult<Appealdetail>> GetPagedAppealdetail(AppealdetailSearchDto model)
        {
            var data = await _dbContext.Appealdetail
                  .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.appealNo) || x.AppealNo.Contains(model.appealNo))
                    && (string.IsNullOrEmpty(model.panelLawer) || x.PanelLawer.Contains(model.panelLawer)))
                 .GetPaged<Appealdetail>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DEMANDLISTNO"):
                        data = null;
                        data = await _dbContext.Appealdetail
                            .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.appealNo) || x.AppealNo.Contains(model.appealNo))
                    && (string.IsNullOrEmpty(model.panelLawer) || x.PanelLawer.Contains(model.panelLawer)))
                           .OrderBy(s => s.DemandListNo)
                           .GetPaged<Appealdetail>(model.PageNumber, model.PageSize);

                        break;
                    case ("APPEAL"):
                        data = null;
                        data = await _dbContext.Appealdetail
                            .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.appealNo) || x.AppealNo.Contains(model.appealNo))
                    && (string.IsNullOrEmpty(model.panelLawer) || x.PanelLawer.Contains(model.panelLawer)))
                           .OrderBy(s => s.AppealNo)
                           .GetPaged<Appealdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("LAWYER"):
                        data = null;
                        data = await _dbContext.Appealdetail
                            .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.appealNo) || x.AppealNo.Contains(model.appealNo))
                    && (string.IsNullOrEmpty(model.panelLawer) || x.PanelLawer.Contains(model.panelLawer)))
                           .OrderBy(s => s.PanelLawer)
                           .GetPaged<Appealdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Appealdetail
                            .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.appealNo) || x.AppealNo.Contains(model.appealNo))
                    && (string.IsNullOrEmpty(model.panelLawer) || x.PanelLawer.Contains(model.panelLawer)))
                           .OrderBy(s => s.DateOfAppeal)
                           .GetPaged<Appealdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Appealdetail
                         .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.appealNo) || x.AppealNo.Contains(model.appealNo))
                    && (string.IsNullOrEmpty(model.panelLawer) || x.PanelLawer.Contains(model.panelLawer)))
                           .OrderByDescending(x => x.IsActive)
                           .GetPaged<Appealdetail>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DEMANDLISTNO"):
                        data = null;
                        data = await _dbContext.Appealdetail
                            .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.appealNo) || x.AppealNo.Contains(model.appealNo))
                    && (string.IsNullOrEmpty(model.panelLawer) || x.PanelLawer.Contains(model.panelLawer)))
                           .OrderByDescending(s => s.DemandListNo)
                           .GetPaged<Appealdetail>(model.PageNumber, model.PageSize);

                        break;
                    case ("APPEAL"):
                        data = null;
                        data = await _dbContext.Appealdetail
                            .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.appealNo) || x.AppealNo.Contains(model.appealNo))
                    && (string.IsNullOrEmpty(model.panelLawer) || x.PanelLawer.Contains(model.panelLawer)))
                           .OrderByDescending(s => s.AppealNo)
                           .GetPaged<Appealdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("LAWYER"):
                        data = null;
                        data = await _dbContext.Appealdetail
                            .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.appealNo) || x.AppealNo.Contains(model.appealNo))
                    && (string.IsNullOrEmpty(model.panelLawer) || x.PanelLawer.Contains(model.panelLawer)))
                           .OrderByDescending(s => s.PanelLawer)
                           .GetPaged<Appealdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Appealdetail
                            .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.appealNo) || x.AppealNo.Contains(model.appealNo))
                    && (string.IsNullOrEmpty(model.panelLawer) || x.PanelLawer.Contains(model.panelLawer)))
                           .OrderByDescending(s => s.DateOfAppeal)
                           .GetPaged<Appealdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Appealdetail
                         .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.appealNo) || x.AppealNo.Contains(model.appealNo))
                    && (string.IsNullOrEmpty(model.panelLawer) || x.PanelLawer.Contains(model.panelLawer)))
                           .OrderBy(x => x.IsActive)
                           .GetPaged<Appealdetail>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;
        }
       

    }
}

