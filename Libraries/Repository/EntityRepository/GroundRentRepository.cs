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

    public class GroundRentRepository : GenericRepository<Groundrent>, IGroundRentService
    {
        public GroundRentRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Groundrent>> GetPagedGroundRent(GroundrentSearchDto model)
        {
            var data = await _dbContext.Groundrent

                            //.Where(x => (string.IsNullOrEmpty(model.name) || x..Contains(model.name)))
                            .GetPaged<Groundrent>(model.PageNumber, model.PageSize);

            //int SortOrder = (int)model.SortOrder;
            //if (SortOrder == 1)
            //{
            //    switch (model.SortBy.ToUpper())
            //    {
            //        case ("NAME"):
            //            data = null;
            //            data = await _dbContext.Structure
            //                .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
            //                .OrderBy(x => x.Name)
            //                .GetPaged<Structure>(model.PageNumber, model.PageSize);
            //            break;
            //        case ("STATUS"):
            //            data = null;
            //            data = await _dbContext.Structure
            //                .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
            //                .OrderByDescending(x => x.IsActive)
            //                .GetPaged<Structure>(model.PageNumber, model.PageSize);
            //            break;

            //    }
            //}
            //else if (SortOrder == 2)
            //{
            //    switch (model.SortBy.ToUpper())
            //    {
            //        case ("NAME"):
            //            data = null;
            //            data = await _dbContext.Structure
            //                .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
            //                .OrderByDescending(x => x.Name)
            //                .GetPaged<Structure>(model.PageNumber, model.PageSize);
            //            break;
            //        case ("STATUS"):
            //            data = null;
            //            data = await _dbContext.Structure
            //                .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
            //                .OrderBy(x => x.IsActive)
            //                .GetPaged<Structure>(model.PageNumber, model.PageSize);
            //            break;
            //    }
            //}
            return data;
            // return await _dbContext.Structure.GetPaged<Structure>(model.PageNumber, model.PageSize);
        }



        public async Task<List<Groundrent>> GetAllGroundRent()
        {
            return await _dbContext.Groundrent.Where(x => x.IsActive == 1).ToListAsync();
        }


    }
}
