using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Master;

namespace Libraries.Repository.EntityRepository
{
   public class DemandLetterRepository : GenericRepository<Demandletters>, IDemandLetterRepository
    {
        public DemandLetterRepository(DataContext dbContext) : base(dbContext)
        {

        }


        public async Task<List<Demandletters>> GetAllDemandletter()
        {
            return await _dbContext.Demandletters.Where(x => x.IsActive == 1).Include(x => x.Locality).ToListAsync();
        }
        public async Task<List<Demandletters>> GetPenaltyImpositionReportList(PenaltyImpositionReportSearchDto model)
        {
            var data = await _dbContext.Demandletters
                    .Include(x => x.Locality)
                    .Where(x => (x.Id == (model.fileNo == 0 ? x.Id : model.fileNo))
                   && (x.LocalityId == (model.locality == 0 ? x.LocalityId : model.locality))).ToListAsync();
            return data;
        }
        public async Task<List<Demandletters>> GetAllDemandletterList(DemandletterSearchDto model)
        {
            var data = await _dbContext.Demandletters
                .Include(x => x.Locality)
                .Include(x => x.PropertyType)

                 .Where(x => (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                   && (string.IsNullOrEmpty(model.demandno) || x.DemandNo.Contains(model.demandno))
                    && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))

                    && (x.IsActive == 1)).ToListAsync();
            return data;
        }
        public async Task<PagedResult<Demandletters>> GetPagedDemandletter(DemandletterSearchDto model) 
        {
            var data = await _dbContext.Demandletters 
                .Include(x => x.Locality)
                .Include(x=>x.PropertyType)

                 .Where(x => (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                   && (string.IsNullOrEmpty(model.demandno) || x.DemandNo.Contains(model.demandno))
                    && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                      
                    && (x.IsActive == 1)
                  )


                .GetPaged<Demandletters>(model.PageNumber, model.PageSize);


            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Demandletters.Include(x => x.Locality)
                 .Where(x => (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                   && (string.IsNullOrEmpty(model.demandno) || x.DemandNo.Contains(model.demandno))
                    && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))

                    && (x.IsActive == 1)
                  ).OrderBy(s => s.Locality)
                                .GetPaged<Demandletters>(model.PageNumber, model.PageSize);
                        break;
                    case ("DEMANDNO"):
                        data = null;
                        data = await _dbContext.Demandletters.Include(x => x.Locality)
                                        .Where(x => (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                          && (string.IsNullOrEmpty(model.demandno) || x.DemandNo.Contains(model.demandno))
                                           && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                                           && (x.IsActive == 1)
                  ).OrderBy(s => s.DemandNo)
                                .GetPaged<Demandletters>(model.PageNumber, model.PageSize);

                        break;
                    case ("FILENO"):
                        data = null;
                        data = await _dbContext.Demandletters.Include(x => x.Locality)
                                       .Where(x => (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                         && (string.IsNullOrEmpty(model.demandno) || x.DemandNo.Contains(model.demandno))
                                          && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                                          && (x.IsActive == 1)
                 ).OrderBy(s => s.FileNo)
                               .GetPaged<Demandletters>(model.PageNumber, model.PageSize);

                        break;

              


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Demandletters.Include(x => x.Locality)
                 .Where(x => (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                   && (string.IsNullOrEmpty(model.demandno) || x.DemandNo.Contains(model.demandno))
                    && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))

                    && (x.IsActive == 1)
                  ).OrderByDescending(s => s.Locality)
                                .GetPaged<Demandletters>(model.PageNumber, model.PageSize);
                        break;
                    case ("DEMANDNO"):
                        data = null;
                        data = await _dbContext.Demandletters.Include(x => x.Locality)
                                        .Where(x => (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                          && (string.IsNullOrEmpty(model.demandno) || x.DemandNo.Contains(model.demandno))
                                           && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                                           && (x.IsActive == 1)
                  ).OrderByDescending(s => s.DemandNo)
                                .GetPaged<Demandletters>(model.PageNumber, model.PageSize);

                        break;
                    case ("FILENO"):
                        data = null;
                        data = await _dbContext.Demandletters.Include(x => x.Locality)
                                       .Where(x => (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                         && (string.IsNullOrEmpty(model.demandno) || x.DemandNo.Contains(model.demandno))
                                          && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                                          && (x.IsActive == 1)
                 ).OrderByDescending(s => s.FileNo)
                               .GetPaged<Demandletters>(model.PageNumber, model.PageSize);

                        break;

                   

                }
            }







            return data;


        }
        public async Task<PagedResult<Demandletters>> GetDefaultListingReportData(DefaulterListingReportSearchDto defaulterListingReportSearchDto)
        {
         
            var data = await _dbContext.Demandletters
               .Include(x => x.Locality)

               .Where(x => x.UptoDate >= defaulterListingReportSearchDto.fromDate
                   && x.UptoDate < defaulterListingReportSearchDto.toDate)
               //.Where(x => x.UptoDate >= defaulterListingReportSearchDto.fromDate
               //    && x.UptoDate <= defaulterListingReportSearchDto.toDate)
                   //.OrderByDescending(x => x.Id)

                   .GetPaged(defaulterListingReportSearchDto.PageNumber, defaulterListingReportSearchDto.PageSize);

            int SortOrder = (int)defaulterListingReportSearchDto.SortOrder;
            if (SortOrder == 1)
            {
                switch (defaulterListingReportSearchDto.SortBy.ToUpper())
                {

                    case ("LOCALITY"):
                        data.Results = data.Results.OrderBy(x => x.Locality.Name).ToList();
                        break;

                    case ("UPTODATE"):
                        data.Results = data.Results.OrderBy(x => x.UptoDate).ToList();
                        break;
                    case ("FILENO"):
                        data.Results = data.Results.OrderBy(x => x.FileNo).ToList();
                        break;
                    case ("DEMANDNO"):
                        data.Results = data.Results.OrderBy(x => x.DemandNo).ToList();
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (defaulterListingReportSearchDto.SortBy.ToUpper())
                {

                    case ("LOCALITY"):
                        data.Results = data.Results.OrderByDescending(x => x.Locality.Name).ToList();
                        break;

                    case ("UPTODATE"):
                        data.Results = data.Results.OrderByDescending(x => x.UptoDate).ToList();
                        break;
                    case ("FILENO"):
                        data.Results = data.Results.OrderByDescending(x => x.FileNo).ToList();
                        break;
                             case ("DEMANDNO"):
                        data.Results = data.Results.OrderByDescending(x => x.DemandNo).ToList();
                        break; 

                }
            } 
            return data;
        }
        public async Task<List<Demandletters>> GetDefaultListingReportDataList(DefaulterListingReportSearchDto defaulterListingReportSearchDto)
        {

            var data = await _dbContext.Demandletters
               .Include(x => x.Locality)

               .Where(x => x.UptoDate >= defaulterListingReportSearchDto.fromDate
                   && x.UptoDate < defaulterListingReportSearchDto.toDate).ToListAsync();
            return data;
        }

        public async Task<List<Demandletters>> BindPropertyNoList()
        {
            var list = await _dbContext.Demandletters
                                    .Where(x => x.IsActive == 1)
                                    .ToListAsync();
            return list;
        }
        public async Task<List<Demandletters>> GetDemandLetterReportList(DownloadDemandLetterReportDto model)
        {
             var data =  await _dbContext.Demandletters
                                    .Include(x => x.Locality)
                                    .Where(x => (x.IsActive == 1)
                                   && (x.Id == (model.PropertyNo == 0 ? x.Id : model.PropertyNo))
                                   && (x.LocalityId == (model.Locality == 0 ? x.LocalityId : model.Locality))
                                    && (string.IsNullOrEmpty(model.FileNo) || x.FileNo.Contains(model.FileNo))
                                    && (x.CreatedDate.Date >= model.FromDate.Date && x.CreatedDate.Date <= model.ToDate.Date)
                                   )
                                   .ToListAsync();
            return data;
            
        }

        public async Task<PagedResult<Demandletters>> GetPagedDemandletterReport(DemandletterreportSearchDto model)
        {
            var data = await _dbContext.Demandletters
                               .Include(x => x.Locality)
                                   .Where(x => (x.IsActive == 1)
                                   && (x.Id == (model.PropertyNo == 0 ? x.Id : model.PropertyNo) )

                                   && (x.LocalityId == (model.Locality == 0 ? x.LocalityId : model.Locality))
                                    && (string.IsNullOrEmpty(model.FileNo) || x.FileNo.Contains(model.FileNo))
                                   
                                    && (x.CreatedDate.Date >= model.FromDate.Date && x.CreatedDate.Date <= model.ToDate.Date)
                                   )
                                   
                               .GetPaged<Demandletters>(model.PageNumber, model.PageSize);



            int SortOrder = (int)model.orderby;
            if (SortOrder == 1)
            {
                switch (model.colname.ToUpper())
                {
                    case ("PROPERTYNO"):
                        data.Results = data.Results.OrderBy(x => x.PropertyNo).ToList();
                        break;
                    case ("FILENO"):
                        data.Results = data.Results.OrderBy(x => x.FileNo).ToList();
                        break;
                    case ("LOCALITY"):
                        data.Results = data.Results.OrderBy(x => x.Locality.Name).ToList();
                        break;
                   

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.colname.ToUpper())
                {
                    case ("PROPERTYNO"):
                        data.Results = data.Results.OrderByDescending(x => x.PropertyNo).ToList();
                        break;
                    case ("FILENO"):
                        data.Results = data.Results.OrderByDescending(x => x.FileNo).ToList();
                        break;
                    case ("LOCALITY"):
                        data.Results = data.Results.OrderByDescending(x => x.Locality.Name).ToList();
                        break;
                   

                }
            }
            return data;




        }




        /*-----------------Relief Report Start------------------*/
        public async Task<PagedResult<Demandletters>> GetPagedReliefReport(ReliefReportSearchDto model)
        {
          var data = await _dbContext.Demandletters
                               .Include(x => x.Locality)
                                   .Where(x => (x.IsActive == 1 )
                                   && (x.Id == (model.FileNo == 0 ? x.Id : model.FileNo))
                                   && (x.LocalityId == (model.Locality == 0 ? x.LocalityId : model.Locality))
                                    && (x.GenerateDate.Date >= model.FromDate.Date && x.GenerateDate.Date <= model.ToDate.Date)
                                   )
                                   .OrderByDescending(x => x.Id)
                               .GetPaged<Demandletters>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("FILENO"):
                        data.Results = data.Results.OrderBy(x => x.FileNo).ToList();
                        break;
                    case ("LOCALITY"):
                        data.Results = data.Results.OrderBy(x => x.LocalityId).ToList();
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("FILENO"):
                        data.Results = data.Results.OrderByDescending(x => x.FileNo).ToList();
                        break;
                    case ("LOCALITY"):
                        data.Results = data.Results.OrderByDescending(x => x.LocalityId).ToList();
                        break;

                }
            }
            return data;
        }
        public async Task<List<Demandletters>> GetAllReliefReportList(ReliefReportSearchDto model)
        {
            var data = await _dbContext.Demandletters
                                 .Include(x => x.Locality)
                                     .Where(x => (x.IsActive == 1)
                                     && (x.Id == (model.FileNo == 0 ? x.Id : model.FileNo))
                                     && (x.LocalityId == (model.Locality == 0 ? x.LocalityId : model.Locality))
                                      && (x.GenerateDate.Date >= model.FromDate.Date && x.GenerateDate.Date <= model.ToDate.Date)
                                     ).ToListAsync();
            return (data);
        }
        public async Task<List<Demandletters>> BindFileNoList()
        {
            var list =await _dbContext.Demandletters
                                    .Where(x => x.IsActive == 1)
                                    .ToListAsync();
            return list;
        }

        public async Task<List<Locality>> BindLoclityList()
        {
            var InLocalitiesId = (from x in _dbContext.Demandletters
                                  where  x.IsActive == 1
                                  select x.LocalityId).ToArray();

            return await _dbContext.Locality
                                    .Where(x => x.IsActive == 1
                                    && (InLocalitiesId).Contains(x.Id))
                                    .ToListAsync();
        }
        /*-----------------Relief Report End------------------*/

        //*******   Penalty Imposition Report**********
        public async Task<List<Locality>> GetLocalityList()
        {
            //Zone id 59 only select damage payee locality list
            var localityList = await _dbContext.Locality.Where(x => x.IsActive == 1 && x.ZoneId == 59).ToListAsync();
            return localityList;
        }
        public async Task<List<PropertyType>> GetPropertyType()
        {
            var propertyList = await _dbContext.PropertyType.Where(x => x.IsActive == 1).ToListAsync();
            return propertyList;

        }
        public async Task<List<Demandletters>> GetFileNoList()
        {
            var fileNoList = await _dbContext.Demandletters.Where(x => x.IsActive == 1).ToListAsync();
            return fileNoList;
        }
        public async Task<PagedResult<Demandletters>> GetPagedPenaltyImpositionReport(PenaltyImpositionReportSearchDto model)
        {
            var data = await _dbContext.Demandletters 
                    .Include(x => x.Locality)
                    .Where(x => (x.Id == (model.fileNo == 0 ? x.Id : model.fileNo))
                   && (x.LocalityId == (model.locality == 0 ? x.LocalityId : model.locality)))
                    .GetPaged(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Demandletters
                                               .Include(x => x.Locality)
                                               .Where(x => (x.Id == (model.fileNo == 0 ? x.Id : model.fileNo))
                                               && (x.LocalityId == (model.locality == 0 ? x.LocalityId : model.locality)))
                                               .OrderBy(s => s.Locality.Name)
                                               .GetPaged(model.PageNumber, model.PageSize);
                        break;
                   
                    case ("FILENO"):
                        data = null;
                        data = await _dbContext.Demandletters
                                               .Include(x => x.Locality)
                                               .Where(x => (x.Id == (model.fileNo == 0 ? x.Id : model.fileNo))
                                               && (x.LocalityId == (model.locality == 0 ? x.LocalityId : model.locality)))
                                               .OrderBy(s => s.FileNo)
                                               .GetPaged(model.PageNumber, model.PageSize);
                        break;
                    case ("PROPERTYNO"):
                        data = null;
                        data = await _dbContext.Demandletters
                                               .Include(x => x.Locality)
                                               .Where(x => (x.Id == (model.fileNo == 0 ? x.Id : model.fileNo))
                                               && (x.LocalityId == (model.locality == 0 ? x.LocalityId : model.locality)))
                                               .OrderBy(s => s.PropertyNo)
                                               .GetPaged(model.PageNumber, model.PageSize);
                        break;
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Demandletters
                                               .Include(x => x.Locality)
                                               .Where(x => (x.Id == (model.fileNo == 0 ? x.Id : model.fileNo))
                                               && (x.LocalityId == (model.locality == 0 ? x.LocalityId : model.locality)))
                                               .OrderBy(s => s.Name)
                                               .GetPaged(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Demandletters
                                               .Include(x => x.Locality)
                                               .Where(x => (x.Id == (model.fileNo == 0 ? x.Id : model.fileNo))
                                               && (x.LocalityId == (model.locality == 0 ? x.LocalityId : model.locality)))
                                               .OrderByDescending(s => s.Locality.Name)
                                               .GetPaged(model.PageNumber, model.PageSize);
                        break;

                    case ("FILENO"):
                        data = null;
                        data = await _dbContext.Demandletters
                                               .Include(x => x.Locality)
                                               .Where(x => (x.Id == (model.fileNo == 0 ? x.Id : model.fileNo))
                                               && (x.LocalityId == (model.locality == 0 ? x.LocalityId : model.locality)))
                                               .OrderByDescending(s => s.FileNo)
                                               .GetPaged(model.PageNumber, model.PageSize);
                        break;
                    case ("PROPERTYNO"):
                        data = null;
                        data = await _dbContext.Demandletters
                                               .Include(x => x.Locality)
                                               .Where(x => (x.Id == (model.fileNo == 0 ? x.Id : model.fileNo))
                                               && (x.LocalityId == (model.locality == 0 ? x.LocalityId : model.locality)))
                                               .OrderByDescending(s => s.PropertyNo)
                                               .GetPaged(model.PageNumber, model.PageSize);
                        break;
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Demandletters
                                               .Include(x => x.Locality)
                                               .Where(x => (x.Id == (model.fileNo == 0 ? x.Id : model.fileNo))
                                               && (x.LocalityId == (model.locality == 0 ? x.LocalityId : model.locality)))
                                               .OrderByDescending(s => s.Name)
                                               .GetPaged(model.PageNumber, model.PageSize);
                        break;

                }
            }

            return data;

        }
        //*******  Imposition Of Charges Report**********
        public async Task<PagedResult<Demandletters>> GetPagedImpositionReportOfCharges(ImpositionOfChargesSearchDto model)
        {
            //return await _dbContext.Demandletters
            //                   .Include(x => x.Locality)
            //                       .Where(x => (x.IsActive == 1)
            //                       && (x.Id == (model.FileNo == 0 ? x.Id : model.FileNo))
            //                       && (x.LocalityId == (model.Locality == 0 ? x.LocalityId : model.Locality))
            //                        && (x.GenerateDate >= model.FromDate && x.GenerateDate <= model.ToDate)
            //                       )
            //                       .OrderByDescending(x => x.Id)
            //                   .GetPaged<Demandletters>(model.PageNumber, model.PageSize);
            var data = await _dbContext.Demandletters
                               .Include(x => x.Locality)
                                   .Where(x => (x.IsActive == 1)
                                   && (x.Id == (model.FileNo == 0 ? x.Id : model.FileNo))
                                   && (x.LocalityId == (model.Locality == 0 ? x.LocalityId : model.Locality))
                                    && (x.GenerateDate.Date >= model.FromDate.Date && x.GenerateDate.Date <= model.ToDate.Date)
                                   )
                                   .OrderByDescending(x => x.Id)
                               .GetPaged<Demandletters>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("FILENO"):
                        data.Results = data.Results.OrderBy(x => x.FileNo).ToList();
                        break;
                    case ("LOCALITY"):
                        data.Results = data.Results.OrderBy(x => x.LocalityId).ToList();
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("FILENO"):
                        data.Results = data.Results.OrderByDescending(x => x.FileNo).ToList();
                        break;
                    case ("LOCALITY"):
                        data.Results = data.Results.OrderByDescending(x => x.LocalityId).ToList();
                        break;

                }
            }
            return data;
        }

        public async Task<List<Demandletters>> GetImpositionReportOfChargesList(ImpositionOfChargesSearchDto model)
        {            
            var data = await _dbContext.Demandletters
                               .Include(x => x.Locality)
                                   .Where(x => (x.IsActive == 1)
                                   && (x.Id == (model.FileNo == 0 ? x.Id : model.FileNo))
                                   && (x.LocalityId == (model.Locality == 0 ? x.LocalityId : model.Locality))
                                    && (x.GenerateDate.Date >= model.FromDate.Date && x.GenerateDate.Date <= model.ToDate.Date)
                                   ).ToListAsync();
            return data;
        }
        public async Task<PagedResult<Demandletters>> GetPagedDemandCollectionLedgerReport(DemandCollectionLedgerSearchDto model)
        {
            var data = await _dbContext.Demandletters
                               .Include(x => x.Locality)
                                   .Where(x => (x.IsActive == 1)
                                   && (x.FileNo.ToUpper().Trim() == (model.FileNo.Trim() == "All" ? x.FileNo.ToUpper().Trim() : model.FileNo.ToUpper().Trim()))
                                   && (x.LocalityId == (model.Locality == 0 ? x.LocalityId : model.Locality))
                                   )
                                   .OrderByDescending(x => x.Id)
                               .GetPaged<Demandletters>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("FILENO"):
                        data.Results = data.Results.OrderBy(x => x.FileNo).ToList();
                        break;
                    case ("LOCALITY"):
                        data.Results = data.Results.OrderBy(x => x.LocalityId).ToList();
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("FILENO"):
                        data.Results = data.Results.OrderByDescending(x => x.FileNo).ToList();
                        break;
                    case ("LOCALITY"):
                        data.Results = data.Results.OrderByDescending(x => x.LocalityId).ToList();
                        break;

                }
            }
            return data;
        }

        public async Task<List<DemandCollectionLedgerListDataDto>> GetPagedDemandCollectionLedgerReport1(DemandCollectionLedgerSearchDto model)
        {
            //      List<Demandletters> olist = new List<Demandletters>();

            //      var Data = await (from a in _dbContext.Demandletters
            //                        where (a.FileNo.ToUpper().Trim() == (model.FileNo.Trim() == "All" ? a.FileNo.ToUpper().Trim() : model.FileNo.ToUpper().Trim()))
            //                        && (a.PropertyNo.ToUpper().Trim() == (model.PropertyNo.Trim() == "All" ? a.PropertyNo.ToUpper().Trim() : model.PropertyNo.ToUpper().Trim()))
            //                        && (a.LocalityId == (model.Locality == 0 ? a.LocalityId : model.Locality))
            //                        select new
            //                        {
            //                            FileNo = a.FileNo,
            //                            PropertyNo = a.PropertyNo,
            //                            DemandAmount = a.DepositDue

            //                        }).Union
            //(from l in _dbContext.Paymentverification
            // where (l.FileNo.ToUpper().Trim() == (model.FileNo.Trim() == "All" ? l.FileNo.ToUpper().Trim() : model.FileNo.ToUpper().Trim()))
            //      && (l.PropertyNo.ToUpper().Trim() == (model.PropertyNo.Trim() == "All" ? l.PropertyNo.ToUpper().Trim() : model.PropertyNo.ToUpper().Trim()))
            // select new
            // {
            //     PaymentAmount = l.AmountPaid,
            //     What = l.PayeeName
            // }).OrderByDescending(c => c.Date).tolist;

            //      if (Data != null)
            //      {
            //          for (int i = 0; i < Data.Count; i++)
            //          {
            //              olist.Add(new Demandletters()
            //              {
            //                  Id = Data[i].ZoneId,
            //                  Name = Data[i].ZoneName,
            //                  Code = Data[i].ZoneCode,
            //                  DepartmentName = Data[i].DepartmentName,
            //                  IsActive = Data[i].IsActive
            //              });
            //          }
            //      }
            //      return olist;

            try
            {
                int SortOrder = (int)model.SortOrder;
                var data = await _dbContext.LoadStoredProcedure("BindDemandCollectionLedgerReport")
                                            .WithSqlParams(("P_FileNo", model.FileNo), ("P_PropertyNo", model.PropertyNo)
                                            , ("P_LocalityId", model.Locality), ("P_SortOrder", SortOrder)
                                            , ("P_SortBy", model.SortBy))
                                            .ExecuteStoredProcedureAsync<DemandCollectionLedgerListDataDto>();

                return (List<DemandCollectionLedgerListDataDto>)data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<PagedResult<Demandletters>> GetPagedDuplicateDemandletter(DuplicateDemandLetterSearchDto model)
        {
            var data = await _dbContext.Demandletters.Include(x => x.Locality)
                 .Where(x => (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                   && (string.IsNullOrEmpty(model.demandno) || x.DemandNo.Contains(model.demandno))
                    && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))

                    && (x.IsActive == 1)
                  )


                .GetPaged<Demandletters>(model.PageNumber, model.PageSize);


            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Demandletters.Include(x => x.Locality)
                 .Where(x => (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                   && (string.IsNullOrEmpty(model.demandno) || x.DemandNo.Contains(model.demandno))
                    && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))

                    && (x.IsActive == 1)
                  ).OrderBy(s => s.Locality)
                                .GetPaged<Demandletters>(model.PageNumber, model.PageSize);
                        break;
                    case ("DEMANDNO"):
                        data = null;
                        data = await _dbContext.Demandletters.Include(x => x.Locality)
                                        .Where(x => (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                          && (string.IsNullOrEmpty(model.demandno) || x.DemandNo.Contains(model.demandno))
                                           && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                                           && (x.IsActive == 1)
                  ).OrderBy(s => s.DemandNo)
                                .GetPaged<Demandletters>(model.PageNumber, model.PageSize);

                        break;
                    case ("FILENO"):
                        data = null;
                        data = await _dbContext.Demandletters.Include(x => x.Locality)
                                       .Where(x => (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                         && (string.IsNullOrEmpty(model.demandno) || x.DemandNo.Contains(model.demandno))
                                          && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                                          && (x.IsActive == 1)
                 ).OrderBy(s => s.FileNo)
                               .GetPaged<Demandletters>(model.PageNumber, model.PageSize);

                        break;




                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Demandletters.Include(x => x.Locality)
                 .Where(x => (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                   && (string.IsNullOrEmpty(model.demandno) || x.DemandNo.Contains(model.demandno))
                    && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))

                    && (x.IsActive == 1)
                  ).OrderByDescending(s => s.Locality)
                                .GetPaged<Demandletters>(model.PageNumber, model.PageSize);
                        break;
                    case ("DEMANDNO"):
                        data = null;
                        data = await _dbContext.Demandletters.Include(x => x.Locality)
                                        .Where(x => (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                          && (string.IsNullOrEmpty(model.demandno) || x.DemandNo.Contains(model.demandno))
                                           && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                                           && (x.IsActive == 1)
                  ).OrderByDescending(s => s.DemandNo)
                                .GetPaged<Demandletters>(model.PageNumber, model.PageSize);

                        break;
                    case ("FILENO"):
                        data = null;
                        data = await _dbContext.Demandletters.Include(x => x.Locality)
                                       .Where(x => (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                         && (string.IsNullOrEmpty(model.demandno) || x.DemandNo.Contains(model.demandno))
                                          && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                                          && (x.IsActive == 1)
                 ).OrderByDescending(s => s.FileNo)
                               .GetPaged<Demandletters>(model.PageNumber, model.PageSize);

                        break;



                }
            }







            return data;


        }









        public async Task<List<DuesVsPaidAmountDto>> GetDuesVsPaidAmountListDto(DuesVsPaidAmountSearchDto model)
   {
            try
            {


                var data = await _dbContext.LoadStoredProcedure("Get_DuesvsPaidAmount")
                                            .WithSqlParams(("P_filenoId", model.fileno)
                                            ,("P_fromdate",Convert.ToDateTime(model.fromdate))
                                            , ("P_todate", Convert.ToDateTime(model.todate))
                                            )



                                            .ExecuteStoredProcedureAsync<DuesVsPaidAmountDto>();

                return (List<DuesVsPaidAmountDto>)data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<List<Damagepayeeregister>> GetFileAutoCompleteDetails(string prefix) 
        {
            var data = await _dbContext.Damagepayeeregister.Where(x => x.FileNo.Contains(prefix)).OrderBy(p => p.FileNo).ToListAsync();
            return data;
        } 
        public async Task<Damagepayeeregister> GetFileDetails(int fileid)
        {
            return await _dbContext.Damagepayeeregister
                .Include(x=>x.Damagepayeepersonelinfo)
                .Where(x=>x.Id == fileid).FirstOrDefaultAsync();
        }
        public async Task<Encrochmenttype> FetchResultEncroachmentType(DateTime date1)
        {
            return await _dbContext.Encrochmenttype
                                .Where(x => x.EncroachStartDate <= date1 && x.EncroachEndDate >= date1)
                                .FirstOrDefaultAsync();
        }
        public async Task<List<Resratelisttypea>> RateListTypeA(DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _dbContext.Resratelisttypea
                                    .Where(x => x.StartDate <= date1 && x.EndDate >= date1
                                    && subEncroachersId.Contains(x.SubEncroachId)
                                    && x.ColonyId == Convert.ToInt32(localityId)
                                    )
                                    .ToListAsync();
        }
        public async Task<List<Resratelisttypeb>> RateListTypeB(DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _dbContext.Resratelisttypeb
                                    .Where(x => x.StartDate <= date1 && x.EndDate >= date1
                                    && subEncroachersId.Contains(x.SubEncroachId)
                                    && x.ColonyId == Convert.ToInt32(localityId)
                                    )
                                    .ToListAsync();
        }
        public async Task<List<Resratelisttypec>> RateListTypeC(DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _dbContext.Resratelisttypec
                                    .Where(x => x.StartDate <= date1 && x.EndDate >= date1
                                    && subEncroachersId.Contains(x.SubEncroachId)
                                    && x.ColonyId == Convert.ToInt32(localityId)
                                    )
                                    .ToListAsync();
        }

        public async Task<List<Resratelisttypeb>> RateListTypeBSpecific(DateTime dateTimeSpecific, DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _dbContext.Resratelisttypeb
                                   .Where(x => ((x.StartDate <= dateTimeSpecific && x.EndDate >= dateTimeSpecific)
                                   || (x.StartDate <= date1 && x.EndDate >= date1))
                                   && (subEncroachersId.Contains(x.SubEncroachId))
                                   && (x.ColonyId == Convert.ToInt32(localityId))
                                   )
                                   .ToListAsync();
        }
        public async Task<List<Resratelisttypea>> RateListTypeASpecific(DateTime specificDateTime, DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _dbContext.Resratelisttypea
                                    .Where(x => ((x.StartDate <= specificDateTime && x.EndDate >= specificDateTime)
                                   || (x.StartDate <= date1 && x.EndDate >= date1))
                                   && (subEncroachersId.Contains(x.SubEncroachId))
                                   && (x.ColonyId == Convert.ToInt32(localityId))
                                   )
                                   .ToListAsync();
        }
        public async Task<Comencrochmenttype> FetchResultCOMEncroachmentType(DateTime date1)
        {
            return await _dbContext.Comencrochmenttype
                               .Where(x => x.EncroachStartDate <= date1 && x.EncroachEndDate >= date1)
                               .FirstOrDefaultAsync();
        }
        public async Task<List<Comratelisttypea>> ComRateListTypeA(DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _dbContext.Comratelisttypea
                                    .Where(x => x.StartDate <= date1 && x.EndDate >= date1
                                    && subEncroachersId.Contains(x.SubEncroachId)
                                    && x.ColonyId == Convert.ToInt32(localityId)
                                    )
                                    .ToListAsync();
        }
        public async Task<List<Comratelisttypeb>> ComRateListTypeB(DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _dbContext.Comratelisttypeb
                                    .Where(x => x.StartDate <= date1 && x.EndDate >= date1
                                    && subEncroachersId.Contains(x.SubEncroachId)
                                    && x.ColonyId == Convert.ToInt32(localityId)
                                    )
                                    .ToListAsync();
        }
        public async Task<List<Comratelisttypec>> ComRateListTypeC(DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _dbContext.Comratelisttypec
                                    .Where(x => x.StartDate <= date1 && x.EndDate >= date1
                                    && subEncroachersId.Contains(x.SubEncroachId)
                                    && x.ColonyId == Convert.ToInt32(localityId)
                                    )
                                    .ToListAsync();
        }
        public async Task<List<Comratelisttypeb>> ComRateListTypeBSpecific(DateTime dateTimeSpecific, DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _dbContext.Comratelisttypeb
                                   .Where(x => ((x.StartDate <= dateTimeSpecific && x.EndDate >= dateTimeSpecific)
                                   || (x.StartDate <= date1 && x.EndDate >= date1))
                                   && (subEncroachersId.Contains(x.SubEncroachId))
                                   && (x.ColonyId == Convert.ToInt32(localityId))
                                   )
                                   .ToListAsync();
        }

        public async Task<List<Comratelisttypea>> ComRateListTypeASpecific(DateTime specificDateTime, DateTime date1, string localityId, int[] subEncroachersId)
        {
            return await _dbContext.Comratelisttypea
                                    .Where(x => ((x.StartDate <= specificDateTime && x.EndDate >= specificDateTime)
                                   || (x.StartDate <= date1 && x.EndDate >= date1))
                                   && (subEncroachersId.Contains(x.SubEncroachId))
                                   && (x.ColonyId == Convert.ToInt32(localityId))
                                   )
                                   .ToListAsync();
        }
    }
}