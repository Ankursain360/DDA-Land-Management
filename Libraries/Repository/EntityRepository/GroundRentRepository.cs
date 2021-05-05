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

    public class GroundRentRepository : GenericRepository<Groundrent>, IGroundRentRepository
    {
        public GroundRentRepository(DataContext dbContext) : base(dbContext)
        {

        }
        //public async Task<List<PropertyType>> GetAllPropertyTypeList()
        //{
        //    return await _dbContext.PropertyType.Where(x=> x.IsActive==1).OrderByDescending(x => x.Id).ToListAsync();
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
        public async Task<PagedResult<Groundrent>> GetPagedGroundRent(GroundrentSearchDto model)
        {
            var data = await _dbContext.Groundrent.Include(x => x.LeasePurposesType)
                       .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)) &&

                  x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                    && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                            .GetPaged<Groundrent>(model.PageNumber, model.PageSize);
            if (model.SortBy == null)
            {
                model.SortBy = "Type";
            }

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("TYPE"):
                        data = null;
                        data = await _dbContext.Groundrent
                                               .Include(x => x.LeasePurposesType)
                            .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)) &&

                  x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                    && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                               .OrderBy(x => x.LeasePurposesType.PurposeUse)
                                               .GetPaged<Groundrent>(model.PageNumber, model.PageSize);
                        break;
                    case ("RATE"):
                        data = null;
                        data = await _dbContext.Groundrent
                                              .Include(x => x.LeasePurposesType)
                            .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)) &&

                  x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                    && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                                .OrderBy(x => x.GroundRate)
                                               .GetPaged<Groundrent>(model.PageNumber, model.PageSize);
                        break;
                    case ("FROM"):
                        data = null;
                        data = await _dbContext.Groundrent
                                              .Include(x => x.LeasePurposesType)
                            .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)) &&

                  x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                    && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                               .OrderBy(x => x.FromDate)
                                               .GetPaged<Groundrent>(model.PageNumber, model.PageSize);
                        break;
                    case ("TO"):
                        data = null;
                        data = await _dbContext.Groundrent
                                              .Include(x => x.LeasePurposesType)
                            .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)) &&

                  x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                    && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                                .OrderBy(x => x.ToDate)
                                               .GetPaged<Groundrent>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Groundrent
                                               .Include(x => x.LeasePurposesType)
                            .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)) &&

                  x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                    && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                               .OrderByDescending(x => x.IsActive)
                                               .GetPaged<Groundrent>(model.PageNumber, model.PageSize);
                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("TYPE"):
                        data = null;
                        data = await _dbContext.Groundrent
                                               .Include(x => x.LeasePurposesType)
                            .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)) &&

                  x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                    && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                               .OrderByDescending(x => x.LeasePurposesType.PurposeUse)
                                               .GetPaged<Groundrent>(model.PageNumber, model.PageSize);
                        break;
                    case ("RATE"):
                        data = null;
                        data = await _dbContext.Groundrent
                                              .Include(x => x.LeasePurposesType)
                            .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)) &&

                  x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                    && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                               .OrderByDescending(x => x.GroundRate)
                                               .GetPaged<Groundrent>(model.PageNumber, model.PageSize);
                        break;
                    case ("FROM"):
                        data = null;
                        data = await _dbContext.Groundrent
                                              .Include(x => x.LeasePurposesType)
                            .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)) &&

                  x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                    && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                                .OrderByDescending(x => x.FromDate)
                                               .GetPaged<Groundrent>(model.PageNumber, model.PageSize);
                        break;
                    case ("TO"):
                        data = null;
                        data = await _dbContext.Groundrent
                                              .Include(x => x.LeasePurposesType)
                            .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)) &&

                  x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                    && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                               .OrderByDescending(x => x.ToDate)
                                               .GetPaged<Groundrent>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Groundrent
                                              .Include(x => x.LeasePurposesType)
                            .Include(x => x.LeaseSubPurpose)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.LeasePurposesType.PurposeUse.Contains(model.name))
                   && (string.IsNullOrEmpty(model.sub) || x.LeaseSubPurpose.SubPurposeUse.Contains(model.sub)) &&

                  x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                    && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                               .OrderBy(x => x.IsActive)
                                               .GetPaged<Groundrent>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;

        }
        public async Task<List<Groundrent>> GetAllGroundRent()
        {
            return await _dbContext.Groundrent.Include(x => x.LeasePurposesType).Include(x => x.LeaseSubPurpose).Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<List<Groundrent>> GetAllGroundRentList()
        {
            return await _dbContext.Groundrent.Include(x => x.LeasePurposesType).Include(x => x.LeaseSubPurpose).ToListAsync();
        }
    }
}
