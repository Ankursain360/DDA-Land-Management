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

    public class NewlandProposaldetailsRepository : GenericRepository<Newlandacquistionproposaldetails>, INewlandProposaldetailsRepository 
    {
        public NewlandProposaldetailsRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Newlandacquistionproposaldetails>> GetPagedProposaldetails(NewlandacquistionproposaldetailsSearchDto model)
        {
            var data = await _dbContext.Newlandacquistionproposaldetails
                 .Include(x => x.Scheme)
                 .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.requiredAgency) || x.RequiredAgency.Contains(model.requiredAgency))
                             && (string.IsNullOrEmpty(model.proposalFileNo) || x.ProposalFileNo.Contains(model.proposalFileNo))
                            /* && ( x.ProposalDate==model.proposalDate??x.ProposalDate)*/)
                .GetPaged<Newlandacquistionproposaldetails>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("SCHEME"):
                        data = null;
                        data = await _dbContext.Newlandacquistionproposaldetails
                            .Include(x => x.Scheme)
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.requiredAgency) || x.RequiredAgency.Contains(model.requiredAgency))
                             && (string.IsNullOrEmpty(model.proposalFileNo) || x.ProposalFileNo.Contains(model.proposalFileNo)))
                             //&& (model.proposalDate == x.ProposalDate? model.proposalDate: x.ProposalDate))
                             .OrderBy(a => a.Scheme.Name)
                             .GetPaged<Newlandacquistionproposaldetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Newlandacquistionproposaldetails
                            .Include(x => x.Scheme)
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.requiredAgency) || x.RequiredAgency.Contains(model.requiredAgency))
                             && (string.IsNullOrEmpty(model.proposalFileNo) || x.ProposalFileNo.Contains(model.proposalFileNo))
                             /*&& model.proposalDate == x.ProposalDate*/)
                             .OrderBy(a => a.Name)
                             .GetPaged<Newlandacquistionproposaldetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("BODY"):
                        data = null;
                        data = await _dbContext.Newlandacquistionproposaldetails
                            .Include(x => x.Scheme)
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.requiredAgency) || x.RequiredAgency.Contains(model.requiredAgency))
                             && (string.IsNullOrEmpty(model.proposalFileNo) || x.ProposalFileNo.Contains(model.proposalFileNo))
                            /* && model.proposalDate == x.ProposalDate*/)
                             .OrderBy(a => a.RequiredAgency)
                             .GetPaged<Newlandacquistionproposaldetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("FILE"):
                        data = null;
                        data = await _dbContext.Newlandacquistionproposaldetails
                            .Include(x => x.Scheme)
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.requiredAgency) || x.RequiredAgency.Contains(model.requiredAgency))
                             && (string.IsNullOrEmpty(model.proposalFileNo) || x.ProposalFileNo.Contains(model.proposalFileNo))
                             /*&& model.proposalDate == x.ProposalDate*/)
                             .OrderBy(a => a.ProposalFileNo)
                             .GetPaged<Newlandacquistionproposaldetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Newlandacquistionproposaldetails
                            .Include(x => x.Scheme)
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.requiredAgency) || x.RequiredAgency.Contains(model.requiredAgency))
                             && (string.IsNullOrEmpty(model.proposalFileNo) || x.ProposalFileNo.Contains(model.proposalFileNo))
                            /* && model.proposalDate == x.ProposalDate*/)
                             .OrderBy(a => a.ProposalDate)
                             .GetPaged<Newlandacquistionproposaldetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandacquistionproposaldetails
                            .Include(x => x.Scheme)
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.requiredAgency) || x.RequiredAgency.Contains(model.requiredAgency))
                             && (string.IsNullOrEmpty(model.proposalFileNo) || x.ProposalFileNo.Contains(model.proposalFileNo))
                             /*&& model.proposalDate == x.ProposalDate*/)
                            .OrderByDescending(a => a.IsActive)
                            .GetPaged<Newlandacquistionproposaldetails>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("SCHEME"):
                        data = null;
                        data = await _dbContext.Newlandacquistionproposaldetails
                            .Include(x => x.Scheme)
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.requiredAgency) || x.RequiredAgency.Contains(model.requiredAgency))
                             && (string.IsNullOrEmpty(model.proposalFileNo) || x.ProposalFileNo.Contains(model.proposalFileNo)))
                             //&& (model.proposalDate == x.ProposalDate? model.proposalDate: x.ProposalDate))
                             .OrderByDescending(a => a.Scheme.Name)
                             .GetPaged<Newlandacquistionproposaldetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Newlandacquistionproposaldetails
                            .Include(x => x.Scheme)
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.requiredAgency) || x.RequiredAgency.Contains(model.requiredAgency))
                             && (string.IsNullOrEmpty(model.proposalFileNo) || x.ProposalFileNo.Contains(model.proposalFileNo))
                            /* && model.proposalDate == x.ProposalDate*/)
                             .OrderByDescending(a => a.Name)
                             .GetPaged<Newlandacquistionproposaldetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("BODY"):
                        data = null;
                        data = await _dbContext.Newlandacquistionproposaldetails
                            .Include(x => x.Scheme)
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.requiredAgency) || x.RequiredAgency.Contains(model.requiredAgency))
                             && (string.IsNullOrEmpty(model.proposalFileNo) || x.ProposalFileNo.Contains(model.proposalFileNo))
                             /*&& model.proposalDate == x.ProposalDate*/)
                             .OrderByDescending(a => a.RequiredAgency)
                             .GetPaged<Newlandacquistionproposaldetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("FILE"):
                        data = null;
                        data = await _dbContext.Newlandacquistionproposaldetails
                            .Include(x => x.Scheme)
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.requiredAgency) || x.RequiredAgency.Contains(model.requiredAgency))
                             && (string.IsNullOrEmpty(model.proposalFileNo) || x.ProposalFileNo.Contains(model.proposalFileNo))
                            /* && model.proposalDate == x.ProposalDate*/)
                             .OrderByDescending(a => a.ProposalFileNo)
                             .GetPaged<Newlandacquistionproposaldetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Newlandacquistionproposaldetails
                            .Include(x => x.Scheme)
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.requiredAgency) || x.RequiredAgency.Contains(model.requiredAgency))
                             && (string.IsNullOrEmpty(model.proposalFileNo) || x.ProposalFileNo.Contains(model.proposalFileNo))
                             /*&& model.proposalDate == x.ProposalDate*/)
                             .OrderByDescending(a => a.ProposalDate)
                             .GetPaged<Newlandacquistionproposaldetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandacquistionproposaldetails
                            .Include(x => x.Scheme)
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.requiredAgency) || x.RequiredAgency.Contains(model.requiredAgency))
                             && (string.IsNullOrEmpty(model.proposalFileNo) || x.ProposalFileNo.Contains(model.proposalFileNo))
                             /*&& model.proposalDate == x.ProposalDate*/)
                            .OrderBy(a => a.IsActive)
                            .GetPaged<Newlandacquistionproposaldetails>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;
        }
        public async Task<List<Newlandacquistionproposaldetails>> GetProposaldetails()
        {
            return await _dbContext.Newlandacquistionproposaldetails.Include(x => x.Scheme).ToListAsync();
        }
        public async Task<List<Newlandacquistionproposaldetails>> GetAllProposaldetails()
        {
            return await _dbContext.Newlandacquistionproposaldetails.Include(x => x.Scheme).ToListAsync();


        }
        public async Task<List<Newlandscheme>> GetAllScheme()
        {
            List<Newlandscheme> schemeList = await _dbContext.Newlandscheme.Where(x => x.IsActive == 1).ToListAsync();
            return schemeList;
        }
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Newlandacquistionproposaldetails.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }


    }
}
