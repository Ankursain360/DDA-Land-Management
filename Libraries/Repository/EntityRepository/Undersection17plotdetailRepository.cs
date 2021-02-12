using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;


namespace Libraries.Repository.EntityRepository
{
    public class Undersection17plotdetailRepository : GenericRepository<Undersection17plotdetail>, IUndersection17plotdetailRepository
    {
        public Undersection17plotdetailRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Undersection17plotdetail>> GetPagedUndersection17plotdetail(Undersection17plotdetailSearchDto model)
        {
            //return await _dbContext.Undersection17plotdetail.
            //    Where(x => x.IsActive == 1).Include(x => x.UnderSection17).Include(x => x.Khasra).Include(x => x.Acquiredlandvillage)
            //    .GetPaged<Undersection17plotdetail>(model.PageNumber, model.PageSize);
            var data = await _dbContext.Undersection17plotdetail
                       .Include(x => x.UnderSection17)
                       .Include(x => x.Khasra)
                       .Include(x => x.Acquiredlandvillage)

                        .Where(x => (string.IsNullOrEmpty(model.undersection17) || x.UnderSection17.Number.Contains(model.undersection17))
                         && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                        .GetPaged<Undersection17plotdetail>(model.PageNumber, model.PageSize);


            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Undersection17plotdetail
                                    .Include(x => x.UnderSection17)
                       .Include(x => x.Khasra)
                       .Include(x => x.Acquiredlandvillage)

                        .Where(x => (string.IsNullOrEmpty(model.undersection17) || x.UnderSection17.Number.Contains(model.undersection17))
                         && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderBy(a => a.Khasra.Name)
                                    .GetPaged<Undersection17plotdetail>(model.PageNumber, model.PageSize);
                        break;

                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Undersection17plotdetail
                                    .Include(x => x.UnderSection17)
                       .Include(x => x.Khasra)
                       .Include(x => x.Acquiredlandvillage)

                        .Where(x => (string.IsNullOrEmpty(model.undersection17) || x.UnderSection17.Number.Contains(model.undersection17))
                         && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderBy(a => a.Acquiredlandvillage.Name)
                                    .GetPaged<Undersection17plotdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("NOTIFICATION"):
                        data = null;
                        data = await _dbContext.Undersection17plotdetail
                                    .Include(x => x.UnderSection17)
                       .Include(x => x.Khasra)
                       .Include(x => x.Acquiredlandvillage)

                        .Where(x => (string.IsNullOrEmpty(model.undersection17) || x.UnderSection17.Number.Contains(model.undersection17))
                         && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                   .OrderBy(a => a.UnderSection17.Number)
                                    .GetPaged<Undersection17plotdetail>(model.PageNumber, model.PageSize);
                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Undersection17plotdetail
                                    .Include(x => x.UnderSection17)
                       .Include(x => x.Khasra)
                       .Include(x => x.Acquiredlandvillage)

                        .Where(x => (string.IsNullOrEmpty(model.undersection17) || x.UnderSection17.Number.Contains(model.undersection17))
                         && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderByDescending(a => a.IsActive)
                                    .GetPaged<Undersection17plotdetail>(model.PageNumber, model.PageSize);
                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                   
               
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Undersection17plotdetail
                                    .Include(x => x.UnderSection17)
                       .Include(x => x.Khasra)
                       .Include(x => x.Acquiredlandvillage)

                        .Where(x => (string.IsNullOrEmpty(model.undersection17) || x.UnderSection17.Number.Contains(model.undersection17))
                         && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderByDescending(a => a.Khasra.Name)
                                    .GetPaged<Undersection17plotdetail>(model.PageNumber, model.PageSize);
                        break;

                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Undersection17plotdetail
                                    .Include(x => x.UnderSection17)
                       .Include(x => x.Khasra)
                       .Include(x => x.Acquiredlandvillage)

                        .Where(x => (string.IsNullOrEmpty(model.undersection17) || x.UnderSection17.Number.Contains(model.undersection17))
                         && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderByDescending(a => a.Acquiredlandvillage.Name)
                                    .GetPaged<Undersection17plotdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("NOTIFICATION"):
                        data = null;
                        data = await _dbContext.Undersection17plotdetail
                                    .Include(x => x.UnderSection17)
                       .Include(x => x.Khasra)
                       .Include(x => x.Acquiredlandvillage)

                        .Where(x => (string.IsNullOrEmpty(model.undersection17) || x.UnderSection17.Number.Contains(model.undersection17))
                         && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                   .OrderByDescending(a => a.UnderSection17.Number)
                                    .GetPaged<Undersection17plotdetail>(model.PageNumber, model.PageSize);
                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Undersection17plotdetail
                                    .Include(x => x.UnderSection17)
                       .Include(x => x.Khasra)
                       .Include(x => x.Acquiredlandvillage)

                        .Where(x => (string.IsNullOrEmpty(model.undersection17) || x.UnderSection17.Number.Contains(model.undersection17))
                         && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.khasra) || x.Khasra.Name.Contains(model.khasra)))
                                    .OrderBy(a => a.IsActive)
                                    .GetPaged<Undersection17plotdetail>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;

        }


        public async Task<List<Acquiredlandvillage>> GetAllVillageList()
        {
            List<Acquiredlandvillage> villageList = await _dbContext.Acquiredlandvillage.ToListAsync();
            return villageList;
        }
        public async Task<List<Khasra>> GetAllKhasraList()
        {
            List<Khasra> khasraList = await _dbContext.Khasra.ToListAsync();
            return khasraList;
        }
        public async Task<List<Undersection17>> GetAllUndersection17List()
        {
            List<Undersection17> undersection17List = await _dbContext.Undersection17.ToListAsync();
            return undersection17List;
        }




        public async Task<List<Undersection17plotdetail>> GetAllUndersection17plotdetail()
        {
            return await _dbContext.Undersection17plotdetail.Include(x => x.UnderSection17).Include(x => x.Khasra).Include(x => x.Acquiredlandvillage).OrderByDescending(x => x.Id).ToListAsync();
        }






    }
}

