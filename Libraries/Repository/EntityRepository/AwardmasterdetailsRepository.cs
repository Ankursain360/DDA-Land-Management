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
    public class AwardmasterdetailsRepository : GenericRepository<Awardmasterdetail>, IAwardmasterdetailsRepository
    {
        public AwardmasterdetailsRepository(DataContext dbContext) : base(dbContext)
        { }
        public async Task<PagedResult<Awardmasterdetail>> GetPagedawardmasterdetails(AwardMasterDetailsSearchDto model)
        {

            var data = await _dbContext.Awardmasterdetail
                               .Include(x => x.Acquiredlandvillage)
                                 //.Include(x=>x.Us17)
                                 //.Include(x=>x.Us4)
                                 //.Include(x=>x.Us6)
                              .Include( x => x.Proposal)
                                 .Where(x => string.IsNullOrEmpty(model.name) || x.AwardNumber.Contains(model.name) && (x.IsActive == 1))
                              .OrderByDescending(s => s.IsActive)
                              .ThenBy(s => s.Acquiredlandvillage.Name)
                             .ThenBy(s => s.Proposal.Name)
                              .GetPaged<Awardmasterdetail>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Awardmasterdetail
                               .Include(x => x.Acquiredlandvillage)
                               //.Include(x => x.Us17)
                               //.Include(x => x.Us4)
                               //.Include(x => x.Us6)
                              .Include(x => x.Proposal)
                               .Where(x => string.IsNullOrEmpty(model.name) || x.AwardNumber.Contains(model.name))
                            .OrderBy(s => s.AwardNumber)
                             .ThenBy(s => s.Acquiredlandvillage.Name)
                            .ThenBy(s => s.Proposal.Name)
                        .GetPaged<Awardmasterdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Awardmasterdetail
                           .Include(x => x.Acquiredlandvillage)
                               //.Include(x => x.Us17)
                               //.Include(x => x.Us4)
                               //.Include(x => x.Us6)
                              .Include(x => x.Proposal)
                             .Where(x => string.IsNullOrEmpty(model.name) || x.AwardNumber.Contains(model.name))
                            .OrderBy(x => x.IsActive == 0)
                             .ThenBy(s => s.Acquiredlandvillage.Name)
                            .ThenBy(s => s.Proposal.Name)
                        .GetPaged<Awardmasterdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("AWARDDATE"):
                        data = null;
                        data = await _dbContext.Awardmasterdetail
                               .Include(x => x.Acquiredlandvillage)
                              //.Include(x => x.Us17)
                              //.Include(x => x.Us4)
                              //.Include(x => x.Us6)
                              .Include(x => x.Proposal)
                               .Where(x => string.IsNullOrEmpty(model.name) || x.AwardNumber.Contains(model.name))
                            .OrderBy(s => s.AwardDate)
                             .ThenBy(s => s.Acquiredlandvillage.Name)
                            .ThenBy(s => s.Proposal.Name)
                        .GetPaged<Awardmasterdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Awardmasterdetail
                               .Include(x => x.Acquiredlandvillage)
                              //.Include(x => x.Us17)
                              //.Include(x => x.Us4)
                              //.Include(x => x.Us6)
                              .Include(x => x.Proposal)
                               .Where(x => string.IsNullOrEmpty(model.name) || x.AwardNumber.Contains(model.name))
                            .OrderBy(s => s.Acquiredlandvillage.Name)
                             .ThenBy(s => s.Acquiredlandvillage.Name)
                            .ThenBy(s => s.Proposal.Name)
                        .GetPaged<Awardmasterdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("PROPOSAL"):
                        data = null;
                        data = await _dbContext.Awardmasterdetail
                               .Include(x => x.Acquiredlandvillage)
                              //.Include(x => x.Us17)
                              //.Include(x => x.Us4)
                              //.Include(x => x.Us6)
                              .Include(x => x.Proposal)
                               .Where(x => string.IsNullOrEmpty(model.name) || x.AwardNumber.Contains(model.name))
                            .OrderBy(s => s.Proposal.Name)
                             .ThenBy(s => s.Acquiredlandvillage.Name)
                            .ThenBy(s => s.Proposal.Name)
                        .GetPaged<Awardmasterdetail>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Awardmasterdetail
                           .Include(x => x.Acquiredlandvillage)
                               //.Include(x => x.Us17)
                               //.Include(x => x.Us4)
                               //.Include(x => x.Us6)
                               .Include(x => x.Proposal)
                             .Where(x => string.IsNullOrEmpty(model.name) || x.AwardNumber.Contains(model.name))
                            .OrderByDescending(x => x.AwardNumber)
                             .ThenBy(s => s.Acquiredlandvillage.Name)
                            .ThenBy(s => s.Proposal.Name)
                        .GetPaged<Awardmasterdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Awardmasterdetail
                           .Include(x => x.Acquiredlandvillage)
                               //.Include(x => x.Us17)
                               //.Include(x => x.Us4)
                               //.Include(x => x.Us6)
                               .Include(x => x.Proposal)
                             .Where(x => string.IsNullOrEmpty(model.name) || x.AwardNumber.Contains(model.name))
                             .OrderByDescending(x => x.IsActive == 0)
                              .ThenBy(s => s.Acquiredlandvillage.Name)
                            .ThenBy(s => s.Proposal.Name)
                              .GetPaged<Awardmasterdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("AWARDDATE"):
                        data = null;
                        data = await _dbContext.Awardmasterdetail
                               .Include(x => x.Acquiredlandvillage)
                              //.Include(x => x.Us17)
                              //.Include(x => x.Us4)
                              //.Include(x => x.Us6)
                              .Include(x => x.Proposal)
                               .Where(x => string.IsNullOrEmpty(model.name) || x.AwardNumber.Contains(model.name))
                            .OrderByDescending(s => s.AwardDate)
                             .ThenBy(s => s.Acquiredlandvillage.Name)
                            .ThenBy(s => s.Proposal.Name)
                        .GetPaged<Awardmasterdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Awardmasterdetail
                               .Include(x => x.Acquiredlandvillage)
                              //.Include(x => x.Us17)
                              //.Include(x => x.Us4)
                              //.Include(x => x.Us6)
                              .Include(x => x.Proposal)
                               .Where(x => string.IsNullOrEmpty(model.name) || x.AwardNumber.Contains(model.name))
                            .OrderByDescending(s => s.Acquiredlandvillage.Name)
                             .ThenBy(s => s.Acquiredlandvillage.Name)
                            .ThenBy(s => s.Proposal.Name)
                        .GetPaged<Awardmasterdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("PROPOSAL"):
                        data = null;
                        data = await _dbContext.Awardmasterdetail
                               .Include(x => x.Acquiredlandvillage)
                              //.Include(x => x.Us17)
                              //.Include(x => x.Us4)
                              //.Include(x => x.Us6)
                              .Include(x => x.Proposal)
                               .Where(x => string.IsNullOrEmpty(model.name) || x.AwardNumber.Contains(model.name))
                            .OrderByDescending(s => s.Proposal.Name)
                             .ThenBy(s => s.Acquiredlandvillage.Name)
                            .ThenBy(s => s.Proposal.Name)
                        .GetPaged<Awardmasterdetail>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;
        }

        public async Task<List<Awardmasterdetail>> Getawardmasterdetails()
        {
            return await _dbContext.Awardmasterdetail.Where(x => x.IsActive == 1).ToListAsync();
            //return await _dbContext.casenature.OrderByDescending(s => s.IsActive).ToListAsync();
        }
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Awardmasterdetail.AnyAsync(t => t.Id != id && t.AwardNumber.ToLower() == name.ToLower());
        }
        public async Task<List<Acquiredlandvillage>> Getvillage()
        {
            var villageList = await _dbContext.Acquiredlandvillage.Where(x => x.IsActive == 1).ToListAsync();
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
