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
    public class RequestforproceedingRepository : GenericRepository<Requestforproceeding>, IRequestforproceedingRepository
    {
        public RequestforproceedingRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Requestforproceeding>> GetAllRequestForProceeding()
        {
            return await _dbContext.Requestforproceeding.Include(x => x.Allotment).OrderByDescending(x => x.Id).ToListAsync();
        }


        public async Task<PagedResult<Requestforproceeding>> GetPagedRequestForProceeding(RequestForProceedingSearchDto model)
        {
            var data = await _dbContext.Requestforproceeding.Include(x => x.Allotment)
                .Where(x => (string.IsNullOrEmpty(model.letterReferenceNo) || x.LetterReferenceNo.Contains(model.letterReferenceNo))
                    && (string.IsNullOrEmpty(model.subject) || x.Subject.Contains(model.subject))

               ).



                GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);



            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("UNDERSECTION4NO"):
                        data = null;
                        data = await _dbContext.Requestforproceeding.Include(x => x.Allotment)
                  .Where(x => (string.IsNullOrEmpty(model.letterReferenceNo) || x.LetterReferenceNo.Contains(model.letterReferenceNo))
                      && (string.IsNullOrEmpty(model.subject) || x.Subject.Contains(model.subject))

                 )
                                .OrderBy(s => s.LetterReferenceNo)
                                .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);
                        break;
                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Requestforproceeding.Include(x => x.Allotment)
                 .Where(x => (string.IsNullOrEmpty(model.letterReferenceNo) || x.LetterReferenceNo.Contains(model.letterReferenceNo))
                     && (string.IsNullOrEmpty(model.subject) || x.Subject.Contains(model.subject))

                   )
                                    .OrderBy(s => s.Subject)
                                .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);

                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Requestforproceeding.Include(x => x.Allotment)
                 .Where(x => (string.IsNullOrEmpty(model.letterReferenceNo) || x.LetterReferenceNo.Contains(model.letterReferenceNo))
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
                    case ("UNDERSECTION4NO"):
                        data = null;
                        data = await _dbContext.Requestforproceeding.Include(x => x.Allotment)
                  .Where(x => (string.IsNullOrEmpty(model.letterReferenceNo) || x.LetterReferenceNo.Contains(model.letterReferenceNo))
                      && (string.IsNullOrEmpty(model.subject) || x.Subject.Contains(model.subject))

                 )
                                .OrderByDescending(s => s.LetterReferenceNo)
                                .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);
                        break;
                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Requestforproceeding.Include(x => x.Allotment)
                 .Where(x => (string.IsNullOrEmpty(model.letterReferenceNo) || x.LetterReferenceNo.Contains(model.letterReferenceNo))
                     && (string.IsNullOrEmpty(model.subject) || x.Subject.Contains(model.subject))

                   )
                                    .OrderByDescending(s => s.Subject)
                                .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);
                        break;

                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Requestforproceeding.Include(x => x.Allotment)
                 .Where(x => (string.IsNullOrEmpty(model.letterReferenceNo) || x.LetterReferenceNo.Contains(model.letterReferenceNo))
                     && (string.IsNullOrEmpty(model.subject) || x.Subject.Contains(model.subject))

               )
                           .OrderBy(s => s.IsActive)
                                .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);
                        break;

                }
            }


            return data;


        }












    }
}
