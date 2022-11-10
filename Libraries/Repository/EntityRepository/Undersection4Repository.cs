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
namespace Libraries.Repository.EntityRepository
{
    public class Undersection4Repository : GenericRepository<Undersection4>, IUndersection4Repository
    {
        public Undersection4Repository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Proposaldetails>> GetAllProposal()
        {
            List<Proposaldetails> purposeList = await _dbContext.Proposaldetails.Where(x => x.IsActive == 1).ToListAsync();
            return purposeList;
        }
        public async Task<List<Undersection4>> GetAllUndersection4()
        {
            return await _dbContext.Undersection4.Include(x => x.Proposal).OrderByDescending(x => x.Id).ToListAsync();
        }


        public async Task<List<Undersection4>> GetAllUndersection4detailsList(Undersection4SearchDto model)
        {
            var data = await _dbContext.Undersection4.Include(x => x.Proposal).Where(x => (string.IsNullOrEmpty(model.name) || x.Proposal.Name.Contains(model.name))
                && (string.IsNullOrEmpty(model.notificationno) || x.Number.Contains(model.notificationno))
                 && (string.IsNullOrEmpty(model.type) || x.TypeDetails.Contains(model.type))).ToListAsync();
            return data; 
        }



        public async Task<bool> Any(int id, string number)
        {
            return await _dbContext.Undersection4.AnyAsync(t => t.Id != id && t.Number == number);
        }


        public async Task<PagedResult<Undersection4>> GetPagedUndersection4details(Undersection4SearchDto model)
        {
            var data = await _dbContext.Undersection4.Include(x => x.Proposal).Where(x => (string.IsNullOrEmpty(model.name) || x.Proposal.Name.Contains(model.name))
                && (string.IsNullOrEmpty(model.notificationno) || x.Number.Contains(model.notificationno))
                 && (string.IsNullOrEmpty(model.type) || x.TypeDetails.Contains(model.type))
            
               )
               .GetPaged<Undersection4>(model.PageNumber, model.PageSize);




            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Undersection4.Include(x => x.Proposal).Where(x => (string.IsNullOrEmpty(model.name) || x.Proposal.Name.Contains(model.name))
                && (string.IsNullOrEmpty(model.notificationno) || x.Number.Contains(model.notificationno))
                 && (string.IsNullOrEmpty(model.type) || x.TypeDetails.Contains(model.type))

               )
                                .OrderBy(s => s.Proposal.Name)
                                .GetPaged<Undersection4>(model.PageNumber, model.PageSize);
                        break;
                    case ("NUMBER"):
                        data = null;
                        data = await _dbContext.Undersection4.Include(x => x.Proposal).Where(x => (string.IsNullOrEmpty(model.name) || x.Proposal.Name.Contains(model.name))
                && (string.IsNullOrEmpty(model.notificationno) || x.Number.Contains(model.notificationno))
                 && (string.IsNullOrEmpty(model.type) || x.TypeDetails.Contains(model.type))

               )
                                 .OrderBy(s => s.Number)
                                .GetPaged<Undersection4>(model.PageNumber, model.PageSize);

                        break;
                    case ("TYPE"):
                        data = null;
                        data = await _dbContext.Undersection4.Include(x => x.Proposal).Where(x => (string.IsNullOrEmpty(model.name) || x.Proposal.Name.Contains(model.name))
               && (string.IsNullOrEmpty(model.notificationno) || x.Number.Contains(model.notificationno))
                && (string.IsNullOrEmpty(model.type) || x.TypeDetails.Contains(model.type))

               )
                                 .OrderBy(s => s.TypeDetails)
                                .GetPaged<Undersection4>(model.PageNumber, model.PageSize);

                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Undersection4.Include(x => x.Proposal).Where(x => (string.IsNullOrEmpty(model.name) || x.Proposal.Name.Contains(model.name))
               && (string.IsNullOrEmpty(model.notificationno) || x.Number.Contains(model.notificationno))
                && (string.IsNullOrEmpty(model.type) || x.TypeDetails.Contains(model.type))

               )
                                .OrderByDescending(s => s.IsActive)
                                .GetPaged<Undersection4>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Undersection4.Include(x => x.Proposal).Where(x => (string.IsNullOrEmpty(model.name) || x.Proposal.Name.Contains(model.name))
               && (string.IsNullOrEmpty(model.notificationno) || x.Number.Contains(model.notificationno))
                && (string.IsNullOrEmpty(model.type) || x.TypeDetails.Contains(model.type))

               )

                             .OrderByDescending(s => s.Proposal.Name)
                                .GetPaged<Undersection4>(model.PageNumber, model.PageSize);
                        break;
                    case ("NUMBER"):
                        data = null;
                        data = await _dbContext.Undersection4.Include(x => x.Proposal).Where(x => (string.IsNullOrEmpty(model.name) || x.Proposal.Name.Contains(model.name))
               && (string.IsNullOrEmpty(model.notificationno) || x.Number.Contains(model.notificationno))
                && (string.IsNullOrEmpty(model.type) || x.TypeDetails.Contains(model.type))

               )
                              .OrderByDescending(s => s.Number)
                                .GetPaged<Undersection4>(model.PageNumber, model.PageSize);

                        break;
                    case ("TYPE"):
                        data = null;
                        data = await _dbContext.Undersection4.Include(x => x.Proposal).Where(x => (string.IsNullOrEmpty(model.name) || x.Proposal.Name.Contains(model.name))
             && (string.IsNullOrEmpty(model.notificationno) || x.Number.Contains(model.notificationno))
              && (string.IsNullOrEmpty(model.type) || x.TypeDetails.Contains(model.type))

             )
                          .OrderByDescending(s => s.TypeDetails)
                                .GetPaged<Undersection4>(model.PageNumber, model.PageSize);

                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Undersection4.Include(x => x.Proposal).Where(x => (string.IsNullOrEmpty(model.name) || x.Proposal.Name.Contains(model.name))
             && (string.IsNullOrEmpty(model.notificationno) || x.Number.Contains(model.notificationno))
              && (string.IsNullOrEmpty(model.type) || x.TypeDetails.Contains(model.type))

             )
                                .OrderBy(s => s.IsActive)
                                .GetPaged<Undersection4>(model.PageNumber, model.PageSize);
                        break;

                }
            }






            return data;

        }

    }
}
