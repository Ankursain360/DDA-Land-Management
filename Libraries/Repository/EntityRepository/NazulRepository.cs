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
    public class NazulRepository : GenericRepository<Nazul>, INazulRepository
    {
        public NazulRepository(DataContext dbContext) : base(dbContext)
        {

        }




       

        public async Task<List<Village>> GetAllVillage()
        {
            List<Village> villagelist = await _dbContext.Village.ToListAsync();
            return villagelist;
        }




        public async Task<List<Nazul>> GetAllNazul()
        {
            return await _dbContext.Nazul.Include(x => x.Village).OrderByDescending(x => x.Id).ToListAsync();
        }
        public async Task<PagedResult<Nazul>> GetPagedNazul(NazulSearchDto model)
        {
            return await _dbContext.Nazul.GetPaged<Nazul>(model.PageNumber, model.PageSize);
            //var data = await _dbContext.Nazul
            //    .Include(x => x.Village)

            //    .Where(x => (x.VillageId == (model.villageId == 0 ? x.VillageId : model.villageId)))

            //    .OrderByDescending(x => x.Id).GetPaged(model.PageNumber, model.PageSize);

            //int SortOrder = (int)model.SortOrder;
            //if (SortOrder == 1)
            //{
            //    switch (model.SortBy.ToUpper())
            //    {
            //        case ("VILLAGE"):
            //            data = null;
            //            data = await _dbContext.Nazul
            //                 .Where(x => (x.VillageId == (model.villageId == 0 ? x.VillageId : model.villageId)))

            //                 .OrderBy(a => a.VillageId)
            //                 .GetPaged<Nazul>(model.PageNumber, model.PageSize);

            //            break;
            //        case ("LASTMUTATIONNO"):
            //            data = null;
            //            data = await _dbContext.Nazul
            //                .Where(x => (x.VillageId == (model.villageId == 0 ? x.VillageId : model.villageId)))
            //                 .OrderBy(a => a.LastMutationNo)
            //                 .GetPaged<Nazul>(model.PageNumber, model.PageSize);

            //            break;

            //        case ("STATUS"):
            //            data = null;
            //            data = await _dbContext.Nazul
            //                  .Where(x => (x.VillageId == (model.villageId == 0 ? x.VillageId : model.villageId)))

            //                .OrderByDescending(a => a.IsActive)
            //                .GetPaged<Nazul>(model.PageNumber, model.PageSize);
            //            break;

            //    }
            //}
            //else if (SortOrder == 2)
            //{
            //    switch (model.SortBy.ToUpper())
            //    {
            //        case ("VILLAGE"):
            //            data = null;
            //            data = await _dbContext.Nazul
            //                 .Where(x => (x.VillageId == (model.villageId == 0 ? x.VillageId : model.villageId)))

            //                 .OrderByDescending(a => a.VillageId)
            //                 .GetPaged<Nazul>(model.PageNumber, model.PageSize);

            //            break;
            //        case ("LASTMUTATIONNO"):
            //            data = null;
            //            data = await _dbContext.Nazul
            //                .Where(x => (x.VillageId == (model.villageId == 0 ? x.VillageId : model.villageId)))
            //                 .OrderByDescending(a => a.LastMutationNo)
            //                 .GetPaged<Nazul>(model.PageNumber, model.PageSize);

            //            break;

            //        case ("STATUS"):
            //            data = null;
            //            data = await _dbContext.Nazul
            //                  .Where(x => (x.VillageId == (model.villageId == 0 ? x.VillageId : model.villageId)))

            //                .OrderBy(a => a.IsActive)
            //                .GetPaged<Nazul>(model.PageNumber, model.PageSize);
            //            break;
            //    }
            //}
            //return data;
        }







    }
}
