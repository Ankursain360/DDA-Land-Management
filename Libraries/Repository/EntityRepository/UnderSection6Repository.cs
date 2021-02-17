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

namespace Libraries.Repository.EntityRepository
{
  public  class UnderSection6Repository : GenericRepository<Undersection6>, IUnderSection6Repository
    {
        public UnderSection6Repository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Undersection4>> GetAllundersection4()
        {
            List<Undersection4> purposeList = await _dbContext.Undersection4.Where(x => x.IsActive == 1).ToListAsync();
            return purposeList;
        }
        public async Task<List<Undersection6>> GetAllUndersection6()
        {
            return await _dbContext.Undersection6.Include(x => x.Undersection4).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<PagedResult<Undersection6>> GetPagedUndersection6details(Undersection6SearchDto model)
        {
            var data = await _dbContext.Undersection6.Include(x => x.Undersection4)
                .Where(x => (string.IsNullOrEmpty(model.numbernotification4) || x.Undersection4.Number.Contains(model.numbernotification4))
                && (string.IsNullOrEmpty(model.notificationno) || x.Number.Contains(model.notificationno))
             
               )
               .GetPaged<Undersection6>(model.PageNumber, model.PageSize);




            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("UNDERSECTION4NO"):
                        data = null;
                        data = await _dbContext.Undersection6.Include(x => x.Undersection4)
                .Where(x => (string.IsNullOrEmpty(model.numbernotification4) || x.Undersection4.Number.Contains(model.numbernotification4))
                && (string.IsNullOrEmpty(model.notificationno) || x.Number.Contains(model.notificationno))

               )
                                .OrderBy(s => s.Undersection4.Number)
                                .GetPaged<Undersection6>(model.PageNumber, model.PageSize);
                        break;
                    case ("NUMBER"):
                        data = null;
                        data = await _dbContext.Undersection6.Include(x => x.Undersection4)
                .Where(x => (string.IsNullOrEmpty(model.numbernotification4) || x.Undersection4.Number.Contains(model.numbernotification4))
                && (string.IsNullOrEmpty(model.notificationno) || x.Number.Contains(model.notificationno))

               )
                                 .OrderBy(s => s.Number)
                                .GetPaged<Undersection6>(model.PageNumber, model.PageSize);

                        break;
              

                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Undersection6.Include(x => x.Undersection4)
               .Where(x => (string.IsNullOrEmpty(model.numbernotification4) || x.Undersection4.Number.Contains(model.numbernotification4))
               && (string.IsNullOrEmpty(model.notificationno) || x.Number.Contains(model.notificationno))

              )
                               .OrderByDescending(s => s.IsActive)
                                .GetPaged<Undersection6>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("UNDERSECTION4NO"):
                        data = null;
                        data = await _dbContext.Undersection6.Include(x => x.Undersection4)
                .Where(x => (string.IsNullOrEmpty(model.numbernotification4) || x.Undersection4.Number.Contains(model.numbernotification4))
                && (string.IsNullOrEmpty(model.notificationno) || x.Number.Contains(model.notificationno))

               )
                                .OrderByDescending(s => s.Undersection4.Number)
                                .GetPaged<Undersection6>(model.PageNumber, model.PageSize);
                        break;
                    case ("NUMBER"):
                        data = null;
                        data = await _dbContext.Undersection6.Include(x => x.Undersection4)
                .Where(x => (string.IsNullOrEmpty(model.numbernotification4) || x.Undersection4.Number.Contains(model.numbernotification4))
                && (string.IsNullOrEmpty(model.notificationno) || x.Number.Contains(model.notificationno))

               )
                                 .OrderByDescending(s => s.Number)
                                .GetPaged<Undersection6>(model.PageNumber, model.PageSize);

                        break;

                 
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Undersection6.Include(x => x.Undersection4)
                .Where(x => (string.IsNullOrEmpty(model.numbernotification4) || x.Undersection4.Number.Contains(model.numbernotification4))
                && (string.IsNullOrEmpty(model.notificationno) || x.Number.Contains(model.notificationno))

               )
                                 .OrderBy(s => s.IsActive)
                                .GetPaged<Undersection6>(model.PageNumber, model.PageSize);
                        break;

                }
            }


            return data;

        }


    }
}
