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

    public class NewLandProposalPlotDetailsRepository : GenericRepository<Newlandacquistionproposalplotdetails>, INewLandProposalPlotDetailsRepository
    {
        public NewLandProposalPlotDetailsRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Newlandacquistionproposalplotdetails>> GetPagedProposalplotdetails(NewLandProposalplotdetailSearchDto model)
        {
            var data = await _dbContext.Newlandacquistionproposalplotdetails
                         .Include(x => x.Proposaldetails)
                         .Include(x => x.Acquiredlandvillage)
                         .Include(x => x.Khasra)
                         .Where(x => (string.IsNullOrEmpty(model.name) || x.Proposaldetails.Name.Contains(model.name))
                          && (string.IsNullOrEmpty(model.locality) || x.Acquiredlandvillage.Name.Contains(model.locality))
                          && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                         .GetPaged<Newlandacquistionproposalplotdetails>(model.PageNumber, model.PageSize);


            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Newlandacquistionproposalplotdetails
                                     .Include(x => x.Proposaldetails)
                                     .Include(x => x.Acquiredlandvillage)
                                     .Include(x => x.Khasra)
                                     .Where(x => (string.IsNullOrEmpty(model.name) || x.Proposaldetails.Name.Contains(model.name))
                                     && (string.IsNullOrEmpty(model.locality) || x.Acquiredlandvillage.Name.Contains(model.locality))
                                     && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderBy(a => a.Proposaldetails.Name)
                                    .GetPaged<Newlandacquistionproposalplotdetails>(model.PageNumber, model.PageSize);
                        break;

                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Newlandacquistionproposalplotdetails
                                     .Include(x => x.Proposaldetails)
                                     .Include(x => x.Acquiredlandvillage)
                                     .Include(x => x.Khasra)
                                     .Where(x => (string.IsNullOrEmpty(model.name) || x.Proposaldetails.Name.Contains(model.name))
                                     && (string.IsNullOrEmpty(model.locality) || x.Acquiredlandvillage.Name.Contains(model.locality))
                                     && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderBy(a => a.Acquiredlandvillage.Name)
                                    .GetPaged<Newlandacquistionproposalplotdetails>(model.PageNumber, model.PageSize);
                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Newlandacquistionproposalplotdetails
                                     .Include(x => x.Proposaldetails)
                                     .Include(x => x.Acquiredlandvillage)
                                     .Include(x => x.Khasra)
                                     .Where(x => (string.IsNullOrEmpty(model.name) || x.Proposaldetails.Name.Contains(model.name))
                                     && (string.IsNullOrEmpty(model.locality) || x.Acquiredlandvillage.Name.Contains(model.locality))
                                     && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderBy(a => a.Khasra.Name)
                                    .GetPaged<Newlandacquistionproposalplotdetails>(model.PageNumber, model.PageSize);
                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandacquistionproposalplotdetails
                                   .Include(x => x.Proposaldetails)
                                   .Include(x => x.Acquiredlandvillage)
                                   .Include(x => x.Khasra)
                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Proposaldetails.Name.Contains(model.name))
                                     && (string.IsNullOrEmpty(model.locality) || x.Acquiredlandvillage.Name.Contains(model.locality))
                                     && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderByDescending(a => a.IsActive)
                                    .GetPaged<Newlandacquistionproposalplotdetails>(model.PageNumber, model.PageSize);
                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Newlandacquistionproposalplotdetails
                                     .Include(x => x.Proposaldetails)
                                     .Include(x => x.Acquiredlandvillage)
                                     .Include(x => x.Khasra)
                                     .Where(x => (string.IsNullOrEmpty(model.name) || x.Proposaldetails.Name.Contains(model.name))
                                     && (string.IsNullOrEmpty(model.locality) || x.Acquiredlandvillage.Name.Contains(model.locality))
                                     && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderByDescending(a => a.Proposaldetails.Name)
                                    .GetPaged<Newlandacquistionproposalplotdetails>(model.PageNumber, model.PageSize);
                        break;

                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Newlandacquistionproposalplotdetails
                                     .Include(x => x.Proposaldetails)
                                     .Include(x => x.Acquiredlandvillage)
                                     .Include(x => x.Khasra)
                                     .Where(x => (string.IsNullOrEmpty(model.name) || x.Proposaldetails.Name.Contains(model.name))
                                     && (string.IsNullOrEmpty(model.locality) || x.Acquiredlandvillage.Name.Contains(model.locality))
                                     && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderByDescending(a => a.Acquiredlandvillage.Name)
                                    .GetPaged<Newlandacquistionproposalplotdetails>(model.PageNumber, model.PageSize);
                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Newlandacquistionproposalplotdetails
                                     .Include(x => x.Proposaldetails)
                                     .Include(x => x.Acquiredlandvillage)
                                     .Include(x => x.Khasra)
                                     .Where(x => (string.IsNullOrEmpty(model.name) || x.Proposaldetails.Name.Contains(model.name))
                                     && (string.IsNullOrEmpty(model.locality) || x.Acquiredlandvillage.Name.Contains(model.locality))
                                     && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderByDescending(a => a.Khasra.Name)
                                    .GetPaged<Newlandacquistionproposalplotdetails>(model.PageNumber, model.PageSize);
                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandacquistionproposalplotdetails
                                     .Include(x => x.Proposaldetails)
                                     .Include(x => x.Acquiredlandvillage)
                                     .Include(x => x.Khasra)
                                     .Where(x => (string.IsNullOrEmpty(model.name) || x.Proposaldetails.Name.Contains(model.name))
                                     && (string.IsNullOrEmpty(model.locality) || x.Acquiredlandvillage.Name.Contains(model.locality))
                                     && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderBy(a => a.IsActive)
                                    .GetPaged<Newlandacquistionproposalplotdetails>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;
        }
        public async Task<List<Newlandacquistionproposalplotdetails>> GetProposalplotdetails()
        {
            return await _dbContext.Newlandacquistionproposalplotdetails.ToListAsync();
        }
        public async Task<List<Newlandacquistionproposalplotdetails>> GetAllProposalplotdetails()
        {
            return await _dbContext.Newlandacquistionproposalplotdetails
                .Include(x => x.Proposaldetails)
                .Include(x => x.Acquiredlandvillage)
                .Include(x => x.Khasra)
                .ToListAsync();


        }
        
        public async Task<List<Newlandacquistionproposaldetails>> GetAllProposaldetails()
        {
            List<Newlandacquistionproposaldetails> proposaldetailsList = await _dbContext.Newlandacquistionproposaldetails.ToListAsync();
            return proposaldetailsList;
        }
       

        public async Task<List<Newlandvillage>> GetAllVillage()
        {
            List<Newlandvillage> villageList = await _dbContext.Newlandvillage.Where(x => x.IsActive == 1).ToListAsync();
            return villageList;
        }
        public async Task<List<Newlandkhasra>> GetAllKhasra(int? villageId)
        {
            List<Newlandkhasra> khasraList = await _dbContext.Newlandkhasra.Where(x => x.NewLandvillageId == villageId && x.IsActive == 1).ToListAsync();
            return khasraList;
        }

        public async Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId)
        {
            return await _dbContext.Newlandkhasra.Where(x => x.Id == khasraId).SingleOrDefaultAsync();
        }



    }
}
