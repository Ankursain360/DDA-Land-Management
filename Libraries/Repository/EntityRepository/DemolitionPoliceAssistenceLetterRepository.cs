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
   public class DemolitionPoliceAssistenceLetterRepository  : GenericRepository<Fixingdemolition>, IDemolitionPoliceAssistenceLetterRepository
    {
        public DemolitionPoliceAssistenceLetterRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<PagedResult<Fixingdemolition>> GetPagedApprovedAnnexureA(DemolitionPoliceAssistenceLetterSearchDto model, int userId)
        {
            return await _dbContext.Fixingdemolition
                                   .Include(x => x.Encroachment)
                                   .Where(x => x.IsActive == 1 && x.ApprovedStatus == model.StatusId
                                   && (model.StatusId == 0 ? x.PendingAt == userId : x.PendingAt == 0))
                                   .GetPaged<Fixingdemolition>(model.PageNumber, model.PageSize);
        }
    }
}
