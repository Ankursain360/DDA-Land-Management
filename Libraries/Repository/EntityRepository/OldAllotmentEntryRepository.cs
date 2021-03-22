using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Libraries.Repository.EntityRepository
{
    public class OldAllotmentEntryRepository : GenericRepository<Leaseapplication>, IOldAllotmentEntryRepository
    {
        public OldAllotmentEntryRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<PropertyType>> GetAllPropertyType()
        {
            List<PropertyType> list = await _dbContext.PropertyType.Where(x => x.IsActive == 1).ToListAsync();
            return list;
        }
        public async Task<List<Leasetype>> GetAllLeaseType()
        {
            List<Leasetype> list = await _dbContext.Leasetype.Where(x => x.IsActive == 1).ToListAsync();
            return list;
        }
        public async Task<List<Leasepurpose>> GetAllLeasepurpose()
        {
            List<Leasepurpose> list = await _dbContext.Leasepurpose.Where(x => x.IsActive == 1).ToListAsync();
            return list;
        }
        public async Task<List<Leasesubpurpose>> GetAllLeaseSubpurpose(int? purposeId)
        {
            List<Leasesubpurpose> list = await _dbContext.Leasesubpurpose.Where(x => x.PurposeUseId == purposeId && x.IsActive == 1).ToListAsync();
            return list;
        }
        public async Task<bool> Update(int id, Allotmententry entry)
        {
            Allotmententry model = await FetchSingleResult(id);
            model.LeasesTypeId = entry.LeasesTypeId;
            model.PlotNo = entry.PlotNo;
            model.BuildingArea = entry.BuildingArea;
            model.PlayGroundArea = entry.PlayGroundArea;
            model.TotalArea = entry.TotalArea;
            model.AllotmentDate = entry.AllotmentDate;
            model.LeasePurposesTypeId = entry.LeasePurposesTypeId;
            model.LeaseSubPurposeId = entry.LeaseSubPurposeId;
            model.PremiumRate = entry.PremiumRate;
            model.PremiumAmount = entry.PremiumAmount;
            model.GroundRent = entry.GroundRent;
            model.AmountLicFee = entry.AmountLicFee;
            model.NoOfYears = entry.NoOfYears;
            
            model.IsActive = 1;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = entry.ModifiedBy;

            _dbContext.Allotmententry.Update(model);
            return await _dbContext.SaveChangesAsync() > 0;


           
        }
        public async Task<bool> UpdateLease(int id, Allotmententry entry)
        {
            Leaseapplication model = await FetchSingleLeaseResult(id);
            model.RefNo = entry.Application.RefNo;
            model.Name = entry.Application.Name;
            model.Address = entry.Application.Address;
            model.Rate = entry.PremiumRate;
            model.IsActive = 1;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = entry.ModifiedBy;

            _dbContext.Leaseapplication.Update(model);
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<Allotmententry> FetchSingleResult(int id)
        {
            return await _dbContext.Allotmententry
                                   .Include(x => x.Application)
                                   .Include(x => x.LeasesType)
                                   .Include(x => x.LeasePurposesType)
                                   .Include(x => x.LeaseSubPurpose)
                                   
                                  .Where(x => x.Id == id )
                                  .OrderByDescending(s => s.Id)
                                  .FirstOrDefaultAsync();
        }

        public async Task<Leaseapplication> FetchSingleLeaseResult(int id)
        {
            return await _dbContext.Leaseapplication
                                 .Where(x => x.Id == id)
                                 .OrderByDescending(s => s.Id)
                                 .FirstOrDefaultAsync();
        }

        public async Task<Possesionplan> FetchSinglePossessionResult(int id)
        {
            try { 
            return await _dbContext.Possesionplan
                                  .Include(x=> x.Allotment)
                                 .Where(x => x.AllotmentId == id)
                                 .OrderByDescending(s => s.Id)
                                 .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<bool> UpdatePossession(int id, Allotmententry entry)
        {
            Possesionplan model = await FetchSinglePossessionResult(id);
            model.PossessionTakenDate = entry.PossessionTakenDate;
            model.AllotedArea = entry.TotalArea;
           
            model.IsActive = 1;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = entry.ModifiedBy;

            _dbContext.Possesionplan.Update(model);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<PagedResult<Allotmententry>> GetPagedOldEntry(OLdAllotmentSearchDto model)
        {
            var data = await _dbContext.Allotmententry
                                       .Include(x => x.LeasesType)
                                       .Include(x => x.Application)
                                       .Where(x =>(x.OldNewEntry=="Old") &&
                                       (string.IsNullOrEmpty(model.name) || x.LeasesType.Type.Contains(model.name)))
                                       .GetPaged<Allotmententry>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("TYPE"):
                        data = null;
                        data = await _dbContext.Allotmententry
                                               .Include(x => x.LeasesType)
                                                .Where(x => (x.OldNewEntry == "Old") &&
                                                (string.IsNullOrEmpty(model.name) || x.LeasesType.Type.Contains(model.name)))
                                                .OrderBy(x => x.LeasesType.Type)
                                               .GetPaged<Allotmententry>(model.PageNumber, model.PageSize);
                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Allotmententry
                                               .Include(x => x.LeasesType)
                                                .Where(x => (x.OldNewEntry == "Old") &&
                                                (string.IsNullOrEmpty(model.name) || x.LeasesType.Type.Contains(model.name)))
                                                .OrderBy(x => x.AllotmentDate)
                                                
                                               .GetPaged<Allotmententry>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Allotmententry
                                               .Include(x => x.LeasesType)
                                                .Where(x => (x.OldNewEntry == "Old") &&
                                                (string.IsNullOrEmpty(model.name) || x.LeasesType.Type.Contains(model.name)))
                                                .OrderByDescending(x => x.IsActive)
                                                .GetPaged<Allotmententry>(model.PageNumber, model.PageSize);
                                               
                                              
                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("TYPE"):
                        data = null;
                        data = await _dbContext.Allotmententry
                                               .Include(x => x.LeasesType)
                                                .Where(x => (x.OldNewEntry == "Old") &&
                                                (string.IsNullOrEmpty(model.name) || x.LeasesType.Type.Contains(model.name)))
                                                .OrderByDescending(x => x.LeasesType.Type)
                                               .GetPaged<Allotmententry>(model.PageNumber, model.PageSize);
                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Allotmententry
                                               .Include(x => x.LeasesType)
                                                .Where(x => (x.OldNewEntry == "Old") &&
                                                (string.IsNullOrEmpty(model.name) || x.LeasesType.Type.Contains(model.name)))
                                                .OrderByDescending(x => x.AllotmentDate)

                                               .GetPaged<Allotmententry>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Allotmententry
                                               .Include(x => x.LeasesType)
                                                .Where(x => (x.OldNewEntry == "Old") &&
                                                (string.IsNullOrEmpty(model.name) || x.LeasesType.Type.Contains(model.name)))
                                                .OrderBy(x => x.IsActive)
                                                .GetPaged<Allotmententry>(model.PageNumber, model.PageSize);


                        break;
                }
            }
            return data;
        }

        //********* save in table  Allotmententry  **********


        public async Task<int> SaveAllotmentDetails(Allotmententry entry)
        {
            _dbContext.Allotmententry.Add(entry);
            var Result = await _dbContext.SaveChangesAsync();
            //return Result > 0 ? true : false;
            return Result;
        }
        public async Task<List<Allotmententry>> GetAllAllotmententry(int id)
        {
            return await _dbContext.Allotmententry.Where(x => x.ApplicationId == id && x.OldNewEntry == "Old").ToListAsync();
        }
        public async Task<bool> DeleteEntry(int Id)
        {
            _dbContext.Remove(_dbContext.Allotmententry.Where(x => x.ApplicationId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        //********* save in table  possesionplan  **********
        public async Task<bool> SavepossessionDetails(Possesionplan entry)
        {
            _dbContext.Possesionplan.Add(entry);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        //public async Task<List<Possesionplan>> GetAllPossesionplan(int id)
        //{
        //    return await _dbContext.Possesionplan.Where(x => x.AllotmentId == id && x.IsActive == 1).ToListAsync();
        //}
        //public async Task<bool> DeletePlan(int Id)
        //{

        //}

       

    }
}
