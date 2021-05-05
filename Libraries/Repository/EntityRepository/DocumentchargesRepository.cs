


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

    public class DocumentchargesRepository : GenericRepository<Documentcharges>, IDocumentchargesRepository
    {
        public DocumentchargesRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Documentcharges>> GetPagedDocumentcharges(DocumentchargesSearchDto model)
        {
            var data = await _dbContext.Documentcharges.Include(x => x.LeasePurposesType)
                       .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)) &&

                  x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                    && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                            .GetPaged<Documentcharges>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("TYPE"):
                        data = null;
                        data = await _dbContext.Documentcharges
                                                .Include(x => x.LeasePurposesType)
                            .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)) &&

                  x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                    && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                               .OrderBy(x => x.LeasePurposesType.PurposeUse)
                                               .GetPaged<Documentcharges>(model.PageNumber, model.PageSize);
                        break;
                    case ("CHARGE"):
                        data = null;
                        data = await _dbContext.Documentcharges
                                               .Include(x => x.LeasePurposesType)
                            .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)) &&

                  x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                    && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                                .OrderBy(x => x.DocumentCharge)
                                               .GetPaged<Documentcharges>(model.PageNumber, model.PageSize);
                        break;
                    case ("FROM"):
                        data = null;
                        data = await _dbContext.Documentcharges
                                              .Include(x => x.LeasePurposesType)
                            .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)) &&

                  x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                    && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                               .OrderBy(x => x.FromDate)
                                               .GetPaged<Documentcharges>(model.PageNumber, model.PageSize);
                        break;
                    case ("TO"):
                        data = null;
                        data = await _dbContext.Documentcharges
                                               .Include(x => x.LeasePurposesType)
                            .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)) &&

                  x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                    && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                                .OrderBy(x => x.ToDate)
                                               .GetPaged<Documentcharges>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Documentcharges
                                                .Include(x => x.LeasePurposesType)
                            .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)) &&

                  x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                    && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                               .OrderByDescending(x => x.IsActive)
                                               .GetPaged<Documentcharges>(model.PageNumber, model.PageSize);
                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("TYPE"):
                        data = null;
                        data = await _dbContext.Documentcharges
                                                .Include(x => x.LeasePurposesType)
                            .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)) &&

                  x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                    && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                               .OrderByDescending(x => x.LeasePurposesType.PurposeUse)
                                               .GetPaged<Documentcharges>(model.PageNumber, model.PageSize);
                        break;
                    case ("CHARGE"):
                        data = null;
                        data = await _dbContext.Documentcharges
                                              .Include(x => x.LeasePurposesType)
                            .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)) &&

                  x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                    && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                               .OrderByDescending(x => x.DocumentCharge)
                                               .GetPaged<Documentcharges>(model.PageNumber, model.PageSize);
                        break;
                    case ("FROM"):
                        data = null;
                        data = await _dbContext.Documentcharges
                                               .Include(x => x.LeasePurposesType)
                            .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)) &&

                  x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                    && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                                .OrderByDescending(x => x.FromDate)
                                               .GetPaged<Documentcharges>(model.PageNumber, model.PageSize);
                        break;
                    case ("TO"):
                        data = null;
                        data = await _dbContext.Documentcharges
                                               .Include(x => x.LeasePurposesType)
                            .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)) &&

                  x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                    && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                               .OrderByDescending(x => x.ToDate)
                                               .GetPaged<Documentcharges>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Documentcharges
                                                .Include(x => x.LeasePurposesType)
                            .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)) &&

                  x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                    && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                               .OrderBy(x => x.IsActive)
                                               .GetPaged<Documentcharges>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;

        }

        //public async Task<List<PropertyType>> GetAllPropertyType()
        //{
        //    List<PropertyType> list = await _dbContext.PropertyType.Where(x => x.IsActive == 1).ToListAsync();
        //    return list;
        //}
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
        public async Task<List<Documentcharges>> GetAllDocumentcharges()
        {
            return await _dbContext.Documentcharges.Include(x => x.LeasePurposesType).Include(x => x.LeaseSubPurpose).Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<List<Documentcharges>> GetAllDocumentchargesList()
        {
            return await _dbContext.Documentcharges.Include(x => x.LeasePurposesType).Include(x => x.LeaseSubPurpose).ToListAsync();
        }

    }
}
