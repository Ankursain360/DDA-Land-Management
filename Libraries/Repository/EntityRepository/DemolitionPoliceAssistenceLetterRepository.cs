using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EntityRepository
{
    public class DemolitionPoliceAssistenceLetterRepository : GenericRepository<Demolitionpoliceassistenceletter>, IDemolitionPoliceAssistenceLetterRepository
    {
        public DemolitionPoliceAssistenceLetterRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<Demolitionpoliceassistenceletter> FetchSingleResult(int id)
        {
            return await _dbContext.Demolitionpoliceassistenceletter
                                   .Include(x => x.FixingDemolition)
                                  .Include(x => x.FixingDemolition.Encroachment).FirstOrDefaultAsync();
        }

        public async Task<PagedResult<Fixingdemolition>> GetPagedApprovedAnnexureA(DemolitionPoliceAssistenceLetterSearchDto model, int userId)
        {
            var InDemolitionPoliceAssistenceTable = (from x in _dbContext.Demolitionpoliceassistenceletter
                                                     where x.FixingDemolitionId == x.FixingDemolition.Id && x.FixingDemolition.IsActive == 1
                                                     select x.FixingDemolitionId).ToArray();

            if (model.StatusId == 1)
            {
                return await _dbContext.Fixingdemolition
                                        .Include(x => x.Encroachment)
                                        .Where(x => x.IsActive == 1 && x.ApprovedStatus == model.StatusId
                                        && (model.StatusId == 0 ? x.PendingAt == userId : x.PendingAt == 0)
                                        && !(InDemolitionPoliceAssistenceTable).Contains(x.Id))
                                        .GetPaged<Fixingdemolition>(model.PageNumber, model.PageSize);
            }
            else
            {

                return await _dbContext.Fixingdemolition
                                       .Include(x => x.Encroachment)
                                       .Where(x => x.IsActive == 1 && x.ApprovedStatus == model.StatusId
                                       && (model.StatusId == 0 ? x.PendingAt == userId : x.PendingAt == 0)
                                       && !(InDemolitionPoliceAssistenceTable).Contains(x.Id))
                                       .GetPaged<Fixingdemolition>(model.PageNumber, model.PageSize);
            }

        }

        public async Task<PagedResult<Demolitionpoliceassistenceletter>> GetPagedApprovedAnnexureAListedit(DemolitionPoliceAssistenceLetterSearchDto model, int userId)
        {
            return await _dbContext.Demolitionpoliceassistenceletter
                                    .Include(x => x.FixingDemolition)
                                   .Include(x => x.FixingDemolition.Encroachment)
                                   .GetPaged<Demolitionpoliceassistenceletter>(model.PageNumber, model.PageSize);
        }
    }
}
