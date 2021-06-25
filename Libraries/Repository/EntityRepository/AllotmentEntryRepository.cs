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
using Dto.Search;


using Repository.Common;


namespace Libraries.Repository.EntityRepository
{
    public class AllotmentEntryRepository : GenericRepository<Allotmententry>, IAllotmentEntryRepository
    {
        public AllotmentEntryRepository(DataContext dbContext) : base(dbContext)
        {

        }



        public async Task<List<Leaseapplication>> GetAllLeaseapplication(int approved)
        {
            var InAllotmentId = (from x in _dbContext.Allotmententry
                                  where x.Application != null && x.IsActive == 1
                                  select x.ApplicationId).ToArray();
            List<Leaseapplication> leaseappList = await _dbContext
                                                  .Leaseapplication
                                                  .Include(x => x.ApprovedStatusNavigation)
                                                  .Where(x => (x.IsActive == 1) 
                                                  &&( x.ApprovedStatusNavigation.StatusCode == approved)
                                                  && !(InAllotmentId).Contains(x.Id))
                                                  .ToListAsync();
            return leaseappList;
        }


        public async Task<List<Leasetype>> GetAllLeasetype()
        {
            List<Leasetype> leaseTypeList = await _dbContext.Leasetype.Where(x => x.IsActive == 1).ToListAsync();
            return leaseTypeList;
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


        public async Task<Leaseapplication> FetchSingleLeaseapplicationResult(int? applicationId)
        {
            return await _dbContext.Leaseapplication.Where(x => x.Id == applicationId).SingleOrDefaultAsync();
        }

        public async Task<Leaseapplication> FetchLeaseApplicationmailDetails(int id)
        {
            return await _dbContext.Leaseapplication.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Allotmententry> FetchSingleCalculationDetails(int? LeasesTypeId)
        {
            var result = await _dbContext.Allotmententry.Where(x => x.Id == LeasesTypeId).SingleOrDefaultAsync();
            var masterPremiumAmount = await _dbContext.Premiumrate.Where(x => x.FromDate <= result.AllotmentDate && x.ToDate >= result.AllotmentDate).FirstOrDefaultAsync();
            result.PremiumRate = masterPremiumAmount.PremiumRate;
           // result.TotalPremiumAmount = Convert.ToDecimal(0.00024711) * masterPremiumAmount.PremiumRate * result.TotalArea;
            return result;
        }



        public async Task<List<Allotmententry>> GetAllAllotmententry()
        {
            return await _dbContext.Allotmententry.Include(x => x.Application).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<PagedResult<Allotmententry>> GetPagedAllotmententry(AllotmentEntrySearchDto model)
        {

            var data = await _dbContext.Allotmententry
                       .Include(x => x.Application)
                       .Include(x => x.LeasesType)
                       .Where(x => (string.IsNullOrEmpty(model.applicantname) || x.Application.Name.Contains(model.applicantname))
                       && (string.IsNullOrEmpty(model.Lease) || x.LeasesType.Type.Contains(model.Lease))
                       && (string.IsNullOrEmpty(model.RefNo) || x.Application.RefNo.Contains(model.RefNo))
                       && x.AllotmentDate >= (model.FromDate == "" ? x.AllotmentDate : Convert.ToDateTime(model.FromDate))
                       && x.AllotmentDate <= (model.ToDate == "" ? x.AllotmentDate : Convert.ToDateTime(model.ToDate)))
                       .GetPaged<Allotmententry>(model.PageNumber, model.PageSize);


            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Allotmententry
                                    .Include(x => x.Application)
                                    .Include(x => x.LeasesType)
                                     .Where(x => (string.IsNullOrEmpty(model.applicantname) || x.Application.Name.Contains(model.applicantname))
                                     && (string.IsNullOrEmpty(model.Lease) || x.LeasesType.Type.Contains(model.Lease))
                                     && (string.IsNullOrEmpty(model.RefNo) || x.Application.RefNo.Contains(model.RefNo))
                                     && x.AllotmentDate >= (model.FromDate == "" ? x.AllotmentDate : Convert.ToDateTime(model.FromDate))
                                     && x.AllotmentDate <= (model.ToDate == "" ? x.AllotmentDate : Convert.ToDateTime(model.ToDate))).OrderBy(a => a.Application.Name)
                                    .GetPaged<Allotmententry>(model.PageNumber, model.PageSize);
                        break;

                    case ("DATE"):

                        data = null;
                        data = await _dbContext.Allotmententry
                                    .Include(x => x.Application)
                                    .Include(x => x.LeasesType)
                                     .Where(x => (string.IsNullOrEmpty(model.applicantname) || x.Application.Name.Contains(model.applicantname))
                                     && (string.IsNullOrEmpty(model.Lease) || x.LeasesType.Type.Contains(model.Lease))
                                     && (string.IsNullOrEmpty(model.RefNo) || x.Application.RefNo.Contains(model.RefNo))
                                     && x.AllotmentDate >= (model.FromDate == "" ? x.AllotmentDate : Convert.ToDateTime(model.FromDate))
                                     && x.AllotmentDate <= (model.ToDate == "" ? x.AllotmentDate : Convert.ToDateTime(model.ToDate))).OrderBy(a => a.Application.Name)
                                    .OrderBy(a => a.AllotmentDate)
                                    .GetPaged<Allotmententry>(model.PageNumber, model.PageSize);
                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Allotmententry
                                    .Include(x => x.Application)
                                    .Include(x => x.LeasesType)
                                     .Where(x => (string.IsNullOrEmpty(model.applicantname) || x.Application.Name.Contains(model.applicantname))
                                     && (string.IsNullOrEmpty(model.Lease) || x.LeasesType.Type.Contains(model.Lease))
                                     && (string.IsNullOrEmpty(model.RefNo) || x.Application.RefNo.Contains(model.RefNo))
                                     && x.AllotmentDate >= (model.FromDate == "" ? x.AllotmentDate : Convert.ToDateTime(model.FromDate))
                                     && x.AllotmentDate <= (model.ToDate == "" ? x.AllotmentDate : Convert.ToDateTime(model.ToDate))).OrderBy(a => a.Application.Name)
                                     .OrderByDescending(a => a.IsActive)
                                    .GetPaged<Allotmententry>(model.PageNumber, model.PageSize);
                        break;



                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {


                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Allotmententry
                                    .Include(x => x.Application)
                                    .Include(x => x.LeasesType)

                                    .Where(x => (string.IsNullOrEmpty(model.applicantname) || x.Application.Name.Contains(model.applicantname))
                                     && (string.IsNullOrEmpty(model.Lease) || x.LeasesType.Type.Contains(model.Lease))
                                     && (string.IsNullOrEmpty(model.RefNo) || x.Application.RefNo.Contains(model.RefNo))
                                     && x.AllotmentDate >= (model.FromDate == "" ? x.AllotmentDate : Convert.ToDateTime(model.FromDate))
                                     && x.AllotmentDate <= (model.ToDate == "" ? x.AllotmentDate : Convert.ToDateTime(model.ToDate))).OrderBy(a => a.Application.Name)
                                     .OrderByDescending(a => a.Application.Name)
                                    .GetPaged<Allotmententry>(model.PageNumber, model.PageSize);
                        break;

                    case ("DATE"):

                        data = null;
                        data = await _dbContext.Allotmententry
                                    .Include(x => x.Application)
                                    .Include(x => x.LeasesType)
                                     .Where(x => (string.IsNullOrEmpty(model.applicantname) || x.Application.Name.Contains(model.applicantname))
                                     && (string.IsNullOrEmpty(model.Lease) || x.LeasesType.Type.Contains(model.Lease))
                                     && (string.IsNullOrEmpty(model.RefNo) || x.Application.RefNo.Contains(model.RefNo))
                                     && x.AllotmentDate >= (model.FromDate == "" ? x.AllotmentDate : Convert.ToDateTime(model.FromDate))
                                     && x.AllotmentDate <= (model.ToDate == "" ? x.AllotmentDate : Convert.ToDateTime(model.ToDate))).OrderBy(a => a.Application.Name)
                                    .OrderByDescending(a => a.AllotmentDate)
                                    .GetPaged<Allotmententry>(model.PageNumber, model.PageSize);
                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Allotmententry
                                    .Include(x => x.Application)
                                    .Include(x => x.LeasesType)
                                     .Where(x => (string.IsNullOrEmpty(model.applicantname) || x.Application.Name.Contains(model.applicantname))
                                     && (string.IsNullOrEmpty(model.Lease) || x.LeasesType.Type.Contains(model.Lease))
                                     && (string.IsNullOrEmpty(model.RefNo) || x.Application.RefNo.Contains(model.RefNo))
                                     && x.AllotmentDate >= (model.FromDate == "" ? x.AllotmentDate : Convert.ToDateTime(model.FromDate))
                                     && x.AllotmentDate <= (model.ToDate == "" ? x.AllotmentDate : Convert.ToDateTime(model.ToDate))).OrderBy(a => a.Application.Name)
                                    .OrderBy(a => a.IsActive)
                                    .GetPaged<Allotmententry>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;

        }


        public async Task<Documentcharges> FetchSingledocumentResult(int? leasePurposeId, int? leaseSubPurposeId, string allotmentDate)
        {
            var data = await _dbContext.Documentcharges.Where(x => x.LeasePurposesTypeId == leasePurposeId && x.LeaseSubPurposeId == leaseSubPurposeId && (Convert.ToDateTime(allotmentDate) >= x.FromDate && Convert.ToDateTime(allotmentDate) <= x.ToDate)).SingleOrDefaultAsync();
            return data;
        }

        public async Task<Premiumrate> FetchSinglerateResult(int? leasePurposeId, int? leaseSubPurposeId, string allotmentDate)
        {
            //var data =  await _dbContext.Premiumrate.Where(x => x.LeasePurposesTypeId == leasePurposeId && x.LeaseSubPurposeId == leaseSubPurposeId && (x.FromDate >= Convert.ToDateTime(allotmentDate) && x.ToDate <= Convert.ToDateTime(allotmentDate))).SingleOrDefaultAsync();
            var data = await _dbContext.Premiumrate.Where(x => x.LeasePurposesTypeId == leasePurposeId && x.LeaseSubPurposeId == leaseSubPurposeId && (Convert.ToDateTime(allotmentDate) >= x.FromDate && Convert.ToDateTime(allotmentDate) <= x.ToDate)).SingleOrDefaultAsync();
            return data;
        }
        public async Task<Groundrent> FetchSinglegroundrentResult(int? leasePurposeId, int? leaseSubPurposeId, string allotmentDate)
        {
            var data = await _dbContext.Groundrent.Where(x => x.LeasePurposesTypeId == leasePurposeId && x.LeaseSubPurposeId == leaseSubPurposeId && (Convert.ToDateTime(allotmentDate) >= x.FromDate && Convert.ToDateTime(allotmentDate) <= x.ToDate)).SingleOrDefaultAsync();
            return data;
        }
        public async Task<Licencefees> FetchSinglefeeResult(int? leasePurposeId, int? leaseSubPurposeId, string allotmentDate)
        {
            var data = await _dbContext.Licencefees.Where(x => x.LeasePurposesTypeId == leasePurposeId && x.LeaseSubPurposeId == leaseSubPurposeId && (Convert.ToDateTime(allotmentDate) >= x.FromDate && Convert.ToDateTime(allotmentDate) <= x.ToDate)).SingleOrDefaultAsync();
            return data;
        }



        public async Task<List<DemandletterdatalistDto>> Getdemandletteralldata(DemandletterDateSearchDto model)

        {
            try
            {


                var data = await _dbContext.LoadStoredProcedure("Get_Demand_Details")
                                            .WithSqlParams(("AllotmentId", model.applicationid)
                                            //,
                                            //("DemandDate", model.demanddate)
                                            )



                                            .ExecuteStoredProcedureAsync<DemandletterdatalistDto>();

                return (List<DemandletterdatalistDto>)data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> CreatePaymentPremiumDr(Payment model)
        {
            _dbContext.Payment.Add(model);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }





     




        public async Task<List<PayemntDescriptionListDto>> GetPagedPaymentReport(PaymentdetailssearchDto model)

        {
            try
            {


                var data = await _dbContext.LoadStoredProcedure("Paymentdescriptiondetails")
                                            .WithSqlParams(("P_allotmentId", model.allotmentid)
                                            //,
                                            //("DemandDate", model.demanddate)
                                            )



                                            .ExecuteStoredProcedureAsync<PayemntDescriptionListDto>();

                return (List<PayemntDescriptionListDto>)data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }















    }
}
