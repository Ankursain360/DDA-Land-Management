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
using Dto.Search;
using System.Linq;

namespace Libraries.Repository.EntityRepository
{
   
      public class ProposaldetailsRepository : GenericRepository<Proposaldetails>, IProposaldetailsRepository
    {
        public ProposaldetailsRepository(DataContext dbContext) : base(dbContext)
        {

        }//date is not defined in dto???? where r u ishu???
        public async Task<PagedResult<Proposaldetails>> GetPagedProposaldetails(ProposaldetailsSearchDto model)
        {
            var data = await _dbContext.Proposaldetails
                 .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.requiredAgency) || x.RequiredAgency.Contains(model.requiredAgency))
                             && (string.IsNullOrEmpty(model.proposalFileNo) || x.ProposalFileNo.Contains(model.proposalFileNo))
                            /* && ( x.ProposalDate==model.proposalDate??x.ProposalDate)*/)
                .GetPaged<Proposaldetails>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Proposaldetails
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.requiredAgency) || x.RequiredAgency.Contains(model.requiredAgency))
                             && (string.IsNullOrEmpty(model.proposalFileNo) || x.ProposalFileNo.Contains(model.proposalFileNo))
                             /*&& model.proposalDate == x.ProposalDate*/)
                             .OrderBy(a => a.Name)
                             .GetPaged<Proposaldetails>(model.PageNumber, model.PageSize);
                           
                        break;
                    case ("BODY"):
                        data = null;
                        data = await _dbContext.Proposaldetails
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.requiredAgency) || x.RequiredAgency.Contains(model.requiredAgency))
                             && (string.IsNullOrEmpty(model.proposalFileNo) || x.ProposalFileNo.Contains(model.proposalFileNo))
                            /* && model.proposalDate == x.ProposalDate*/)
                             .OrderBy(a => a.RequiredAgency)
                             .GetPaged<Proposaldetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("FILE"):
                        data = null;
                        data = await _dbContext.Proposaldetails
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.requiredAgency) || x.RequiredAgency.Contains(model.requiredAgency))
                             && (string.IsNullOrEmpty(model.proposalFileNo) || x.ProposalFileNo.Contains(model.proposalFileNo))
                             /*&& model.proposalDate == x.ProposalDate*/)
                             .OrderBy(a => a.ProposalFileNo)
                             .GetPaged<Proposaldetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Proposaldetails
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.requiredAgency) || x.RequiredAgency.Contains(model.requiredAgency))
                             && (string.IsNullOrEmpty(model.proposalFileNo) || x.ProposalFileNo.Contains(model.proposalFileNo))
                            /* && model.proposalDate == x.ProposalDate*/)
                             .OrderBy(a => a.ProposalDate)
                             .GetPaged<Proposaldetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Proposaldetails
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.requiredAgency) || x.RequiredAgency.Contains(model.requiredAgency))
                             && (string.IsNullOrEmpty(model.proposalFileNo) || x.ProposalFileNo.Contains(model.proposalFileNo))
                             /*&& model.proposalDate == x.ProposalDate*/)
                            .OrderByDescending(a => a.IsActive)
                            .GetPaged<Proposaldetails>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Proposaldetails
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.requiredAgency) || x.RequiredAgency.Contains(model.requiredAgency))
                             && (string.IsNullOrEmpty(model.proposalFileNo) || x.ProposalFileNo.Contains(model.proposalFileNo))
                            /* && model.proposalDate == x.ProposalDate*/)
                             .OrderByDescending(a => a.Name)
                             .GetPaged<Proposaldetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("BODY"):
                        data = null;
                        data = await _dbContext.Proposaldetails
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.requiredAgency) || x.RequiredAgency.Contains(model.requiredAgency))
                             && (string.IsNullOrEmpty(model.proposalFileNo) || x.ProposalFileNo.Contains(model.proposalFileNo))
                             /*&& model.proposalDate == x.ProposalDate*/)
                             .OrderByDescending(a => a.RequiredAgency)
                             .GetPaged<Proposaldetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("FILE"):
                        data = null;
                        data = await _dbContext.Proposaldetails
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.requiredAgency) || x.RequiredAgency.Contains(model.requiredAgency))
                             && (string.IsNullOrEmpty(model.proposalFileNo) || x.ProposalFileNo.Contains(model.proposalFileNo))
                            /* && model.proposalDate == x.ProposalDate*/)
                             .OrderByDescending(a => a.ProposalFileNo)
                             .GetPaged<Proposaldetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Proposaldetails
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.requiredAgency) || x.RequiredAgency.Contains(model.requiredAgency))
                             && (string.IsNullOrEmpty(model.proposalFileNo) || x.ProposalFileNo.Contains(model.proposalFileNo))
                             /*&& model.proposalDate == x.ProposalDate*/)
                             .OrderByDescending(a => a.ProposalDate)
                             .GetPaged<Proposaldetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Proposaldetails
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.requiredAgency) || x.RequiredAgency.Contains(model.requiredAgency))
                             && (string.IsNullOrEmpty(model.proposalFileNo) || x.ProposalFileNo.Contains(model.proposalFileNo))
                             /*&& model.proposalDate == x.ProposalDate*/)
                            .OrderBy(a => a.IsActive)
                            .GetPaged<Proposaldetails>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;
        }
        public async Task<List<Proposaldetails>> GetProposaldetails()
        {
            return await _dbContext.Proposaldetails.ToListAsync();
        }
        public async Task<List<Proposaldetails>> GetAllProposaldetails()
        {
            return await _dbContext.Proposaldetails.Include(x => x.Scheme).ToListAsync();

           
        }
        public async Task<List<Scheme>> GetAllScheme()
        {
            List<Scheme> schemeList = await _dbContext.Scheme.ToListAsync();
            return schemeList;
        }
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Proposaldetails.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }


    }
}
