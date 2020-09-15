using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    
      public class ProposalplotdetailsRepository : GenericRepository<Proposalplotdetails>, IProposalplotdetailsRepository
    {
        public ProposalplotdetailsRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Proposalplotdetails>> GetProposalplotdetails()
        {
            return await _dbContext.Proposalplotdetails.ToListAsync();
        }
        public async Task<List<Proposalplotdetails>> GetAllProposalplotdetails()
        {
            return await _dbContext.Proposalplotdetails.Include(x => x.Proposaldetails).Include(x => x.Village).ToListAsync();


        }
        public async Task<List<Proposaldetails>> GetAllProposaldetails()
        {
            List<Proposaldetails> proposaldetailsList = await _dbContext.Proposaldetails.ToListAsync();
            return proposaldetailsList;
        }
        public async Task<List<Village>> GetAllVillage()
        {
            List<Village> villageList = await _dbContext.Village.ToListAsync();
            return villageList;
        }

        //public async Task<List<Khasra>> GetAllKhasra()
        //{
        //    List<Khasra> khasraList = await _dbContext.Khasra.ToListAsync();
        //    return khasraList;
        //}

        //public async Task<bool> Any(int id, string name)
        //{
        //    return await _dbContext.Proposaldetails.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        //}


    }
}
