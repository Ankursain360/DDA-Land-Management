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

namespace Libraries.Repository.EntityRepository
{
    
      public class ProposalplotdetailsRepository : GenericRepository<Proposalplotdetails>, IProposalplotdetailsRepository
    {
        public ProposalplotdetailsRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Proposalplotdetails>> GetPagedProposalplotdetails(ProposalplotdetailSearchDto model)
        {
            return await _dbContext.Proposalplotdetails
                .Include(x => x.Proposaldetails)
                .Include(x => x.Locality)
                .Include(x => x.Khasra)
                .Where(x => x.IsActive==1)
                .GetPaged<Proposalplotdetails>(model.PageNumber, model.PageSize);
        }
        public async Task<List<Proposalplotdetails>> GetProposalplotdetails()
        {
            return await _dbContext.Proposalplotdetails.ToListAsync();
        }
        public async Task<List<Proposalplotdetails>> GetAllProposalplotdetails()
        {
            return await _dbContext.Proposalplotdetails
                .Include(x => x.Proposaldetails)
                .Include(x => x.Locality)
                .Include(x => x.Khasra)
                .Where(x => x.IsActive == 1).ToListAsync();


        }
        public async Task<List<Proposaldetails>> GetAllProposaldetails()
        {
            List<Proposaldetails> proposaldetailsList = await _dbContext.Proposaldetails.Where(x => x.IsActive == 1).ToListAsync();
            return proposaldetailsList;
        }
        public async Task<List<Locality>> GetAllLocality()
        {
            List<Locality> localityList = await _dbContext.Locality.Where(x => x.IsActive==1).ToListAsync();
            return localityList;
        }

        public async Task<List<Khasra>> GetAllKhasra()
        {
            List<Khasra> khasraList = await _dbContext.Khasra.Where(x => x.IsActive == 1).ToListAsync();
            return khasraList;
        }

        //public async Task<bool> Any(int id, string name)
        //{
        //    return await _dbContext.Proposaldetails.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        //}


    }
}
