using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Repository.EntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
   
      public class ProposaldetailsRepository : GenericRepository<Proposaldetails>, IProposaldetailsRepository
    {
        public ProposaldetailsRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Proposaldetails>> GetProposaldetails()
        {
            return await _dbContext.Proposaldetails.ToListAsync();
        }
        public async Task<List<Proposaldetails>> GetAllProposaldetails()
        {
            //return await _dbContext.Page.Include(x => x.Scheme).ToListAsync();

            return await _dbContext.Proposaldetails.ToListAsync();
        }
        //public async Task<List<Module>> GetAllModule()
        //{
        //    List<Module> moduleList = await _dbContext.Module.ToListAsync();
        //    return moduleList;
        //}
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Proposaldetails.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }


    }
}
