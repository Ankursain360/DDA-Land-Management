using System;
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
using Repository.IEntityRepository;

namespace Repository.EntityRepository
{

    public class LeasepaymentdetailsRepository : GenericRepository<Leasepaymentdetails>, ILeasepaymentdetailsRepository
    {
        public LeasepaymentdetailsRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Leasepaymenttype>> GetAllPaymentType()
        {
            List<Leasepaymenttype> paymenttypeList = await _dbContext.Leasepaymenttype.Where(x => x.IsActive == 1).ToListAsync();
            return paymenttypeList;
        }

        public async Task<List<Allotmententry>> GetAllAllotmententry()
        {
            List<Allotmententry> allotmententryList = await _dbContext.Allotmententry
                .Include(x => x.Application)
                .Where(x => (x.ApplicationId == x.Application.Id && x.IsActive == 1)).ToListAsync();
            return allotmententryList;
        }
        public async Task<List<Leasepaymentdetails>> GetAllLeasepaymentdetails()
        {
            return await _dbContext.Leasepaymentdetails.Include(x => x.Ref)
                //.Where(x=> x.Id == Allotmententry.Id)
                .OrderByDescending(x => x.Id).ToListAsync();
        }


        public async Task<List<Leaseapplication>> GetAllLeaseApplication()
        {
            List<Leaseapplication> leaseapplicationList = await _dbContext.Leaseapplication.Include(x => x.Allotmententry)
                .Where(x => x.IsActive == 1)
                .ToListAsync();
            return leaseapplicationList;
         
        }



        public async Task<List<Allotmententry>> BindAllotmentDetails(int? AllotmentId)
        {
            return await _dbContext.Allotmententry.Where(x => x.Id == AllotmentId).ToListAsync();
        }
        public async Task<List<Leaseapplication>> BindLeaseApplicationDetails(int? AppId)
        {
            return await _dbContext.Leaseapplication.Where(x => x.Id == AppId).ToListAsync();

        }

        public async Task<PagedResult<Leasepaymentdetails>> GetPagedLeasepaymentdetails(LeasepaymentdetailsSearchDto model)

        {

            var data = await _dbContext.Leasepaymentdetails
                                       .Include(x => x.Ref)
                                      .Include(x => x.PaymentType)
                                      .Include(x => x.Ref.Application)
                                      .Where(x => (string.IsNullOrEmpty(model.AllotmentId) || (x.Ref.Id == Convert.ToInt32(model.AllotmentId))))
                                      .OrderByDescending(x => x.Id)
                          .GetPaged<Leasepaymentdetails>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1 && model.AllotmentId != "0")
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("ALLOTMENTNUMBER"):
                        data = null;
                        data = await _dbContext.Leasepaymentdetails
                                               .Include(x => x.Ref)
                                        .Include(x => x.PaymentType)
                                       .Include(x => x.Ref.Application)
                                       .Where(x => (string.IsNullOrEmpty(model.AllotmentId) || (x.Ref.Id == Convert.ToInt32(model.AllotmentId)))
                                       && (x.PaymentMode.Contains(model.Mode)) && (x.ChallanUtrnumber.Contains(model.Number)))
                                                              // && (x.PaymentDate == Convert.ToDateTime(model.PaymentDate)))
                                                              //&& x.PaymentMode.Contains(string.IsNullOrEmpty(model.Mode) || (x.PaymentMode.Contains (model.Mode)))
                                                              //&& (string.IsNullOrEmpty(model.Number) || (x.ChallanUtrnumber.Contains(model.Number)))
                                                              //&& (string.IsNullOrEmpty(model.PaymentDate) || (x.PaymentDate==Convert.ToDateTime(model.PaymentDate))))
                                                              .OrderBy(x => x.Ref.Application.RefNo)
                                .GetPaged<Leasepaymentdetails>(model.PageNumber, model.PageSize);
                        break;
                    case ("TYPE"):
                        data = null;
                        data = await _dbContext.Leasepaymentdetails
                                        .Include(x => x.Ref)
                                        .Include(x => x.PaymentType)
                                        .Include(x => x.Ref.Application)
                                 // .Where(x => string.IsNullOrEmpty(model.AllotmentId) || (x.Ref.Id == Convert.ToInt32(model.AllotmentId)))
                                 .Where(x => (string.IsNullOrEmpty(model.AllotmentId) || (x.Ref.Id == Convert.ToInt32(model.AllotmentId)))
                                       && (x.PaymentMode.Contains(model.Mode)) && (x.ChallanUtrnumber.Contains(model.Number))
                                    //  && (x.PaymentDate == Convert.ToDateTime(model.PaymentDate))
                                    )
                                    .OrderBy(x => x.PaymentType.Name)
                                .GetPaged<Leasepaymentdetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("MODE"):
                        data = null;
                        data = await _dbContext.Leasepaymentdetails
                                        .Include(x => x.Ref)
                                        .Include(x => x.PaymentType)
                                        .Include(x => x.Ref.Application)
                                       .Where(x => (string.IsNullOrEmpty(model.AllotmentId) || (x.Ref.Id == Convert.ToInt32(model.AllotmentId)))
                                       && (x.PaymentMode.Contains(model.Mode)) && (x.ChallanUtrnumber.Contains(model.Number))
                                      // && (x.PaymentDate == Convert.ToDateTime(model.PaymentDate))
                                      )
                                    .OrderBy(x => x.PaymentMode)
                                .GetPaged<Leasepaymentdetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("NUMBER"):
                        data = null;
                        data = await _dbContext.Leasepaymentdetails
                                        .Include(x => x.Ref)
                                        .Include(x => x.PaymentType)
                                        .Include(x => x.Ref.Application)
                                        .Where(x => (string.IsNullOrEmpty(model.AllotmentId) || (x.Ref.Id == Convert.ToInt32(model.AllotmentId)))
                                       && (x.PaymentMode.Contains(model.Mode)) && (x.ChallanUtrnumber.Contains(model.Number)))
                                      //  && (x.PaymentDate == Convert.ToDateTime(model.PaymentDate))).OrderBy(x => x.ChallanUtrnumber)
                                      .OrderByDescending(x => x.ChallanUtrnumber)
                                .GetPaged<Leasepaymentdetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("AMOUNT"):
                        data = null;
                        data = await _dbContext.Leasepaymentdetails
                                        .Include(x => x.Ref)
                                        .Include(x => x.PaymentType)
                                        .Include(x => x.Ref.Application)
                              .Where(x => (string.IsNullOrEmpty(model.AllotmentId) || (x.Ref.Id == Convert.ToInt32(model.AllotmentId)))
                                       && (x.PaymentMode.Contains(model.Mode)) && (x.ChallanUtrnumber.Contains(model.Number))
                                       && (x.PaymentDate == Convert.ToDateTime(model.PaymentDate)))
                                 .OrderBy(x => x.PaymentAmount)
                                .GetPaged<Leasepaymentdetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Leasepaymentdetails
                                        .Include(x => x.Ref)
                                        .Include(x => x.PaymentType)
                                        .Include(x => x.Ref.Application)
                               .Where(x => (string.IsNullOrEmpty(model.AllotmentId) || (x.Ref.Id == Convert.ToInt32(model.AllotmentId)))
                                       && (x.PaymentMode.Contains(model.Mode)) && (x.ChallanUtrnumber.Contains(model.Number)))
                                     //  && (x.PaymentDate == Convert.ToDateTime(model.PaymentDate)))
                                    .OrderBy(x => x.PaymentDate)
                                .GetPaged<Leasepaymentdetails>(model.PageNumber, model.PageSize);

                        break;
                }
            }
            else if (SortOrder == 2 && model.AllotmentId != "0")
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("ALLOTMENTNUMBER"):
                        data = null;
                        data = await _dbContext.Leasepaymentdetails
                                        .Include(x => x.Ref)
                                         .Include(x => x.Ref.Application)
                                         .Include(x => x.PaymentType)
.Where(x => (string.IsNullOrEmpty(model.AllotmentId) || (x.Ref.Id == Convert.ToInt32(model.AllotmentId)))
                                       && (x.PaymentMode.Contains(model.Mode)) && (x.ChallanUtrnumber.Contains(model.Number)))
                                     //  && (x.PaymentDate == Convert.ToDateTime(model.PaymentDate)))
                                .OrderByDescending(x => x.Ref.Application.RefNo)
                                .GetPaged<Leasepaymentdetails>(model.PageNumber, model.PageSize);
                        break;
                    case ("TYPE"):
                        data = null;
                        data = await _dbContext.Leasepaymentdetails
                                        .Include(x => x.Ref)
                                        .Include(x => x.PaymentType)
                                        .Include(x => x.Ref.Application)
                               .Where(x => (string.IsNullOrEmpty(model.AllotmentId) || (x.Ref.Id == Convert.ToInt32(model.AllotmentId)))
                                       && (x.PaymentMode.Contains(model.Mode)) && (x.ChallanUtrnumber.Contains(model.Number)))
                                  //     && (x.PaymentDate == Convert.ToDateTime(model.PaymentDate)))
                                    .OrderByDescending(x => x.PaymentType.Name)
                                .GetPaged<Leasepaymentdetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("MODE"):
                        data = null;
                        data = await _dbContext.Leasepaymentdetails
                                        .Include(x => x.Ref)
                                        .Include(x => x.PaymentType)
                                        .Include(x => x.Ref.Application)
                       .Where(x => (string.IsNullOrEmpty(model.AllotmentId) || (x.Ref.Id == Convert.ToInt32(model.AllotmentId)))
                                       && (x.PaymentMode.Contains(model.Mode)) && (x.ChallanUtrnumber.Contains(model.Number)))
                                    //   && (x.PaymentDate == Convert.ToDateTime(model.PaymentDate)))
                                    .OrderByDescending(x => x.PaymentMode)
                                .GetPaged<Leasepaymentdetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("NUMBER"):
                        data = null;
                        data = await _dbContext.Leasepaymentdetails
                                        .Include(x => x.Ref)
                                        .Include(x => x.PaymentType)
                                        .Include(x => x.Ref.Application)
                           .Where(x => (string.IsNullOrEmpty(model.AllotmentId) || (x.Ref.Id == Convert.ToInt32(model.AllotmentId)))
                                       && (x.PaymentMode.Contains(model.Mode)) && (x.ChallanUtrnumber.Contains(model.Number)))
                                     //  && (x.PaymentDate == Convert.ToDateTime(model.PaymentDate)))
                                    .OrderByDescending(x => x.ChallanUtrnumber)
                                .GetPaged<Leasepaymentdetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("AMOUNT"):
                        data = null;
                        data = await _dbContext.Leasepaymentdetails
                                        .Include(x => x.Ref)
                                        .Include(x => x.PaymentType)
                                        .Include(x => x.Ref.Application)
                              .Where(x => (string.IsNullOrEmpty(model.AllotmentId) || (x.Ref.Id == Convert.ToInt32(model.AllotmentId)))
                                       && (x.PaymentMode.Contains(model.Mode)) && (x.ChallanUtrnumber.Contains(model.Number)))
                                   //    && (x.PaymentDate == Convert.ToDateTime(model.PaymentDate))))
                                    .OrderByDescending(x => x.PaymentAmount)
                                .GetPaged<Leasepaymentdetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Leasepaymentdetails
                                        .Include(x => x.Ref)
                                        .Include(x => x.PaymentType)
                                        .Include(x => x.Ref.Application)
                                .Where(x => (string.IsNullOrEmpty(model.AllotmentId) || (x.Ref.Id == Convert.ToInt32(model.AllotmentId)))
                                       && (x.PaymentMode.Contains(model.Mode)) && (x.ChallanUtrnumber.Contains(model.Number)))
                                    //   && (x.PaymentDate == Convert.ToDateTime(model.PaymentDate)))
                                    .OrderByDescending(x => x.PaymentDate)
                                .GetPaged<Leasepaymentdetails>(model.PageNumber, model.PageSize);

                        break;
                }
            }
            return data;
        }

        public Task<Khasra> FetchSingleKhasraResult(int? khasraId)
        {
            throw new NotImplementedException();
        }
    }
}
