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

    public class LicenceFeesRepository : GenericRepository<Licencefees>, ILicenceFeesRepository
    {
        public LicenceFeesRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Licencefees>> GetPagedLicencefees(LicencefeesSearchDto model)
        {
            var data = await _dbContext.Licencefees.Include(x => x.LeasePurposesType)
                       .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub))) 
                            .GetPaged<Licencefees>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("TYPE"):
                        data = null;
                        data = await _dbContext.Licencefees
                                                .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)))
                                               .OrderBy(x => x.LeasePurposesType.PurposeUse)
                                               .GetPaged<Licencefees>(model.PageNumber, model.PageSize);
                        break;
                    case ("RATE"):
                        data = null;
                        data = await _dbContext.Licencefees
                                               .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)))
                                                .OrderBy(x => x.LicenceFees)
                                               .GetPaged<Licencefees>(model.PageNumber, model.PageSize);
                        break;
                    case ("FROM"):
                        data = null;
                        data = await _dbContext.Licencefees
                                               .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)))
                                               .OrderBy(x => x.FromDate)
                                               .GetPaged<Licencefees>(model.PageNumber, model.PageSize);
                        break;
                    case ("TO"):
                        data = null;
                        data = await _dbContext.Licencefees
                                              .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)))
                                                .OrderBy(x => x.ToDate)
                                               .GetPaged<Licencefees>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Licencefees
                                               .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)))
                                               .OrderByDescending(x => x.IsActive)
                                               .GetPaged<Licencefees>(model.PageNumber, model.PageSize);
                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("TYPE"):
                        data = null;
                        data = await _dbContext.Licencefees
                                              .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)))
                                               .OrderByDescending(x => x.LeasePurposesType.PurposeUse)
                                               .GetPaged<Licencefees>(model.PageNumber, model.PageSize);
                        break;
                    case ("RATE"):
                        data = null;
                        data = await _dbContext.Licencefees
                                              .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)))
                                               .OrderByDescending(x => x.LicenceFees)
                                               .GetPaged<Licencefees>(model.PageNumber, model.PageSize);
                        break;
                    case ("FROM"):
                        data = null;
                        data = await _dbContext.Licencefees
                                              .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)))
                                                .OrderByDescending(x => x.FromDate)
                                               .GetPaged<Licencefees>(model.PageNumber, model.PageSize);
                        break;
                    case ("TO"):
                        data = null;
                        data = await _dbContext.Licencefees
                                               .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)))
                                               .OrderByDescending(x => x.ToDate)
                                               .GetPaged<Licencefees>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Licencefees
                                               .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)))
                                               .OrderBy(x => x.IsActive)
                                               .GetPaged<Licencefees>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;

        }

        public async Task<List<Leasepurpose>> GetAllLeasepurpose()
        {
            List<Leasepurpose> leasePurposeList = await _dbContext.Leasepurpose.Where(x => x.IsActive == 1).ToListAsync();
            return leasePurposeList;
        }
        public async Task<List<Leasesubpurpose>> GetAllLeaseSubpurpose(int purposeId)
        {
            List<Leasesubpurpose> leaseSubPurposeList = await _dbContext.Leasesubpurpose.Where(x => x.PurposeUseId == purposeId && x.IsActive == 1).ToListAsync();
            return leaseSubPurposeList;
        }

        public async Task<List<Licencefees>> GetAllLicencefees()
        {
            return await _dbContext.Licencefees.Include(x => x.LeasePurposesType).Include(x => x.LeaseSubPurpose).Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Licencefees>> GetAllLicencefeesList()
        {
            return await _dbContext.Licencefees.Include(x => x.LeasePurposesType).Include(x => x.LeaseSubPurpose).ToListAsync();
        }

    }
}
