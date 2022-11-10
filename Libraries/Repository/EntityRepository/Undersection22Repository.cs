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
  
     public class Undersection22Repository : GenericRepository<Undersection22>, IUndersection22Repository
    {
        public Undersection22Repository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<PagedResult<Undersection22>> GetPagedUndersection22(Undersection22SearchDto model)
        {
            var data = await _dbContext.Undersection22
                                       .Where(x => (string.IsNullOrEmpty(model.notification) || x.NotificationNo.Contains(model.notification)))
                                       .GetPaged<Undersection22>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NOTIFICATION"):
                        data = null;
                        data = await _dbContext.Undersection22
                                       .Where(x => (string.IsNullOrEmpty(model.notification) || x.NotificationNo.Contains(model.notification)))
                                       .OrderBy(a => a.NotificationNo)
                                       .GetPaged<Undersection22>(model.PageNumber, model.PageSize);

                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Undersection22
                                         .Where(x => (string.IsNullOrEmpty(model.notification) || x.NotificationNo.Contains(model.notification)))
                                         .OrderBy(a => a.NotificationDate)
                                         .GetPaged<Undersection22>(model.PageNumber, model.PageSize);

                        break;
                   
                   
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Undersection22
                                         .Where(x => (string.IsNullOrEmpty(model.notification) || x.NotificationNo.Contains(model.notification)))
                                         .OrderByDescending(a => a.IsActive)
                                         .GetPaged<Undersection22>(model.PageNumber, model.PageSize);

                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NOTIFICATION"):
                        data = null;
                        data = await _dbContext.Undersection22
                                       .Where(x => (string.IsNullOrEmpty(model.notification) || x.NotificationNo.Contains(model.notification)))
                                       .OrderByDescending(a => a.NotificationNo)
                                       .GetPaged<Undersection22>(model.PageNumber, model.PageSize);

                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Undersection22
                                         .Where(x => (string.IsNullOrEmpty(model.notification) || x.NotificationNo.Contains(model.notification)))
                                         .OrderByDescending(a => a.NotificationDate)
                                         .GetPaged<Undersection22>(model.PageNumber, model.PageSize);

                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Undersection22
                                         .Where(x => (string.IsNullOrEmpty(model.notification) || x.NotificationNo.Contains(model.notification)))
                                         .OrderBy(a => a.IsActive)
                                         .GetPaged<Undersection22>(model.PageNumber, model.PageSize);

                        break;
                }
            }
            return data;
        }


        public async Task<List<Undersection22>> GetUndersection22()
        {
            return await _dbContext.Undersection22.ToListAsync();
        }
        public async Task<List<Undersection22>> GetAllUndersection22List(Undersection22SearchDto model) 
        {
            var data = await _dbContext.Undersection22
                                       .Where(x => (string.IsNullOrEmpty(model.notification) || x.NotificationNo.Contains(model.notification))).ToListAsync();
            return data;
        }

        //public async Task<bool> Any(int id, string name)
        //{
        //    return await _dbContext.Module.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        //}
        }
}
