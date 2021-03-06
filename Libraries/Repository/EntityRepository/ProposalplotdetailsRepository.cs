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
            var data = await _dbContext.Proposalplotdetails
                         .Include(x => x.Proposaldetails)
                         .Include(x => x.Acquiredlandvillage)
                         .Include(x => x.Khasra)
                         .Where(x => (string.IsNullOrEmpty(model.name) || x.Proposaldetails.Name.Contains(model.name))
                          && (string.IsNullOrEmpty(model.locality) || x.Acquiredlandvillage.Name.Contains(model.locality))
                          && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                         .GetPaged<Proposalplotdetails>(model.PageNumber, model.PageSize);


            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Proposalplotdetails
                                     .Include(x => x.Proposaldetails)
                                     .Include(x => x.Acquiredlandvillage)
                                     .Include(x => x.Khasra)
                                     .Where(x => (string.IsNullOrEmpty(model.name) || x.Proposaldetails.Name.Contains(model.name))
                                     && (string.IsNullOrEmpty(model.locality) || x.Acquiredlandvillage.Name.Contains(model.locality))
                                     && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderBy(a => a.Proposaldetails.Name)
                                    .GetPaged<Proposalplotdetails>(model.PageNumber, model.PageSize);
                        break;

                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Proposalplotdetails
                                     .Include(x => x.Proposaldetails)
                                     .Include(x => x.Acquiredlandvillage)
                                     .Include(x => x.Khasra)
                                     .Where(x => (string.IsNullOrEmpty(model.name) || x.Proposaldetails.Name.Contains(model.name))
                                     && (string.IsNullOrEmpty(model.locality) || x.Acquiredlandvillage.Name.Contains(model.locality))
                                     && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderBy(a => a.Acquiredlandvillage.Name)
                                    .GetPaged<Proposalplotdetails>(model.PageNumber, model.PageSize);
                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Proposalplotdetails
                                     .Include(x => x.Proposaldetails)
                                     .Include(x => x.Acquiredlandvillage)
                                     .Include(x => x.Khasra)
                                     .Where(x => (string.IsNullOrEmpty(model.name) || x.Proposaldetails.Name.Contains(model.name))
                                     && (string.IsNullOrEmpty(model.locality) || x.Acquiredlandvillage.Name.Contains(model.locality))
                                     && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderBy(a => a.Khasra.Name)
                                    .GetPaged<Proposalplotdetails>(model.PageNumber, model.PageSize);
                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Proposalplotdetails
                                     .Include(x => x.Proposaldetails)
                                     .Include(x => x.Acquiredlandvillage)
                                     .Include(x => x.Khasra)
                                     .Where(x => (string.IsNullOrEmpty(model.name) || x.Proposaldetails.Name.Contains(model.name))
                                     && (string.IsNullOrEmpty(model.locality) || x.Acquiredlandvillage.Name.Contains(model.locality))
                                     && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderByDescending(a => a.IsActive)
                                    .GetPaged<Proposalplotdetails>(model.PageNumber, model.PageSize);
                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Proposalplotdetails
                                     .Include(x => x.Proposaldetails)
                                     .Include(x => x.Acquiredlandvillage)
                                     .Include(x => x.Khasra)
                                     .Where(x => (string.IsNullOrEmpty(model.name) || x.Proposaldetails.Name.Contains(model.name))
                                     && (string.IsNullOrEmpty(model.locality) || x.Acquiredlandvillage.Name.Contains(model.locality))
                                     && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderByDescending(a => a.Proposaldetails.Name)
                                    .GetPaged<Proposalplotdetails>(model.PageNumber, model.PageSize);
                        break;

                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Proposalplotdetails
                                     .Include(x => x.Proposaldetails)
                                     .Include(x => x.Acquiredlandvillage)
                                     .Include(x => x.Khasra)
                                     .Where(x => (string.IsNullOrEmpty(model.name) || x.Proposaldetails.Name.Contains(model.name))
                                     && (string.IsNullOrEmpty(model.locality) || x.Acquiredlandvillage.Name.Contains(model.locality))
                                     && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderByDescending(a => a.Acquiredlandvillage.Name)
                                    .GetPaged<Proposalplotdetails>(model.PageNumber, model.PageSize);
                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Proposalplotdetails
                                     .Include(x => x.Proposaldetails)
                                     .Include(x => x.Acquiredlandvillage)
                                     .Include(x => x.Khasra)
                                     .Where(x => (string.IsNullOrEmpty(model.name) || x.Proposaldetails.Name.Contains(model.name))
                                     && (string.IsNullOrEmpty(model.locality) || x.Acquiredlandvillage.Name.Contains(model.locality))
                                     && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderByDescending(a => a.Khasra.Name)
                                    .GetPaged<Proposalplotdetails>(model.PageNumber, model.PageSize);
                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Proposalplotdetails
                                     .Include(x => x.Proposaldetails)
                                     .Include(x => x.Acquiredlandvillage)
                                     .Include(x => x.Khasra)
                                     .Where(x => (string.IsNullOrEmpty(model.name) || x.Proposaldetails.Name.Contains(model.name))
                                     && (string.IsNullOrEmpty(model.locality) || x.Acquiredlandvillage.Name.Contains(model.locality))
                                     && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderBy(a => a.IsActive)
                                    .GetPaged<Proposalplotdetails>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;
        }
        public async Task<List<Proposalplotdetails>> GetProposalplotdetails()
        {
            return await _dbContext.Proposalplotdetails.ToListAsync();
        }
        public async Task<List<Proposalplotdetails>> GetAllProposalplotdetails()
        {
            return await _dbContext.Proposalplotdetails
                .Include(x => x.Proposaldetails)
                .Include(x => x.Acquiredlandvillage)
                .Include(x => x.Khasra)
                .ToListAsync();


        }
        public async Task<List<Proposaldetails>> GetAllProposaldetails()
        {
            List<Proposaldetails> proposaldetailsList = await _dbContext.Proposaldetails.Where(x => x.IsActive == 1).ToListAsync();
            return proposaldetailsList;
        }
        public async Task<List<Acquiredlandvillage>> GetAllVillage()
        {
            List<Acquiredlandvillage> villageList = await _dbContext.Acquiredlandvillage.Where(x => x.IsActive==1).ToListAsync();
            return villageList;
        }
      

      
        public async Task<List<Khasra>> GetAllKhasra(int? villageId)
        {
            List<Khasra> khasraList = await _dbContext.Khasra.Where(x => x.AcquiredlandvillageId == villageId && x.IsActive == 1).ToListAsync();
            return khasraList;
        }

        public async Task<Khasra> FetchSingleKhasraResult(int? khasraId)
        {
            return await _dbContext.Khasra.Where(x => x.Id == khasraId).SingleOrDefaultAsync();
        }


    }
}
