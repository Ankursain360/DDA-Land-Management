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
using Dto.Master;
using Repository.Common;


namespace Libraries.Repository.EntityRepository
{
    public class RequestforproceedingRepository : GenericRepository<Requestforproceeding>, IRequestforproceedingRepository
    {
        public RequestforproceedingRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Requestforproceeding>> GetAllRequestForProceeding()
        {
            return await _dbContext.Requestforproceeding
                                   .Include(x => x.Allotment)
                                   .Include(x => x.Allotment.Application)
                                   .Include(x=>x.Honble)
                                   .OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<List<Honble>> GetAllHonble()
        {
            List<Honble> villageList = await _dbContext.Honble.Where(x => x.IsActive == 1).ToListAsync();
            return villageList;
        }

        public async Task<List<Allotmententry>> GetAllAllotment()
        {
            //List<Allotmententry> villageList = await _dbContext.Allotmententry.Include(x=>x.Application).Where(x => (x.ApplicationId == x.Application.Id && x.IsActive == 1)).ToListAsync();
            //return villageList;

            List<Allotmententry> villageList = await _dbContext.Allotmententry.Include(x => x.Application).
                Include(x => x.LeasePurposesType).Where(x => (x.ApplicationId == x.Application.Id && x.IsActive == 1 && x.LeasePurposesTypeId==x.LeasePurposesType.Id)).ToListAsync();
            return villageList;
        }



        public async Task<PagedResult<Requestforproceeding>> GetPagedRequestForProceeding(RequestForProceedingSearchDto model)
        {
            var data = await _dbContext.Requestforproceeding
               // .Include(x => x.Allotment).Include(x => x.Allotment.Application)
               //   .Where(x => (string.IsNullOrEmpty(model.letterReferenceNo) || x.LetterReferenceNo.Contains(model.letterReferenceNo))
               //     && (string.IsNullOrEmpty(model.AllotmentNo) || x.Allotment.Application.RefNo.Contains(model.AllotmentNo))
               //     && (string.IsNullOrEmpty(model.subject) || x.Subject.Contains(model.subject))

               //)
               //
               . GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);




            int SortOrder = (int)model.SortOrder;
            //if (SortOrder == 0)
            //{
            //     data = await _dbContext.Requestforproceeding
            //       // .Include(x => x.Allotment).Include(x => x.Allotment.Application)
            //       //   .Where(x => (string.IsNullOrEmpty(model.letterReferenceNo) || x.LetterReferenceNo.Contains(model.letterReferenceNo))
            //       //     && (string.IsNullOrEmpty(model.AllotmentNo) || x.Allotment.Application.RefNo.Contains(model.AllotmentNo))
            //       //     && (string.IsNullOrEmpty(model.subject) || x.Subject.Contains(model.subject))

            //       //).



            //       . GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);
            //}

            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("LETTERREFNO"):
                        data = null;
                        data = await _dbContext.Requestforproceeding.Include(x => x.Allotment).Include(x => x.Allotment.Application)
                  .Where(x => (string.IsNullOrEmpty(model.letterReferenceNo) || x.Allotment.Application.Name.Contains(model.letterReferenceNo))
                     && (string.IsNullOrEmpty(model.AllotmentNo) || x.Allotment.Application.RefNo.Contains(model.AllotmentNo))
                      && (string.IsNullOrEmpty(model.subject) || x.Subject.Contains(model.subject))

                 )
                                .OrderBy(s => s.Allotment.Application.Name)
                                .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);
                        break;


                    case ("ALLOTMENTNO"):
                        data = null;
                        data = await _dbContext.Requestforproceeding.Include(x => x.Allotment).Include(x => x.Allotment.Application)
                  .Where(x => (string.IsNullOrEmpty(model.letterReferenceNo) || x.Allotment.Application.Name.Contains(model.letterReferenceNo))
                     && (string.IsNullOrEmpty(model.AllotmentNo) || x.Allotment.Application.RefNo.Contains(model.AllotmentNo))
                      && (string.IsNullOrEmpty(model.subject) || x.Subject.Contains(model.subject))

                 )
                                .OrderBy(s => s.Allotment.Application.RefNo)
                                .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);
                        break;


                    case ("SUBJECT"):
                        data = null;
                        data = await _dbContext.Requestforproceeding.Include(x => x.Allotment).Include(x => x.Allotment.Application)
                 .Where(x => (string.IsNullOrEmpty(model.letterReferenceNo) || x.Allotment.Application.Name.Contains(model.letterReferenceNo))
                    && (string.IsNullOrEmpty(model.AllotmentNo) || x.Allotment.Application.RefNo.Contains(model.AllotmentNo))
                     && (string.IsNullOrEmpty(model.subject) || x.Subject.Contains(model.subject))

                   )
                                    .OrderBy(s => s.Subject)
                                .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);

                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Requestforproceeding.Include(x => x.Allotment).Include(x => x.Allotment.Application)
                 .Where(x => (string.IsNullOrEmpty(model.letterReferenceNo) || x.Allotment.Application.Name.Contains(model.letterReferenceNo))
                    && (string.IsNullOrEmpty(model.AllotmentNo) || x.Allotment.Application.RefNo.Contains(model.AllotmentNo))
                     && (string.IsNullOrEmpty(model.subject) || x.Subject.Contains(model.subject))

               )
                           .OrderByDescending(s => s.IsActive)
                                .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("LETTERREFNO"):
                        data = null;
                        data = await _dbContext.Requestforproceeding.Include(x => x.Allotment).Include(x => x.Allotment.Application)
                  .Where(x => (string.IsNullOrEmpty(model.letterReferenceNo) || x.Allotment.Application.Name.Contains(model.letterReferenceNo))
                     && (string.IsNullOrEmpty(model.AllotmentNo) || x.Allotment.Application.RefNo.Contains(model.AllotmentNo))
                      && (string.IsNullOrEmpty(model.subject) || x.Subject.Contains(model.subject))

                 )
                                .OrderByDescending(s => s.Allotment.Application.Name)
                                .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);
                        break;


                    case ("ALLOTMENTNO"):
                        data = null;
                        data = await _dbContext.Requestforproceeding.Include(x => x.Allotment).Include(x => x.Allotment.Application)
                  .Where(x => (string.IsNullOrEmpty(model.letterReferenceNo) || x.Allotment.Application.Name.Contains(model.letterReferenceNo))
                     && (string.IsNullOrEmpty(model.AllotmentNo) || x.Allotment.Application.RefNo.Contains(model.AllotmentNo))
                      && (string.IsNullOrEmpty(model.subject) || x.Subject.Contains(model.subject))

                 )
                                .OrderByDescending(s => s.Allotment.Application.RefNo)
                                .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);
                        break;


                    case ("SUBJECT"):
                        data = null;
                        data = await _dbContext.Requestforproceeding.Include(x => x.Allotment).Include(x => x.Allotment.Application)
                 .Where(x => (string.IsNullOrEmpty(model.letterReferenceNo) || x.Allotment.Application.Name.Contains(model.letterReferenceNo))
                    && (string.IsNullOrEmpty(model.AllotmentNo) || x.Allotment.Application.RefNo.Contains(model.AllotmentNo))
                     && (string.IsNullOrEmpty(model.subject) || x.Subject.Contains(model.subject))

                   )
                                    .OrderByDescending(s => s.Subject)
                                .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);
                        break;

                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Requestforproceeding.Include(x => x.Allotment).Include(x => x.Allotment.Application)
                 .Where(x => (string.IsNullOrEmpty(model.letterReferenceNo) || x.Allotment.Application.Name.Contains(model.letterReferenceNo))
                    && (string.IsNullOrEmpty(model.AllotmentNo) || x.Allotment.Application.RefNo.Contains(model.AllotmentNo))
                     && (string.IsNullOrEmpty(model.subject) || x.Subject.Contains(model.subject))

               )
                           .OrderBy(s => s.IsActive)
                                .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);
                        break;

                }
            }


            return data;


        }






        public async Task<List<UserBindDropdownDto>> BindUsernameNameList()
        {
            try
            {
                var data = await _dbContext.LoadStoredProcedure("BindUserZoneDropDown")
                                            .WithOutParams()
                                            .ExecuteStoredProcedureAsync<UserBindDropdownDto>();

                return (List<UserBindDropdownDto>)data;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Cancellationentry>> GetCancellationListData()
        {
            return await _dbContext.Cancellationentry
                                    .Include(x => x.Allotment)
                                    .Include(x => x.Allotment.Application)
                                    .Where(x => x.IsActive == 1)
                                    .ToListAsync();
        }
        public async Task<Cancellationentry> FetchCancellationDetailsDetails(int CancellationId)
        {
            return await _dbContext.Cancellationentry
                                    .Include(x => x.Allotment)
                                    .Include(x => x.Allotment.Application)
                                    .Include(x => x.Allotment.LeasePurposesType)
                                    .Where(x => x.Id == CancellationId)
                                    .FirstOrDefaultAsync();
        }
    }
}
