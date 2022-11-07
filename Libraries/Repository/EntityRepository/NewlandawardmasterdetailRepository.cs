using System.Collections.Generic;
using Dto.Search;
using System.Threading.Tasks;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Repository.IEntityRepository;

namespace Libraries.Repository.EntityRepository
{
    public class NewlandawardmasterdetailRepository : GenericRepository<Newlandawardmasterdetail>, INewlandawardmasterdetailRepository
    {
        public NewlandawardmasterdetailRepository(DataContext dbContext) : base(dbContext)
        { }
        public async Task<PagedResult<Newlandawardmasterdetail>> GetPagedawardmasterdetails(NewlandawardmasterSearchDto model)
        {

            var data = await _dbContext.Newlandawardmasterdetail
                               .Include(x => x.Newlandvillage)
                                .Include(x => x.Proposal)
                                 .Where(x => string.IsNullOrEmpty(model.name) || x.AwardNumber.Contains(model.name) && (x.IsActive == 1))
                              .OrderByDescending(s => s.IsActive)
                              .ThenBy(s => s.Newlandvillage.Name)
                             .ThenBy(s => s.Proposal.Name)
                              .GetPaged<Newlandawardmasterdetail>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Newlandawardmasterdetail
                               .Include(x => x.Newlandvillage)
                                .Include(x => x.Proposal)
                               .Where(x => string.IsNullOrEmpty(model.name) || x.AwardNumber.Contains(model.name))
                            .OrderBy(s => s.AwardNumber)
                             .ThenBy(s => s.Newlandvillage.Name)
                            .ThenBy(s => s.Proposal.Name)
                        .GetPaged<Newlandawardmasterdetail>(model.PageNumber, model.PageSize);
                        break;
                    //case ("STATUS"):
                    //    data = null;
                    //    data = await _dbContext.Newlandawardmasterdetail
                    //           .Include(x => x.Newlandvillage)
                    //            .Include(x => x.Proposal)
                    //           .Where(x => string.IsNullOrEmpty(model.name) || x.AwardNumber.Contains(model.name))
                    //        .OrderByDescending(s => s.IsActive)
                    //         .ThenBy(s => s.Newlandvillage.Name)
                    //        .ThenBy(s => s.Proposal.Name)
                    //    .GetPaged<Newlandawardmasterdetail>(model.PageNumber, model.PageSize);
                    //    break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandawardmasterdetail
                               .Include(x => x.Newlandvillage)
                                .Include(x => x.Proposal)
                               .Where(x => string.IsNullOrEmpty(model.name) || x.AwardNumber.Contains(model.name))
                        
                                    .OrderByDescending(a => a.IsActive)
                                    .GetPaged<Newlandawardmasterdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("AWARDDATE"):
                        data = null;
                        data = await _dbContext.Newlandawardmasterdetail
                               .Include(x => x.Newlandvillage)
                                .Include(x => x.Proposal)
                               .Where(x => string.IsNullOrEmpty(model.name) || x.AwardNumber.Contains(model.name))
                            .OrderBy(s => s.AwardDate)
                             .ThenBy(s => s.Newlandvillage.Name)
                            .ThenBy(s => s.Proposal.Name)
                        .GetPaged<Newlandawardmasterdetail>(model.PageNumber, model.PageSize); break;
                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Newlandawardmasterdetail
                               .Include(x => x.Newlandvillage)
                                .Include(x => x.Proposal)
                               .Where(x => string.IsNullOrEmpty(model.name) || x.AwardNumber.Contains(model.name))
                            .OrderBy(s => s.Newlandvillage.Name)
                             .ThenBy(s => s.Newlandvillage.Name)
                            .ThenBy(s => s.Proposal.Name)
                        .GetPaged<Newlandawardmasterdetail>(model.PageNumber, model.PageSize); break;
                    case ("PROPOSAL"):
                        data = null;
                        data = await _dbContext.Newlandawardmasterdetail
                               .Include(x => x.Newlandvillage)
                                .Include(x => x.Proposal)
                               .Where(x => string.IsNullOrEmpty(model.name) || x.AwardNumber.Contains(model.name))
                            .OrderBy(s => s.Proposal.Name)
                             .ThenBy(s => s.Newlandvillage.Name)
                            .ThenBy(s => s.Proposal.Name)
                        .GetPaged<Newlandawardmasterdetail>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Newlandawardmasterdetail
                               .Include(x => x.Newlandvillage)
                                .Include(x => x.Proposal)
                               .Where(x => string.IsNullOrEmpty(model.name) || x.AwardNumber.Contains(model.name))
                            .OrderByDescending(s => s.AwardNumber)
                             .ThenBy(s => s.Newlandvillage.Name)
                            .ThenBy(s => s.Proposal.Name)
                        .GetPaged<Newlandawardmasterdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandawardmasterdetail
                               .Include(x => x.Newlandvillage)
                                .Include(x => x.Proposal)
                               .Where(x => string.IsNullOrEmpty(model.name) || x.AwardNumber.Contains(model.name))

                                    .OrderBy(a => a.IsActive)
                                    .GetPaged<Newlandawardmasterdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("AWARDDATE"):
                        data = null;
                        data = await _dbContext.Newlandawardmasterdetail
                                 .Include(x => x.Newlandvillage)
                                  .Include(x => x.Proposal)
                                 .Where(x => string.IsNullOrEmpty(model.name) || x.AwardNumber.Contains(model.name))
                               //.OrderBy(s => s.AwardDate)
                               .OrderByDescending(a => a.AwardDate)
                                    .GetPaged<Newlandawardmasterdetail>(model.PageNumber, model.PageSize);

                        //     .ThenBy(s => s.Newlandvillage.Name)
                        //    .ThenBy(s => s.Proposal.Name)
                        //.GetPaged<Newlandawardmasterdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Newlandawardmasterdetail
                               .Include(x => x.Newlandvillage)
                                .Include(x => x.Proposal)
                               .Where(x => string.IsNullOrEmpty(model.name) || x.AwardNumber.Contains(model.name))
                        //    .OrderBy(s => s.Newlandvillage.Name)
                        //     .ThenBy(s => s.Newlandvillage.Name)
                        //    .ThenBy(s => s.Proposal.Name)
                        //.GetPaged<Newlandawardmasterdetail>(model.PageNumber, model.PageSize); 
                         .OrderByDescending(a => a.Newlandvillage.Name)
                                    .GetPaged<Newlandawardmasterdetail>(model.PageNumber, model.PageSize);
                        break;
                        
                    case ("PROPOSAL"):
                        data = null;
                        data = await _dbContext.Newlandawardmasterdetail
                              .Include(x => x.Newlandvillage)
                               .Include(x => x.Proposal)
                              .Where(x => string.IsNullOrEmpty(model.name) || x.AwardNumber.Contains(model.name))
                           .OrderByDescending(s => s.Proposal.Name)
                            .ThenBy(s => s.Newlandvillage.Name)
                           .ThenBy(s => s.Proposal.Name)
                       .GetPaged<Newlandawardmasterdetail>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;
        }

        public async Task<List<Newlandawardmasterdetail>> Getawardmasterdetails()
        {
            return await _dbContext.Newlandawardmasterdetail
                 .Include(x => x.Newlandvillage)
                        .Include(x => x.Proposal)
                        .Where(x => x.IsActive == 1).ToListAsync();
            //return await _dbContext.casenature.OrderByDescending(s => s.IsActive).ToListAsync();
        }
        public async Task<List<Newlandawardmasterdetail>> GetAllawardmasterdetailsList(NewlandawardmasterSearchDto model)
        {

            var data = await _dbContext.Newlandawardmasterdetail
                               .Include(x => x.Newlandvillage)
                                .Include(x => x.Proposal)
                                 .Where(x => string.IsNullOrEmpty(model.name) || x.AwardNumber.Contains(model.name) && (x.IsActive == 1))
                              .OrderByDescending(s => s.IsActive)
                              .ThenBy(s => s.Newlandvillage.Name)
                             .ThenBy(s => s.Proposal.Name).ToListAsync();
            return data;
        }
        public async Task<bool> Any(int id, string AwardNumber)
        {
            return await _dbContext.Newlandawardmasterdetail.AnyAsync(t => t.Id != id && t.AwardNumber.ToLower() == AwardNumber.ToLower());
        }
        public async Task<List<Newlandvillage>> Getvillage()
        {
            var villageList = await _dbContext.Newlandvillage.Where(x => x.IsActive == 1).ToListAsync();
            return villageList;
        }
        public async Task<List<Proposaldetails>> GetPurposal()
        {
            var purposalList = await _dbContext.Proposaldetails.Where(x => x.IsActive == 1).ToListAsync();
            return purposalList;
        }
        public async Task<List<Undersection17>> Getundersection17()
        {
            var section17List = await _dbContext.Undersection17.Where(x => x.IsActive == 1).ToListAsync();
            return section17List;
        }
        public async Task<List<Undersection6>> Getundersection6()
        {
            var section6List = await _dbContext.Undersection6.Where(x => x.IsActive == 1).ToListAsync();
            return section6List;
        }
        public async Task<List<Undersection4>> Getundersection4()
        {
            var section4List = await _dbContext.Undersection4.Where(x => x.IsActive == 1).ToListAsync();
            return section4List;
        }
    }
}
