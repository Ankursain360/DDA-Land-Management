using Dto.Master;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Libraries.Repository.EntityRepository
{
    public class Undersection17Repository : GenericRepository<Undersection17>, IUndersection17Repository
    {
        public Undersection17Repository(DataContext dbContext) : base(dbContext)
        {

        }
      

       
        public async Task<List<Undersection6>> GetAllUndersection6List()
        {
            List<Undersection6> undersection6List = await _dbContext.Undersection6.ToListAsync();
            return undersection6List;
        }

        public async Task<PagedResult<Undersection17>> GetPagedUndersection17(UnderSection17SearchDto model)
        {
            //return await _dbContext.Undersection17.
            //    Where(x => x.IsActive == 1).Include(x => x.UnderSection6)
            //   .GetPaged<Undersection17>(model.PageNumber, model.PageSize);
            var data = await _dbContext.Undersection17
                        .Include(x => x.UnderSection6)
                        .Where(x => (string.IsNullOrEmpty(model.number) || x.Number.Contains(model.number))
                         && (string.IsNullOrEmpty(model.undersection6) || x.UnderSection6.Number.Contains(model.undersection6)))

                        .GetPaged<Undersection17>(model.PageNumber, model.PageSize);


            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NUMBER"):
                        data = null;
                        data = await _dbContext.Undersection17
                                     .Include(x => x.UnderSection6)
                        .Where(x => (string.IsNullOrEmpty(model.number) || x.Number.Contains(model.number))
                         && (string.IsNullOrEmpty(model.undersection6) || x.UnderSection6.Number.Contains(model.undersection6)))
                                    .OrderBy(a => a.Number)
                                    .GetPaged<Undersection17>(model.PageNumber, model.PageSize);
                        break;

                    case ("SECTION6"):
                        data = null;
                        data = await _dbContext.Undersection17
                                     .Include(x => x.UnderSection6)
                        .Where(x => (string.IsNullOrEmpty(model.number) || x.Number.Contains(model.number))
                         && (string.IsNullOrEmpty(model.undersection6) || x.UnderSection6.Number.Contains(model.undersection6)))
                                    .OrderBy(a => a.UnderSection6.Number)
                                    .GetPaged<Undersection17>(model.PageNumber, model.PageSize);
                        break;
                    case ("NOTIFICATIONDATE"):
                        data = null;
                        data = await _dbContext.Undersection17
                                     .Include(x => x.UnderSection6)
                        .Where(x => (string.IsNullOrEmpty(model.number) || x.Number.Contains(model.number))
                         && (string.IsNullOrEmpty(model.undersection6) || x.UnderSection6.Number.Contains(model.undersection6)))
                                   .OrderBy(a => a.NotificationDate)
                                    .GetPaged<Undersection17>(model.PageNumber, model.PageSize);
                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Undersection17
                                     .Include(x => x.UnderSection6)
                        .Where(x => (string.IsNullOrEmpty(model.number) || x.Number.Contains(model.number))
                         && (string.IsNullOrEmpty(model.undersection6) || x.UnderSection6.Number.Contains(model.undersection6)))
                                    .OrderByDescending(a => a.IsActive)
                                    .GetPaged<Undersection17>(model.PageNumber, model.PageSize);
                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("Number"):
                        data = null;
                        data = await _dbContext.Undersection17
                                     .Include(x => x.UnderSection6)
                        .Where(x => (string.IsNullOrEmpty(model.number) || x.Number.Contains(model.number))
                         && (string.IsNullOrEmpty(model.undersection6) || x.UnderSection6.Number.Contains(model.undersection6)))
                                    .OrderByDescending(a => a.Number)
                                    .GetPaged<Undersection17>(model.PageNumber, model.PageSize);
                        break;

                    case ("SECTION6"):
                        data = null;
                        data = await _dbContext.Undersection17
                                     .Include(x => x.UnderSection6)
                        .Where(x => (string.IsNullOrEmpty(model.number) || x.Number.Contains(model.number))
                         && (string.IsNullOrEmpty(model.undersection6) || x.UnderSection6.Number.Contains(model.undersection6)))
                                    .OrderByDescending(a => a.UnderSection6.Number)
                                    .GetPaged<Undersection17>(model.PageNumber, model.PageSize);
                        break;
                    case ("NOTIFICATIONDATE"):
                        data = null;
                        data = await _dbContext.Undersection17
                                     .Include(x => x.UnderSection6)
                        .Where(x => (string.IsNullOrEmpty(model.number) || x.Number.Contains(model.number))
                         && (string.IsNullOrEmpty(model.undersection6) || x.UnderSection6.Number.Contains(model.undersection6)))
                                   .OrderByDescending(a => a.NotificationDate)
                                    .GetPaged<Undersection17>(model.PageNumber, model.PageSize);
                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Undersection17
                                     .Include(x => x.UnderSection6)
                        .Where(x => (string.IsNullOrEmpty(model.number) || x.Number.Contains(model.number))
                         && (string.IsNullOrEmpty(model.undersection6) || x.UnderSection6.Number.Contains(model.undersection6)))
                                    .OrderBy(a => a.IsActive)
                                    .GetPaged<Undersection17>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;
        }


            public async Task<List<Undersection17>> GetAllUndersection17()
        {
            return await _dbContext.Undersection17.Include(x => x.UnderSection6).OrderByDescending(x => x.Id).ToListAsync();
        }






    }
}
