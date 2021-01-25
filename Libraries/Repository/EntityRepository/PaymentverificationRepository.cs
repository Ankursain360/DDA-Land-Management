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
using Repository.Common;

namespace Libraries.Repository.EntityRepository
{
    public class PaymentverificationRepository : GenericRepository<Paymentverification>, IPaymentverificationRepository
    {
        public PaymentverificationRepository(DataContext dbContext) : base(dbContext)
        {

        }
      
        //public async Task<PagedResult<Paymentverification>> GetPagedPaymentList(PaymentverificationSearchDto model)
        //{
        //    return await _dbContext.Paymentverification
        //        .Where(x => x.IsVerified == 0)
        //        .GetPaged<Paymentverification>(model.PageNumber, model.PageSize);
        //}
        public async Task<PagedResult<Paymentverification>> GetPagedPaymentListUnverified(PaymentverificationSearchDto model)
        {
            var data = await _dbContext.Paymentverification
                           // .Where(a => a.IsVerified == 0)
                          .Where(x => (string.IsNullOrEmpty(model.name) || x.PayeeName.Contains(model.name)) && (x.IsVerified == 0))
                            .OrderByDescending(s => s.IsActive)
                            .GetPaged<Paymentverification>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderBy(x => x.PayeeName).ToList();
                        break;
                    case ("STATUS"):
                        data.Results = data.Results.OrderBy(x => x.IsActive).ToList();
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderByDescending(x => x.PayeeName).ToList();
                        break;
                    case ("STATUS"):
                        data.Results = data.Results.OrderByDescending(x => x.IsActive).ToList();
                        break;
                }
            }
            return data;
           
        }
        public async Task<PagedResult<Paymentverification>> GetPagedPaymentListVerified(PaymentverificationSearchDto model)
        {
            var data = await _dbContext.Paymentverification
                           // .Where(a => a.IsVerified == 0)
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.PayeeName.Contains(model.name)) && (x.IsVerified == 1))

                            .OrderByDescending(s => s.IsActive)
                            .GetPaged<Paymentverification>(model.PageNumber, model.PageSize);
         
            return data;

        }

        public async Task<List<Paymentverification>> GetAllPaymentList()
        {
            return await _dbContext.Paymentverification
           .Where(x => x.IsActive == 1)
           .ToListAsync();
        }
        public async Task<List<Locality>> GetAllLocality()
        {
            List<Locality> localityList = await _dbContext.Locality.Where(x => x.IsActive == 1).ToListAsync();
            return localityList;
        }
        public async Task<List<Paymentverification>> BindFileNoList()
        {
            var list = await _dbContext.Paymentverification
                                    .Where(x => x.IsActive == 1)
                                    .ToListAsync();
            return list;
        }

        public async Task<List<Locality>> BindLoclityList()
        {
            var InLocalitiesId = (from x in _dbContext.Demandletters
                                  where x.IsActive == 1
                                  select x.LocalityId).ToArray();

            return await _dbContext.Locality
                                    .Where(x => x.IsActive == 1
                                    && (InLocalitiesId).Contains(x.Id))
                                    .ToListAsync();
        }
        //        public async Task<PagedResult<Paymentverification>> GetPaymentTransactionReportData(PaymentTransactionReportSearchDto paymentTransactionReportSearchDto)
        //        {
        //            var data = await _dbContext.Paymentverification
        //                //.Include(x => x.Locality)

        //                .Where(x => x.CreatedDate >= paymentTransactionReportSearchDto.fromDate

        //               && x.CreatedDate <= paymentTransactionReportSearchDto.toDate)
        //                .OrderByDescending(x => x.Id).GetPaged(paymentTransactionReportSearchDto.PageNumber, paymentTransactionReportSearchDto.PageSize);

        //            int SortOrder = (int)paymentTransactionReportSearchDto.SortOrder;
        //            if (SortOrder == 1)
        //            {
        //                switch (paymentTransactionReportSearchDto.SortBy.ToUpper())
        //                {

        //                    //case ("LOCALITY"):
        //                    //    data.Results = data.Results.OrderBy(x => x.Locality.Name).ToList();
        //                    //    break;

        //                    case ("CREATEDDATE"):
        //                        data.Results = data.Results.OrderBy(x => x.CreatedDate).ToList();
        //                        break;
        //                    //case ("LANDMARK"):
        //                    //    data.Results = data.Results.OrderBy(x => x.Landmark).ToList();
        //                    //    break;

        //                }
        //            }
        //            else if (SortOrder == 2)
        //            {
        //                switch (paymentTransactionReportSearchDto.SortBy.ToUpper())
        //                {

        //                    //case ("LOCALITY"):
        //                    //    data.Results = data.Results.OrderByDescending(x => x.Locality.Name).ToList();
        //                    //    break;

        //                    case ("CREATEDDATE"):
        //                        data.Results = data.Results.OrderByDescending(x => x.CreatedDate).ToList();
        //                        break;
        //                    //case ("LANDMARK"):
        //                    //    data.Results = data.Results.OrderByDescending(x => x.Landmark).ToList();
        //                    //    break;

        //                }
        //            }
        //            return data;
        //        }
        public async Task<List<PaymentTransactionReportListDataDto>> GetPagedPaymentTransactionReportData(PaymentTransactionReportSearchDto model)
        {
            
            try
            {
                int SortOrder = (int)model.SortOrder;
                var data = await _dbContext.LoadStoredProcedure("BindPaymentTransactionReport")
                                            .WithSqlParams(("P_FileNo", model.FileNo), ("P_FromDate", model.FromDate)
                                            , ("P_ToDate", model.ToDate)
                                            , ("P_LocalityId", model.Locality), ("P_SortOrder", SortOrder)
                                            , ("P_SortBy", model.SortBy))
                                            .ExecuteStoredProcedureAsync<PaymentTransactionReportListDataDto>();

                return (List<PaymentTransactionReportListDataDto>)data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}